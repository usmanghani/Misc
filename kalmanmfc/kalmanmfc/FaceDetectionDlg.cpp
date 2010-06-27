// FaceDetectionDlg.cpp : implementation file
//

#include "stdafx.h"
#include "kalmanmfc.h"
#include "kalmanmfcdlg.h"
#include "facedetect.h"
#include "FaceDetectionDlg.h"



// FaceDetectionDlg dialog

IMPLEMENT_DYNAMIC(FaceDetectionDlg, CDialog)

FaceDetectionDlg::FaceDetectionDlg(CWnd* pParent /*=NULL*/)
	: CDialog(FaceDetectionDlg::IDD, pParent)
{
#ifndef _WIN32_WCE
	EnableActiveAccessibility();
#endif

	parent = NULL;

}

FaceDetectionDlg::~FaceDetectionDlg()
{
}

void FaceDetectionDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_USEIMAGEFILE, useStillImage);
}


BEGIN_MESSAGE_MAP(FaceDetectionDlg, CDialog)
	ON_BN_CLICKED(IDC_BGCLASS1, &FaceDetectionDlg::OnBnClickedBgclass1)
	ON_BN_CLICKED(IDC_BGCLASS2, &FaceDetectionDlg::OnBnClickedBgclass2)
	ON_BN_CLICKED(IDC_BGCLASS3, &FaceDetectionDlg::OnBnClickedBgclass3)
	ON_BN_CLICKED(IDC_BGCLASS4, &FaceDetectionDlg::OnBnClickedBgclass4)
	ON_BN_CLICKED(IDC_BGCLASS5, &FaceDetectionDlg::OnBnClickedBgclass5)
	ON_BN_CLICKED(IDC_CLASS1, &FaceDetectionDlg::OnBnClickedClass1)
	ON_BN_CLICKED(IDC_CLASS2, &FaceDetectionDlg::OnBnClickedClass2)
	ON_BN_CLICKED(IDC_CLASS3, &FaceDetectionDlg::OnBnClickedClass3)
	ON_BN_CLICKED(IDC_CLASS4, &FaceDetectionDlg::OnBnClickedClass4)
	ON_BN_CLICKED(IDC_CLASS5, &FaceDetectionDlg::OnBnClickedClass5)
	ON_BN_CLICKED(IDC_BGCLASS6, &FaceDetectionDlg::OnBnClickedBgclass6)
	ON_BN_CLICKED(IDC_BGCLASS7, &FaceDetectionDlg::OnBnClickedBgclass7)
	ON_BN_CLICKED(IDC_BGCLASS8, &FaceDetectionDlg::OnBnClickedBgclass8)
	ON_BN_CLICKED(IDC_CLASS6, &FaceDetectionDlg::OnBnClickedClass6)
	ON_BN_CLICKED(IDC_CLASS7, &FaceDetectionDlg::OnBnClickedClass7)
	ON_BN_CLICKED(IDC_CLASS8, &FaceDetectionDlg::OnBnClickedClass8)
END_MESSAGE_MAP()


void FaceDetectionDlg::SetDlgParent ( CkalmanmfcDlg* p )
{
	this->parent = p;
}

void FaceDetectionDlg::callFaceDetectWithBGSep ( const char* filename )
{

	USES_CONVERSION;

	CString fname = ((CkalmanmfcApp*)AfxGetApp ())->currdir + _T("\\") + A2W(filename);

	CvHaarClassifierCascade* cascade = (CvHaarClassifierCascade*)cvLoad ( W2A(fname.GetBuffer () ) );

	//stringstream str;
	//str << ( int ) cascade ;
	//str << W2A(fname.GetBuffer ( ));
	//parent->LogString ( A2W(str.str().c_str  ( ) ) );


	if ( cascade )
	{
		if  ( ! parent->GetCapture() ) 
		{
			AfxMessageBox ( _T("You must select a capture source first") );
			
		}
		else
		{
			cvSetCaptureProperty ( parent->GetCapture(), CV_CAP_PROP_POS_AVI_RATIO, 0 );
			facedetectwithbgsep ( parent->GetCapture ( ), cascade, parent );
			
		}

	}
	else
	{
		AfxMessageBox ( _T ("Can't load Haar Classifier" ) );
		cvReleaseHaarClassifierCascade ( &cascade );

	}


}

void FaceDetectionDlg::callFaceDetectWithoutBGSep ( const char* filename )
{

	USES_CONVERSION;

	CString fname = ((CkalmanmfcApp*)AfxGetApp ())->currdir + _T("\\") + A2W(filename);

	CvHaarClassifierCascade* cascade = (CvHaarClassifierCascade*)cvLoad ( W2A(fname.GetBuffer () ) );

	//stringstream str;
	//str << ( int ) cascade ;
	//str << W2A(fname.GetBuffer ( ));
	//parent->LogString ( A2W(str.str().c_str  ( ) ) );

	if ( cascade )
	{
		if ( useStillImage.GetCheck ( ) )
		{
			const char* fileName = parent->GetImageFileName ( ) ;
			if ( strcmp ( fileName, "" ) )
			{
				MyCvImage img = cvLoadImage ( fileName );
				facedetectstill ( img, cascade, parent );
			}
			else
			{
				AfxMessageBox ( _T("Please close this dialog and select an image file") );

			}

		}
		else
		{

			if  ( ! parent->GetCapture() ) 
			{
				AfxMessageBox ( _T("You must select a capture source first") );
			}
			else
			{
				cvSetCaptureProperty ( parent->GetCapture(), CV_CAP_PROP_POS_AVI_RATIO, 0 );
				facedetectwithoutbgsep ( parent->GetCapture() , cascade, parent );

			}

		}

	}
	else
	{
		AfxMessageBox ( _T ("Can't load Haar Classifier" ) );
		cvReleaseHaarClassifierCascade ( &cascade );
		
	}
	
}

// FaceDetectionDlg message handlers

void FaceDetectionDlg::OnBnClickedBgclass1()
{
	callFaceDetectWithBGSep("haarcascade_frontalface_alt.xml");

}

void FaceDetectionDlg::OnBnClickedBgclass2()
{
	callFaceDetectWithBGSep("haarcascade_frontalface_alt2.xml");	
}

void FaceDetectionDlg::OnBnClickedBgclass3()
{
	callFaceDetectWithBGSep( "haarcascade_frontalface_alt_tree.xml" );

}

void FaceDetectionDlg::OnBnClickedBgclass4()
{
	callFaceDetectWithBGSep( "haarcascade_frontalface_default.xml" );

}

void FaceDetectionDlg::OnBnClickedBgclass5()
{
	callFaceDetectWithBGSep("haarcascade_profileface.xml");
}

void FaceDetectionDlg::OnBnClickedClass1()
{

	callFaceDetectWithoutBGSep("haarcascade_frontalface_alt.xml");
	
}

void FaceDetectionDlg::OnBnClickedClass2()
{

	callFaceDetectWithoutBGSep("haarcascade_frontalface_alt2.xml");
	
}


void FaceDetectionDlg::OnBnClickedClass3()
{
	callFaceDetectWithoutBGSep("haarcascade_frontalface_alt_tree.xml");

}


void FaceDetectionDlg::OnBnClickedClass4()
{
	callFaceDetectWithoutBGSep("haarcascade_frontalface_default.xml");
}


void FaceDetectionDlg::OnBnClickedClass5()
{
	callFaceDetectWithoutBGSep("haarcascade_profileface.xml");
}


void FaceDetectionDlg::OnBnClickedBgclass6()
{
	callFaceDetectWithBGSep("haarcascade_fullbody.xml");

}

void FaceDetectionDlg::OnBnClickedBgclass7()
{
	callFaceDetectWithBGSep("haarcascade_lowerbody.xml");
}

void FaceDetectionDlg::OnBnClickedBgclass8()
{
	callFaceDetectWithBGSep("haarcascade_upperbody.xml");
}

void FaceDetectionDlg::OnBnClickedClass6()
{
	callFaceDetectWithoutBGSep("haarcascade_fullbody.xml");
}

void FaceDetectionDlg::OnBnClickedClass7()
{
	callFaceDetectWithoutBGSep("haarcascade_lowerbody.xml");
}

void FaceDetectionDlg::OnBnClickedClass8()
{
	callFaceDetectWithoutBGSep("haarcascade_upperbody.xml");
}
