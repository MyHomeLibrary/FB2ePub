// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the JSCTYPE_CONNECTOR_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// JSCTYPE_CONNECTOR_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef JSCTYPE_CONNECTOR_EXPORTS
#define JSCTYPE_CONNECTOR_API __declspec(dllexport)
#else
#define JSCTYPE_CONNECTOR_API __declspec(dllimport)
#endif

EXTERN_C
{

	// Path related functions
	bool JSCTYPE_CONNECTOR_API CNTR_GetPathsCount(UINT32& uiPathCount);
	bool JSCTYPE_CONNECTOR_API CNTR_GetPath(UINT32 uiPath,LPWSTR strPath, UINT32& uiPathLength);
	bool JSCTYPE_CONNECTOR_API CNTR_GetPathName(UINT32 uiPath,LPWSTR strPathName, UINT32& uiPathLength);

	// Converter unctions
	bool JSCTYPE_CONNECTOR_API CNTR_Convert(LPCWSTR inputPath,LPCWSTR outputPath);

}

