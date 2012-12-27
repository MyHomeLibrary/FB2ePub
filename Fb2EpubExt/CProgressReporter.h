// CProgressReporter.h : Declaration of the CCProgressReporter

#pragma once
#include "resource.h"       // main symbols



#include "SimpleExt_i.h"
#include "ProgressDialog.h"
#include <iostream>


#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;
using namespace std;


// CCProgressReporter

class ATL_NO_VTABLE CCProgressReporter :
	public CComObjectRootEx<CComMultiThreadModel>,
	public CComCoClass<CCProgressReporter, &CLSID_CProgressReporter>,
	public ICProgressReporter,
	public IDispatchImpl<IProgressUpdateInterface, &__uuidof(IProgressUpdateInterface), &LIBID_FB2EPubConverter, /* wMajor = */ 1>
{
private:
	CProgressDialog m_Dialog;
public:
	CCProgressReporter()
		:m_hwnd(HWND_DESKTOP)
	{
	}


	void SetCallerHwnd(HWND hwnd)
	{
		m_hwnd = hwnd;
	}

	DECLARE_REGISTRY_RESOURCEID(IDR_CPROGRESSREPORTER)


	BEGIN_COM_MAP(CCProgressReporter)
		COM_INTERFACE_ENTRY(ICProgressReporter)
		COM_INTERFACE_ENTRY(IProgressUpdateInterface)
	END_COM_MAP()



	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:

private:
	HWND m_hwnd;  //caller window handle

protected:
//void vout(wchar_t *wcs, size_t n, wchar_t *fmt, ...)
//{
//   va_list arg_ptr;
// 
//   va_start(arg_ptr, fmt);
//   vswprintf(wcs, n, fmt, arg_ptr);
//   va_end(arg_ptr);
//   return;
//}
// 

	// IProgressUpdateInterface Methods
public:
	STDMETHOD_(HRESULT, ConvertStarted)(long total)
	{
		m_Dialog.Start(m_hwnd,total);
		return S_OK;
	}


	STDMETHOD_(HRESULT, ConvertFinished)(long total)
	{
		m_Dialog.FinishProgress();
		m_Dialog.DestroyWindow();
		return S_OK;
	}

	STDMETHOD_(HRESULT, Processed)(BSTR bstrFileName,long count, long total)
	{
		m_Dialog.AdvancePhase(bstrFileName);
	/*	wchar_t text[512];
		::ZeroMemory(text,sizeof(wchar_t)*512);
		basic_string<wchar_t> fileFullPath = bstrFileName;
		vout(text,512,L"Processed %s %d out of %d",fileFullPath.c_str(),count,total);*/
		//MessageBox(NULL,text,L"Status Message",MB_OK);
		return S_OK;
	}
	STDMETHOD_(HRESULT, ProcessingStarted)(BSTR bstrFileName,long count, long total)
	{
		m_Dialog.AdvancePhase(bstrFileName);
		//wchar_t text[512];
		//::ZeroMemory(text,sizeof(wchar_t)*512);
		//basic_string<wchar_t> fileFullPath = bstrFileName;
		//vout(text,512,L"ProcessingStarted %s %d out of %d",fileFullPath.c_str(),count,total);
		//MessageBox(NULL,text,L"Status Message",MB_OK);
		return S_OK;
	}
	STDMETHOD_(HRESULT, ProcessingSaving)(BSTR bstrFileName,long count, long total)
	{
		m_Dialog.AdvancePhase(bstrFileName);
	/*	wchar_t text[512];
		::ZeroMemory(text,sizeof(wchar_t)*512);
		basic_string<wchar_t> fileFullPath = bstrFileName;
		vout(text,512,L"ProcessingSaving %s %d out of %d",fileFullPath.c_str(),count,total);*/
		//MessageBox(NULL,text,L"Status Message",MB_OK);
		return S_OK;
	}
	STDMETHOD_(HRESULT, SkippedDueError)(BSTR bstrFileName)
	{
		m_Dialog.AbortPhase();
	/*	wchar_t text[512];
		basic_string<wchar_t> fileFullPath = bstrFileName;
		vout(text,512,L"SkippedDueError %s",fileFullPath.c_str());*/
		//MessageBox(NULL,text,L"Status Message",MB_OK);
		return S_OK;
	}
};

OBJECT_ENTRY_AUTO(__uuidof(CProgressReporter), CCProgressReporter)
