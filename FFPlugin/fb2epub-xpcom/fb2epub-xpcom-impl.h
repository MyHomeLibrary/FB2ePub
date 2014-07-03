#pragma once

#ifndef _Fb2EpubConverterCaller_H_
#define _Fb2EpubConverterCaller_H_

#include "fb2epub_xpcom.h"
#include <Windows.h>

// just to get rid of warning
#ifdef _CHAR16T
#undef _CHAR16T
#endif 

#define Fb2EpubConverterCaller_CLASSNAME "Fb2EpubConverterCaller"
#define Fb2EpubConverterCaller_CID  { 0x77030d87, 0x6fac, 0x4f95, { 0x9a, 0xa1, 0xc7, 0x10, 0x4a, 0xb3, 0x3a, 0xf4 } }; // {77030D87-6FAC-4F95-9AA1-C7104AB33AF4}


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
int	MessageBoxNS(HWND hWnd,const nsAString& nsaText,const nsAString& nsaCaption,UINT uType);

};

#endif // _Fb2EpubConverterCaller_H_