#include "fb2epub-xpcom-impl.h"
#include "nsStringAPI.h"


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
NS_IMETHODIMP CFb2EpubConverterCaller::Convert(const nsAString & inputPath, const nsAString & putputPath, int32_t *_retval)
{
    //return NS_ERROR_NOT_IMPLEMENTED;
	MessageBoxNS(NULL,inputPath,putputPath,MB_OK);
	return NS_OK;
}
