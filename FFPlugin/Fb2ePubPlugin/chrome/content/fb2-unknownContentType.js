window.addEventListener("load", function load(event){
    window.removeEventListener("load", load, false); //remove listener, no longer needed
    fb2SaveContent.init();  
},false);

var fb2SaveContent = {

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

insertMenu: function (saveGroup)
{
  var saveItem = document.getElementById('save');
  if ( saveItem == null)
  {
    dump("\nItem with ID save not found");
  }
  else
  {
    var saveIndex = saveGroup.getIndexOfItem(saveItem);
    var newMenuItem = null;
	if (saveIndex  == saveGroup.itemCount ) // if 'save' - last item we append
	{
	  newMenuItem = saveGroup.appendItem("Fb2ePub (convert to ePub)");
	}
	else // else insert before it
	{
	  newMenuItem = saveGroup.insertItemAt(saveIndex+1,"Fb2ePub (convert to ePub)");
	}
	if ( newMenuItem == null )
	{
		dump("\nProblem");
	}
	else
	{
		newMenuItem.id = "fb2epub-radio";
	}
  }
},

init: function()
{
dump("\n");
	if ( this.isfb2extension(dialog.mLauncher.source))
	{
		//dump("\nFB2 file downloading");
		var saveGroup = document.getElementById('mode');
		if ( saveGroup == null )
		{
		  dump ("\nMode radiogroup not found");
		}
		else
		{
			this.insertMenu(saveGroup);
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

