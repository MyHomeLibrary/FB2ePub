// js-ctype_connector.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "js-ctype_connector.h"
#include "Fb2EpubConverterPaths.h"



//// This is an example of an exported variable
//JSCTYPE_CONNECTOR_API int njsctype_connector=0;
//
//// This is an example of an exported function.
//JSCTYPE_CONNECTOR_API int fnjsctype_connector(void)
//{
//	return 42;
//}
//
//// This is the constructor of a class that has been exported.
//// see js-ctype_connector.h for the class definition
//Cjsctype_connector::Cjsctype_connector()
//{
//	return;
//}

 
EXTERN_C
{

bool JSCTYPE_CONNECTOR_API CNTR_GetPathsCount(UINT32& uiPathCount)
{
	CFb2EpubConverterPaths PathsConverterObj;
	return PathsConverterObj.GetPathsCount(uiPathCount);
}

bool JSCTYPE_CONNECTOR_API CNTR_GetPath(UINT32 uiPath,LPWSTR strPath, UINT32& uiPathLength)
{
	CFb2EpubConverterPaths PathsConverterObj;
	return  PathsConverterObj.GetPath(uiPath,strPath,uiPathLength);
}

bool JSCTYPE_CONNECTOR_API CNTR_GetPathName(UINT32 uiPath,LPWSTR strPathName, UINT32& uiPathLength)
{
	CFb2EpubConverterPaths PathsConverterObj;
	return  PathsConverterObj.GetPathName(uiPath,strPathName,uiPathLength);
}

void JSCTYPE_CONNECTOR_API CNTR_FreeString(LPWSTR strPath)
{
	CFb2EpubConverterPaths::FreeString(strPath); 
}

}
