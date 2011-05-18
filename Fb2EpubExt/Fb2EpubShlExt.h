// Fb2EpubShlExt.h : Declaration of the CFB2EPUBShlExt

#pragma once
#include "resource.h"       // main symbols

#include "Fb2EpubExt_i.h"
#include <shlobj.h>


typedef std::list< std::basic_string<TCHAR> > string_list;



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif



// CFB2EPUBShlExt

class ATL_NO_VTABLE CFB2EPUBShlExt :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CFB2EPUBShlExt, &CLSID_Fb2EpubShlExt>,
	public IShellExtInit,
	public IContextMenu
{
public:
	CFB2EPUBShlExt()
		: m_hBitmap(NULL)
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_FB2EPUBSHLEXT)


BEGIN_COM_MAP(CFB2EPUBShlExt)
	COM_INTERFACE_ENTRY(IShellExtInit)
	COM_INTERFACE_ENTRY(IContextMenu)
	//COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()



	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
		if (m_hBitmap != NULL)
		{
			DeleteObject(m_hBitmap);
			m_hBitmap = NULL;
		}
	}

public:

	// IShellExtInit
  STDMETHODIMP Initialize(LPCITEMIDLIST, LPDATAOBJECT, HKEY);

	// IContextMenu
  STDMETHODIMP GetCommandString(UINT_PTR, UINT, UINT*, LPSTR, UINT);
  STDMETHODIMP InvokeCommand(LPCMINVOKECOMMANDINFO);
  STDMETHODIMP QueryContextMenu(HMENU, UINT, UINT, UINT, UINT);

protected:
	string_list m_lsFiles;
	HBITMAP m_hBitmap;

	HRESULT  CreateFb2SubMenu(HMENU hmenu, UINT uMenuIndex,UINT uidFirstCmd);

	HRESULT  CreateFb2SubItem(HMENU hmenu, UINT uMenuIndex,UINT uidFirstCmd,TCHAR* tchName);

};

OBJECT_ENTRY_AUTO(__uuidof(Fb2EpubShlExt), CFB2EPUBShlExt)
