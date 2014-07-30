"use strict";

this.EXPORTED_SYMBOLS = ["ConverterPaths"];

Components.utils.import("resource://gre/modules/XPCOMUtils.jsm");
Components.utils.import("resource://gre/modules/ctypes.jsm");
Components.utils.import("resource://gre/modules/AddonManager.jsm");

var ConverterPaths = {
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
				ConverterPaths.lib	=	ctypes.open(uri.file.path);
			}
			else
			{
				ConverterPaths.error = true; //  mark we need to exit wait in case of error
				dump("\nAddonManager.getAddonByID : Unable to locate DLL " + "js-ctype_connector.dll" + "\n");
			}
			});
			// wait until we open the library
			var thread = Components.classes["@mozilla.org/thread-manager;1"].getService().currentThread;
			while (this.lib == null && !this.error )
			 {
				thread.processNextEvent(true);
			 }
	
		// Declarations part
		//////////////////////////
		this.impGetPathsCount = this.lib.declare("CNTR_GetPathsCount",
							ctypes.winapi_abi,
							ctypes.bool,
							ctypes.uint32_t.ptr);	 

		this.impGetPath = this.lib.declare("CNTR_GetPath",
							ctypes.winapi_abi,
							ctypes.bool,
							ctypes.uint32_t,
							ctypes.jschar.ptr,
							ctypes.uint32_t.ptr);

		this.impGetPathName = this.lib.declare("CNTR_GetPathName",
							ctypes.winapi_abi,
							ctypes.bool,
							ctypes.uint32_t,
							ctypes.jschar.ptr,
							ctypes.uint32_t.ptr);
							
		//////////////////////////
		}
		
    },
	
	
	
	GetPathsCount: function()
	{
		var tempOut =ctypes.uint32_t(0);
		if (this.impGetPathsCount(tempOut.address()) == false )
		{
			return 0;
		}		
		return tempOut.value;
	},
	
	GetPath: function(pathNumber)
	{
		var Failed	=	false;
		var dataLength = 260;
		var NewString = ctypes.ArrayType(ctypes.jschar);
		var myArray = new NewString(dataLength);
		var DataLengthOut =ctypes.uint32_t(dataLength);
		if (this.impGetPath(pathNumber,myArray,DataLengthOut.address()) == false )
		{
			var result = { failed: true, path: ""};
			dump("\nGetPath: Failed!");
			return result;
			
		}
		if ( DataLengthOut.value <= dataLength )
		{
			var result = { failed: false, path: myArray.readString()};
			return result;
		}
		myArray = ctypes.jschar.array()(DataLengthOut.value);
		if (this.impGetPath(pathNumber,myArray,DataLengthOut.address()) == false )
		{
			var result = { failed: true, path: ""};
			dump("\nGetPath: Failed!");
			return result;
		}
		var result = { failed: false, path: myArray.readString()};
		return result;		
	},

	GetPathName: function(pathNumber)
	{
		var Failed	=	false;
		var dataLength = 260;
		var NewString = ctypes.ArrayType(ctypes.jschar);
		var myArray = new NewString(dataLength);
		var DataLengthOut =ctypes.uint32_t(dataLength);
		if (this.impGetPathName(pathNumber,myArray,DataLengthOut.address()) == false )
		{
			var result = { failed: true, name: ""};
			dump("\nGetPathName Failed!");
			return result;
			
		}
		if ( DataLengthOut.value <= dataLength )
		{
			var result = { failed: false, name: myArray.readString()};
			return result;
		}
		myArray = ctypes.jschar.array()(DataLengthOut.value);
		if (this.impGetPathName(pathNumber,myArray,DataLengthOut.address()) == false )
		{
			var result = { failed: true, name: ""};
			dump("\nGetPathName Failed!");
			return result;
		}
		var result = { failed: false, name: myArray.readString()};
		return result;		
	},
	
    //need to close the library once we're finished with it
    close: function() {
		if ( this.lib != null )
		{
			this.lib.close();
			this.lib= null;
		}
    }
};
