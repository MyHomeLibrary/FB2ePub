// dllmain.h : Declaration of module class.

class CFBE2EpubPluginModule : public ATL::CAtlDllModuleT< CFBE2EpubPluginModule >
{
public :
	DECLARE_LIBID(LIBID_FBE2EpubPluginLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_FBE2EPUBPLUGIN, "{87B37C6E-F07E-4C50-9156-590E57AA93F0}")
};

extern class CFBE2EpubPluginModule _AtlModule;
