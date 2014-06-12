"use strict";

this.EXPORTED_SYMBOLS = [];

////////////////////////////////////////////////////////////////////////////////
//// Globals

const Cc = Components.classes;
const Ci = Components.interfaces;
const Cu = Components.utils;
const Cr = Components.results;

Cu.import("resource://gre/modules/XPCOMUtils.jsm");


XPCOMUtils.defineLazyModuleGetter(this, "DownloadSaver","resource://gre/modules/DownloadCore.jsm");
XPCOMUtils.defineLazyModuleGetter(this, "DownloadCopySaver","resource://gre/modules/DownloadCore.jsm");
XPCOMUtils.defineLazyModuleGetter(this, "DownloadLegacySaver","resource://gre/modules/DownloadCore.jsm");
XPCOMUtils.defineLazyModuleGetter(this, "Fb2DownloadCopySaverToEPub","chrome://fb2epub/content/fb-2download_fb2Saver.jsm");

/**
 * Returns true if the given value is a primitive string or a String object.
 */
function isString(aValue) {
  // We cannot use the "instanceof" operator reliably across module boundaries.
  return (typeof aValue == "string") ||
         (typeof aValue == "object" && "charAt" in aValue);
}

/*
 * overrides type of savers creation to "inject" our own saver
*/
this.DownloadSaver.fromSerializable = function (aSerializable) {
  let serializable = isString(aSerializable) ? { type: aSerializable }
                                             : aSerializable;
  let saver;
  switch (serializable.type) {
    case "copy":
      saver = DownloadCopySaver.fromSerializable(serializable);
      break;
    case "legacy":
      saver = DownloadLegacySaver.fromSerializable(serializable);
      break;
	case "Fb2DownloadCopySaverToEPub":
	  saver = Fb2DownloadCopySaverToEPub.fromSerializable(serializable);
	  break;
    default:
      throw new Error("Unrecoginzed download saver type.");
  }
  return saver;
};
