// ProgressDialog.h : Declaration of the CProgressDialog

#pragma once

#include "resource.h"       // main symbols

#include <atlhost.h>

#include <string>
#include <list>


typedef std::list< std::basic_string<TCHAR> > string_list;

// CProgressDialog

class CProgressDialog : 
	public CAxDialogImpl<CProgressDialog>
{
public:
	CProgressDialog(string_list* plsFiles,LPCTSTR szPath)
	{
		m_plsFiles = plsFiles;
		m_szOutPath = szPath;
	}

	~CProgressDialog()
	{
	}

	enum { IDD = IDD_PROGRESSDIALOG };

BEGIN_MSG_MAP(CProgressDialog)
	MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
	CHAIN_MSG_MAP(CAxDialogImpl<CProgressDialog>)
END_MSG_MAP()

// Handler prototypes:
//  LRESULT MessageHandler(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
//  LRESULT CommandHandler(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
//  LRESULT NotifyHandler(int idCtrl, LPNMHDR pnmh, BOOL& bHandled);




	LRESULT OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);

private:
	static void RunThread(void* pobject);
	string_list* m_plsFiles;
	LPCTSTR m_szOutPath;
	void DoWork();

};


