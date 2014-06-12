const {Services} = Components.utils.import("resource://gre/modules/Services.jsm", {});
const {Task} = Components.utils.import("resource://gre/modules/Task.jsm", {});
Components.utils.import("resource://gre/modules/XPCOMUtils.jsm");



//Components.utils.import("resource://gre/modules/DownloadCore.jsm");
Components.utils.import("resource://gre/modules/Downloads.jsm");
Components.utils.import("chrome://fb2epub/content/DownloadsExt.jsm");



window.addEventListener("load", function load(event){
    window.removeEventListener("load", load, false); //remove listener, no longer needed
    fb2SaveContent.init();  
},false);

var fb2SaveContent = {
	_fb2MenuItem: null, // menu item we insert
	_fb2ItemSelected: false,
	_rememberButtonDisabledPrevState: false,
	_rememberButtonCheckedPrevState: false,

	
// Check if the passed source name is one of FB2 extensions
isfb2extension: function(fileName)
{
 // var myURL = sourceURI.QueryInterface(Components.interfaces.nsIURL);

 // var extension = myURL.fileExtension.toLowerCase();
 var extension = fileName.split('.').pop().toLowerCase();
 if ( extension == null || extension == "") // if no extension
 {
 	return false;
 }
 if ( extension == "fb2") // if FB2 extension then FB2
 {
    return true;
 }
 // now let's check if it double extension for archives
 if ( extension == "zip" || extension == "rar")
 {
	 // get name without extension
	 var partWithoutExtension = "";
	 
	var start = fileName.lastIndexOf("."); // index of last extension
	if (start == -1) 
	{
		dump("\nisfb2extension: Error - can't find extension");
		return false;
	}
	// remove the extension
	partWithoutExtension = fileName.substring(0,start);
	// detect 2nd level extension
	var extension2ndLvl = partWithoutExtension.split('.').pop();
	if ( extension2ndLvl != null && extension2ndLvl != "")
	{
		if( extension2ndLvl.toLowerCase() == "fb2") // if 2nd level extension is FB2
		{
			return true;
		}
	}
 }
 return false;
},

// set the disabled state for remember button
disableRememberButton: function(disable)
{
	var rememberbutton = document.getElementById('rememberChoice');
	if ( rememberbutton == null )
	{
	  dump("\ndisableRememberButton: - rememberChoice button not found");
	  return;
	}
	// set disabled attribute
	rememberbutton.setAttribute("disabled", disable);
},

// set the checked state for remember button
checkRememberButton: function(check)
{
	var rememberbutton = document.getElementById('rememberChoice');
	if ( rememberbutton == null )
	{
	  dump("\ncheckRememberButton: - rememberChoice button not found");
	  return;
	}
	// set checked attribute
	rememberbutton.setAttribute("checked", check);
},

// react on user clicking on one of the radio buttons
toggleChoice: function(event)
{
  // get pointer to radio menu
  var saveGroup = document.getElementById('mode');
  if ( saveGroup == null ) // if radio menu not found
  {
    dump("\ntoggleChoice: SaveGroup not found in document");
    return;
  }
  // get selected item
  var selectedItem = saveGroup.selectedItem;
  if ( selectedItem.id == "fb2epub-radio" ) // if selected item's id is id of our (fb2epub) menu entry
  {
	if ( this._fb2ItemSelected == false ) // if this is first time in a row user clicked on fb2epub button (to avoid multiple clicks)
	{
		// record current enabled/disabled state of remember button
		this._rememberButtonDisabledPrevState = this.getRememberButtonDisabledState();
		this._rememberButtonCheckedPrevState = this.getRememberButtonCheckedState();
		// disable the remember button
		this.disableRememberButton(true);
		// uncheck the button by default
		this.checkRememberButton(false);
		// mark that fb2epub item was selected as last select operation
		this._fb2ItemSelected = true;
	}
  }
  else // if not our menu entry
  {
	if ( this._fb2ItemSelected == true) // if prev. selection was fb2epub menu entry
	{
		this.disableRememberButton(this._rememberButtonDisabledPrevState); // restore the enabled/disabled state that was before fb2epub menu entry was clicked first time
		this.checkRememberButton(this._rememberButtonCheckedPrevState); // restore the checked state that was before fb2epub menu entry was clicked first time
		// clear fb2epub menu entry selection flag
	    this._fb2ItemSelected = false;
	}
  }
},

insertMenu: function (saveGroup)
{
  var saveItem = document.getElementById('save');
  if ( saveItem == null)
  {
    dump("\ninsertMenu: Item with ID save not found");
  }
  else
  {
  // get pointer to save item in the group
    var saveIndex = saveGroup.getIndexOfItem(saveItem);
	if (saveIndex  == saveGroup.itemCount ) // if 'save' - last item we append
	{
	  this._fb2MenuItem = saveGroup.appendItem("Fb2ePub (convert to ePub)");
	}
	else // else insert before it
	{
	  this._fb2MenuItem = saveGroup.insertItemAt(saveIndex+1,"Fb2ePub (convert to ePub)");
	}
	if ( this._fb2MenuItem == null )
	{
		dump("\ninsertMenu: adding menu entry failed");
	}
	else
	{
		this._fb2MenuItem.id = "fb2epub-radio";
		saveGroup.addEventListener(
      "select", function(event) {fb2SaveContent.toggleChoice(event);},true);
	}
  }
},

// Return if rememberChoise button checked or not
getRememberButtonCheckedState: function()
{
	// get the button
	var rememberbutton = document.getElementById('rememberChoice');
	if ( rememberbutton == null ) // if no button exit 
	{
	  dump("\ngetRememberButtonCheckedState: - rememberChoice button not found");
	  return false; // not checked by default, just in case
	}
	var state = rememberbutton.getAttribute("checked");	
	if ( state == "true") // if checked then return true
	{
	  return true;
	}
	// if attribute set to false or not set at all
	return false;
},

// Return if rememberChoise button disabled or not
getRememberButtonDisabledState: function()
{
	// get the button
	var rememberbutton = document.getElementById('rememberChoice');
	if ( rememberbutton == null ) // if no button exit 
	{
	  dump("\ngetRememberButtonDisabledState: - rememberChoice button not found");
	  return false; // not disabled by default, just in case
	}
	var state = rememberbutton.getAttribute("disabled");	
	if ( state == true) // if disabled then return true
	{
	  return true;
	}
	// if attribute set to false or not set at all
	return false;
},

// Download a file from internet, to file named "filename" (should include path) from URI path "uri"
download: function (filename, uri)
{
  try {
    var nsILocalFile = Components.classes["@mozilla.org/file/local;1"]
                      .getService(Components.interfaces.nsILocalFile);
    // var nsIURI = Components.classes["@mozilla.org/network/standard-url;1"]
                 // .getService(Components.interfaces.nsIURI);
    nsILocalFile.initWithPath(filename);
    // nsIURI.spec = url;
    var nsIWBP = Components.interfaces.nsIWebBrowserPersist;
	if ( nsIWBP == null )
	{
		dump("\ndownload: - Unable to create nsIWebBrowserPersist interface");
		return null;
	}
    var persist = Components.classes["@mozilla.org/embedding/browser/nsWebBrowserPersist;1"]
                 .createInstance(nsIWBP);
	if ( persist == null )
	{
		dump("\ndownload: - Unable to create nsIWebBrowserPersist interface object instance");
		return null;
	}
    persist.persistFlags = nsIWBP.PERSIST_FLAGS_REPLACE_EXISTING_FILES;

	var wm = Components.classes["@mozilla.org/appshell/window-mediator;1"].getService(Components.interfaces.nsIWindowMediator);
	if ( wm == null )
	{
		dump("\ndownload: - Unable to get nsIWindowMediator");
		return null;
	}
	var wrecent = wm.getMostRecentWindow("navigator:browser");
	if ( wrecent == null )
	{
		dump("\ndownload: - Unable to get latest window");
		return null;
	}
	Components.utils.import("resource://gre/modules/PrivateBrowsingUtils.jsm");	
	var privacy = PrivateBrowsingUtils.privacyContextFromWindow(wrecent);
	if ( privacy == null )
	{
		dump("\ndownload: - Unable to get privacy from recent window");
		return null;
	}
    persist.saveURI(uri, null, null, null, null, nsILocalFile,privacy);
    return nsILocalFile;
  }
  catch(ex) {
	dump("\n"+ex);
    return null;
  }
},

// Generate temporary file name of required length
// length - length of temporal string
makeTempFileName: function(length)
{
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for( var i=0; i < length; i++ )
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text +".tmp";
},

test: function(destination,source)
{
Task.spawn(function () {

  let list = yield Downloads.getList(Downloads.ALL);

  let view = {
    onDownloadAdded: download => dump("\nAdded: " + download.target.path),
    onDownloadChanged: download => dump("\nChanged: " + download.target.path),
    onDownloadRemoved: download => dump("\nRemoved: "+ download.target.path),
  };

  yield list.addView(view);
  try {
    let download = yield Downloads.createDownload({
      source: source,
      target:  destination,
	  saver: "Fb2DownloadCopySaverToEPub",
    });
    list.add(download);
    try {
      yield download.start();
    } finally {
      //yield list.remove(download);
      yield download.finalize(true);
	  dump("\nDone");
    }
  } finally {
    yield list.removeView(view);
  }

}).then(null, Components.utils.reportError);
},

downloadAndConvert: function()
{
	// get file picker dialog interface
	var nsIFilePicker = Components.interfaces.nsIFilePicker;
	var fp = Components.classes["@mozilla.org/filepicker;1"].createInstance(nsIFilePicker);
	fp.init(window, "Enter a File Name to save", nsIFilePicker.modeSave);
	fp.appendFilter("ePub file","*.ePub");
	fp.defaultExtension = "ePub";
	//var myURL = dialog.mLauncher.source.QueryInterface(Components.interfaces.nsIURL);
	var fileToDownload = document.getElementById("location").value;
	if ( fileToDownload == null || fileToDownload == "")
	{
		dump("\ndownloadAndConvert: Can't get file to download");
	}
	// get the extension
	var extension = fileToDownload.split('.').pop().toLowerCase();
	var start = fileToDownload.lastIndexOf("."); // index of last extension
	var basename = fileToDownload.substring(0,start)  + ".ePub";	 // just remove last extension and add eub
	if (extension != "fb2") //if some kind of archive
	{
		var withoutFirstExtension = fileToDownload.substring(0,start);
		start = withoutFirstExtension.lastIndexOf(".");
		if (start != -1) // if no more extension , then 'basename' is ok, otherwise - need to remove
		{
			basename = withoutFirstExtension.substring(0,start)  + ".ePub";
		}
	}
	dump("\n"+ basename);
	fp.defaultString = basename;
	var result = fp.show();
	if (result == nsIFilePicker.returnOK || result == nsIFilePicker.returnReplace)
	{
		Components.utils.import("resource://gre/modules/osfile.jsm")
		var path1 = OS.Path.join(OS.Constants.Path.tmpDir, this.makeTempFileName(8));
		dump("\nTemp path: " +path1);
		// var savedFile = this.download(path1,dialog.mLauncher.source);
		// if ( savedFile == null )
		// {
			// dump("\ndownloadAndConvert: Unable to download file: " + fp.file.path);
		// }
	
		// dump("\n"+fp.file.leafName);
		// var wm = Components.classes["@mozilla.org/appshell/window-mediator;1"].getService(Components.interfaces.nsIWindowMediator);
		// var wrecent = wm.getMostRecentWindow("navigator:browser");
		// wrecent.saveURL(dialog.mLauncher.source, fp.file.leafName);
	}
	this.test(fp.file.path,dialog.mLauncher.source);
	//Components.utils.import("resource://gre/modules/Downloads.jsm");
	//let list = yield Downloads.getList(Downloads.ALL);
	// if ( list == null )
	// {
		// dump("\nNo list");
	// }
},

dialogAccepted: function() {
	if ( this._fb2ItemSelected ) // if user selected convert to FB2
	{
		// call function to download file and when completed - convert it
		this.downloadAndConvert();
		return false;
	}
	return true;
},


init: function()
{
	dump("\n\nStarting: \n" );
	
	var fileToDownload = document.getElementById("location");
	if ( this.isfb2extension(fileToDownload.value))
	{
		this._fb2MenuItem =  null;
		this._fb2ItemSelected =  false;
		// record initial button state
		this._rememberButtonDisabledPrevState = this.getRememberButtonDisabledState();
		this._rememberButtonCheckedPrevState = this.getRememberButtonCheckedState();
		var saveGroup = document.getElementById('mode');
		if ( saveGroup == null )
		{
		  dump ("\nMode radiogroup not found");
		}
		else
		{
			this.insertMenu(saveGroup);
			var d = document.documentElement;
			if ( d == null )
			{
			  dump("\nError");
			}
			else
			{
				// redirect ondialogaccept event, if it's "our" we do what we want, if not - we call original implementation
				d.setAttribute('ondialogaccept','if(fb2SaveContent.dialogAccepted()) { ' + document.documentElement.getAttribute('ondialogaccept') +'}');
			}
		}
	}
	else
	{
		//dump("\nNot FB2 file downloading");
	}
},

onload: function()
{

},



};
