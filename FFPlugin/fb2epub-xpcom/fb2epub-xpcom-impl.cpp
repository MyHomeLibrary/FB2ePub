#include "fb2epub-xpcom-impl.h"
#include "nsStringAPI.h"
#include "..\..\Fb2EpubExt\fb2epubconverter.tlh"


NS_IMPL_ISUPPORTS1(CFb2EpubConverterCaller, IFb2EpubConverter)

CFb2EpubConverterCaller::CFb2EpubConverterCaller()
{
  /* member initializers and constructor code */
}

CFb2EpubConverterCaller::~CFb2EpubConverterCaller()
{
  /* destructor code */
}

int	CFb2EpubConverterCaller::MessageBoxNS(HWND hWnd,const nsAString& nsaText,const nsAString& nsaCaption,UINT uType)
{
	char16_t* chText = ToNewUnicode(nsaText);
	char16_t* chTitle = ToNewUnicode(nsaCaption);

	int iRes = MessageBox(NULL,chText,chTitle,MB_OK);

	NS_Free(chText);
	NS_Free(chTitle);

	return iRes;
}

/* long Convert (in AString inputPath, in AString putputPath); */
NS_IMETHODIMP CFb2EpubConverterCaller::Convert(const nsAString & inputPath, const nsAString & outputPath, int32_t *_retval)
{
	CoInitialize(NULL);
	IEPubConverterInterfacePtr pConverter = NULL;
	HRESULT hr = pConverter.CreateInstance(CLSID_ConvertProcessor);
	if ( FAILED(hr))
	{
		MessageBox(NULL,L"Unable to create converter instance.\nConverter probably not registered",L"Error",MB_OK|MB_ICONERROR);
	}
	// convert input string to BSTR
	char16_t* chText = ToNewUnicode(inputPath);
	BSTR bstrInput = ::SysAllocString(chText);
	NS_Free(chText);
	// convert output string to BSTR
	chText = ToNewUnicode(outputPath);
	BSTR bstrOutput = ::SysAllocString(chText);
	NS_Free(chText);
	hr = pConverter->ConvertSingleFile(bstrInput,bstrOutput,NULL);
	::SysFreeString(bstrInput);
	::SysFreeString(bstrOutput);
	if ( FAILED(hr))
	{
		MessageBox(NULL,L"Unable to convert file",L"Error",MB_OK|MB_ICONERROR);
	}
    //return NS_ERROR_NOT_IMPLEMENTED;
	//MessageBoxNS(NULL,inputPath,outputPath,MB_OK);
	CoUninitialize();
	return NS_OK;
}
