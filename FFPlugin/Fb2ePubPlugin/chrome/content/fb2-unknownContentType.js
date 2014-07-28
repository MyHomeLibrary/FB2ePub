"use strict";
const {Services} = Components.utils.import("resource://gre/modules/Services.jsm", {});
const {Task} = Components.utils.import("resource://gre/modules/Task.jsm", {});
Components.utils.import("resource://gre/modules/XPCOMUtils.jsm");



Components.utils.import("resource://gre/modules/Downloads.jsm");
Components.utils.import("chrome://fb2epub/content/DownloadsExt.jsm");
Components.utils.import("chrome://fb2epub/content/converter_path_exports.jsm");

const defaultNS = "http://www.mozilla.org/keymaster/gatekeeper/there.is.only.xul";



// attach event listener to "load" event , fired when window loaded
window.addEventListener("load", function load(event){
    window.removeEventListener("load", load, false); //remove listener, no longer needed
	ConverterPaths.init();
    fb2SaveContent.init();  
},false);

// attach event listener to "unload" event , fired when window unloaded
window.addEventListener("unload", function unload(event){
    window.removeEventListener("unload", unload, false); //remove listener, no longer needed
	ConverterPaths.close();
},false);


var fb2SaveContent = {
	_fb2MenuItem: null, // menu item we insert
	_fb2ItemSelected: false,
	_rememberButtonDisabledPrevState: false,
	_rememberButtonCheckedPrevState: false,
	_selectedDestinationId: 'fb2epub-menubrowseItem',
	
	
// Check if the passed source name is one of FB2 extensions
isfb2extension: function(fileName)
{
 var extension = fileName.split('.').pop().toLowerCase();
 if ( extension == null || extension == "") // if no extension
 {
 	return false;
 }
 if ( extension == "fb2") // if FB2 extension then FB2
 {
    return true;
 }
 // access to service that read preferences
 var prefs = Components.classes["@mozilla.org/preferences-service;1"].getService(Components.interfaces.nsIPrefService);
 // access my extension preferences branch
 prefs = prefs.getBranch("extensions.fb2epub.");
 // read if we support ANY .ZIP file
 var value = prefs.getBoolPref("checkforANYZIP");			 
 if ( value == true && extension == "zip" )
 {
	return true;
 }
 // read if we support ANY .RAR file
 value = prefs.getBoolPref("checkforANYRAR");
 if ( value == true && extension == "rar" )
 {
	return true;
 }
 // read if we need to check additional extensions at all
 value = prefs.getBoolPref("checkAdditional");
 if ( value == true )
 {
	value = prefs.getCharPref("additionalExtensions").toLowerCase(); // making lower case so easier to search
	var extensionsPassed = value.split(';');
	for (var elm in extensionsPassed )
	{
		if ( extensionsPassed[elm] == extension)
		{
			return true;
		}
	}
 }
 // now let's check if it double extension for archives
 // for .FB2.ZIP
 value = prefs.getBoolPref("checkforFB2ZIP");
 if ( (extension == "zip") && (value == true) )
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
 // for .FB2.RAR
 value = prefs.getBoolPref("checkforFB2RAR");
 if ( ( value == true) && (extension == "rar") )
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

toggleDestinationChoice: function(event)
{
	var topMenu = document.getElementById('fb2epub-menulist');
	if ( topMenu == null )
	{
		dump("\ntoggleDestinationChoice - error getting top menu!");
		return;
	}
	var selectedItem = topMenu.selectedItem;
	this._selectedDestinationId = selectedItem.id;
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

// Creates a FB2ePub sub-menu in radio button selection and adds all possible destinations (if any)
createMenuList: function (parent)
{ 
	var menulist = document.createElementNS(defaultNS,'menulist');
	if ( menulist == null)
	{
		dump("\ncreateMenuList: Unable to create menulist\n");
	}
	menulist.setAttribute( 'flex', '1' ); 
	menulist.setAttribute( 'id', 'fb2epub-menulist' ); 
	
	// Create a menu popup
	var menuPopup = document.createElementNS(defaultNS,'menupopup');
	if ( menuPopup == null)
	{
		dump("\ncreateMenuList: Unable to create menu popup\n");
	}
	menuPopup.setAttribute( 'id', 'fb2epub-menupopup' ); 
	
	// create "browse for folder" menu item
	var browseItem = document.createElementNS(defaultNS,'menuitem');
	browseItem.setAttribute( 'id', 'fb2epub-menubrowseItem' ); 
    var translator = document.getElementById('download_dialog');
    if ( translator == null)
    {
      dump("\ncreateMenuList: 'download_dialog' not found\n");  
    }
	
	browseItem.setAttribute( 'label', translator.getString("fb2epub_browse4folder.label")); 
	menuPopup.appendChild(browseItem); //add popup to menulist
	
	
	var pathsCount =	ConverterPaths.GetPathsCount(pathsCount);
	for (var i = 0; i < pathsCount; i++) 
	{
		let path = ConverterPaths.GetPath(i).path;
		let name = ConverterPaths.GetPathName(i).name;
		let menuItem = document.createElementNS(defaultNS,'menuitem');
		menuItem.setAttribute( 'id', i ); 
		var itemLabel;
		if ( name == null || name == "")
		{
			itemLabel = path;
		}
		else
		{
			itemLabel = name + " ( " + path + " ) ";
		}
		menuItem.setAttribute( 'label',itemLabel ); 
		menuPopup.appendChild(menuItem); //add popup to menulist
	}
	
	menulist.appendChild(menuPopup); //add popup to menulist

	// in case only "browse" destination (no destinations in INI)
	if ( pathsCount == 0 )
	{
		// disable selection box
		menulist.setAttribute("disabled", true);
	}
	menulist.addEventListener("select", function(event) {fb2SaveContent.toggleDestinationChoice(event);},true);
	parent.appendChild(menulist); // add to container
},

insertMenu: function (saveGroup)
{ 
  var translator = document.getElementById('download_dialog');
  if ( translator == null)
  {
    dump("\ninsertMenu: 'download_dialog' not found\n");  
  }
  var saveItem = document.getElementById('save');
  if ( saveItem == null)
  {
    dump("\ninsertMenu: Item with ID save not found\n");
  }
  else
  {
	// get pointer to save item in the group
    var saveIndex = saveGroup.getIndexOfItem(saveItem);
	
	// create a HBox container
	var fb2ePubHBox = document.createElementNS(defaultNS,'hbox');
	if ( fb2ePubHBox == null)
	{
		dump("\ninsertMenu: Unable to create hbox container\n");
	}
	fb2ePubHBox.setAttribute( 'id', 'fb2epubcontainer' ); 
	fb2ePubHBox.setAttribute( 'flex', '1' ); 
	
	// Create a FB2EPub radio button
	this._fb2MenuItem = document.createElementNS(defaultNS,'radio');
	if ( this._fb2MenuItem == null)
	{
		dump("\ninsertMenu: Unable to create radio button\n");
	}
	this._fb2MenuItem.setAttribute( 'label', translator.getString("fb2epub_menu_item_id.label") ); // uses locale 
	this._fb2MenuItem.setAttribute( 'flex', '1' ); 
	this._fb2MenuItem.setAttribute( 'id', 'fb2epub-radio' ); 
	fb2ePubHBox.appendChild(this._fb2MenuItem); // add to container
	
	// Create and populate  menu list
	this.createMenuList(fb2ePubHBox);

	var addedElement;
	if (saveIndex  == saveGroup.itemCount ) // if 'save' - last item we append
	{
		addedElement = saveGroup.appendChild(fb2ePubHBox);
	}
	else // else insert before it
	{
	  addedElement = saveGroup.insertBefore(fb2ePubHBox,saveItem);
	}
	if ( addedElement == null )
	{
		dump("\ninsertMenu: adding menu entry failed");
	}
	else
	{
		saveGroup.addEventListener("select", function(event) {fb2SaveContent.toggleChoice(event);},true);
		saveGroup.selectedItem = this._fb2MenuItem;
	}
  }
  window.sizeToContent();
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


// starts an actual download process
// destination - path to save file to (including name)
// source - source file link
download: function(destination,source)
{
Task.spawn(function () {

  let list = yield Downloads.getList(Downloads.ALL);

  let view = {
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
      yield download.finalize(true);
    }
  } finally {
    yield list.removeView(view);
  }

}).then(null, Components.utils.reportError);
},

// Pop up a dialogue requesting user to select ePub file save path and location
// originalName - name to start with (without path)
getFileNameToSaveFromUser: function(originalName)
{
	// get file picker dialogue interface
	var nsIFilePicker = Components.interfaces.nsIFilePicker;
	var fp = Components.classes["@mozilla.org/filepicker;1"].createInstance(nsIFilePicker);
	fp.init(window, "Enter a File Name to save", nsIFilePicker.modeSave);
	fp.appendFilter("ePub file","*.ePub");
	fp.defaultExtension = "ePub";
	fp.defaultString = originalName;
	var result = fp.show();
	if (result == nsIFilePicker.returnOK || result == nsIFilePicker.returnReplace)
	{
		return fp.file.path;
	}
	return null;
},

isNumber: function(n) {
  return !isNaN(parseFloat(n)) && isFinite(n);
},

// React on user pressed "Accept" in SaveAs dialogue
downloadAndConvert: function()
{
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
	var destPath = null;
	if ( this._selectedDestinationId == 'fb2epub-menubrowseItem' )
	{
		destPath = this.getFileNameToSaveFromUser(basename);
	}
	else if (this.isNumber(this._selectedDestinationId))
	{
		var pathsCount =	ConverterPaths.GetPathsCount(pathsCount);
		if (this._selectedDestinationId >= pathsCount)
		{
			dump("\ndownloadAndConvert: Selected value " + this._selectedDestinationId + " is higher then total paths count: " + pathsCount + " !");
			return;
		}
		let path= ConverterPaths.GetPath(this._selectedDestinationId).path;
		destPath = path + basename;
	}
	else 
	{
		dump("\ndownloadAndConvert: Unknown item " + + "selected, calling user selection dialogue");
		destPath = this.getFileNameToSaveFromUser(basename);
	}
	if ( destPath != null)
	{
		this.download(destPath,dialog.mLauncher.source);
	}
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

