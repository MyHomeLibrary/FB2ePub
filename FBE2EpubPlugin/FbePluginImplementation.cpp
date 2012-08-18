// FbePluginImplementation.cpp : Implementation of CFbePluginImplementation

#include "stdafx.h"
#include "FbePluginImplementation.h"
#pragma warning( push )
#pragma warning(disable,4279)
#import "..\ExtLibs\msxml4.dll" 
#pragma warning( pop )
using namespace MSXML2;
#include "atlstr.h"
using namespace System;



// CFbePluginImplementation

HRESULT CFbePluginImplementation::Export(long hWnd,BSTR filename,IDispatch *doc)
{
	//MessageBox((HWND)hWnd,L"This is the Test",L"Test message",MB_OK);
	int maxSize = MAX_PATH*10;
	MSXML2::IXMLDOMDocument2Ptr	    source(doc);
	OPENFILENAME saveObject;
	::ZeroMemory(&saveObject,sizeof(OPENFILENAME));
	saveObject.lStructSize = sizeof(OPENFILENAME);
	saveObject.hwndOwner = (HWND)hWnd;
	saveObject.lpstrFilter = L"ePub file\0*.ePub\0\0";
	saveObject.nMaxFile = maxSize;
	saveObject.Flags = OFN_EXPLORER;
	saveObject.lpstrDefExt = L"ePub";
	USES_CONVERSION;
	CString strName(filename);
	saveObject.lpstrFile = strName.GetBuffer(maxSize);
	if(!GetSaveFileName(&saveObject))
	{
		return S_FALSE;
	}
	strName.ReleaseBuffer();
	MessageBox((HWND)hWnd,strName,L"Test message",MB_OK);

	Xml::XmlDocument^ xmlDoc = gcnew Xml::XmlDocument;
	BSTR bstrXml = source->xml;
	String^ xmlString = gcnew String(bstrXml);
	xmlDoc->LoadXml(xmlString);
	
	return S_OK;
}
