// FbePluginImplementation.cpp : Implementation of CFbePluginImplementation

#include "stdafx.h"
#include "FbePluginImplementation.h"
#pragma warning( push )
#pragma warning(disable:4279)
#import "..\ExtLibs\msxml4.dll" 
#pragma warning( pop )
using namespace MSXML2;
#include "atlstr.h"
using namespace System;
using namespace System::IO;
#import "..\FB2EPubConverter.tlb" raw_interfaces_only, raw_native_types, no_namespace, named_guids, auto_search






// CFbePluginImplementation

HRESULT CFbePluginImplementation::Export(long hWnd,BSTR filename,IDispatch *doc)
{
	HRESULT hr(S_OK);
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
	
	WCHAR Buffer[MAX_PATH+1];
	::ZeroMemory(&Buffer,(MAX_PATH+1)*sizeof(WCHAR));
	DWORD dwResult = GetTempPath(MAX_PATH,(LPWSTR)&Buffer);
	if ( dwResult == 0 )
	{
		MessageBox((HWND)hWnd,L"Error getting temporary path",L"Error",MB_OK);
		return E_FAIL;
	}
	if ( dwResult > MAX_PATH -14 )
	{
		MessageBox((HWND)hWnd,L"Error temporary path variable is too long",L"Error",MB_OK);
		return E_FAIL;
	}
	WCHAR FileNameBuffer[MAX_PATH+1];
	::ZeroMemory(&FileNameBuffer,(MAX_PATH+1)*sizeof(WCHAR));
	dwResult	=	GetTempFileName(Buffer,L"FB2",0,(LPWSTR)&FileNameBuffer);
	if ( dwResult == 0 )
	{
		MessageBox((HWND)hWnd,L"Error creating temporary file name",L"Error",MB_OK);
		return E_FAIL;
	}
	
	// "fake" FB2 extension to "fool" the file type detection
	wcscat_s(FileNameBuffer,MAX_PATH,L".FB2");
	CComVariant bstrTempFileName(FileNameBuffer);

  	hr = source->save(bstrTempFileName);
	if ( FAILED(hr) )
	{
		MessageBox((HWND)hWnd,L"Error saving temporary file",L"Error",MB_OK);
		return hr;
	}

	IEPubConverterInterfacePtr converter;
	hr = converter.CreateInstance(CLSID_ConvertProcessor);
	if ( FAILED(hr) )
	{
		MessageBox((HWND)hWnd,L"Error creating converter object",L"Error",MB_OK);
		DeleteFile(FileNameBuffer);
		return hr;
	}

	CComBSTR bstrOutName(strName);
	hr = converter->ConvertSingleFile(bstrTempFileName.bstrVal,bstrOutName,NULL);
	if ( FAILED(hr) )
	{
		MessageBox((HWND)hWnd,L"Error cconverting",L"Error",MB_OK);
		DeleteFile(FileNameBuffer);
		return hr;
	}
	DeleteFile(FileNameBuffer);
	MessageBox((HWND)hWnd,L"File was exported successfully",L"Conversion result",MB_OK);
	return hr;
}
