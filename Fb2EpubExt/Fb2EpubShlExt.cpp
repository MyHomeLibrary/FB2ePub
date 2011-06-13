// Fb2EpubShlExt.cpp : Implementation of CFB2EPUBShlExt

#include "stdafx.h"
#include "Fb2EpubShlExt.h"
#include "dllmain.h"
#include <string>
#include <algorithm>
#include <locale>
#include "ProgressDialog.h"
#include <iostream>

using namespace std;


// CFB2EPUBShlExt

extern class CFb2EpubExtModule _AtlModule;

// Forward declarations
bool IsFb2File(LPTSTR);

STDMETHODIMP CFB2EPUBShlExt::Initialize ( 
										LPCITEMIDLIST pidlFolder,
										LPDATAOBJECT pDataObj,
										HKEY hProgID )
{
	_AtlModule.StartLog(); // in case was stopped
	UINT      uNumFiles;
	HDROP     hdrop;
	FORMATETC etc = { CF_HDROP, NULL, DVASPECT_CONTENT,
		-1, TYMED_HGLOBAL };
	STGMEDIUM stg = { TYMED_HGLOBAL };
	TCHAR     szFile[1024];


	if ( FAILED( pDataObj->GetData ( &etc, &stg ) ))
	{
		clog << "Error initializing menu - can't get data" << endl;
		return E_INVALIDARG;
	}

	if ( _AtlModule.m_Fb2EpubApplication.empty() )
	{
		clog << "Error initializing menu - converter application path not set" << endl;
		return E_INVALIDARG;
	}

	// Get an HDROP handle.

	hdrop = (HDROP) GlobalLock ( stg.hGlobal );

	if ( NULL == hdrop )
	{
		ReleaseStgMedium ( &stg );
		clog << "Error initializing menu - can't lock data" << endl;
		return E_INVALIDARG;
	}

	// Determine how many files are involved in this operation.

	uNumFiles = DragQueryFile ( hdrop, 0xFFFFFFFF, NULL, 0 );

	for ( UINT uFile = 0; uFile < uNumFiles; uFile++ )
	{
		// Get the next filename.

		if ( 0 == DragQueryFile ( hdrop, uFile, szFile, 1024 ) )
			continue;
		if ( _AtlModule.m_bAllowAllExtension || IsFb2File(szFile))
		{		
			m_lsFiles.push_back ( szFile );
		}

	}

	GlobalUnlock ( stg.hGlobal );
	ReleaseStgMedium ( &stg );

	m_hBitmap = (HBITMAP)LoadImage((HMODULE)_AtlBaseModule.m_hInst,
		MAKEINTRESOURCE(IDB_BITMAP1),IMAGE_BITMAP,0,0,LR_DEFAULTSIZE);
	if ( m_hBitmap == NULL)
	{
		clog << "Got error reading bitmap :" <<  GetLastError() <<endl;
	}


	return  (m_lsFiles.size() > 0) ? S_OK : E_INVALIDARG;
}

HRESULT  CFB2EPUBShlExt::CreateFb2SubMenu(HMENU hmenu, UINT uMenuIndex,UINT uidFirstCmd)
{
	int iItemCount = uidFirstCmd;
	MENUITEMINFO ItemInfo;

	// Create sub menu
	HMENU hSubMenu = CreatePopupMenu(); 

	// Top separator
	::ZeroMemory(&ItemInfo,sizeof(MENUITEMINFO));
	ItemInfo.cbSize = sizeof(MENUITEMINFO);
	ItemInfo.fMask = MIIM_FTYPE | MIIM_ID;
	ItemInfo.fType = MFT_SEPARATOR;
	ItemInfo.wID = iItemCount++;
	if(InsertMenuItem ( hmenu, uMenuIndex, TRUE,  &ItemInfo) == 0)
	{
		DWORD dwErr = GetLastError();
		_ASSERT(FALSE);
	}


	// Menu popo up item
	const TCHAR* name = _T("FB2EPUB");
	size_t length = lstrlen(name);

	::ZeroMemory(&ItemInfo,sizeof(MENUITEMINFO));
	ItemInfo.cbSize = sizeof(MENUITEMINFO);
	ItemInfo.fMask = MIIM_SUBMENU | MIIM_STRING | MIIM_ID;
	ItemInfo.hSubMenu = hSubMenu;
	ItemInfo.wID = iItemCount++;
	ItemInfo.cch = (UINT)length;
	ItemInfo.dwTypeData = new TCHAR[length+1];
	lstrcpyn(ItemInfo.dwTypeData ,name,(UINT)length+1);

	if(InsertMenuItem ( hmenu, uMenuIndex, TRUE,  &ItemInfo) == 0)
	{
		DWORD dwErr = GetLastError();
		_ASSERT(FALSE);
	}



	// "Here" action item
	const TCHAR* szHereName = _T("Here");
	length = lstrlen(szHereName);
	::ZeroMemory(&ItemInfo,sizeof(MENUITEMINFO));
	ItemInfo.cbSize = sizeof(MENUITEMINFO);
	ItemInfo.fMask = MIIM_STRING | MIIM_ID ;
	ItemInfo.hSubMenu = hSubMenu;
	ItemInfo.fType = MFT_STRING;
	ItemInfo.wID = iItemCount++;
	ItemInfo.cch = (UINT)length;
	ItemInfo.dwTypeData = new TCHAR[length+1];
	lstrcpyn(ItemInfo.dwTypeData ,szHereName,(UINT)length+1);
	if(InsertMenuItem(hSubMenu,0,TRUE,&ItemInfo) == 0 )
	{
		DWORD dwErr = GetLastError();
		_ASSERT(FALSE);
	}
	SetMenuItemBitmaps(hSubMenu, 0, MF_BYPOSITION, m_hBitmap, m_hBitmap);

	int counter = 1;
	for ( target_list::iterator t = _AtlModule.m_targets.begin(); t != _AtlModule.m_targets.end(); t++)
	{
		length = t->name.length();
		::ZeroMemory(&ItemInfo,sizeof(MENUITEMINFO));
		ItemInfo.cbSize = sizeof(MENUITEMINFO);
		ItemInfo.fMask = MIIM_STRING | MIIM_ID ;
		ItemInfo.hSubMenu = hSubMenu;
		ItemInfo.fType = MFT_STRING;
		ItemInfo.wID = iItemCount++;
		ItemInfo.cch = (UINT)length;
		ItemInfo.dwTypeData = new TCHAR[length+1];
		lstrcpyn(ItemInfo.dwTypeData ,t->name.c_str(),(UINT)length+1);
		if(InsertMenuItem(hSubMenu,counter++,TRUE,&ItemInfo) == 0 )
		{
			DWORD dwErr = GetLastError();
			_ASSERT(FALSE);
		}
		SetMenuItemBitmaps(hSubMenu, counter-1, MF_BYPOSITION, m_hBitmap, m_hBitmap);
	}

	// Bottom separator
	::ZeroMemory(&ItemInfo,sizeof(MENUITEMINFO));
	ItemInfo.cbSize = sizeof(MENUITEMINFO);
	ItemInfo.fMask = MIIM_FTYPE | MIIM_ID;
	ItemInfo.fType = MFT_SEPARATOR;
	ItemInfo.wID = iItemCount++;
	if(InsertMenuItem ( hmenu, uMenuIndex, TRUE,  &ItemInfo) == 0)
	{
		DWORD dwErr = GetLastError();
		_ASSERT(FALSE);
	}


	return MAKE_HRESULT ( SEVERITY_SUCCESS, FACILITY_NULL, iItemCount - uidFirstCmd +1 );
}

HRESULT  CFB2EPUBShlExt::CreateFb2SubItem(HMENU hmenu, UINT uMenuIndex,UINT uidFirstCmd,TCHAR* tchName)
{
	MENUITEMINFO ItemInfo;
	
	const TCHAR* szHereName = tchName;
	size_t length = lstrlen(szHereName);
	::ZeroMemory(&ItemInfo,sizeof(MENUITEMINFO));
	ItemInfo.cbSize = sizeof(MENUITEMINFO);
	ItemInfo.fMask = MIIM_STRING | MIIM_ID ;
	ItemInfo.hSubMenu = hmenu;
	ItemInfo.fType = MFT_STRING;
	ItemInfo.wID = uidFirstCmd;
	ItemInfo.cch = (UINT)length;
	ItemInfo.dwTypeData = new TCHAR[length+1];
	lstrcpyn(ItemInfo.dwTypeData ,szHereName,(UINT)length+1);
	if(InsertMenuItem(hmenu,0,TRUE,&ItemInfo) == 0 )
	{
		DWORD dwErr = GetLastError();
		_ASSERT(FALSE);
	}
	SetMenuItemBitmaps(hmenu, uidFirstCmd, MF_BYCOMMAND, m_hBitmap, m_hBitmap);

	return MAKE_HRESULT ( SEVERITY_SUCCESS, FACILITY_NULL, uidFirstCmd);
}


HRESULT CFB2EPUBShlExt::QueryContextMenu (
	HMENU hmenu, UINT uMenuIndex, UINT uidFirstCmd,
	UINT uidLastCmd, UINT uFlags )
{
	// If the flags include CMF_DEFAULTONLY then we shouldn't do anything.

	if ( uFlags & CMF_DEFAULTONLY )
		return MAKE_HRESULT ( SEVERITY_SUCCESS, FACILITY_NULL, 0 );


	if (_AtlModule.m_targets.size() > 0 )
	{
		if ( _AtlModule.m_targets.size() == 1 && _AtlModule.m_bUseSingleDestination )
		{
			target_list::iterator t;
			t = _AtlModule.m_targets.begin();
			return CreateFb2SubItem(hmenu,uMenuIndex,uidFirstCmd,(TCHAR*)t->name.c_str());
		}
		return CreateFb2SubMenu(hmenu,uMenuIndex,uidFirstCmd);
	}
	return CreateFb2SubItem(hmenu,uMenuIndex,uidFirstCmd,_T("FB2ePub [Here]"));
}


HRESULT CFB2EPUBShlExt::GetCommandString (
	UINT_PTR idCmd, UINT uFlags, UINT* pwReserved,
	LPSTR pszName, UINT cchMax )
{
	USES_CONVERSION;

	// Check idCmd, it must be 0 since we have only one menu item.

	if ( 0 != idCmd )
		return E_INVALIDARG;

	// If Explorer is asking for a help string, copy our string into the

	// supplied buffer.

	if ( uFlags & GCS_HELPTEXT )
	{
		LPCTSTR szText = _T("This is the Fb2Epub shell extension's help");

		if ( uFlags & GCS_UNICODE )
		{
			// We need to cast pszName to a Unicode string, and then use the

			// Unicode string copy API.

			lstrcpynW ( (LPWSTR) pszName, T2CW(szText), cchMax );
		}
		else
		{
			// Use the ANSI string copy API to return the help string.

			lstrcpynA ( pszName, T2CA(szText), cchMax );
		}

		return S_OK;
	}

	return E_INVALIDARG;
}

HRESULT CFB2EPUBShlExt::InvokeCommand (
									   LPCMINVOKECOMMANDINFO pCmdInfo )
{
	// If lpVerb really points to a string, ignore this function call and bail out.

	if ( 0 != HIWORD( pCmdInfo->lpVerb ) )
	{
		_AtlModule.EndLog();
		return E_INVALIDARG;
	}

	// Get the command index - the only valid one is 0.

	WORD wCommand = LOWORD( pCmdInfo->lpVerb );
	if ( _AtlModule.m_targets.size() > 0 )
	{
		if ( _AtlModule.m_bUseSingleDestination )
		{
			clog << endl << "Processing user selection ..." << endl;
			target_list::iterator t;
			t = _AtlModule.m_targets.begin();
			CProgressDialog dlg(&m_lsFiles,t->path.c_str());
			dlg.DoModal();
			_AtlModule.EndLog();
			return S_OK;
		}
		else if ( (wCommand > 2) && (wCommand <= 2 + _AtlModule.m_targets.size()) )
		{
			clog << endl << "Processing user selection ..." << endl;
			int count =1;
			target_list::iterator t;
			for ( t = _AtlModule.m_targets.begin(); t != _AtlModule.m_targets.end(), count < (wCommand -2) ; t++ , count++)
			{

			}
			CProgressDialog dlg(&m_lsFiles,t->path.c_str());
			dlg.DoModal();
			_AtlModule.EndLog();
			return S_OK;
		}
	}

	clog << endl << "Processing user selection ..." << endl;
	CProgressDialog dlg(&m_lsFiles,NULL);
	dlg.DoModal();
	_AtlModule.EndLog();
	return S_OK;

}


bool IsFb2File(LPTSTR filePath)
{
	using namespace std;
	basic_string<TCHAR> fileFullPath = filePath;
	transform(fileFullPath.begin(), fileFullPath.end(), fileFullPath.begin(), toupper);
	LPTSTR fex = PathFindExtension(fileFullPath.c_str());
	if ( lstrcmp (fex,_T(".FB2"))  == 0)
	{
		return true;
	}
	if ( (lstrcmp (fex,_T(".ZIP"))  == 0) )
	{
		if ( _AtlModule.m_bAllowAllZip )
		{
			return true;
		}
		string::size_type index =  fileFullPath.rfind(fex);
		basic_string<TCHAR> name = fileFullPath.substr(0,index);
		if ( name.find(_T(".")) != string::npos )
		{
			fex = PathFindExtension(name.c_str());
			if ( lstrcmp (fex,_T(".FB2"))  == 0)
			{
				return true;
			}
		}
	}
	if ( (lstrcmp (fex,_T(".RAR"))  == 0))
	{
		if ( _AtlModule.m_bAllowAllRar)
		{
			return true;
		}
		string::size_type index =  fileFullPath.rfind(fex);
		basic_string<TCHAR> name = fileFullPath.substr(0,index);
		if ( name.find(_T(".")) != string::npos )
		{
			fex = PathFindExtension(name.c_str());
			if ( lstrcmp (fex,_T(".FB2"))  == 0)
			{
				return true;
			}
		}
	}
	
	return false;
}