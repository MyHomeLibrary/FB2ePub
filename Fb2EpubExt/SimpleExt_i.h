

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0500 */
/* at Tue Jan 25 21:33:21 2011
 */
/* Compiler settings for .\Fb2EpubExt.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext, robust
    error checks: stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __SimpleExt_i_h__
#define __SimpleExt_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IFb2EpubShlExt_FWD_DEFINED__
#define __IFb2EpubShlExt_FWD_DEFINED__
typedef interface IFb2EpubShlExt IFb2EpubShlExt;
#endif 	/* __IFb2EpubShlExt_FWD_DEFINED__ */


#ifndef __Fb2EpubShlExt_FWD_DEFINED__
#define __Fb2EpubShlExt_FWD_DEFINED__

#ifdef __cplusplus
typedef class Fb2EpubShlExt Fb2EpubShlExt;
#else
typedef struct Fb2EpubShlExt Fb2EpubShlExt;
#endif /* __cplusplus */

#endif 	/* __Fb2EpubShlExt_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IFb2EpubShlExt_INTERFACE_DEFINED__
#define __IFb2EpubShlExt_INTERFACE_DEFINED__

/* interface IFb2EpubShlExt */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IFb2EpubShlExt;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("3EFA062B-9215-41DE-96CE-4A7DC38B0EEB")
    IFb2EpubShlExt : public IDispatch
    {
    public:
    };
    
#else 	/* C style interface */

    typedef struct IFb2EpubShlExtVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IFb2EpubShlExt * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IFb2EpubShlExt * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IFb2EpubShlExt * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IFb2EpubShlExt * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IFb2EpubShlExt * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IFb2EpubShlExt * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IFb2EpubShlExt * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        END_INTERFACE
    } IFb2EpubShlExtVtbl;

    interface IFb2EpubShlExt
    {
        CONST_VTBL struct IFb2EpubShlExtVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IFb2EpubShlExt_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IFb2EpubShlExt_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IFb2EpubShlExt_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IFb2EpubShlExt_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IFb2EpubShlExt_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IFb2EpubShlExt_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IFb2EpubShlExt_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IFb2EpubShlExt_INTERFACE_DEFINED__ */



#ifndef __Fb2EpubExtLib_LIBRARY_DEFINED__
#define __Fb2EpubExtLib_LIBRARY_DEFINED__

/* library Fb2EpubExtLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_Fb2EpubExtLib;

EXTERN_C const CLSID CLSID_Fb2EpubShlExt;

#ifdef __cplusplus

class DECLSPEC_UUID("5FFF3DF0-E213-4CA6-A37E-0C3BA1FE35FC")
Fb2EpubShlExt;
#endif
#endif /* __Fb2EpubExtLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


