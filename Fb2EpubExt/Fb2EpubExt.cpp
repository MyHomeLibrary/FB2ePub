// Fb2EpubExt.cpp : Implementation of DLL Exports.


#include "stdafx.h"
#include "resource.h"
#include "Fb2EpubExt_i.h"
#include "dllmain.h"
#include <iostream>

#define MAX_LENGTH 1024

using namespace std;

//Forward declarations
HRESULT RegisterContextMenuExtension(LPTSTR);
HRESULT UnregisterContextMenuExtension(LPTSTR);
HRESULT RegisterFileExtension(LPTSTR,LPTSTR);
HRESULT UnregisterFileExtension(LPTSTR);

// Used to determine whether the DLL can be unloaded by OLE
STDAPI DllCanUnloadNow(void)
{
    return _AtlModule.DllCanUnloadNow();
}


// Returns a class factory to create an object of the requested type
STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
    return _AtlModule.DllGetClassObject(rclsid, riid, ppv);
}


// DllRegisterServer - Adds entries to the system registry
STDAPI DllRegisterServer(void)
{
	CRegKey reg;
	HRESULT hr = S_OK;

	hr = _AtlModule.DllRegisterServer(FALSE);
	if (FAILED(hr))
	{
		clog << "Unable to register COM server";
	}
	return hr;
}



// DllUnregisterServer - Removes entries from the system registry
STDAPI DllUnregisterServer(void)
{
	CRegKey reg;
	HRESULT hr = S_OK;


	hr = _AtlModule.DllUnregisterServer(FALSE);
	return hr;
}



// DllInstall - Adds/Removes entries to the system registry per user
//              per machine.	
STDAPI DllInstall(BOOL bInstall, LPCWSTR pszCmdLine)
{
    HRESULT hr = E_FAIL;
    static const wchar_t szUserSwitch[] = _T("user");

    if (pszCmdLine != NULL)
    {
    	if (_wcsnicmp(pszCmdLine, szUserSwitch, _countof(szUserSwitch)) == 0)
    	{
    		AtlSetPerUserRegistration(true);
    	}
    }

    if (bInstall)
    {	
    	hr = DllRegisterServer();
    	if (FAILED(hr))
    	{	
    		DllUnregisterServer();
    	}
    }
    else
    {
    	hr = DllUnregisterServer();
    }

    return hr;
}


