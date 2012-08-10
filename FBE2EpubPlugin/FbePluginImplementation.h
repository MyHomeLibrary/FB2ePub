// FbePluginImplementation.h : Declaration of the CFbePluginImplementation

#pragma once
#include "resource.h"       // main symbols



#include "FBE2EpubPlugin_i.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;


// CFbePluginImplementation

class ATL_NO_VTABLE CFbePluginImplementation :
	public CComObjectRootEx<CComMultiThreadModel>,
	public CComCoClass<CFbePluginImplementation, &CLSID_FbePluginImplementation>,
	public IFbePluginImplementation,
	public IDispatchImpl<IFBEExportPlugin, &__uuidof(IFBEExportPlugin)>
{
public:
	CFbePluginImplementation()
	{
	}

	DECLARE_REGISTRY_RESOURCEID(IDR_FBEPLUGINIMPLEMENTATION)


	BEGIN_COM_MAP(CFbePluginImplementation)
		COM_INTERFACE_ENTRY(IFbePluginImplementation)
		COM_INTERFACE_ENTRY(IFBEExportPlugin)
	END_COM_MAP()



	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:




	// IFBEExportPlugin Methods
public:
	STDMETHOD(Export)(long hWnd,BSTR filename,IDispatch *doc);
};

OBJECT_ENTRY_AUTO(__uuidof(FbePluginImplementation), CFbePluginImplementation)
