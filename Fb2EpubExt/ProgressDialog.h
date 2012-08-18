// ProgressDialog.h : Declaration of the CProgressDialog

#pragma once

#include "resource.h"       // main symbols

#include <atlhost.h>

#include <string>
#include <list>


#define UM_INIT (WM_USER+1)
#define UM_ADVANCE (WM_USER+2)
#define UM_SKIP (WM_USER+3)
#define UM_FINISH (WM_USER+4)

typedef std::list< std::basic_string<TCHAR> > string_list;

// CProgressDialog

class CProgressDialog : 
	public CAxDialogImpl<CProgressDialog>
{
public:
	CProgressDialog(/*string_list* plsFiles,LPCTSTR szPath*/)
		//: m_bExit(FALSE)
		: m_total(0)
	{
		//m_plsFiles = plsFiles;
		//m_szOutPath = szPath;
	}

	~CProgressDialog()
	{
	}

	enum { IDD = IDD_PROGRESSDIALOG };

	void Finished()
	{
		//m_bExit = TRUE;
	}

BEGIN_MSG_MAP(CProgressDialog)
	MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
	MESSAGE_HANDLER(UM_INIT,OnLocalInit)
	MESSAGE_HANDLER(UM_ADVANCE,OnLocalAdvance)
	MESSAGE_HANDLER(UM_SKIP,OnLocalSkip)
	MESSAGE_HANDLER(UM_FINISH,OnFinish)
	CHAIN_MSG_MAP(CAxDialogImpl<CProgressDialog>)
END_MSG_MAP()

// Handler prototypes:
//  LRESULT MessageHandler(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
//  LRESULT CommandHandler(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
//  LRESULT NotifyHandler(int idCtrl, LPNMHDR pnmh, BOOL& bHandled);




	LRESULT OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnLocalInit(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnLocalAdvance(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnLocalSkip(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	LRESULT OnFinish(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);

	// Setup progress dialog
	void Start(HWND hwndParent,long total);

	void AdvancePhase(BSTR bstrFileName);

	void AbortPhase();

	void FinishProgress();

private:
	long m_total;
	//static void RunThread(void* pobject);
	//string_list* m_plsFiles;
	//LPCTSTR m_szOutPath;
	//BOOL m_bExit;
	//void DoWork();

};


