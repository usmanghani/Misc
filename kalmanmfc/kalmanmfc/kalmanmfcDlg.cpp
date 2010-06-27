// kalmanmfcDlg.cpp : implementation file
//


#include "stdafx.h"
#include "kalmanmfc.h"
#include "kalmanmfcDlg.h"
#include "kalmantest.h"
#include "MyCvImage.h"
#include "CvDisplayWnd.h"
#include "facedetectiondlg.h"
#include "fyptest.h"
#include "logger.h"
#include "MHIObjectTracker.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{

}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()


// CkalmanmfcDlg dialog




CkalmanmfcDlg::CkalmanmfcDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CkalmanmfcDlg::IDD, pParent)
{

	useCamera = false;
	fileName = _T("");
	capture = NULL;
	imgFile = _T("");

	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CkalmanmfcDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LSTLOG, lstLog);
	DDX_Control(pDX, IDC_SPIN_STILL_THRESHOLD, spinStillThreshold);
	DDX_Control(pDX, IDC_SPIN_VIDEO_THRESHOLD, spinVideoThreshold);
	DDX_Control(pDX, IDC_SPIN_BLOCK_SIZE, spinBlockSize);
	DDX_Control(pDX, IDC_SPIN_CHANGE_THRESHOLD, spinChangeThreshold);
	DDX_Control(pDX, IDC_SPIN_DIFF_THRESHOLD, spinDiffThreshold);
	DDX_Control(pDX, IDC_SPIN_DEGREES, spinDegrees);
}

BEGIN_MESSAGE_MAP(CkalmanmfcDlg, CDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_USECAMERA, &CkalmanmfcDlg::OnBnClickedUsecamera)
	ON_BN_CLICKED(IDC_USEVIDEOFILE, &CkalmanmfcDlg::OnBnClickedUsevideofile)
	ON_BN_CLICKED(IDC_KALMAN, &CkalmanmfcDlg::OnBnClickedKalman)
	//ON_BN_CLICKED(IDC_BUTTON1, &CkalmanmfcDlg::OnBnClickedButton1)
	//ON_LBN_SELCHANGE(IDC_LSTLOG, &CkalmanmfcDlg::OnLbnSelchangeLstlog)
	ON_BN_CLICKED(IDC_USEIMAGE, &CkalmanmfcDlg::OnBnClickedUseimage)
	/*ON_BN_CLICKED(IDC_BUTTON3, &CkalmanmfcDlg::OnBnClickedButton3)*/
	ON_BN_CLICKED(IDC_BGSUB, &CkalmanmfcDlg::OnBnClickedBgsub)
	ON_BN_CLICKED(IDC_MOTIONTRACK, &CkalmanmfcDlg::OnBnClickedMotiontrack)
	ON_BN_CLICKED(IDC_FACEDETECT, &CkalmanmfcDlg::OnBnClickedFacedetect)
	ON_BN_CLICKED(IDC_BUTTON2, &CkalmanmfcDlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON4, &CkalmanmfcDlg::OnBnClickedButton4)
	ON_NOTIFY(UDN_DELTAPOS, IDC_SPIN_BLOCK_SIZE, &CkalmanmfcDlg::OnDeltaposSpinBlockSize)
	ON_BN_CLICKED(IDC_BUTTON5, &CkalmanmfcDlg::OnBnClickedButton5)
	ON_BN_CLICKED(IDC_BUTTON6, &CkalmanmfcDlg::OnBnClickedButton6)
	ON_BN_CLICKED(IDC_PYRA_UP, &CkalmanmfcDlg::OnBnClickedPyraUp)
	ON_BN_CLICKED(IDC_PYRA_DOWN, &CkalmanmfcDlg::OnBnClickedPyraDown)
	ON_BN_CLICKED(IDC_BUTTON7, &CkalmanmfcDlg::OnBnClickedButton7)
END_MESSAGE_MAP()


// CkalmanmfcDlg message handlers

BOOL CkalmanmfcDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon


	// TODO: Add extra initialization here
	spinStillThreshold.SetRange32 ( 0, 100 );
	spinVideoThreshold.SetRange32 ( 0, 100 );
	spinChangeThreshold.SetRange32 ( 0, 100 );
	spinDiffThreshold.SetRange32 ( 0, 100 );
	spinBlockSize.SetRange32 ( 2, 1024 );
	spinDegrees.SetRange32 (0 , 1000 );
		
	spinStillThreshold.SetBuddy ( GetDlgItem ( IDC_STILL_THRESHOLD ) );
	spinVideoThreshold.SetBuddy ( GetDlgItem ( IDC_VIDEO_THRESHOLD ) );
	spinBlockSize.SetBuddy ( GetDlgItem ( IDC_BLOCK_SIZE ) );
	spinChangeThreshold.SetBuddy ( GetDlgItem ( IDC_CHANGE_THRESHOLD ));
	spinDiffThreshold.SetBuddy ( GetDlgItem ( IDC_DIFF_THRESHOLD ) );
	spinDegrees.SetBuddy ( GetDlgItem ( IDC_DEGREES ) );

	CString fmt;

	spinStillThreshold.SetPos32 ( 85 );
	fmt.Format ( _T("%u"), spinStillThreshold.GetPos32 (  ) ) ;
	((CEdit*)GetDlgItem ( IDC_STILL_THRESHOLD ))->SetWindowTextW ( fmt );

	spinVideoThreshold.SetPos32 ( 85 );
	fmt.Format ( _T("%u"), spinVideoThreshold.GetPos32 (  ) ) ;
	((CEdit*)GetDlgItem ( IDC_VIDEO_THRESHOLD ))->SetWindowTextW ( fmt );	
	
	spinChangeThreshold.SetPos32 ( 50 );
	fmt.Format ( _T("%u"), spinChangeThreshold.GetPos32 (  ) ) ;
	((CEdit*)GetDlgItem ( IDC_CHANGE_THRESHOLD ))->SetWindowTextW ( fmt );	

	spinDiffThreshold.SetPos32 ( 10 );
	fmt.Format ( _T("%u"), spinDiffThreshold.GetPos32 (  ) ) ;
	((CEdit*)GetDlgItem ( IDC_DIFF_THRESHOLD ))->SetWindowTextW ( fmt );	

	spinBlockSize.SetPos32 ( 32 );
	fmt.Format ( _T("%u"), spinBlockSize.GetPos32 (  ) ) ;
	((CEdit*)GetDlgItem ( IDC_BLOCK_SIZE ))->SetWindowTextW ( fmt );	

	spinDegrees.SetPos32 ( 0 );
	fmt.Format ( _T("%u"), spinDegrees.GetPos32() );
	((CEdit*)GetDlgItem(IDC_DEGREES))->SetWindowTextW ( fmt );	

	return TRUE;  // return TRUE  unless you set the focus to a control

}

void CkalmanmfcDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CkalmanmfcDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CkalmanmfcDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


const char* CkalmanmfcDlg::GetFileName ( )
{
	USES_CONVERSION;
	return W2A(fileName.GetBuffer());

}
bool CkalmanmfcDlg::UseCamera ( )
{
	return useCamera;

}


void CkalmanmfcDlg::OnBnClickedUsecamera()
{
	if ( capture )
		cvReleaseCapture( &capture );
	 
	
	capture = cvCaptureFromCAM ( 0 );
	
	
	if ( capture ) 
		MessageBox ( _T("Camera Detected."), _T("Kalman Tracker"), MB_OK | MB_ICONINFORMATION );
	else
		MessageBox ( _T("Camera Not Detected."), _T("Kalman Tracker"), MB_OK | MB_ICONERROR );
	//cvReleaseCapture ( &capture );
	useCamera = true;

}

void CkalmanmfcDlg::OnBnClickedUsevideofile()
{

	USES_CONVERSION;
	CFileDialog dlg(TRUE, _T("avi"), _T("*.avi"), OFN_FILEMUSTEXIST| OFN_HIDEREADONLY, _T("Video Files (*.avi)|*.avi|All Files (*.*)|*.*||"), this);
	dlg.GetOFN().lpstrTitle = _T("Open Video File..." );
	if ( dlg.DoModal () == IDOK )
	{

		fileName = dlg.GetPathName();
		if ( capture ) 
			cvReleaseCapture(&capture);
		
		
		capture = cvCaptureFromFile ( W2A(fileName.GetBuffer()) );		
		
		
		if ( capture ) 
			MessageBox ( _T("Video File OK."), _T("Kalman Tracker"), MB_OK | MB_ICONINFORMATION );
		else
			MessageBox ( _T("Video File Not OK."), _T("Kalman Tracker"), MB_OK | MB_ICONERROR );
		//cvReleaseCapture ( &capture );
		useCamera = false;

	}
	
	
}

void CkalmanmfcDlg::OnBnClickedKalman()
{
	doKalman ( this );	
}
CvCapture* CkalmanmfcDlg::GetCapture()
{
	return capture;
}


//void CkalmanmfcDlg::OnBnClickedButton1()
//{
//	lstLog.AddString ( _T("thing" ) );
//}

void CkalmanmfcDlg::LogString(CString msg)
{
	CDC* pDC = this->GetWindowDC ( );
	CFont* pOldFont;
	CFont* pListFont = lstLog.GetFont ();
	pOldFont = pDC->SelectObject ( pListFont );
	CRect rect;
	pDC->DrawText ( msg, &rect, DT_CALCRECT | DT_SINGLELINE );
	pDC->SelectObject ( pOldFont );
	this->ReleaseDC(pDC);
	if ( rect.Width() > lstLog.GetHorizontalExtent ( ) )
	{
		lstLog.SetHorizontalExtent ( rect.Width() + 10 );

	}

	this->lstLog.InsertString(/*Append*/-1, msg.GetBuffer() );
}


void CkalmanmfcDlg::ClearLog(void)
{
	lstLog.ResetContent();
	lstLog.ShowScrollBar ( SB_BOTH, FALSE );

}

//void CkalmanmfcDlg::OnLbnSelchangeLstlog()
//{
//	// TODO: Add your control notification handler code here
//}

void CkalmanmfcDlg::OnBnClickedUseimage()
{
	USES_CONVERSION;
	CFileDialog dlg(TRUE, _T(""), _T(""), OFN_FILEMUSTEXIST| OFN_HIDEREADONLY, _T("Image Files (*.bmp, *.jpg)|*.bmp;*.jpg|All Files (*.*)|*.*||"), this);
	dlg.GetOFN().lpstrTitle = _T("Open Image File..." );
	if ( dlg.DoModal () == IDOK )
		imgFile = dlg.GetPathName();
	
	
}
const char* CkalmanmfcDlg::GetImageFileName ( )
{
	USES_CONVERSION;
	return W2A(imgFile.GetBuffer());

}

void CkalmanmfcDlg::OnBnClickedButton3()
{
	
	if ( capture )
	{
		cvSetCaptureProperty ( capture, CV_CAP_PROP_POS_AVI_RATIO, 0 );
		MHIObjectTracker tracker;
		tracker.doTracking(capture, this);

	}
	else
	{
		MessageBox ( _T("You must select a capture source first."), _T("Kalman Tracker"), MB_OK | MB_ICONINFORMATION );

	}
	
}


void CkalmanmfcDlg::OnBnClickedBgsub()
{
	USES_CONVERSION ;
	if ( capture )
	{
		cvSetCaptureProperty ( capture, CV_CAP_PROP_POS_AVI_RATIO, 0 );
		testBGthing ( capture, this );
		
	}
	else
	{
		MessageBox ( _T("You must select a capture source first."), _T("Kalman Tracker"), MB_OK | MB_ICONINFORMATION );

	}
	
}


void CkalmanmfcDlg::OnBnClickedMotiontrack()
{
	if ( capture )
	{
		cvSetCaptureProperty ( capture, CV_CAP_PROP_POS_AVI_RATIO, 0 );
		MHIObjectTracker tracker;
		tracker.doTracking( capture, this );
	}
	else
	{
		MessageBox ( _T("You must select a capture source first."), _T("Kalman Tracker"), MB_OK | MB_ICONINFORMATION );

	}
	
}

void CkalmanmfcDlg::OnBnClickedFacedetect()
{

	FaceDetectionDlg dlg ;
	dlg.SetDlgParent ( this );
	dlg.DoModal ( );
	
		
}

void CkalmanmfcDlg::OnBnClickedButton2()
{

	USES_CONVERSION;
	double threshold = spinVideoThreshold.GetPos32()/100.;
	
	if ( capture ) 
	{
		cvSetCaptureProperty ( capture, CV_CAP_PROP_POS_AVI_RATIO, 0 );
		MyCvImage tgt = FindObjectInVideoUI ( capture );
		cvSetCaptureProperty ( capture, CV_CAP_PROP_POS_AVI_RATIO, 0 );
		if ( tgt.image ) 
			FindObjectInVideo ( capture, tgt, threshold );
	}
	else
	{
		AfxMessageBox ( _T("You must select a video souce first." ) );

	}


	
}

CString CkalmanmfcDlg::SelectImageFile()
{
	USES_CONVERSION;

	CFileDialog dlg ( true, 0, 0, 4|2, _T("Image Files ( *.jpg, *.bmp )|*.jpg;*.bmp|All Files ( *.* )|*.*||"), 0, 0 );
	if ( dlg.DoModal ( ) == IDOK )
	{
		return dlg.GetPathName();

	}

	return _T("");

}

CString CkalmanmfcDlg::SelectVideoFile ( )
{
	USES_CONVERSION;

	CFileDialog dlg ( true, 0, 0, 4|2, _T("Video Files ( *.avi )|*.avi|All Files ( *.* )|*.*||"), 0, 0 );
	if ( dlg.DoModal ( ) == IDOK )
	{
		return dlg.GetPathName();

	}

	return _T("");

}

void CkalmanmfcDlg::OnBnClickedButton4()
{
	double threshold = spinStillThreshold.GetPos32() / 100.;
	USES_CONVERSION;
	CString filename = SelectImageFile ( );
	if ( filename != _T("") )
	{
		MyCvImage src = W2A ( filename.GetBuffer ( ) );
		MyCvImage tgt = FindObjectUI ( src );
		if ( tgt.image )
		{
			FindObjectStill ( src, tgt, threshold );

		}


	}

}

void CkalmanmfcDlg::OnDeltaposSpinBlockSize(NMHDR *pNMHDR, LRESULT *pResult)
{

	LPNMUPDOWN pNMUpDown = reinterpret_cast<LPNMUPDOWN>(pNMHDR);
	

	int currentval = spinBlockSize.GetPos32 ( );
	int powerof2 = (int ) ( log ( (double)currentval ) / log ( (double)2 ) );
	
	// find the next powerof2
	if ( pNMUpDown->iDelta > 0 )
		powerof2 ++ ;
	else if (pNMUpDown->iDelta < 0 )
		powerof2--;
	else
		;
	
	int nextval = (int)pow ( (double)2, (double)powerof2 );
	spinBlockSize.SetPos32 ( nextval );
	
	*pResult = 1;

}

void CkalmanmfcDlg::OnBnClickedButton5()
{
	USES_CONVERSION;
	CString file1 = SelectImageFile ( );
	CString file2 = SelectImageFile ( );

	if ( file1 != _T("") && file2 != _T("") )
	{

		MyCvImage img1 = W2A(file1.GetBuffer());
		MyCvImage img2 = W2A(file2.GetBuffer());

		bool result = DetectSceneChange ( img1, img2, (double)spinDiffThreshold.GetPos32() / 100, (double)spinChangeThreshold.GetPos32() / 100, spinBlockSize.GetPos32 ( ) );

		if ( result ) 
			AfxMessageBox ( _T("Scene Change Detected") );
		else
			AfxMessageBox ( _T("Same Scene") );

		
	}

}

void CkalmanmfcDlg::OnBnClickedButton6()
{

	USES_CONVERSION;

	
	//stringstream str;
	//vector<MyCvImage> imgs;
	//
	//for ( int i = 1 ; i <= 191  ; i ++ )
	//{
	//	
	//	str.str ( "" );
	//	str << "c:\\image\\images\\c4\\c4f" << i << ".bmp";
	//	imgs.push_back ( str.str ().c_str () );

	//}

	//track ( imgs );

	CString fn = SelectImageFile ( );
	//if ( fn != _T("") )
	//{
	//	//testContours ( W2A ( fn.GetBuffer () ) );
	//	MyCvImage img = W2A( fn.GetBuffer () );
	//	goodfeatures ( img );


	//}

	//MyCvImage img = "bottle.jpg";
	if ( fn != _T("") )
	{

		MyCvImage img = W2A(fn.GetBuffer () );
		MyCvImage result = RotateImage ( img, spinDegrees.GetPos32() );
		//MyCvImage result2 = RotateImage ( result, 582 );
		CvDisplayWnd wnd2 ( "original image", 0 );
		wnd2.ShowImage ( img );
		CvDisplayWnd wnd ( "rotated image" , 0 );
		wnd.ShowImage ( result );
		cvWaitKey ( );
	}


}


void CkalmanmfcDlg::OnBnClickedPyraUp()
{

	USES_CONVERSION;
	CString fn = SelectImageFile ( );
	
	if ( fn != _T("") )
	{

		MyCvImage img = W2A(fn.GetBuffer());
		vector<MyCvImage> expandedlist = GetExpandedImages ( img, 2 );
		vector<CvDisplayWnd*> wndlist;
		for ( int i = 0 ; i < expandedlist.size () ; i ++ )
		{
			stringstream str ;
			str.str("");			
			str << "Pyra " << i;
			char* wndName = _strdup(str.str().c_str());
			logger.LogString ( wndName );
			CvDisplayWnd* wnd = new CvDisplayWnd ( wndName, 0 );
			wndlist.push_back ( wnd );
			wnd->ShowImage ( expandedlist[i] );
			logger.LogString ( wnd->wndName );
			free ( (void*)wndName );

		}	
		
		cvWaitKey ();
		
		for ( int i = 0 ; i < wndlist.size () ; i ++ )
		{
			logger.LogString ( wndlist [i]->wndName );		
			delete wndlist[i];

		}

		
	}

}

void CkalmanmfcDlg::OnBnClickedPyraDown()
{
	USES_CONVERSION;
	CString fn = SelectImageFile ( );
	
	if ( fn != _T("") )
	{

		MyCvImage img = W2A(fn.GetBuffer());
		vector<MyCvImage> reducedlist = GetReducedImages ( img, 2 );
		vector<CvDisplayWnd*> wndlist;
		for ( vector<MyCvImage>::iterator iter = reducedlist.begin (); iter != reducedlist.end () ; iter ++ )
		{
			
			stringstream str ;
			str.str ( "" );
			str << "Pyra " << iter - reducedlist.begin ();
			char* wndName = _strdup ( str.str().c_str() );
			logger.LogString ( wndName );
			CvDisplayWnd* wnd = new CvDisplayWnd ( wndName, 0 );
			wndlist.push_back ( wnd );
			wndlist.back()->ShowImage ( *iter );
			logger.LogString ( wndlist.back()->wndName );
			free ( wndName );

		}	
		cvWaitKey ();
		for ( vector<CvDisplayWnd*>::iterator iter = wndlist.begin () ; iter != wndlist.end () ; iter ++ )
		{
			logger.LogString ( (*iter)->wndName ) ;
			delete *iter;

		}

	}

}
void CkalmanmfcDlg::DoImageTest ( )
{
	CImage img ;
	CDC* hDC = GetDC();
	img.Load ( "fahadface.jpg" );
	RECT rect;
	GetDlgItem(IDC_STATIC)->GetWindowRect( &rect );
	//ScreenToClient(&rect);
	int width = rect.right - rect.left;
	int height = rect.bottom - rect.top;
	img.DrawToHDC( hDC->m_hDC, &rect );

}

void CkalmanmfcDlg::OnBnClickedButton7()
{
	DoImageTest ( );
}
