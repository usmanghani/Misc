#include "stdafx.h"
#include "DrawHist.h"

void DrawHistDeltaX ( vector<int> ptsx )
{
	
	IplImage* img = cvCreateImage ( cvSize ( 500, 500 ) , IPL_DEPTH_64F, 1 );
	int max = *max_element ( ptsx.begin ( ), ptsx.end ( ) );
	int dx = cvCeil ( img->width / ptsx.size ( ) );
	int dy = cvCeil ( img->height / max ) ;
	int cx = 0;
	int prevpt = 0;
	CV_IMAGE_ELEM ( img, double, img->height - ptsx.at(0)*dy, 0 ) = CV_RGB(255,255,255).val[0];
	for ( int i = 1 ; i < ptsx.size ( ) ; i ++ )
	{
		int pt = ptsx.at ( i );
		CV_IMAGE_ELEM ( img, double, img->height - ptsx.at(i)*dy, i * dx ) = CV_RGB(255,255,255).val[0];
		cvLine ( img, cvPoint ( (i-1)*dx, img->height - ptsx.at(i-1)*dy ), cvPoint ( i * dx, img->height - ptsx.at(i)*dy ), CV_RGB(255,255,255), 4 ) ;
        
	}
	cvNamedWindow ( "DeltaX Histogram", 1 ) ;
	cvShowImage ("DeltaX Histogram", img ) ;
	cvWaitKey ( );
	cvDestroyWindow ( "DeltaX Histogram" );
	cvReleaseImage ( &img ) ;

}
void DrawHistDeltaY ( vector < int > ptsy )
{
	IplImage* img = cvCreateImage ( cvSize ( 500, 500 ) , IPL_DEPTH_64F, 1 );
	int max = *max_element ( ptsy.begin ( ), ptsy.end ( ) );
	int dx = cvCeil ( img->width / ptsy.size ( ) );
	int dy = cvCeil ( img->height / max ) ;
	int cx = 0;
	int prevpt = 0;
	CV_IMAGE_ELEM ( img, double, img->height - ptsy.at(0)*dy, 0 ) = CV_RGB(255,255,255).val[0];
	for ( int i = 1 ; i < ptsy.size ( ) ; i ++ )
	{
		int pt = ptsy.at ( i );
		CV_IMAGE_ELEM ( img, double, img->height - ptsy.at(i)*dy, i * dx ) = CV_RGB(255,255,255).val[0];
		cvLine ( img, cvPoint ( (i-1)*dx, img->height - ptsy.at(i-1)*dy ), cvPoint ( i * dx, img->height - ptsy.at(i)*dy ), CV_RGB(255,255,255), 4 ) ;
        
	}
	cvNamedWindow ( "DeltaY Histogram", 1 ) ;
	cvShowImage ("DeltaY Histogram", img ) ;
	cvWaitKey ( );
	cvDestroyWindow ( "DeltaY Histogram" );
	cvReleaseImage ( &img ) ;

}

void DrawHistogram ( vector<int> ptsx, vector<int> ptsy )
{
	
	if ( ptsx.size ( ) > 0 ) 
	{
		DrawHistDeltaX ( ptsx ) ;

	}
	if ( ptsy.size ( ) > 0 )
	{
		DrawHistDeltaY ( ptsy );

	}

}
