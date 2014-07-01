#pragma once

#include "fb2epub_xpcom.h"

class CFb2EpubConverterCaller : public IFb2EpubConverter
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_IFB2EPUBCONVERTER

  CFb2EpubConverterCaller();

private:
  ~CFb2EpubConverterCaller();

protected:
  /* additional members */
};
