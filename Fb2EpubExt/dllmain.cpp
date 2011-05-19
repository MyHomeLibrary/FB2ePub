// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "Fb2EpubExt_i.h"
#include "dllmain.h"
#include <iostream>
#include "Shlobj.h"




const LPTSTR  c_tzINIFileName = _T("\\FB2EPUBExt.INI");
const LPTSTR  c_tzEXEFileName = _T("\\FB2EPUB.EXE");
const LPTSTR  c_tzLOGFileName = _T("\\FB2EPUBEXT.LOG");
const LPTSTR  FB2EPUB_SECTION = _T("FB2EPUB");

const LPTSTR  TARGETS_SECTION = _T("Targets");
const LPTSTR  FILTERS_SECTION = _T("Filter");

#define PATH_SIZE (1024*2+32)

CFb2EpubExtModule _AtlModule;

using namespace std;

std::basic_string<TCHAR> format_arg_list(const TCHAR *fmt, va_list args)
{
    if (!fmt) return _T("");
    int   result = -1, length = 256;
    TCHAR *buffer = 0;
    while (result == -1)
    {
        if (buffer) delete [] buffer;
        buffer = new TCHAR [length + 1];
        memset(buffer, 0, length + 1);
		result = _vsntprintf_s(buffer,length*sizeof(TCHAR), length, fmt, args);
        length *= 2;
    }
    std::basic_string<TCHAR> s= buffer;
    delete [] buffer;
    return s;
}

std::basic_string<TCHAR> format(const TCHAR *fmt, ...)
{
    va_list args;
    va_start(args, fmt);
    std::basic_string<TCHAR> s = format_arg_list(fmt, args);
    va_end(args);
    return s;
}


// DLL Entry Point
extern "C" BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID lpReserved)
{
	if ( DLL_PROCESS_ATTACH == dwReason )
	{
		_AtlModule.Init();
		_AtlModule.SetDllPath((HMODULE)hInstance);
		_AtlModule.StartLog(TRUE);
		clog << endl << "FB2EPUB shell extension DLL started." << endl;
		_AtlModule.LoadINI();
	}
	else if ( DLL_PROCESS_DETACH == dwReason )
	{
		_AtlModule.EndLog();
	}
	return _AtlModule.DllMain(dwReason, lpReserved); 
}

void CFb2EpubExtModule::Init()
{
	m_bAllowAllExtension = false;
	m_bAllowAllZip = false;
	m_bAllowAllRar = false;
}

void CFb2EpubExtModule::LoadINI()
{
	USES_CONVERSION;

	m_targets.clear();

	TCHAR iniFile[PATH_SIZE];
	::ZeroMemory(iniFile,sizeof(TCHAR)*PATH_SIZE);

	bool bDetected = false;
	if ( SHGetSpecialFolderPath(NULL,iniFile,CSIDL_APPDATA,FALSE))
	{
		lstrcat(iniFile,L"\\Lord KiRon\\FB2ePubExt");
		lstrcat(iniFile,c_tzINIFileName);
		if ( PathFileExists(iniFile) )
		{
			m_INIPath = iniFile;
			clog << "Located INI file in User Data folder at: " << W2A(m_INIPath.c_str()) << endl;
			bDetected = true;
		}
		else
		{
			clog << "NOT Located INI file in User Data folder at: " << W2A(m_INIPath.c_str()) << endl;
		}

	}
	else
	{
		clog << "Error getting special folder"  << endl;
	}
	if ( !bDetected)
	{
		lstrcpy(iniFile,GetPathWithoutFileName((LPTSTR)m_DLLPath.c_str()));
		lstrcat(iniFile,c_tzINIFileName);

		if ( PathFileExists(iniFile) ) // First check for INI in working directory
		{
			m_INIPath = iniFile;
			clog << "Located INI file at: " << W2A(m_INIPath.c_str()) << endl;
			bDetected = true;
		}
		else 
		{
			clog << "Unable to locate INI file " << endl;
		}
	}

	DWORD dwRes = 0;
	TCHAR path[PATH_SIZE+1];
	::ZeroMemory(path,sizeof(TCHAR)*(PATH_SIZE+1));
	if ( bDetected ) 	// If INI file located - try to load from INI
	{
		clog << "Attempting to read FB2EPUB.EXE location from INI" << endl;
		// try to read setting from INI
		dwRes = ::GetPrivateProfileString(FB2EPUB_SECTION,_T("Location"),NULL,path,1024,m_INIPath.c_str());
	}
	if ( !bDetected || dwRes == 0 || !PathFileExists(path) ) // is setting not found or referenced file does not exist, try to locate
	{
		clog << "Attempting to detect FB2EPUB.EXE location" << endl;
		if ( !FindConverterApp(path,PATH_SIZE) )
		{
			clog << "Unable to locate FB2EPUB converter. Aborting..."  << endl;
			return;
		}
	}
	
	m_Fb2EpubApplication = path;
	clog << "Detected FB2EPUB.EXE location " << W2A(m_Fb2EpubApplication.c_str())<< endl;

	ReadFilters();
	ReadTargets();
}


LPCTSTR CFb2EpubExtModule::GetPathWithoutFileName(LPTSTR filePath)
{
	using namespace std;

  	basic_string<TCHAR> path = filePath;
	string::size_type pos = path.rfind(_T('\\'));
	if ( pos != string::npos )
	{
		basic_string<TCHAR> withoutex;
		withoutex = path.substr(0,pos);
		LPTSTR Res = new TCHAR[withoutex.length()+2];
		lstrcpy(Res, withoutex.c_str());
		return Res;
	}
	return NULL;
}

BOOL CFb2EpubExtModule::FindConverterApp(LPTSTR outFile,unsigned int size)
{
	TCHAR exeFile[PATH_SIZE];
	::ZeroMemory(exeFile,sizeof(TCHAR)*(PATH_SIZE));
	lstrcpy(exeFile,GetPathWithoutFileName((LPTSTR)m_DLLPath.c_str()) );
	lstrcat(exeFile,c_tzEXEFileName);
	if ( PathFileExists(exeFile))
	{
		lstrcpyn(outFile,exeFile,size);
		return TRUE;
	}
	return FALSE;
}

void CFb2EpubExtModule::StartLog(BOOL bOverwrite)
{
	TCHAR szLogPath[PATH_SIZE];
	::ZeroMemory(szLogPath,(PATH_SIZE)*sizeof(TCHAR));
	bool bPathFound = false;
	DWORD dwResultSize = GetEnvironmentVariable(L"LOCALAPPDATA",NULL,0);
	if (  dwResultSize != 0 )
	{
		TCHAR* pBuffer = new TCHAR[dwResultSize+1];
		GetEnvironmentVariable(L"LOCALAPPDATA",pBuffer,dwResultSize);
		lstrcpy(szLogPath,pBuffer);
		if ( pBuffer[dwResultSize] != '\\' && pBuffer[dwResultSize] != '/' )
		{
			lstrcat(szLogPath,L"\\");
		}
		delete pBuffer;
		lstrcat(szLogPath,L"Lord KiRon\\");
		CreateDirectory(szLogPath,NULL);
		bPathFound = true;
	}
	if (!bPathFound)
	{
		CRegKey key;
		if (key.Open(HKEY_CURRENT_USER,L"Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Folders",KEY_READ) == ERROR_SUCCESS)
		{
			ULONG uChars = PATH_SIZE;
			if ( key.QueryStringValue(L"Local AppData",szLogPath,&uChars) == ERROR_SUCCESS)
			{
				if ( (uChars > 0) && (szLogPath[uChars-1] != '\\') && (szLogPath[uChars-1] != '/') )
				{
					lstrcat(szLogPath,L"\\");
				}
				lstrcat(szLogPath,L"Lord KiRon\\");
				CreateDirectory(szLogPath,NULL);
				bPathFound = true;
			}
		}
	}
	if (!bPathFound)
	{
		LPCTSTR szAppPath = GetPathWithoutFileName((LPTSTR)m_DLLPath.c_str());
		lstrcpy(szLogPath,szAppPath);
	}
	lstrcat(szLogPath,c_tzLOGFileName);
	if ( !out.is_open() )
	{
		ios_base::open_mode mode = ios_base::out;
		if ( bOverwrite )
		{
			mode |= ios_base::trunc;
		}
		else
		{
			mode |= ios_base::app;
		}
		out.open(szLogPath,mode);
	}
	if ( out )
	{
		clog.rdbuf(out.rdbuf());
	}
}

void CFb2EpubExtModule::EndLog()
{
	if ( out.is_open() )
	{
		out.flush();
		out.close();
	}
}

void CFb2EpubExtModule::SetDllPath(HMODULE hModule)
{
	TCHAR module[1024];
	::ZeroMemory(module,1024*sizeof(TCHAR));
	GetModuleFileName(hModule,module,1024);
	m_DLLPath = module;
}


void CFb2EpubExtModule::ReadTargets()
{
	TCHAR temp[PATH_SIZE+1];
	::ZeroMemory(temp,sizeof(TCHAR)*PATH_SIZE);
	UINT count = ::GetPrivateProfileInt(TARGETS_SECTION,_T("TargetsCount"),0,m_INIPath.c_str());	
	if (count > 0)
	{
		clog << count << " targets readed" << endl;
		int iSingleDestination = ::GetPrivateProfileInt(TARGETS_SECTION,_T("SingleDestination"),-1,m_INIPath.c_str());	
		clog << iSingleDestination << " set as SingleDestination" << endl;
		m_bUseSingleDestination = false;
		TCHAR section[1024];
		if ( iSingleDestination != -1 )
		{
				::ZeroMemory(section,sizeof(TCHAR)*1024);
				wsprintf(section,_T("Target%d"),iSingleDestination);
			bool bAdd = (::GetPrivateProfileInt(section,_T("ShowInShell"),1,m_INIPath.c_str()) == 1);
			if (bAdd)
			{
				m_bUseSingleDestination = true;
				target tempTarget;
				DWORD dwRes = ::GetPrivateProfileString(section,_T("TargetPath"),NULL,temp,1024,m_INIPath.c_str());
				if (dwRes != 0) 
				{
					tempTarget.path = temp;
					::ZeroMemory(temp,sizeof(TCHAR)*PATH_SIZE);
					DWORD dwRes = ::GetPrivateProfileString(section,_T("TargetName"),NULL,temp,1024,m_INIPath.c_str());
					std::basic_string<TCHAR> tempString;
					if ( dwRes == 0 )
					{
						tempString = tempTarget.path;
					}
					else
					{
						tempString = temp;
					}
					tempTarget.name = format(L"FB2ePub [%s]",tempString.c_str());
					m_targets.push_back(tempTarget);	
				}
				return;
			}
		}
		if (!m_bUseSingleDestination)
		{
			for (UINT i=1; i <= count; i ++ )
			{
				::ZeroMemory(section,sizeof(TCHAR)*1024);
				wsprintf(section,_T("Target%d"),i);
				::ZeroMemory(temp,sizeof(TCHAR)*PATH_SIZE);
				target tempTarget;
				bool bAdd = (::GetPrivateProfileInt(section,_T("ShowInShell"),1,m_INIPath.c_str()) == 1);
				if ( bAdd )
				{
					DWORD dwRes = ::GetPrivateProfileString(section,_T("TargetPath"),NULL,temp,1024,m_INIPath.c_str());
					if (dwRes != 0) 
					{
						tempTarget.path = temp;
						::ZeroMemory(temp,sizeof(TCHAR)*PATH_SIZE);
						DWORD dwRes = ::GetPrivateProfileString(section,_T("TargetName"),NULL,temp,1024,m_INIPath.c_str());
						if ( dwRes == 0 )
						{
							tempTarget.name = tempTarget.path;
						}
						else
						{
							tempTarget.name = temp;
						}
						m_targets.push_back(tempTarget);	
					}
				}
			}
		}
	}
}

void CFb2EpubExtModule::ReadFilters()
{
	m_bAllowAllExtension = (::GetPrivateProfileInt(FILTERS_SECTION,_T("AllowAny"),0,m_INIPath.c_str()) != 0);
	m_bAllowAllZip= (::GetPrivateProfileInt(FILTERS_SECTION,_T("AllowAllZIP"),0,m_INIPath.c_str()) != 0);
	m_bAllowAllRar= (::GetPrivateProfileInt(FILTERS_SECTION,_T("AllowAllRAR"),0,m_INIPath.c_str()) != 0);
}