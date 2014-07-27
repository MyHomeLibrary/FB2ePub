#pragma once
class CFb2EpubConverterPaths
{
public:
	CFb2EpubConverterPaths(void);
	virtual ~CFb2EpubConverterPaths(void);

	bool GetPathsCount(UINT32& uiPathsCount);
	bool GetPath(UINT32 uiPath,LPWSTR strPath, UINT32& uiPathLength);
	bool GetPathName(UINT32 uiPath,LPWSTR strPathName, UINT32& uiPathLength);
	
	static void FreeString(LPWSTR strPath);

protected:
  /* additional members */
	void Init();
	LPTSTR DetectINILocation();
	void ReadTargets(LPTSTR strINILocation);
	void ReleaseTargets();

	typedef struct 
	{
		LPTSTR path;
		LPTSTR name;
	}target;


	target* m_pTargetsArray;
	UINT32	m_uiTargetsCount;
};

