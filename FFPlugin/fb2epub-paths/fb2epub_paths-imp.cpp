#include "fb2epub_paths-imp.h"
#include "nsStringAPI.h"
#include "Shlobj.h"
#include "Shlwapi.h"
#include <atlbase.h>


#define PATH_SIZE (1024*2+32)

const LPTSTR  c_tzINIFileName = L"\\FB2EPUBExt.INI";
const LPTSTR  TARGETS_SECTION = L"Targets";

NS_IMPL_ISUPPORTS1(CFb2EpubConverterPaths, IFb2EpubConverterPaths)

CFb2EpubConverterPaths::CFb2EpubConverterPaths()
: m_pTargetsArray(NULL)
,m_lTargetsCount(0)
{
  /* member initializers and constructor code */
	//Init();
}

CFb2EpubConverterPaths::~CFb2EpubConverterPaths()
{
  /* destructor code */
	//ReleaseTargets();
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
		for(int i = 0; i < m_lTargetsCount; i++)
		{
			if ( m_pTargetsArray[i].path != NULL && m_pTargetsArray[i].path != L"")
			{
				free(m_pTargetsArray[i].path);
				m_pTargetsArray[i].path	= NULL;
			}
			if ( m_pTargetsArray[i].name != NULL && m_pTargetsArray[i].name != L"")
			{
				free(m_pTargetsArray[i].name);
				m_pTargetsArray[i].name = NULL;
			}
		}
		delete []m_pTargetsArray;
		m_pTargetsArray	=	NULL;
	}
	m_lTargetsCount	=	0;
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
				pTempTargets[m_lTargetsCount++] = tempTarget;
			}
		}
	}
	if ( m_lTargetsCount > 0 ) // if at least one target detected
	{
		m_pTargetsArray	=	new target[m_lTargetsCount];
		for(int i=0; i < m_lTargetsCount; i++ )
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

/* long GetPathsCount (); */
NS_IMETHODIMP CFb2EpubConverterPaths::GetPathsCount(int32_t *_retval)
{
	*_retval = m_lTargetsCount;
    return NS_OK;
}

/* void GetPath (in long iPath, out AString path); */
NS_IMETHODIMP CFb2EpubConverterPaths::GetPath(int32_t iPath, nsAString & _retval)
{
	if ( iPath > m_lTargetsCount )
	{
		return NS_ERROR_INVALID_ARG;
	}
	_retval.Assign(m_pTargetsArray[iPath].path);	
    return NS_OK;
}


/* AString GetPathName (in long iPath); */
NS_IMETHODIMP CFb2EpubConverterPaths::GetPathName(int32_t iPath, nsAString & _retval)
{
	if ( iPath > m_lTargetsCount )
	{
		return NS_ERROR_INVALID_ARG;
	}
	_retval.Assign(m_pTargetsArray[iPath].name);	
    return NS_OK;
}
