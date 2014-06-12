"use strict";

this.EXPORTED_SYMBOLS = [
    "Fb2DownloadCopySaverToEPub",
  ];
  
Components.utils.import("resource://gre/modules/XPCOMUtils.jsm");

// make sure we have a base class here
XPCOMUtils.defineLazyModuleGetter(this, "DownloadSaver","resource://gre/modules/DownloadCore.jsm");
  
////////////////////////////////////////////////////////////////////////////////
//// Fb2DownloadCopySaverToEPub

/**
 * Saver object that simply copies the entire source file to the target.
 */
this.Fb2DownloadCopySaverToEPub = function () {}

this.Fb2DownloadCopySaverToEPub.prototype = {
  __proto__: DownloadSaver.prototype,

  /**
   * BackgroundFileSaver object currently handling the download.
   */
  _backgroundFileSaver: null,

  /**
   * Indicates whether the "cancel" method has been called.  This is used to
   * prevent the request from starting in case the operation is canceled before
   * the BackgroundFileSaver instance has been created.
   */
  _canceled: false,

  /**
   * Save the SHA-256 hash in raw bytes of the downloaded file. This is null
   * unless BackgroundFileSaver has successfully completed saving the file.
   */
  _sha256Hash: null,

  /**
   * Save the signature info as an nsIArray of nsIX509CertList of nsIX509Cert
   * if the file is signed. This is empty if the file is unsigned, and null
   * unless BackgroundFileSaver has successfully completed saving the file.
   */
  _signatureInfo: null,

  /**
   * Save the redirects chain as an nsIArray of nsIPrincipal.
   */
  _redirects: null,

  /**
   * True if the associated download has already been added to browsing history.
   */
  alreadyAddedToHistory: false,

  /**
   * String corresponding to the entityID property of the nsIResumableChannel
   * used to execute the download, or null if the channel was not resumable or
   * the saver was instructed not to keep partially downloaded data.
   */
  entityID: null,

  /**
   * Implements "DownloadSaver.execute".
   */
  execute: function DCS_execute(aSetProgressBytesFn, aSetPropertiesFn)
  {
    let copySaver = this;

    this._canceled = false;

    let download = this.download;
    let targetPath = download.target.path;
    let partFilePath = download.target.partFilePath;
    let keepPartialData = download.tryToKeepPartialData;

    return Task.spawn(function task_DCS_execute() {
      // Add the download to history the first time it is started in this
      // session.  If the download is restarted in a different session, a new
      // history visit will be added.  We do this just to avoid the complexity
      // of serializing this state between sessions, since adding a new visit
      // does not have any noticeable side effect.
      if (!this.alreadyAddedToHistory) {
        this.addToHistory();
        this.alreadyAddedToHistory = true;
      }

      // To reduce the chance that other downloads reuse the same final target
      // file name, we should create a placeholder as soon as possible, before
      // starting the network request.  The placeholder is also required in case
      // we are using a ".part" file instead of the final target while the
      // download is in progress.
      try {
        // If the file already exists, don't delete its contents yet.
        let file = yield OS.File.open(targetPath, { write: true });
        yield file.close();
      } catch (ex if ex instanceof OS.File.Error) {
        // Throw a DownloadError indicating that the operation failed because of
        // the target file.  We cannot translate this into a specific result
        // code, but we preserve the original message using the toString method.
        let error = new DownloadError({ message: ex.toString() });
        error.becauseTargetFailed = true;
        throw error;
      }

      try {
        let deferSaveComplete = Promise.defer();

        if (this._canceled) {
          // Don't create the BackgroundFileSaver object if we have been
          // canceled meanwhile.
          throw new DownloadError({ message: "Saver canceled." });
        }

        // Create the object that will save the file in a background thread.
        let backgroundFileSaver = new BackgroundFileSaverStreamListener();
        try {
          // When the operation completes, reflect the status in the promise
          // returned by this download execution function.
          backgroundFileSaver.observer = {
            onTargetChange: function () { },
            onSaveComplete: (aSaver, aStatus) => {
              // Send notifications now that we can restart if needed.
              if (Components.isSuccessCode(aStatus)) {
                // Save the hash before freeing backgroundFileSaver.
                this._sha256Hash = aSaver.sha256Hash;
                this._signatureInfo = aSaver.signatureInfo;
                this._redirects = aSaver.redirects;
                deferSaveComplete.resolve();
              } else {
                // Infer the origin of the error from the failure code, because
                // BackgroundFileSaver does not provide more specific data.
                let properties = { result: aStatus, inferCause: true };
                deferSaveComplete.reject(new DownloadError(properties));
              }
              // Free the reference cycle, to release resources earlier.
              backgroundFileSaver.observer = null;
              this._backgroundFileSaver = null;
            },
          };

          // Create a channel from the source, and listen to progress
          // notifications.
          let channel = NetUtil.newChannel(NetUtil.newURI(download.source.url));
          if (channel instanceof Ci.nsIPrivateBrowsingChannel) {
            channel.setPrivate(download.source.isPrivate);
          }
          if (channel instanceof Ci.nsIHttpChannel &&
              download.source.referrer) {
            channel.referrer = NetUtil.newURI(download.source.referrer);
          }

          // If we have data that we can use to resume the download from where
          // it stopped, try to use it.
          let resumeAttempted = false;
          let resumeFromBytes = 0;
          if (channel instanceof Ci.nsIResumableChannel && this.entityID &&
              partFilePath && keepPartialData) {
            try {
              let stat = yield OS.File.stat(partFilePath);
              channel.resumeAt(stat.size, this.entityID);
              resumeAttempted = true;
              resumeFromBytes = stat.size;
            } catch (ex if ex instanceof OS.File.Error &&
                           ex.becauseNoSuchFile) { }
          }

          channel.notificationCallbacks = {
            QueryInterface: XPCOMUtils.generateQI([Ci.nsIInterfaceRequestor]),
            getInterface: XPCOMUtils.generateQI([Ci.nsIProgressEventSink]),
            onProgress: function DCSE_onProgress(aRequest, aContext, aProgress,
                                                 aProgressMax)
            {
              let currentBytes = resumeFromBytes + aProgress;
              let totalBytes = aProgressMax == -1 ? -1 : (resumeFromBytes +
                                                          aProgressMax);
              aSetProgressBytesFn(currentBytes, totalBytes, aProgress > 0 &&
                                  partFilePath && keepPartialData);
            },
            onStatus: function () { },
          };

          // Open the channel, directing output to the background file saver.
          backgroundFileSaver.QueryInterface(Ci.nsIStreamListener);
          channel.asyncOpen({
            onStartRequest: function (aRequest, aContext) {
              backgroundFileSaver.onStartRequest(aRequest, aContext);

              // Check if the request's response has been blocked by Windows
              // Parental Controls with an HTTP 450 error code.
              if (aRequest instanceof Ci.nsIHttpChannel &&
                  aRequest.responseStatus == 450) {
                // Set a flag that can be retrieved later when handling the
                // cancellation so that the proper error can be thrown.
                this.download._blockedByParentalControls = true;
                aRequest.cancel(Cr.NS_BINDING_ABORTED);
                return;
              }

              aSetPropertiesFn({ contentType: channel.contentType });

              // Ensure we report the value of "Content-Length", if available,
              // even if the download doesn't generate any progress events
              // later.
              if (channel.contentLength >= 0) {
                aSetProgressBytesFn(0, channel.contentLength);
              }

              // If the URL we are downloading from includes a file extension
              // that matches the "Content-Encoding" header, for example ".gz"
              // with a "gzip" encoding, we should save the file in its encoded
              // form.  In all other cases, we decode the body while saving.
              if (channel instanceof Ci.nsIEncodedChannel &&
                  channel.contentEncodings) {
                let uri = channel.URI;
                if (uri instanceof Ci.nsIURL && uri.fileExtension) {
                  // Only the first, outermost encoding is considered.
                  let encoding = channel.contentEncodings.getNext();
                  if (encoding) {
                    channel.applyConversion =
                      gExternalHelperAppService.applyDecodingForExtension(
                                                uri.fileExtension, encoding);
                  }
                }
              }

              if (keepPartialData) {
                // If the source is not resumable, don't keep partial data even
                // if we were asked to try and do it.
                if (aRequest instanceof Ci.nsIResumableChannel) {
                  try {
                    // If reading the ID succeeds, the source is resumable.
                    this.entityID = aRequest.entityID;
                  } catch (ex if ex instanceof Components.Exception &&
                                 ex.result == Cr.NS_ERROR_NOT_RESUMABLE) {
                    keepPartialData = false;
                  }
                } else {
                  keepPartialData = false;
                }
              }

              // Enable hashing and signature verification before setting the
              // target.
              backgroundFileSaver.enableSha256();
              backgroundFileSaver.enableSignatureInfo();
              if (partFilePath) {
                // If we actually resumed a request, append to the partial data.
                if (resumeAttempted) {
                  // TODO: Handle Cr.NS_ERROR_ENTITY_CHANGED
                  backgroundFileSaver.enableAppend();
                }

                // Use a part file, determining if we should keep it on failure.
                backgroundFileSaver.setTarget(new FileUtils.File(partFilePath),
                                              keepPartialData);
              } else {
                // Set the final target file, and delete it on failure.
                backgroundFileSaver.setTarget(new FileUtils.File(targetPath),
                                              false);
              }
            }.bind(copySaver),

            onStopRequest: function (aRequest, aContext, aStatusCode) {
              try {
                backgroundFileSaver.onStopRequest(aRequest, aContext,
                                                  aStatusCode);
              } finally {
                // If the data transfer completed successfully, indicate to the
                // background file saver that the operation can finish.  If the
                // data transfer failed, the saver has been already stopped.
                if (Components.isSuccessCode(aStatusCode)) {
                  if (partFilePath) {
                    // Move to the final target if we were using a part file.
                    backgroundFileSaver.setTarget(
                                        new FileUtils.File(targetPath), false);
                  }
                  backgroundFileSaver.finish(Cr.NS_OK);
                }
              }
            }.bind(copySaver),

            onDataAvailable: function (aRequest, aContext, aInputStream,
                                       aOffset, aCount) {
              backgroundFileSaver.onDataAvailable(aRequest, aContext,
                                                  aInputStream, aOffset,
                                                  aCount);
            }.bind(copySaver),
          }, null);

          // We should check if we have been canceled in the meantime, after
          // all the previous asynchronous operations have been executed and
          // just before we set the _backgroundFileSaver property.
          if (this._canceled) {
            throw new DownloadError({ message: "Saver canceled." });
          }

          // If the operation succeeded, store the object to allow cancellation.
          this._backgroundFileSaver = backgroundFileSaver;
        } catch (ex) {
          // In case an error occurs while setting up the chain of objects for
          // the download, ensure that we release the resources of the saver.
          backgroundFileSaver.finish(Cr.NS_ERROR_FAILURE);
          throw ex;
        }

        // We will wait on this promise in case no error occurred while setting
        // up the chain of objects for the download.
        yield deferSaveComplete.promise;
      } catch (ex) {
        // Ensure we always remove the placeholder for the final target file on
        // failure, independently of which code path failed.  In some cases, the
        // background file saver may have already removed the file.
        try {
          yield OS.File.remove(targetPath);
        } catch (e2) {
          // If we failed during the operation, we report the error but use the
          // original one as the failure reason of the download.  Note that on
          // Windows we may get an access denied error instead of a no such file
          // error if the file existed before, and was recently deleted.
          if (!(e2 instanceof OS.File.Error &&
                (e2.becauseNoSuchFile || e2.becauseAccessDenied))) {
            Cu.reportError(e2);
          }
        }
        throw ex;
      }
    }.bind(this));
  },

  /**
   * Implements "DownloadSaver.cancel".
   */
  cancel: function DCS_cancel()
  {
    this._canceled = true;
    if (this._backgroundFileSaver) {
      this._backgroundFileSaver.finish(Cr.NS_ERROR_FAILURE);
      this._backgroundFileSaver = null;
    }
  },

  /**
   * Implements "DownloadSaver.removePartialData".
   */
  removePartialData: function ()
  {
    return Task.spawn(function task_DCS_removePartialData() {
      if (this.download.target.partFilePath) {
        try {
          yield OS.File.remove(this.download.target.partFilePath);
        } catch (ex if ex instanceof OS.File.Error && ex.becauseNoSuchFile) { }
      }
    }.bind(this));
  },

  /**
   * Implements "DownloadSaver.toSerializable".
   */
  toSerializable: function ()
  {
    // Simplify the representation if we don't have other details.
    if (!this.entityID && !this._unknownProperties) {
      return "Fb2ePubConvertSaver";
    }

    let serializable = { type: "Fb2ePubConvertSaver",
                         entityID: this.entityID };
    serializeUnknownProperties(this, serializable);
    return serializable;
  },

  /**
   * Implements "DownloadSaver.getSha256Hash"
   */
  getSha256Hash: function ()
  {
    return this._sha256Hash;
  },

  /*
   * Implements DownloadSaver.getSignatureInfo.
   */
  getSignatureInfo: function ()
  {
    return this._signatureInfo;
  },

  /*
   * Implements DownloadSaver.getRedirects.
   */
  getRedirects: function ()
  {
    return this._redirects;
  }
};

/**
 * Creates a new Fb2DownloadCopySaverToEPub object, with its initial state derived from
 * its serializable representation.
 *
 * @param aSerializable
 *        Serializable representation of a Fb2DownloadCopySaverToEPub object.
 *
 * @return The newly created Fb2DownloadCopySaverToEPub object.
 */
this.Fb2DownloadCopySaverToEPub.fromSerializable = function (aSerializable) {
  let saver = new Fb2DownloadCopySaverToEPub();
  if ("entityID" in aSerializable) {
    saver.entityID = aSerializable.entityID;
  }

  deserializeUnknownProperties(saver, aSerializable, property =>
    property != "entityID" && property != "type");

  return saver;
};
