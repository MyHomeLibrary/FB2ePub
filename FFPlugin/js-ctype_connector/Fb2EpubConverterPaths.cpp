#include "stdafx.h"
#include "Fb2EpubConverterPaths.h"
#include "Shlobj.h"
#include "Shlwapi.h"
#include <atlbase.h>


#define PATH_SIZE (1024*2+32)

const LPTSTR  c_tzINIFileName = L"\\FB2EPUBExt.INI";
const LPTSTR  TARGETS_SECTION = L"Targets";


CFb2EpubConverterPaths::CFb2EpubConverterPaths(void)
: m_pTargetsArray(NULL)
,m_uiTargetsCount(0)
{
  /* member initialization and constructor code */
	Init();
}


CFb2EpubConverterPaths::~CFb2EpubConverterPaths(void)
{
  /* destructor code */
	ReleaseTargets();
}


void CFb2EpubConverterPaths::Init()
{
	LPTSTR strINILocation = DetectINILocation();
	if ( strINILocation == NULL ) // if not detected - exit
	{
		return;
	}
	ReadTargets(strINILocation);
	delete []strINILocation;
}

void CFb2EpubConverterPaths::ReleaseTargets()
{
	if ( m_pTargetsArray != NULL )
	{
		for(UINT32 i = 0; i < m_uiTargetsCount; i++)
		{
			if ( m_pTargetsArray[i].path != NULL && StrCmpCW(m_pTargetsArray[i].path,L"") != 0)
			{
				free(m_pTargetsArray[i].path);
				m_pTargetsArray[i].path	= NULL;
			}
			if ( m_pTargetsArray[i].name != NULL && StrCmpCW(m_pTargetsArray[i].name,L"") != 0)
			{
				free(m_pTargetsArray[i].name);
				m_pTargetsArray[i].name = NULL;
			}
		}
		delete []m_pTargetsArray;
		m_pTargetsArray	=	NULL;
	}
	m_uiTargetsCount	=	0;
}

void  CFb2EpubConverterPaths::ReadTargets(LPTSTR strINILocation)
{
	ReleaseTargets();

	WCHAR temp[PATH_SIZE+1];
	::ZeroMemory(temp,sizeof(WCHAR)*PATH_SIZE);
	UINT count = ::GetPrivateProfileInt(TARGETS_SECTION,L"TargetsCount",0,strINILocation);	
	if ( count <= 0 )
	{
		return;
	}
	target* pTempTargets = new target[count];
	::ZeroMemory(pTempTargets,count*sizeof(target));
	WCHAR section[1024];
	for (UINT i=1; i <= count; i ++ )
	{
		::ZeroMemory(section,sizeof(WCHAR)*1024);
		wsprintf(section,L"Target%d",i);
		::ZeroMemory(temp,sizeof(WCHAR)*PATH_SIZE);
		target tempTarget;
		bool bAdd = (::GetPrivateProfileInt(section,L"ShowInShell",1,strINILocation) == 1);
		if ( bAdd )
		{
			DWORD dwRes = ::GetPrivateProfileString(section,L"TargetPath",NULL,temp,1024,strINILocation);
			if (dwRes != 0) 
			{
				tempTarget.path = _wcsdup(temp);
				::ZeroMemory(temp,sizeof(WCHAR)*PATH_SIZE);
				DWORD dwRes = ::GetPrivateProfileString(section,L"TargetName",NULL,temp,1024,strINILocation);
				if (dwRes == 0) 
				{
					tempTarget.name = L"";
				}
				else
				{
					 tempTarget.name = _wcsdup(temp);
				}
				pTempTargets[m_uiTargetsCount++] = tempTarget;
			}
		}
	}
	if ( m_uiTargetsCount > 0 ) // if at least one target detected
	{
		m_pTargetsArray	=	new target[m_uiTargetsCount];
		for(UINT32 i=0; i < m_uiTargetsCount; i++ )
		{
			m_pTargetsArray[i] = pTempTargets[i];
		}
	}
	::ZeroMemory(pTempTargets,count*sizeof(target));
	delete []pTempTargets;
}

LPTSTR CFb2EpubConverterPaths::DetectINILocation()
{
	WCHAR iniFile[PATH_SIZE];
	::ZeroMemory(iniFile,sizeof(WCHAR)*PATH_SIZE);
	if ( SHGetSpecialFolderPath(NULL,iniFile,CSIDL_COMMON_APPDATA ,FALSE)) // detect APPData folder location
	{
		lstrcat(iniFile,L"\\Lord_KiRon\\FB2ePub"); // add Fb2ePub INI settings subfolder
		lstrcat(iniFile,c_tzINIFileName); // and default INI file name
		if ( PathFileExists(iniFile) )
		{
			size_t bufferSize =  _tcslen (iniFile) + 1;
			WCHAR * pszAllocatedReturnBuf = new WCHAR [bufferSize];
			::ZeroMemory(pszAllocatedReturnBuf,bufferSize);
			_tcscpy_s (pszAllocatedReturnBuf,bufferSize, iniFile);
			// Caller must free
			return pszAllocatedReturnBuf;
		}
	}
	// if we got here, means it's not in %AppData% folder
	return NULL;
}


bool CFb2EpubConverterPaths::GetPathsCount(UINT32& uiPathsCount)
{
	uiPathsCount	=	m_uiTargetsCount;
	return true;
}

bool CFb2EpubConverterPaths::GetPath(UINT32 uiPath,LPWSTR strPath, UINT32& uiPathLength)
{
	if ( uiPath >= m_uiTargetsCount )
	{
		return false;
	}
	UINT32 uiTempLen	=	_tcslen(m_pTargetsArray[uiPath].path);
	if ( uiTempLen <= uiPathLength )
	{
		::CopyMemory(strPath,m_pTargetsArray[uiPath].path,uiTempLen*sizeof(WCHAR));
	}
	uiPathLength	=	uiTempLen;
	return true;
}

bool CFb2EpubConverterPaths::GetPathName(UINT32 uiPath,LPWSTR strPathName, UINT32& uiPathLength)
{
	if ( uiPath >= m_uiTargetsCount )
	{
		return false;
	}
	UINT32 uiTempLen	=	_tcslen(m_pTargetsArray[uiPath].name);
	if ( uiTempLen <= uiPathLength )
	{
		::CopyMemory(strPathName,m_pTargetsArray[uiPath].name,uiTempLen*sizeof(WCHAR));
	}
	uiPathLength	=	uiTempLen;
	return true;
}


