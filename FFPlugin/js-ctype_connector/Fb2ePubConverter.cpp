#include "stdafx.h"
#include "Fb2ePubConverter.h"
#import "..\..\FB2EPubConverter.tlb" raw_interfaces_only, raw_native_types, no_namespace, named_guids, auto_search

#define Fb2EpubConverterCaller_CLASSNAME "Fb2EpubConverterCaller"
#define Fb2EpubConverterCaller_CID  { 0x77030d87, 0x6fac, 0x4f95, { 0x9a, 0xa1, 0xc7, 0x10, 0x4a, 0xb3, 0x3a, 0xf4 } }; // {77030D87-6FAC-4F95-9AA1-C7104AB33AF4}


CFb2ePubConverter::CFb2ePubConverter(void)
{
}


CFb2ePubConverter::~CFb2ePubConverter(void)
{
}


bool CFb2ePubConverter::Convert(LPCWSTR inputPath,LPCWSTR outputPath)
{
	HRESULT hr(S_OK);
	CoInitialize(NULL);
	IEPubConverterInterfacePtr pConverter = NULL;
	do
	{
		hr = pConverter.CreateInstance(CLSID_ConvertProcessor);
		if ( FAILED(hr))
		{
			MessageBox(NULL,L"Unable to create converter instance.\nConverter probably not registered",L"Error",MB_OK|MB_ICONERROR);
			break;
		}
		// convert input string to BSTR
		BSTR bstrInput = ::SysAllocString(inputPath);
		// convert output string to BSTR
		BSTR bstrOutput = ::SysAllocString(outputPath);
		hr = pConverter->ConvertSingleFile(bstrInput,bstrOutput,NULL);
		::SysFreeString(bstrInput);
		::SysFreeString(bstrOutput);
		if ( FAILED(hr))
		{
			MessageBox(NULL,L"Unable to convert file",L"Error",MB_OK|MB_ICONERROR);
			break;
		}
	}
	while(false);
	CoUninitialize();

	if ( FAILED(hr) )
	{
		return false;
	}
	return true;
}