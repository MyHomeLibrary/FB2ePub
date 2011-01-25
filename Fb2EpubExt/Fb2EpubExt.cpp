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
	LONG    lRet;
	HRESULT hr = S_OK;

	if ( 0 == (GetVersion() & 0x80000000UL) )
	{

		lRet = reg.Open ( HKEY_LOCAL_MACHINE,
			_T("Software\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Approved"),
			KEY_SET_VALUE );

		if ( ERROR_SUCCESS != lRet )
		{
			clog << "Unable to write to aproved extensions registry branch";
			return E_ACCESSDENIED;
		}

		lRet = reg.SetStringValue( _T("Fb2EpubShlExt extension"), 
			_T("{5FFF3DF0-E213-4CA6-A37E-0C3BA1FE35FC}") );

		if ( ERROR_SUCCESS != lRet )
		{
			clog << "Unable to regisdter as aproved extension";
			return E_ACCESSDENIED;
		}
		reg.Close();
	}
	else
	{
		clog << "Not NT and higher!";
	}


	// Register .FB2
	hr = RegisterFileExtension(_T(".fb2"),_T("FictionBook.2"));
	if ( FAILED(hr) )
	{
		clog << "Unable to register FB2 extension";
		return hr;
	}

	// ZIP
	hr = RegisterFileExtension(_T(".zip"),_T("CompressedFolder"));
	if ( FAILED(hr) )
	{
		clog << "Unable to register ZIP extension";
		return hr;
	}


	// RAR
	hr = RegisterFileExtension(_T(".rar"),_T("WinRAR"));
	if ( FAILED(hr) )
	{
		return hr;
	}

	hr = _AtlModule.DllRegisterServer(FALSE);
	if (FAILED(hr))
	{
		clog << "Unable to register COM server";
	}
	return hr;
}

HRESULT RegisterFileExtension(LPTSTR fileExtension,LPTSTR defaultProgID)
{
	TCHAR Fb2ProgId[MAX_LENGTH];
	::ZeroMemory(&Fb2ProgId,sizeof(TCHAR)*MAX_LENGTH);
	CRegKey reg;
	LONG lRet;

	lRet = reg.Open(HKEY_CLASSES_ROOT,fileExtension);
	if ( ERROR_SUCCESS != lRet )
	{
		lRet = reg.Create(HKEY_CLASSES_ROOT,fileExtension);
		if ( ERROR_SUCCESS != lRet )
		{
			return E_ACCESSDENIED;
		}


	}
	ULONG iLen = MAX_LENGTH;
	lRet = reg.QueryStringValue(NULL,Fb2ProgId,&iLen);
	if ( ERROR_SUCCESS != lRet )
	{
		lstrcpy(Fb2ProgId,defaultProgID);
		reg.SetStringValue(NULL,Fb2ProgId);
	}
	reg.Close();

	HRESULT hr = RegisterContextMenuExtension(Fb2ProgId);
	if ( FAILED(hr) )
	{
		return hr;
	}
	return S_OK;
}

HRESULT RegisterContextMenuExtension(LPTSTR ProgId)
{
	CRegKey reg;
	LONG lRet = reg.Open(HKEY_CLASSES_ROOT,ProgId);
	if ( ERROR_SUCCESS != lRet )
	{
		reg.Create(HKEY_CLASSES_ROOT,ProgId);
		if ( ERROR_SUCCESS != lRet )
		{
			return E_ACCESSDENIED;
		}
	}
	reg.Close();

	TCHAR TempBuffer[MAX_LENGTH];
	::ZeroMemory(&TempBuffer,sizeof(TCHAR)*MAX_LENGTH);
	lstrcpy(TempBuffer,ProgId);
	lstrcat(TempBuffer,_T("\\ShellEx"));
	lRet = reg.Open(HKEY_CLASSES_ROOT,TempBuffer);
	if ( ERROR_SUCCESS != lRet )
	{
		lRet = reg.Create(HKEY_CLASSES_ROOT,TempBuffer);
		if ( ERROR_SUCCESS != lRet )
		{
			return E_ACCESSDENIED;
		}
	}
	reg.Close();

	lstrcat(TempBuffer,_T("\\ContextMenuHandlers"));
	lRet = reg.Open(HKEY_CLASSES_ROOT,TempBuffer);
	if ( ERROR_SUCCESS != lRet )
	{
		lRet = reg.Create(HKEY_CLASSES_ROOT,TempBuffer);
		if ( ERROR_SUCCESS != lRet )
		{
			return E_ACCESSDENIED;
		}
	}
	reg.Close();

	lstrcat(TempBuffer,_T("\\Fb2EpubShlExt"));
	lRet = reg.Open(HKEY_CLASSES_ROOT,TempBuffer);
	if ( ERROR_SUCCESS != lRet )
	{
		lRet = reg.Create(HKEY_CLASSES_ROOT,TempBuffer);
		if ( ERROR_SUCCESS != lRet )
		{
			return E_ACCESSDENIED;
		}
	}
	
	::ZeroMemory(TempBuffer,sizeof(TCHAR)*MAX_LENGTH);
	StringFromGUID2(CLSID_Fb2EpubShlExt,TempBuffer,MAX_LENGTH);
	lRet = reg.SetStringValue(NULL,TempBuffer);
	if ( ERROR_SUCCESS != lRet )
	{
		return E_ACCESSDENIED;
	}

	reg.Close();

	return S_OK;
}

HRESULT UnregisterContextMenuExtension(LPTSTR ProgId)
{
	CRegKey reg;
	TCHAR TempBuffer[MAX_LENGTH];
	::ZeroMemory(&TempBuffer,sizeof(TCHAR)*MAX_LENGTH);
	lstrcpy(TempBuffer,ProgId);
	lstrcat(TempBuffer,_T("\\ShellEx\\ContextMenuHandlers"));
	LONG lRet = reg.Open(HKEY_CLASSES_ROOT,TempBuffer);
	if ( ERROR_SUCCESS == lRet )
	{
		lRet = reg.RecurseDeleteKey(_T("Fb2EpubShlExt"));
		if ( ERROR_SUCCESS != lRet )
		{
			return E_ACCESSDENIED;
		}
	}
	return S_OK;
}

HRESULT UnregisterFileExtension(LPTSTR extension)
{
	CRegKey reg;
	LONG    lRet;

	TCHAR Fb2ProgId[MAX_LENGTH];
	::ZeroMemory(&Fb2ProgId,sizeof(TCHAR)*MAX_LENGTH);

	lRet = reg.Open(HKEY_CLASSES_ROOT,extension);
	if ( ERROR_SUCCESS == lRet )
	{
		ULONG iLen = MAX_LENGTH;
		lRet = reg.QueryStringValue(NULL,Fb2ProgId,&iLen);
		if ( ERROR_SUCCESS == lRet && Fb2ProgId[0] != 0)
		{
			HRESULT hr = UnregisterContextMenuExtension(Fb2ProgId);
			if ( FAILED(hr) )
			{
				return hr;
			}
		}
		reg.Close();
	}
	return S_OK;
}

// DllUnregisterServer - Removes entries from the system registry
STDAPI DllUnregisterServer(void)
{
	CRegKey reg;
	LONG    lRet;
	HRESULT hr = S_OK;

	if ( 0 == (GetVersion() & 0x80000000UL) )
	{

		lRet = reg.Open ( HKEY_LOCAL_MACHINE,
			_T("Software\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Approved"),
			KEY_SET_VALUE );

		if ( ERROR_SUCCESS == lRet )
		{
			lRet = reg.DeleteValue ( _T("{5FFF3DF0-E213-4CA6-A37E-0C3BA1FE35FC}") );
		}
		reg.Close();
	}
	
	hr = UnregisterFileExtension(_T(".fb2"));
	if ( FAILED(hr) )
	{
		return hr;
	}

	hr = UnregisterFileExtension(_T(".zip"));
	if ( FAILED(hr) )
	{
		return hr;
	}

	hr = UnregisterFileExtension(_T(".rar"));
	if ( FAILED(hr) )
	{
		return hr;
	}

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


