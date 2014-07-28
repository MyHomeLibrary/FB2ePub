"use strict";

this.EXPORTED_SYMBOLS = ["Fb2ePubConverter"];

Components.utils.import("resource://gre/modules/XPCOMUtils.jsm");
Components.utils.import("resource://gre/modules/ctypes.jsm");
Components.utils.import("resource://gre/modules/AddonManager.jsm");

var Fb2ePubConverter = {
    lib: null,
	error:false,

    init: function() {
	if ( this.lib == null )
	{
		//Open the library you want to call
		AddonManager.getAddonByID("fb2epub_plugin@fb2epub.net", function(addon)
		{
		var uri = addon.getResourceURI("components/js-ctype_connector.dll");
		if (uri instanceof Components.interfaces.nsIFileURL)
		{
			Fb2ePubConverter.lib	=	ctypes.open(uri.file.path);
		}
		else
		{
			Fb2ePubConverter.error = true; //  mark we need to exit wait in case of error
		}
		});
		// wait until we open the library
		var thread = Components.classes["@mozilla.org/thread-manager;1"].getService().currentThread;
		while (this.lib == null && !this.error  && thread != null)
		 {
			thread.processNextEvent(true);
		 }
		// Declarations part
		//////////////////////////
		this.impConvert = this.lib.declare("CNTR_Convert",
							ctypes.winapi_abi,
							ctypes.bool,
							ctypes.jschar.ptr,
							ctypes.jschar.ptr);
		//////////////////////////
	}
    },
	 
	Convert: function(inputPath,outputPath)
	{
		if (this.impConvert(inputPath,outputPath) == false )
		{
			return false;
		}		
		return true;
	},	 
	//need to close the library once we're finished with it
    close: function() {
        this.lib.close();
		this.lib = null;
    }
};