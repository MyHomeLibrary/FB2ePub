// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "Fb2EpubExt_i.h"
#include "dllmain.h"
#include <iostream>




const LPTSTR  c_tzINIFileName = _T("\\FB2EPUBExt.INI");
const LPTSTR  c_tzEXEFileName = _T("\\FB2EPUB.EXE");
const LPTSTR  c_tzLOGFileName = _T("\\FB2EPUBEXT.LOG");
const LPTSTR  FB2EPUB_SECTION = _T("FB2EPUB");

const LPTSTR  TARGETS_SECTION = _T("Targets");
const LPTSTR  FILTERS_SECTION = _T("Filter");

#define PATH_SIZE (1024+32)

CFb2EpubExtModule _AtlModule;

using namespace std;

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
	BOOL bINIFound = FALSE;

	m_targets.clear();

	TCHAR iniFile[PATH_SIZE];
	::ZeroMemory(iniFile,sizeof(TCHAR)*PATH_SIZE);
	lstrcpy(iniFile,GetPathWithoutFileName((LPTSTR)m_DLLPath.c_str()));
	lstrcat(iniFile,c_tzINIFileName);

	if ( PathFileExists(iniFile) ) // First check for INI in working directory
	{
		m_INIPath = iniFile;
		clog << "Located INI file at: " << W2A(m_INIPath.c_str()) << endl;
		bINIFound = TRUE;
	}
	else 
	{
		clog << "Unable to locate INI file " << endl;
	}

	DWORD dwRes = 0;
	TCHAR path[PATH_SIZE+1];
	::ZeroMemory(path,sizeof(TCHAR)*(PATH_SIZE+1));
	if ( bINIFound ) 	// If INI file located - try to load from INI
	{
		clog << "Attempting to read FB2EPUB.EXE location from INI" << endl;
		// try to read setting from INI
		dwRes = ::GetPrivateProfileString(FB2EPUB_SECTION,_T("Location"),NULL,path,1024,m_INIPath.c_str());
		ReadTargets();
	}
	if ( !bINIFound || dwRes == 0 || !PathFileExists(path) ) // is setting not found or referenced file does not exist, try to locate
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
	LPCTSTR szAppPath = GetPathWithoutFileName((LPTSTR)m_DLLPath.c_str());
	TCHAR szLogPath[PATH_SIZE];
	::ZeroMemory(szLogPath,(PATH_SIZE)*sizeof(TCHAR));
	lstrcpy(szLogPath,szAppPath);
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
	UINT count = ::GetPrivateProfileInt(TARGETS_SECTION,_T("TargetsCount"),-1,m_INIPath.c_str());	
	if (count != (UINT)-1)
	{
		clog << count << " targets readed" << endl;
		TCHAR section[1024];
		for (UINT i=1; i <= count; i ++ )
		{
			::ZeroMemory(section,sizeof(TCHAR)*1024);
			wsprintf(section,_T("Target%d"),i);
			::ZeroMemory(temp,sizeof(TCHAR)*PATH_SIZE);
			target tempTarget;
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

void CFb2EpubExtModule::ReadFilters()
{
	m_bAllowAllExtension = (::GetPrivateProfileInt(FILTERS_SECTION,_T("AllowAny"),0,m_INIPath.c_str()) != 0);
	m_bAllowAllZip= (::GetPrivateProfileInt(FILTERS_SECTION,_T("AllowAllZIP"),0,m_INIPath.c_str()) != 0);
	m_bAllowAllRar= (::GetPrivateProfileInt(FILTERS_SECTION,_T("AllowAllRAR"),0,m_INIPath.c_str()) != 0);
}