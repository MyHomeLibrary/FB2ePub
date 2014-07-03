#pragma once

#include "fb2epub_xpcom.h"

#ifdef _CHAR16T
#undef _CHAR16T
#endif 


#define Fb2EpubPathsCaller_CLASSNAME "Fb2EpubPathsCaller"
#define Fb2EpubPathsCaller_CID  { 0x84662189, 0x77e1, 0x4a7b, { 0x8d, 0x90, 0x74, 0x31, 0x6f, 0x48, 0xe1, 0xdf } };  // {84662189-77E1-4A7B-8D90-74316F48E1DF}


class CFb2EpubConverterPaths : public IFb2EpubConverterPaths
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_IFB2EPUBCONVERTERPATHS

  CFb2EpubConverterPaths();

private:
  ~CFb2EpubConverterPaths();

protected:
  /* additional members */
};
