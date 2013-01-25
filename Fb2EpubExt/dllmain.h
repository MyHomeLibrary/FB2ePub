// dllmain.h : Declaration of module class.
#include <fstream>

typedef std::list< std::basic_string<TCHAR> > string_list;

typedef struct 
{
	std::basic_string<TCHAR> path;
	std::basic_string<TCHAR> name;
}target;

typedef std::list< target > target_list;

class CFb2EpubExtModule : public CAtlDllModuleT< CFb2EpubExtModule >
{
public :
	std::basic_string<TCHAR> m_INIPath;
	target_list m_targets;
	bool m_bAllowAllExtension;
	bool m_bAllowAllZip;
	bool m_bAllowAllRar;
	bool m_bUseSingleDestination;


	DECLARE_LIBID(LIBID_Fb2EpubExtLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_FB2EPUBEXT, "{4A62D35B-DFF9-4901-A0EF-397236523B33}")
	void LoadINI();

	static LPCTSTR GetPathWithoutFileName(LPTSTR);

	void StartLog(BOOL bOverwrite = FALSE);

	void EndLog();

	void SetDllPath(HMODULE hModule);

	void Init();

private:
	// Read target from INI
	void ReadTargets();

	// Read extension filter settings from INI
	void ReadFilters();

	BOOL FindConverterApp(LPTSTR,unsigned int);

	std::basic_string<TCHAR> m_DLLPath; 

	std::ofstream out;

};

extern class CFb2EpubExtModule _AtlModule;
