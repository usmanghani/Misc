#include "stdafx.h"
#include "MyCvImage.h"
#include "CvDisplayWnd.h"
#include "kalmanmfc.h"
#include "logger.h"

MyCvImage CvtToGray ( MyCvImage img )
{
	MyCvImage result = cvCreateImage (cvGetSize ( img ) , 8, 1 );
	cvCvtColor ( img, result, CV_BGR2GRAY );
	return result;
}

	

CvRect FindObject ( MyCvImage src, MyCvImage patt, double threshold/*[0,1]*/ )
{


	CvRect result = { 0 , 0 , 0 , 0 };

	//MyCvImage graysrc = cvCreateImage ( cvSize ( src->width + patt->width - 1, src->height + patt->height - 1 ), 8, 1 );
	//cvZero ( graysrc );
	//CvRect roi = cvRect ( patt->width / 2, patt->height / 2, src->width , src->height );
	//cvSetImageROI ( graysrc, roi );
	MyCvImage graysrc;
	if (src->nChannels > 1 )
		graysrc = cvCreateImage ( cvGetSize ( src ), 8, 1);
	else graysrc = src;

	MyCvImage graypatt;
	if ( patt->nChannels > 1)
		graypatt = cvCreateImage ( cvGetSize ( patt ), 8, 1);
	else graypatt = patt;

	cvCvtColor ( src, graysrc, CV_BGR2GRAY );
	cvCvtColor ( patt, graypatt, CV_BGR2GRAY );

	
	//CvMat* subrect = cvCreateMat ( 200, 200, CV_8UC1 );

	stringstream str ;
	str << "Source size : width = " << src->width << " -- height = " << src->height ;
	logger.LogString ( str.str().c_str ( ) ) ;

	str.str ( "" );
	str << "Pattern size : width = " << patt->width << " -- height = " << patt->height ;
	logger.LogString ( str.str().c_str () );


	//cvResetImageROI ( graysrc );

	//cvGetSubRect ( graysrc, subrect, cvRect ( 400, 400, 200, 200 ) );

	//width and height of the image after corr.
	int width = graysrc->width - graypatt->width + 1;
	int height = graysrc->height - graypatt->height + 1;

	//int width = src->width - patt->width + 1;
	//int height = src->height - patt->height + 1;

	CvSize rsize = cvSize ( width, height );
	MyCvImage r = cvCreateImage ( rsize, IPL_DEPTH_32F, 1 );
	cvMatchTemplate ( graysrc, graypatt, r, CV_TM_CCORR_NORMED );
	//cvMatchTemplate ( src, patt, r, CV_TM_CCORR_NORMED );


	double minval = 0;
	double maxval = 0;
	CvPoint maxloc;
	CvPoint minloc;

	cvMinMaxLoc( r, &minval, &maxval, &minloc, &maxloc );

	str.str ( "" );
	str << minval << " to " << maxval ;
	logger.LogString ( str.str().c_str ( ) ) ;

	str.str ( "" );
	str << "Min Location : (" << minloc.x << ", " << minloc.y << ")";
	logger.LogString ( str.str () .c_str ( ) );

	str.str ( "" );
	str << "Max Location : (" << maxloc.x << ", " << maxloc.y << ")";
	logger.LogString  ( str.str ( ) .c_str  ( ) );

	
	if ( maxval >= threshold ) 
	{

		CvPoint loc = maxloc;
		CvPoint startpt = cvPoint ( loc.x /*- graypatt->width / 2*/, loc.y /*- graypatt->height / 2*/ );
		CvPoint endpt = cvPoint ( startpt.x + graypatt->width , startpt.y + graypatt->height );
		cvRectangle ( graysrc, startpt, endpt, cvScalar(255), 2 );
		result = cvRect ( loc.x, loc.y, endpt.x - loc.x , endpt.y - loc.y );

	}


	return result;
	
}

void FindObjectStill ( MyCvImage src, MyCvImage patt, double threshold )
{

	CvDisplayWnd orig( "Image", 0 );
	CvDisplayWnd tgt( "Target", 0 );

	CvRect pos = FindObject ( src, patt, threshold );
	cvRectangle ( src, cvPoint ( pos.x , pos.y ) , cvPoint ( pos.x + pos.width, pos.y + pos.height ) , CV_RGB ( 255, 255 , 255 ) );

	orig.ShowImage ( src );
	tgt.ShowImage ( patt );

	cvWaitKey ( );

}
struct MouseState
{
	IplImage* originalimg;
	IplImage* img;
	CvPoint startpt;
	CvPoint endpt;
	bool tracingrect;

};

void mousecallback ( int evt, int x, int y, int flags, void* param  )
{
	stringstream str;
	MouseState* state = NULL;
	bool tracingrect = false;
	CvPoint startpt;
	CvPoint endpt;

	IplImage* img = NULL;
	if ( param )
	{

		state = (MouseState*) param;
		img = state->img;
		tracingrect = state->tracingrect;
		startpt = state->startpt ;
		endpt = state->endpt;

		//str.str( "" );
		//str << boolalpha << tracingrect;
		//logger.LogString ( str.str ().c_str (  ));
		//str.str( "" );
		//str << startpt.x << " " << startpt.y;
		//logger.LogString ( str.str ().c_str (  ));

	}

	//str << (int) param << " -- " << (int) img ;
	//logger.LogString ( str.str ().c_str ( ) );
	
	switch ( evt )
	{
	case CV_EVENT_LBUTTONDOWN:
		{
			state->tracingrect = true;
			state->startpt = cvPoint ( x, y );
			str.str("");
			str << "Left Button Down at : (" << x << ", " << y << ")";
			logger.LogString ( str.str().c_str ()  );
			str.str("");
			str << boolalpha << tracingrect ;
			logger.LogString ( str.str().c_str ( ) );
			break;

		}
	case CV_EVENT_LBUTTONUP:
		{
			state->tracingrect = false;
			state->endpt = cvPoint (x ,y );
			cvReleaseImage ( &state->img ) ;
			state->img = cvCloneImage ( state->originalimg );
			cvRectangle ( state->img, state->startpt, state->endpt, CV_RGB ( 255, 255, 255 ), 2 );
			//cvRectangle ( state->img, state->startpt, state->endpt , CV_RGB ( 255, 255, 255 ) , 2 );
			str.str("");
			str << "Left Button Up at : (" << x << ", " << y << ")";
			logger.LogString ( str.str().c_str ()  );
			str.str("");
			str << boolalpha << tracingrect ;
			logger.LogString ( str.str().c_str ( ) );
			break;
		}

	case CV_EVENT_MOUSEMOVE:
		{
			
			if ( tracingrect )
			{
				if ( img )
				{
					state->endpt = cvPoint( x, y );
					cvShowImage ( "Select Target", state->originalimg );
					cvReleaseImage ( &state->img ) ;
					state->img = cvCloneImage ( state->originalimg );
					cvRectangle ( state->img, state->startpt, state->endpt, CV_RGB ( 255, 255, 255 ), 2 );
					cvShowImage ( "Select Target", state->img );
					//str.str("");
					//str << "(" << x << ", " << y << ")" ;
					//logger.LogString ( str.str().c_str (  ));

				}

				
			}

			break;
		}

	}

	
}


MyCvImage FindObjectUI ( MyCvImage src )
{

	MouseState state;
	CvDisplayWnd wnd ( "Select Target", 0 );
	CvDisplayWnd wnd2( "Selected Target", 0 );
	state.img = cvCloneImage ( src.image );
	state.originalimg = cvCloneImage ( src.image );
	state.tracingrect = false;
	cvSetMouseCallback ( wnd.wndName, mousecallback, (void*)&state );
	wnd.ShowImage ( src );
	MyCvImage selectedimage;

	while ( 1 )
	{
		int key = cvWaitKey ();
		if ( key == 32 )
		{	
			logger.LogString ( "we are here" );
			int x = state.startpt.x;
			int y = state.startpt.y;
			int width = state.endpt.x - state.startpt.x;
			int height = state.endpt.y - state.startpt.y;
			selectedimage = cvCreateImage ( cvSize ( width, height  ) , src->depth , src->nChannels ) ;
			cvSetImageROI ( state.originalimg, cvRect ( x, y, width, height ) );
			cvCopy ( state.originalimg, selectedimage );
			cvResetImageROI(state.originalimg);
			if ( selectedimage.image )
			{
				wnd2.ShowImage ( selectedimage );
			}

		}
		if ( key == 27 ) break;

	}

	cvReleaseImage ( & state.originalimg );
	cvReleaseImage ( & state.img );
	return selectedimage;

}

void FindObjectInVideo ( CvCapture* capture, MyCvImage target, double threshold )
{

	CvDisplayWnd orig ( "Video", 0 );
	CvDisplayWnd tgt ( "Target", 0 );
	tgt.ShowImage ( target );

	IplImage* frame = NULL;


	while ( frame = cvQueryFrame ( capture ) )
	{
		if ( frame->origin == IPL_ORIGIN_BL ) 
			cvFlip ( frame );

		frame->origin = IPL_ORIGIN_TL;
		CvRect pos = FindObject ( frame, target, threshold );
		cvRectangle ( frame, cvPoint ( pos.x, pos.y ), cvPoint ( pos.x + pos.width , pos.y + pos.height ) , CV_RGB ( 255, 255, 255 ) , 2, 8, 0  ) ;
		orig.ShowImage ( frame );
		if ( cvWaitKey ( 100 ) > 0 ) break;

	}

	//cvReleaseCapture ( &capture );

}

MyCvImage FindObjectInVideoUI ( CvCapture* capture )
{
	MyCvImage tgt;	
	MyCvImage tmp;
	if (capture)
	{
		tmp = cvQueryFrame ( capture );
		if ( tmp->origin == IPL_ORIGIN_BL )
			cvFlip ( tmp );
		tmp->origin = IPL_ORIGIN_TL;
		//CvDisplayWnd charya( "test", 0 );
		//charya.ShowImage ( tmp );
		//cvWaitKey ( );
		if ( tmp.image )
			tgt = FindObjectUI ( tmp );
	}
	return tgt;
}

vector<MyCvImage> GetExpandedImages (MyCvImage image, int levels )
{

	vector<MyCvImage> expandedlist;
	MyCvImage img = image;
	
	for ( int i = 0 ; i < levels ; i ++ )
	{
		int width = img->width * 2;
		int height = img->height * 2;
		MyCvImage temp = cvCreateImage ( cvSize ( width, height ) , img->depth, img->nChannels );
		//CvMat subrect;
		//cvGetSubRect ( image, &subrect, cvRect ( 0, 0, image->width & -2, image->height & -2 ) );
		//IplImage subimg;
		//cvGetImage ( &subrect, &subimg );
		cvPyrUp ( img, temp );
		expandedlist.push_back ( temp );
		img = temp;
		
	}

	return expandedlist;

}

vector<MyCvImage> GetReducedImages (MyCvImage image, int levels )
{

	vector<MyCvImage> reducedlist;
	MyCvImage img = image;
	
	if ( img ->height <= 1 || img->width <= 1 ) return reducedlist;

	for ( int i = 0 ; i < levels ; i ++ )
	{
		int width = img->width / 2;
		int height = img->height / 2;
		MyCvImage temp = cvCreateImage ( cvSize ( width, height ) , img->depth, img->nChannels );
		CvMat subrect;
		cvGetSubRect ( img, &subrect, cvRect ( 0, 0, img->width & -2, img->height & -2 ) );
		IplImage subimg;
		cvGetImage ( &subrect, &subimg );
		cvPyrDown ( &subimg, temp );
		reducedlist.push_back ( temp );
		img = temp;		
	}

	return reducedlist;

}

CvRect FindObjectWithPyramiding ( MyCvImage src, MyCvImage patt, double threshold/*[0,1]*/, int levels )
{

	CvRect result = {0,0,0,0};
	//TODO: Add your code here.
	MyCvImage pattgray = CvtToGray ( patt );
	MyCvImage srcgray = CvtToGray ( src );
	vector<MyCvImage> expandedlist;
	vector<MyCvImage> reducedlist;
	reducedlist = GetReducedImages ( pattgray, levels );
	expandedlist = GetExpandedImages ( pattgray, levels );

	return result;
}


bool DetectSceneChange ( MyCvImage im1, MyCvImage im2, double threshold1/*[0,1]Change Threshold*/, double threshold2/*[0,1] Percentage of blocks that should change*/, int blksize )
{

	CvDisplayWnd wnd1 ("First Image");
	CvDisplayWnd wnd2 ("Second Image");

	wnd1.ShowImage ( im1 );
	wnd2.ShowImage ( im2 );

	cvWaitKey ( );

	stringstream str;
	
	str.str("");
	str << blksize ;
	logger.LogString ( str.str().c_str () );


	MyCvImage padded1;
	MyCvImage padded2;
	//if the two pics are not the same size, pad them
	if ( cvGetSize ( im1 ).width != cvGetSize ( im2 ).width || cvGetSize ( im1 ).height != cvGetSize ( im2 ).height )
	{

		int width = max ( im1->width , im2->width );
		int height = max ( im1->height, im2->height );

		padded1 = cvCreateImage ( cvSize ( width, height ), im1->depth, im1->nChannels );
		padded2 = cvCreateImage ( cvSize ( width, height ), im2->depth, im2->nChannels );

		cvCopyMakeBorder ( im1, padded1, cvPoint ( (width - im1->width) / 2, (height - im1->height) / 2 ), IPL_BORDER_CONSTANT );
		cvCopyMakeBorder ( im2, padded2, cvPoint ( (width - im2->width) / 2, (height - im2->height) / 2 ), IPL_BORDER_CONSTANT );


	}
	else
	{
		padded1 = im1;
		padded2 = im2;

	}

		
	MyCvImage gray1 = cvCreateImage ( cvGetSize ( padded1 ), 8, 1 );
	MyCvImage gray2 = cvCreateImage ( cvGetSize ( padded2 ), 8, 1 );

	cvCvtColor ( padded1, gray1, CV_BGR2GRAY );
	cvCvtColor ( padded2, gray2, CV_BGR2GRAY );
	
	MyCvImage canny1 = cvCreateImage ( cvGetSize ( gray1 ) , 8 , 1/*im1->nChannels*/ );
	MyCvImage canny2 = cvCreateImage ( cvGetSize ( gray2 ) , 8 , 1/*im2->nChannels*/ );
	
	cvCanny ( gray1, canny1, 10, 20 );
	cvCanny ( gray2, canny2, 10, 20 );

	//CvDisplayWnd wnd1( "gray1" );
	//CvDisplayWnd wnd2( "gray2" );

	//wnd1.ShowImage ( canny1 );
	//wnd2.ShowImage ( canny2 );

	//cvWaitKey ( );

	int colsadd = 0;
	int rowsadd = 0;

	int residue = canny1->width % blksize;
	if ( residue ) colsadd = blksize - residue;
	
	residue = canny1->height % blksize;
	if ( residue ) rowsadd = blksize - residue;

	MyCvImage edge1 = cvCreateImage ( cvSize ( canny1->width + colsadd, canny1->height + rowsadd ), 8, canny1->nChannels );
	MyCvImage edge2 = cvCreateImage ( cvSize ( canny2->width + colsadd, canny2->height + rowsadd ), 8, canny2->nChannels );

	cvZero ( edge1 );
	cvZero ( edge2 );

	cvSetImageROI ( edge1, cvRect ( 0, 0, canny1->width, canny1->height ) );
	cvSetImageROI ( edge2, cvRect ( 0, 0, canny2->width, canny2->height ) );

	cvCopy ( canny1, edge1 );
	cvCopy ( canny2, edge2 );

	cvResetImageROI ( edge1 );
	cvResetImageROI ( edge2 );

	//wnd1.ShowImage ( edge1 );
	//wnd2.ShowImage ( edge2 );

	//cvWaitKey ( );


	str.str("");
	str << edge1->width << " -- " << edge1->height ;
	logger.LogString ( str.str().c_str () );

	str.str("");
	str << edge2->width << " -- " << edge2->height ;
	logger.LogString ( str.str().c_str () );

	int count = 0;

	for ( int i = 0 ; i < edge1->height / blksize ; i ++ )
	{
		for ( int j = 0 ; j < edge1->width / blksize ; j ++ )
		{
			CvMat subrect1;
			cvGetSubRect ( edge1, &subrect1, cvRect ( blksize*j, blksize*i, blksize, blksize ) );
			CvMat subrect2;
			cvGetSubRect ( edge2, &subrect2, cvRect ( blksize*j, blksize*i, blksize, blksize  ) );
			double avg1 = cvAvg ( &subrect1 ).val[0];
			double avg2 = cvAvg ( &subrect2 ).val[0];
			if ( fabs ( avg1 - avg2 ) / 100 > threshold1 )
			{
				count ++;
			}
			
		}

	}

	int numblks = edge1->height / blksize * edge1->width / blksize;
	if ( count > threshold2 * numblks )
	{
		return true;

	}

	return false;

}

// Rotates an image without losing any data.
MyCvImage RotateImage ( MyCvImage im, int angle )
{

	if ( angle >= 360 ) angle-= 360;

	stringstream str;

	CvMat* transmat = cvCreateMat ( 2, 3, CV_32FC1 );
	cvZero ( transmat );

	//int width = cvRound ( fabs( im->height * sin ( angle * M_PI / 180 ) )  + fabs ( im->width * cos ( angle * M_PI / 180 ) ) );
	//int height = cvRound ( fabs ( im->width * sin ( angle * M_PI / 180  )  ) + fabs ( im->height * cos ( angle * M_PI / 180 ) ));

	CvPoint2D32f center = cvPoint2D32f (0, 0);
	cv2DRotationMatrix ( center, angle, 1.0, transmat );

	//matrix that calculates the resulting positions of the four corners of the image under rotation.
	CvMat* src = cvCreateMat ( 3, 3, CV_32FC1 );

	CV_MAT_ELEM ( *src, float, 0, 0 ) = im->width;
	CV_MAT_ELEM ( *src, float, 1, 0 ) = 0;
	CV_MAT_ELEM ( *src, float, 2, 0 ) = 1;
	CV_MAT_ELEM ( *src, float, 0, 1 ) = 0;
	CV_MAT_ELEM ( *src, float, 1, 1 ) = im->height;
	CV_MAT_ELEM ( *src, float, 2, 1 ) = 1;
	CV_MAT_ELEM ( *src, float, 0, 2 ) = im->width;
	CV_MAT_ELEM ( *src, float, 1, 2 ) = im->height;
	CV_MAT_ELEM ( *src, float, 2, 2 ) = 1;

	CvMat* dst = cvCreateMat ( transmat->rows, src->cols, CV_32FC1 );

	cvMatMul ( transmat, src, dst );

	//for ( int i = 0 ; i < transmat->rows ; i ++ )
	//{
	//	
	//	str.str("");
	//	for ( int j = 0 ; j < transmat->cols ; j ++ )
	//		str << CV_MAT_ELEM ( *transmat, float, i, j ) << ", ";
	//	logger.LogString ( str.str () .c_str ( ) );
	//}

	//for ( int i = 0 ; i < src->rows ; i ++ )
	//{

	//	str.str("");
	//	for ( int j = 0 ; j < src->cols ; j ++ )
	//		str << CV_MAT_ELEM ( *src, float, i, j ) << ", ";
	//	logger.LogString ( str.str () .c_str ( ) );
	//}

	//for ( int i = 0 ; i < dst->rows ; i ++ )
	//{

	//	str.str("");
	//	for ( int j = 0 ; j < dst->cols ; j ++ )
	//		str << CV_MAT_ELEM ( *dst, float, i, j ) << ", ";
	//	logger.LogString ( str.str () .c_str ( ) );
	//}


	int width = abs ( cvRound ( CV_MAT_ELEM(*dst,float,0,0) ) ) + abs ( cvRound ( CV_MAT_ELEM ( *dst, float, 0, 1) ) );
	int height = abs ( cvRound ( CV_MAT_ELEM ( *dst, float, 1, 0 ) ) ) + abs ( cvRound ( CV_MAT_ELEM ( *dst, float, 1, 1 ) ) ) ;

	//str.str ( "" );
	//str << width << "--" << height ;
	//logger.LogString ( str.str ().c_str ( ) );

	MyCvImage result = cvCreateImage ( cvSize ( width, height ), im->depth, im->nChannels );
	
	vector<float> xvec;
	for ( int i = 0 ; i < dst->cols ; i ++ )
	{
		xvec.push_back ( CV_MAT_ELEM ( *dst, float, 0, i ) );

	}

	vector<float> yvec;
	for ( int i = 0 ; i < dst->cols ; i ++ )
	{
		yvec.push_back ( CV_MAT_ELEM ( *dst, float, 1, i ) );

	}
	
	float xshift = - *min_element ( xvec.begin(), xvec.end () );
	float yshift = - *min_element ( yvec.begin (), yvec.end () );

	if ( angle > 0 && angle < 90 ) xshift = 0;
	if ( angle > 270 && angle < 360 ) yshift = 0;

	CV_MAT_ELEM ( *transmat, float, 0, 2 ) = xshift;
	CV_MAT_ELEM ( *transmat, float, 1, 2 ) = yshift;

	//str.str ( "" );
	//str << xshift << "--" << yshift;
	//logger.LogString ( str.str ().c_str ( ) );

	cvWarpAffine ( im, result, transmat );
	
	cvReleaseMat ( &transmat );
	cvReleaseMat ( &src );
	cvReleaseMat ( &dst );

	return result;

}

