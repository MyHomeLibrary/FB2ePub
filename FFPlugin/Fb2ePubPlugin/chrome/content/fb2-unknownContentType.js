window.addEventListener("load", function load(event){
    window.removeEventListener("load", load, false); //remove listener, no longer needed
    fb2SaveContent.init();  
},false);

var fb2SaveContent = {
	_original_onaccept_action: null, // used to store original "onaccept" action
	_fb2MenuItem: null, // menu item we insert
	_fb2ItemSelected: false,
	_rememberButtonDisabledPrevState: false,
	_rememberButtonCheckedPrevState: false,

// Check if the passed source name is one of FB2 extensions
isfb2extension: function(sourceURI)
{
 var myURL = sourceURI.QueryInterface(Components.interfaces.nsIURL);

 var extension = myURL.fileExtension.toLowerCase();
 if ( extension == "fb2") // if FB2 extension then FB2
 {
    return true;
 }
 
 // now let's check if it double extension for archives
 if ( extension == "zip" || extension == "rar")
 {
	 // get name without extension
	 var partWithoutExtension = myURL.fileBaseName;
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
	rememberbutton.setAttribute("checked", check);
},

// react on user clicking on one of the radio buttons
toggleChoice: function(event)
{
  // get pointer to radio menu
  var saveGroup = document.getElementById('mode');
  if ( saveGroup == null ) // if rqadio menu not found
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

init: function()
{
	dump("\n\nStarting:\n");
	if ( this.isfb2extension(dialog.mLauncher.source))
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
				this._original_onaccept_action = d.getAttribute('ondialogaccept'); // save original action performed on accept
			    //d.setAttribute('ondialogaccept',"return fb2SaveContent.onConvAccept();");
			}
		}
	}
	else
	{
		//dump("\nNot FB2 file downloading");
	}
},

onConvAccept: function()
{
	dump("\nAccept pressed");
	return _original_onaccept_action;
},

onload: function()
{

},



};

