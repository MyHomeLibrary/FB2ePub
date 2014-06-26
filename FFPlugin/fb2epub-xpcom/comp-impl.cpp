#include "comp-impl.h"

NS_IMPL_ISUPPORTS1(CSpecialThing, ISpecialThing)

CSpecialThing::CSpecialThing()
{
	/* member initializers and constructor code */
	mName.Assign(L"Default Name");
}

CSpecialThing::~CSpecialThing()
{
	/* destructor code */
}

/* long add (in long a, in long b); */
NS_IMETHODIMP CSpecialThing::Add(PRInt32 a, PRInt32 b, PRInt32 *_retval)
{
	*_retval = a + b;
	return NS_OK;
}

/* attribute AString name; */
NS_IMETHODIMP CSpecialThing::GetName(nsAString & aName)
{
	aName.Assign(mName);
	return NS_OK;
}
NS_IMETHODIMP CSpecialThing::SetName(const nsAString & aName)
{
	mName.Assign(aName);
	return NS_OK;
}
