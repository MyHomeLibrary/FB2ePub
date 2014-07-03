#include "fb2epub_paths-imp.h"
#include "nsStringAPI.h"

NS_IMPL_ISUPPORTS1(CFb2EpubConverterPaths, IFb2EpubConverterPaths)

CFb2EpubConverterPaths::CFb2EpubConverterPaths()
{
  /* member initializers and constructor code */
}

CFb2EpubConverterPaths::~CFb2EpubConverterPaths()
{
  /* destructor code */
}

/* long GetPathsCount (); */
NS_IMETHODIMP CFb2EpubConverterPaths::GetPathsCount(int32_t *_retval)
{
	*_retval = 2;
    return NS_OK;
}

/* void GetPath (in long iPath, out AString path); */
NS_IMETHODIMP CFb2EpubConverterPaths::GetPath(int32_t iPath, nsAString & _retval)
{
	if ( iPath == 0 )
	{
		_retval.Assign(L"C:\\");	
	}
	else
	{
		_retval.Assign(L"D:\\");	
	}
    return NS_OK;
}


/* AString GetPathName (in long iPath); */
NS_IMETHODIMP CFb2EpubConverterPaths::GetPathName(int32_t iPath, nsAString & _retval)
{
	if ( iPath == 0 )
	{
		_retval.Assign(L"First");	
	}
	else
	{
		_retval.Assign(L"Second");	
	}
    return NS_OK;
}
