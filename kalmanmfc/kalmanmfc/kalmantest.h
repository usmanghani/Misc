#pragma once

#include "Kalmanmfcdlg.h"
#include "MyCvImage.h"


#define draw_cross(img,center,color,d,thickness) \
	cvLine( (img), cvPoint( (center).x - (d), (center).y - (d) ), \
	cvPoint( (center).x + (d), (center).y + (d) ), (color), (thickness), 0 ); \
	cvLine( (img), cvPoint( (center).x + (d), (center).y - (d) ), \
	cvPoint( (center).x - (d), (center).y + (d) ), (color), (thickness), 0 )


void doVideo ( CkalmanmfcDlg* parent );

void doKalmanTest ( CvCapture* capture, CkalmanmfcDlg* parent );

void testLogging ( CkalmanmfcDlg* parent );

void doKalman ( CkalmanmfcDlg* parent );

void DrawLegend ( MyCvImage& img ) ;

void ShowNotice ( MyCvImage& img ) ;

void EraseNotice ( MyCvImage& img ) ;

void testBGthing ( CvCapture* capture, CkalmanmfcDlg* parent );

void testContours ( const char* filename );

void goodfeatures ( MyCvImage img );

void track ( vector<MyCvImage> imgs );

