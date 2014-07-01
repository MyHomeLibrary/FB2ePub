#include "fb2epub-xpcom-impl.h"

NS_IMPL_ISUPPORTS1(CFb2EpubConverterCaller, IFb2EpubConverter)

CFb2EpubConverterCaller::CFb2EpubConverterCaller()
{
  /* member initializers and constructor code */
}

CFb2EpubConverterCaller::~CFb2EpubConverterCaller()
{
  /* destructor code */
}

/* long Convert (in AString inputPath, in AString putputPath); */
NS_IMETHODIMP CFb2EpubConverterCaller::Convert(const nsAString & inputPath, const nsAString & putputPath, int32_t *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
