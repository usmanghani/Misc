#pragma once
#include "afxwin.h"


// FaceDetectionDlg dialog

class FaceDetectionDlg : public CDialog
{
	DECLARE_DYNAMIC(FaceDetectionDlg)
	CkalmanmfcDlg* parent;
	CButton useStillImage;

public:
	FaceDetectionDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~FaceDetectionDlg();
	void callFaceDetectWithBGSep ( const char* filename );
	void callFaceDetectWithoutBGSep ( const char* filename );

// Dialog Data
	enum { IDD = IDD_FACEDETECT_DIALOG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedBgclass1();

public:
	void SetDlgParent ( CkalmanmfcDlg* p );

public:
	afx_msg void OnBnClickedBgclass2();
public:
	afx_msg void OnBnClickedBgclass3();
public:
	afx_msg void OnBnClickedBgclass4();
public:
	afx_msg void OnBnClickedBgclass5();
public:
	afx_msg void OnBnClickedClass1();
public:
	afx_msg void OnBnClickedClass2();
public:
	afx_msg void OnBnClickedClass3();
public:
	afx_msg void OnBnClickedClass4();
public:
	afx_msg void OnBnClickedClass5();
public:
	afx_msg void OnBnClickedButton4();
public:
	afx_msg void OnBnClickedBgclass6();
public:
	afx_msg void OnBnClickedBgclass7();
public:
	afx_msg void OnBnClickedBgclass8();
public:
	afx_msg void OnBnClickedClass6();
public:
	afx_msg void OnBnClickedClass7();
public:
	afx_msg void OnBnClickedClass8();
};
