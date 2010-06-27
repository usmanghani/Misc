// kalmanmfcDlg.h : header file
//

#pragma once
#include "afxwin.h"
#include "Resource.h"
#include "afxcmn.h"


// CkalmanmfcDlg dialog
class CkalmanmfcDlg : public CDialog
{

private:

	bool useCamera;
	CString fileName;
	CvCapture* capture;
	// shows log messages
	CListBox lstLog;
	CString imgFile;

	CString SelectImageFile ( );
	CString SelectVideoFile ( );

	CSpinButtonCtrl spinStillThreshold;
	CSpinButtonCtrl spinVideoThreshold;
	CSpinButtonCtrl spinBlockSize;
	CSpinButtonCtrl spinChangeThreshold;
	CSpinButtonCtrl spinDiffThreshold;
	CSpinButtonCtrl spinDegrees;


public:
	CkalmanmfcDlg(CWnd* pParent = NULL);	// standard constructor
	const char* GetFileName () ;
	bool UseCamera () ;
	CvCapture* GetCapture ();
	void DoImageTest ();

// Dialog Data
	enum { IDD = IDD_KALMANMFC_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	
public:
	afx_msg void OnBnClickedUsecamera();
public:
	afx_msg void OnBnClickedUsevideofile();
public:
	afx_msg void OnBnClickedKalman();

public:
	//afx_msg void OnBnClickedButton1();
public:
	void LogString(CString msg);
public:
	void ClearLog(void);
public:
//	afx_msg void OnLbnSelchangeLstlog();
public:
	afx_msg void OnBnClickedUseimage();
public:
	const char* GetImageFileName ( ) ;
public:
	afx_msg void OnBnClickedButton3();
public:
	afx_msg void OnBnClickedBgsub();
public:
	afx_msg void OnBnClickedMotiontrack();
public:
	afx_msg void OnBnClickedFacedetect();
public:
	afx_msg void OnBnClickedButton2();
public:
	afx_msg void OnBnClickedButton4();
public:
	afx_msg void OnDeltaposSpinBlockSize(NMHDR *pNMHDR, LRESULT *pResult);
public:
	afx_msg void OnBnClickedButton5();
public:
	afx_msg void OnBnClickedButton6();
public:
	afx_msg void OnBnClickedPyraUp();
public:
	afx_msg void OnBnClickedPyraDown();
public:
	afx_msg void OnBnClickedButton7();
};
