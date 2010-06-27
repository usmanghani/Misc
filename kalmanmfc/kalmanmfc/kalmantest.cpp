/* 

Notes :

Draw actual path of the particle
Draw Predicted Path  ( predicted through Kalman Filter )
Beep if Kalman Path differs from the actual path by more than some threshold.

Track/Trace the movement of dynamic object.
Predict the next movement of the dynamic object
compare the predicted movement and actual movement
find error ( difference ) rate.
show difference in histogram / graph
apply kalman filter in real time ( webcam ) scenario, and identify abnormal behaviour.

*/

#include "stdafx.h"
#include "DrawHist.h"
#include "MyCvImage.h"
#include "CvDisplayWnd.h"
#include "kalmanmfcdlg.h"
#include "kalmantest.h"

void DrawLegend ( MyCvImage& img ) 
{
	CvFont font;
	int font_face = CV_FONT_HERSHEY_COMPLEX_SMALL;
	float xscale = 1;
	float yscale = 1;
	cvInitFont ( &font, font_face, xscale, yscale, 0, 2 );
	cvPutText ( img, "State Points", cvPoint ( 10, 20 ) , &font, CV_RGB(255,255,255));
	cvPutText ( img, "Measured Points", cvPoint ( 10, 50 ) , &font, CV_RGB(255,0,0));
	cvPutText ( img, "Predicted Points", cvPoint ( 10, 80 ) , &font, CV_RGB(0,255,0));
	cvPutText ( img, "Deviation", cvPoint ( 10, 110 ) , & font, CV_RGB( 255, 255, 0 ) );
}
void ShowNotice ( MyCvImage& img ) 
{
	CvFont font;
	int font_face = CV_FONT_HERSHEY_COMPLEX_SMALL;
	float xscale = 1;
	float yscale = 1;
	cvInitFont ( &font, font_face, xscale, yscale, 0, 2 );
	cvPutText ( img, "WARNING !! DEVIATION !!", cvPoint ( 10, 550 ) , &font, CV_RGB(255,255,255));

}
void EraseNotice ( MyCvImage& img ) 
{
	CvFont font;
	int font_face = CV_FONT_HERSHEY_COMPLEX_SMALL;
	float xscale = 1;
	float yscale = 1;
	cvInitFont ( &font, font_face, xscale, yscale, 0, 2 );
	cvPutText ( img, "WARNING !! DEVIATION !!", cvPoint ( 10, 550 ) , &font, CV_RGB(0,0,0));

}




void pyra_segmentation(MyCvImage image)
{
	MyCvImage dst;
	dst = cvCreateImage( cvSize(image->width,image->height), IPL_DEPTH_8U, 1 );
	cvCvtColor(image,dst,CV_RGB2GRAY);	
	MyCvImage eroded = cvCloneImage ( dst );
	cvZero ( eroded );
	cvErode ( dst, eroded, 0 , 3 );
	//cvNamedWindow("GRAY",1);
	//cvShowImage("GRAY", dst);
	//cvNamedWindow ( "ERODED", 1 );
	//cvShowImage ( "ERODED", eroded );
	cvNamedWindow("contours",1);
	CvMemStorage* storage = cvCreateMemStorage ( 0 );
	CvSeq* contours = 0;
	cvThreshold ( eroded, eroded, 1, 255, CV_THRESH_BINARY );
	cvFindContours( eroded, storage, &contours, sizeof(CvContour),CV_RETR_EXTERNAL, CV_CHAIN_APPROX_SIMPLE, cvPoint(0,0) );
	contours = cvApproxPoly( contours, sizeof(CvContour), storage, CV_POLY_APPROX_DP, 3, 1 );
	MyCvImage cnt_img = cvCreateImage( cvSize(500,500), 8, 3 );
	CvSeq* _contours = contours;
	cvZero( cnt_img );
	vector<CvRect> rects;
	//CvMemStorage* bbstore = cvCreateMemStorage ( 0 );
	//CvSeq* bboxes = cvCreateSeq ( 0, sizeof ( CvSeq ) , sizeof ( CvRect ), bbstore );
	for ( ; _contours ; _contours = _contours->h_next )
	{
		CvRect rect = cvBoundingRect ( _contours ); 
		rects.push_back ( rect );
		//cvSeqPush ( bboxes, &rect ) ;
		cvDrawContours( cnt_img, _contours, CV_RGB(255,0,0), CV_RGB(0,255,0), -1, CV_FILLED, 8);
		//cvRectangle ( cnt_img, cvPoint ( rect.x, rect.y ) , cvPoint ( rect.x + rect.width, rect.y + rect.height ) , CV_RGB(255,255,255), 2 );
	}    	
	//find the max size rectangle and draw it.
	CvRect max = { 0 , 0 , 0 , 0 };
	for ( int i = 0 ; i < rects.size ( ) ; i ++ )
	{
		if ( rects[i].height > max.height && rects[i].width > max.width ) 
			max = rects[ i ];

	}
	CvPoint midpt = { 0 , 0 };
	midpt.x = max.x + max.width / 2;
	midpt.y = max.y + max.height / 2;
	cvRectangle ( cnt_img, cvPoint ( max.x, max.y ) , cvPoint ( max.x + max.width, max.y + max.height ) , CV_RGB(255,255,255), 2 );	
	draw_cross (cnt_img,  midpt, CV_RGB ( 255, 255, 255 ), 3, 4 ) ;
	int font_face = CV_FONT_HERSHEY_COMPLEX_SMALL;
	float xscale = 1.0f;
	float yscale = 1.0f;
	CvFont font;
	cvInitFont ( &font, font_face, xscale, yscale, 0, 2 );	
	cvPutText ( cnt_img, "The Cross(x) in the middle shows", cvPoint ( 10, 20 ), &font, CV_RGB ( 255,255,255 ) );
	cvPutText ( cnt_img, "the center point (Point of Interest)!!", cvPoint ( 10, 40 ), &font, CV_RGB ( 255,255,255 ) );
	cvShowImage( "contours", cnt_img );
	cvWaitKey (  );
	cvDestroyAllWindows ( );
	//cvReleaseImage( &cnt_img );
	//cvReleaseImage ( &dst );
	//cvReleaseImage ( &eroded );

}
void motion_detect_and_draw(MyCvImage img1,MyCvImage img2,int Thresh)
{

	MyCvImage frame_diff,frame_thresh,frame_restore;
	frame_diff = cvCreateImage( cvSize(img1->width,img1->height),IPL_DEPTH_8U, img1->nChannels);
	frame_thresh = cvCreateImage( cvSize(img1->width,img1->height),IPL_DEPTH_8U, img1->nChannels);
	frame_restore = cvCreateImage( cvSize(img1->width,img1->height),IPL_DEPTH_8U, img1->nChannels);
	cvAbsDiff(img1,img2,frame_diff);
	cvThreshold(frame_diff,frame_thresh,Thresh,1,CV_THRESH_BINARY);
	cvDilate(frame_thresh,frame_thresh,NULL,2);		
	cvMul(img1,frame_thresh,frame_restore);
	
	////start pyramidal segmentation.
	//IplImage *dst = cvCreateImage( cvSize(img1->width, img1->height), IPL_DEPTH_8U, 1 );
	//cvCvtColor(frame_restore,dst,CV_RGB2GRAY);	
	//MyCvImage eroded = cvCloneImage ( dst );
	//cvZero ( eroded );
	//cvErode ( dst, eroded, 0 , 3 );
	//CvMemStorage* storage = cvCreateMemStorage ( 0 );
	//CvSeq* contours = 0;
	//cvThreshold ( eroded, eroded, 1, 255, CV_THRESH_BINARY );
	//cvFindContours( eroded, storage, &contours, sizeof(CvContour),CV_RETR_EXTERNAL, CV_CHAIN_APPROX_SIMPLE, cvPoint(0,0) );
	//MyCvImage cnt_img = cvCloneImage ( dst );
	//cvZero ( cnt_img ) ;
	//if ( contours ) 
	//{
	//	contours = cvApproxPoly( contours, sizeof(CvContour), storage, CV_POLY_APPROX_DP, 3, 1 );
	//	
	//	CvSeq* _contours = contours;
	//	cvZero( cnt_img );
	//	vector<CvRect> rects;
	//	for ( ; _contours ; _contours = _contours->h_next )
	//	{
	//		CvRect rect = cvBoundingRect ( _contours ); 
	//		rects.push_back ( rect );
	//		cvDrawContours( cnt_img, _contours, CV_RGB(255,0,0), CV_RGB(0,255,0), -1, CV_FILLED, 8);

	//	}    	

	//	//find the max size rectangle and draw it.
	//	CvRect max = { 0 , 0 , 0 , 0 };
	//	for ( int i = 0 ; i < rects.size ( ) ; i ++ )
	//	{
	//		if ( rects[i].height > max.height && rects[i].width > max.width ) 
	//			max = rects[ i ];

	//	}
	//	CvPoint midpt = { 0 , 0 };
	//	midpt.x = max.x + max.width / 2;
	//	midpt.y = max.y + max.height / 2;
	//	cvRectangle ( cnt_img, cvPoint ( max.x, max.y ) , cvPoint ( max.x + max.width, max.y + max.height ) , CV_RGB(255,255,255), 2 );	
	//	draw_cross (cnt_img,  midpt, CV_RGB ( 255, 255, 255 ), 3, 4 ) ;

	//}

	////end pyramidal segmentation

	//cvShowImage("Camera", cnt_img);
	cvShowImage("Camera", frame_restore);
	//cvReleaseImage( &cnt_img );
	//cvReleaseImage ( &dst );
	//cvReleaseImage ( &eroded );
	

}

void  motiondetect () 
{
	CvCapture* capture = 0;
	const char* input_name;
	cout << "Detecting Camera....\n";
	
	capture = cvCaptureFromCAM(0);//cvCaptureFromAVI("viptraffic(uncomp).avi");
		
	MyCvImage frame, frame_prev, frame_copy;
	int threshold = 40;
	//const char* cascade_name1 ="C:\\export\\Classifiers\\haarcascade_frontalface_alt2.xml";
	//cascade = (CvHaarClassifierCascade*)cvLoad( cascade_name1, 0, 0, 0 );
	int frame_no = 0;
	if(capture)
	{
		MessageBox(NULL, _T("Camera Detected"),_T("Message"), MB_OK);
	}
	else
	{
		MessageBox(NULL, _T("Camera Not Detected"),_T("Message"), MB_OK);
		
	}
	cvNamedWindow( "Camera", 1 );
	for(;;)
	{	
		if( !cvGrabFrame( capture ))
			break;
		frame = cvRetrieveFrame( capture );
		if(frame_no == 0)
		{
			frame_prev = cvCreateImage( cvSize(frame->width,frame->height),
				IPL_DEPTH_8U, frame->nChannels );
			if( frame->origin == IPL_ORIGIN_TL )
				cvCopy( frame, frame_prev, 0 );
			else
				cvFlip( frame, frame_prev, 0 );
		}
		if(!frame)
			break;
		if( !frame_copy )
			frame_copy = cvCreateImage( cvSize(frame->width,frame->height),
			IPL_DEPTH_8U, frame->nChannels );
		if( frame->origin == IPL_ORIGIN_TL )
			cvCopy( frame, frame_copy, 0 );
		else
			cvFlip( frame, frame_copy, 0 );
		//storage = cvCreateMemStorage(0);
		//UpdateData();
		motion_detect_and_draw( frame_copy,frame_prev ,threshold);
		cvCopy( frame_copy, frame_prev  );
		cvNamedWindow("Previous Frame",1);
		cvShowImage("Previous Frame",frame_prev);
		if( cvWaitKey( 100 ) >= 0 )
			break;
		frame_no=1;
	}
	cvReleaseCapture( &capture );
	cvDestroyAllWindows ( );

}

void doVideo ( CkalmanmfcDlg* parent ) 
{

	USES_CONVERSION;

	CString vidpath = _T("");
	CFileDialog dlg(TRUE, _T("*.avi"), _T(""), OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST|OFN_HIDEREADONLY,_T("video files (*.avi)|*.avi"),NULL);
	CString title = _T("Open Video File...");
	dlg.m_ofn.lpstrTitle= title;
	if (dlg.DoModal() == IDOK) 
	{
		vidpath = dlg.GetPathName();

	}
	else return;
	if ( vidpath.IsEmpty() )
		return;

	cvNamedWindow ( "Video", 1 );
	const char* path = W2A(vidpath.GetBuffer ( ));
	MessageBox ( NULL, A2W(path), _T(""), 0 );
	CvCapture* capture = cvCaptureFromFile ( path ) ;
	
	if ( !capture ) 
		MessageBox(NULL, _T("Kill ya!!") , _T("Kill ya!!"), 0);

	MyCvImage img = cvQueryFrame(capture);
	for (;img.image;img=cvQueryFrame(capture))
	{	
		if ( img->origin == IPL_ORIGIN_BL ) cvFlip  ( img );
		cvShowImage ( "Video", img );        			
		if ( cvWaitKey ( 1 ) > 0 ) break;
	}
	
	cvReleaseCapture ( &capture );
	cvDestroyAllWindows ( );
	return;
}
void doKalman(CkalmanmfcDlg* parent)
{
	USES_CONVERSION ;
	/* A matrix data */
	const float A[] = { 1, 1, 0, 1 };

	MyCvImage img = cvCreateImage( cvSize(600,600), 8, 3 );
	CvKalman* kalman = cvCreateKalman( 2, 1, 0 );
	/* state is (phi, delta_phi) - angle and angle increment */
	CvMat* state = cvCreateMat( 2, 1, CV_32FC1 );
	CvMat* process_noise = cvCreateMat( 2, 1, CV_32FC1 );
	/* only phi (angle) is measured */
	CvMat* measurement = cvCreateMat( 1, 1, CV_32FC1 );
	CvRandState rng;
	int code = -1;

	cvRandInit( &rng, 0, 1, -1, CV_RAND_UNI );

	cvZero( measurement );
	cvNamedWindow( "Kalman (INFORMS 2006 International Conference HK)", 1 );

	fstream datafile( "datafile.txt", ios_base::in | ios_base::out | ios_base::trunc );
	vector<int> ptsx;
	vector<int> ptsy;

	for(;;)
	{
		parent->ClearLog();
		cvRandSetRange( &rng, 0, 0.1, 0 );
		rng.disttype = CV_RAND_NORMAL;

		cvRand( &rng, state );

		memcpy( kalman->transition_matrix->data.fl, A, sizeof(A));
		cvSetIdentity( kalman->measurement_matrix, cvRealScalar(1) );
		cvSetIdentity( kalman->process_noise_cov, cvRealScalar(1e-5) );
		cvSetIdentity( kalman->measurement_noise_cov, cvRealScalar(0.1) );
		cvSetIdentity( kalman->error_cov_post, cvRealScalar(1));
		/* choose random initial state */
		cvRand( &rng, kalman->state_post );

		rng.disttype = CV_RAND_NORMAL;

		cvZero ( img ) ;
		DrawLegend ( img );
		ptsx.clear ( );
		ptsy.clear ();

		for(;;)
		{


#define calc_point(angle)                                      \
	cvPoint( cvRound(img->width/2 + img->width/4*cos(angle)),  \
	cvRound(img->height/2 - 1.5 * img->width/4*sin(angle)))

			float state_angle = state->data.fl[0];

			CvPoint state_pt = calc_point(state_angle);

			/* predict point position */
			const CvMat* prediction = cvKalmanPredict( kalman, 0 );
			float predict_angle = prediction->data.fl[0];
			CvPoint predict_pt = calc_point(predict_angle);
			float measurement_angle;
			CvPoint measurement_pt;

			cvRandSetRange( &rng, 0, sqrt(kalman->measurement_noise_cov->data.fl[0]), 0 );
			cvRand( &rng, measurement );

			/* generate measurement */
			cvMatMulAdd( kalman->measurement_matrix, state, measurement, measurement );

			measurement_angle = measurement->data.fl[0];
			measurement_pt = calc_point(measurement_angle);


			/* plot points */
//#define draw_cross( center, color, d )                                 \
//	cvLine( img, cvPoint( center.x - d, center.y - d ),                \
//	cvPoint( center.x + d, center.y + d ), color, 1, 0 ); \
//	cvLine( img, cvPoint( center.x + d, center.y - d ),                \
//	cvPoint( center.x - d, center.y + d ), color, 1, 0 )

			//cvZero( img );
			draw_cross(img, state_pt, CV_RGB(255,255,255), 3 , 1);
			draw_cross(img, measurement_pt, CV_RGB(255,0,0), 3, 1 );
			draw_cross(img, predict_pt, 
				CV_RGB(0,255,0), 
				3, 1 );

			cvLine( img, state_pt, predict_pt, CV_RGB(255,255,0), 1, 0 );
			//cvLine( img, measurement_pt, predict_pt, CV_RGB(255,255,0), 1, 0 );

			/* adjust Kalman filter state */
			cvKalmanCorrect( kalman, measurement );

			cvRandSetRange( &rng, 0, sqrt(kalman->process_noise_cov->data.fl[0]), 0 );
			cvRand( &rng, process_noise );
			cvMatMulAdd( kalman->transition_matrix, state, process_noise, state );

			int deltax = abs(predict_pt.x - state_pt.x) ;
			int deltay = abs(predict_pt.y - state_pt.y) ;
			/*int deltax = abs(predict_pt.x - measurement_pt.x) ;
			int deltay = abs(predict_pt.y - measurement_pt.y) ;*/

			if ( deltax > 50 || deltay > 50 ) 
			{
				ShowNotice ( img );
				cvShowImage( "Kalman (INFORMS 2006 International Conference HK)", img );
				//Beep ( 750, 300 );
				MessageBeep ( MB_ICONASTERISK );
				cvWaitKey  ( 500 );
				EraseNotice ( img );	
				cvShowImage ( "Kalman (INFORMS 2006 International Conference HK)", img );
				char timetemp [ 80 ] = { 0 };
				char datetemp [ 80 ] = { 0 };
				_strtime_s ( timetemp, 80 );
				_strdate_s ( datetemp, 80 );
				//cout << "Deviation at : (" << state_pt.x << ", " << state_pt.y << "). DeltaX = " << deltax << "\tDeltaY = " << deltay << "\t" << timetemp << "\t" << datetemp << endl;
				//datafile << "Deviation at : (" << state_pt.x << ", " << state_pt.y << "). DeltaX = " << deltax << "\tDeltaY = " << deltay << "\t" << timetemp << "\t" << datetemp << endl;
				//cout << "Deviation at : (" << measurement_pt.x << ", " << measurement_pt.y << "). DeltaX = " << deltax << "\tDeltaY = " << deltay << "\t" << timetemp << "\t" << datetemp << endl;
				stringstream log;
				log << "Deviation at : (" << measurement_pt.x << ", " << measurement_pt.y << "). DeltaX = " << deltax << " DeltaY = " << deltay << " " << timetemp << " " << datetemp << endl;
				parent->LogString ( A2W(log.str().c_str() ) ) ;
				datafile << "Deviation at : (" << measurement_pt.x << ", " << measurement_pt.y << "). DeltaX = " << deltax << "\tDeltaY = " << deltay << "\t" << timetemp << "\t" << datetemp << endl;
				ptsx.push_back ( deltax );
				ptsy.push_back ( deltay );

			} 
			else
			{
				cvShowImage( "Kalman (INFORMS 2006 International Conference HK)", img );
			}

			code = cvWaitKey( 100 );

			if( code > 0 ) /* break current simulation by pressing a key */
			{

				int resp = MessageBox ( NULL, _T("Beginning new simulation !! All data related to the previous simulation will be erased !! \n Do you want to save it now ?") , _T("Kalman"), MB_YESNOCANCEL | MB_ICONWARNING );
				if ( resp == IDYES ) 
				{
					stringstream str ;
					str << "old data - " << (unsigned long)time ( NULL ) << ".txt";
					cout << str.str ( ) << endl;
					fstream olddatafile( str.str().c_str() , ios_base::in | ios_base::out | ios_base::trunc ) ;
					datafile.seekg ( 0, ios_base::beg );
					string line;
					while ( getline ( datafile, line, '\n' ) )
					{
						olddatafile << line << endl;

					}
					olddatafile.close ( );
					datafile.clear();
					datafile.close () ;
					datafile.open ( "datafile.txt", ios_base::in | ios_base::out | ios_base::trunc );
					stringstream log;
					//cout << "Erasing data file!!" << endl;
					log << "Erasing data file!!" << endl;
					parent->LogString ( A2W(log.str().c_str ()) );

					if ( ! datafile.is_open () )
					{
						log.str("");
						//cout << "warning !! data file not open !!"<<endl;
						log<< "warning !! data file not open !!"<<endl;
						parent->LogString ( A2W(log.str().c_str ( ) ));

					}

					if ( ! datafile.good ( ) ) 
					{
						log.str("");
						//cout << "warning !! data file not open !!"<<endl;
						log<< " wont write to data file !! File bad !! "<<endl;
						parent->LogString ( A2W(log.str().c_str ( ) ));

						//cout << " wont write to data file !! File bad !! " << endl;

					}

						
					cvZero ( img );
					DrawLegend ( img );
					//DrawHistogram ( ptsx, ptsy );
					break;

				}
				else if ( resp == IDNO )
				{
					datafile.close () ;
					datafile.open ( "datafile.txt", ios_base::in | ios_base::out | ios_base::trunc );
					cvZero ( img );
					DrawLegend ( img );
					//DrawHistogram ( ptsx, ptsy );
					break;

				}

				else if ( resp == IDCANCEL )
				{

				}


			}


		}
		if( code == 27 ) /* exit by ESCAPE */
			break;
	}
	cvDestroyAllWindows ();

}
vector< MyCvImage >  getThresholdedImage( MyCvImage img, int threshold )
{
	
	MyCvImage bin = cvCloneImage( img ) ;
	cvZero ( bin ) ;
	cvThreshold ( img, bin, threshold, 255, CV_THRESH_BINARY );


	vector< MyCvImage > bins;

	if ( bin->nChannels == 1 ) 
	{
		bins.push_back ( cvCloneImage ( bin ) ) ;
		return bins;

	}

	
	for ( int i = 0 ; i < bin->nChannels; i ++ )
		bins.push_back ( cvCreateImage ( cvSize ( bin->width , bin->height ) , IPL_DEPTH_8U , 1 ) );

	for ( int i = 1 ; i <= bin->nChannels ; i ++ )
	{
		cvSetImageCOI ( bin, i );	
		cvCopy ( bin, bins[i-1] );
		
	}
	return bins;

}
void DisplayImage ( const char* title , MyCvImage image )
{
	cvNamedWindow ( title, 0 );
	cvShowImage ( title, image ) ;
	cvWaitKey ( ) ;
	cvDestroyWindow ( title ) ;

}
#define cvClose(src,dst,kernel,iterations) for ( int i = 0 ; i < iterations ; i ++ ){cvDilate(src,dst,kernel,1);cvErode(src,dst,kernel,1);} 

void testContours ( const char* filename )
{

	MyCvImage img = cvLoadImage ( filename );
	MyCvImage inverted = cvCreateImage ( cvGetSize ( img ), img->depth, img->nChannels ) ;
	cvNot ( img, inverted );
	img.Release ( );
	img = inverted;
	MyCvImage grayimg  = cvCreateImage ( cvGetSize ( img ) , img->depth , 1 );
	cvCvtColor ( img, grayimg, CV_RGB2GRAY );
	DisplayImage ( "Original Image", img ) ;
	DisplayImage ( "Original Grayscale Image", grayimg ) ;
	vector< MyCvImage > bins = getThresholdedImage ( grayimg, 40 );		
	int nChannels = bins.size ( );
	CvMemStorage* storage = cvCreateMemStorage (  );
	for ( int i = 0 ; i < nChannels; i ++ )
	{
		
		MyCvImage currentimg = bins [ i ];
		stringstream winname;
		winname << "Original Thresholded Image : Channel " << i + 1 ;
		DisplayImage ( winname.str().c_str ( ), currentimg );
		CvSeq* contours = 0;
		
		int vals[] = { 0, 1, 0, 1, 1, 1, 0, 1, 0 };
		IplConvKernel* kernel = cvCreateStructuringElementEx ( 3, 3, 1, 1, CV_SHAPE_CUSTOM, vals );
		//cvErode ( currentimg, currentimg, kernel, 1 );
		cvDilate ( currentimg, currentimg, kernel, 1 );
		//cvErode ( currentimg, currentimg, 0, 1 );
		cvReleaseStructuringElement ( &kernel );

		winname.str ( "" );
		winname << "Result of closing : Channel " << i + 1 ;
		DisplayImage ( winname.str ().c_str ( ) , currentimg );
		//cvClose ( currentimg, currentimg, 0, 5 );
		cvFindContours ( currentimg, storage, &contours, sizeof ( CvContour ) , CV_RETR_EXTERNAL, CV_CHAIN_APPROX_SIMPLE );
		//contours = cvApproxPoly ( contours, sizeof(CvContour), storage, CV_POLY_APPROX_DP, 0, 0 );
		winname.str("");
		winname << "Contours : Channel " << i + 1;
		MyCvImage temp = cvCloneImage ( currentimg ) ;
		cvThreshold ( currentimg, temp, 1, 255, CV_THRESH_BINARY );
		DisplayImage ( winname.str().c_str ( ), temp );
		MyCvImage cnt_img = cvCreateImage ( cvGetSize ( currentimg ), 8, 3 );		
		CvSeq* _contours = contours;
		vector<CvRect> rects;
		for ( ; contours ; contours = contours->h_next )			
		{
			CvRect rect = cvBoundingRect ( contours );
			rects.push_back ( rect );
			cvDrawContours(cnt_img, contours, CV_RGB(255,0,0), CV_RGB(0,255,0), -1);
		}

		winname.str("");
		winname << "Contours Drawn : Channel " << i + 1 ;
		DisplayImage ( winname.str().c_str(), cnt_img );

		//MyCvImage rectsimg = cvCloneImage ( cnt_img ) ;
		//cvZero ( rectsimg );
		vector<MyCvImage> rectsimgs;
		for ( vector<CvRect>::iterator iter = rects.begin ( ); iter!=rects.end (); iter ++ )
		{	
			MyCvImage rectimg = cvCloneImage ( cnt_img ) ;
			cvZero ( rectimg );
			CvScalar color = CV_RGB ( rand()&255, rand()&255 , rand()&255 );
			cvRectangle ( rectimg, cvPoint ( iter->x, iter->y ) , 
				cvPoint ( iter->x + iter->width , iter->y + iter->height ) , color, 1 );
			cvRectangle ( img, cvPoint ( iter->x, iter->y ) , 
				cvPoint ( iter->x + iter->width , iter->y + iter->height ) , color, 1 );			
			rectsimgs.push_back ( rectimg ) ;

		}	
		DisplayImage ( "The Thing", img );
		//for ( int j = 0 ; j < rectsimgs.size () ; j ++ ) 
		//{
		//	winname.str ( "" );
		//	winname << "Rectangles Drawn : Channel " << i + 1 << " Object " << j + 1;
		//	DisplayImage ( winname.str().c_str ( ), rectsimgs[j] );

		//
		//}

	}	
	
}

void testThreshold ( )
{
	MyCvImage img = cvLoadImage ( "blob_image.jpg" );
	MyCvImage bin = cvCloneImage ( img ) ;//cvCreateImage ( cvSize ( img->width, img->height ), IPL_DEPTH_8U, img->nChannels );
	cvZero ( bin ) ;
	MyCvImage bin1 = cvCreateImage ( cvSize ( bin->width, bin->height ), IPL_DEPTH_8U, 1 );
	cvThreshold ( img, bin, 50, 255, CV_THRESH_BINARY );
	cvNamedWindow( "Thresholded Image", 1 );
	for ( int i = 1 ; i <= bin->nChannels ; i ++ )
	{
		cvSetImageCOI ( bin, i );	
		cvCopy ( bin, bin1 );
		cvShowImage ( "Thresholded Image", bin1 );
		cvWaitKey ( );
		
	}
	cvDestroyAllWindows ( );
}
struct DetectedObject 
{
	CvRect rect;
	CvPoint pos;

};

vector<DetectedObject> DetectnSeparateObjects ( MyCvImage img, int pixthresh, CvRect sizethresh )
{

	MyCvImage grayimg  = cvCreateImage ( cvGetSize ( img ) , img->depth , 1 );
	if ( img->nChannels > 1 ) 
		cvCvtColor ( img, grayimg, CV_RGB2GRAY );
	else
		cvCopy ( img, grayimg );
	//MyCvImage morphedframe = 
	cvEqualizeHist ( grayimg, grayimg );
	//cvAdaptiveThreshold ( grayimg, grayimg, 255, CV_ADAPTIVE_THRESH_GAUSSIAN_C, CV_THRESH_BINARY );
	cvThreshold ( grayimg, grayimg, pixthresh, 255, CV_THRESH_BINARY );
	//cvErode ( grayimg, grayimg, kernel, 1 );
	//cvDilate ( grayimg, grayimg, kernel, 1 );
	//cvErode ( grayimg, grayimg, kernel, 1 );
	IplConvKernel* kernel = cvCreateStructuringElementEx ( 5, 5, 2, 2, CV_SHAPE_RECT, 0 );

	//cvDilate ( grayimg, grayimg, kernel, 1 );
	cvErode ( grayimg, grayimg, kernel, 1 );
	cvDilate ( grayimg, grayimg, kernel, 1 );
	cvErode ( grayimg, grayimg, kernel, 1 );

	cvShowImage ( "Morphed Frame", grayimg );
	CvMemStorage* storage = cvCreateMemStorage (  );
	CvSeq* contours = 0;
	cvFindContours ( grayimg, storage, &contours, sizeof ( CvContour ) , CV_RETR_EXTERNAL, CV_CHAIN_APPROX_SIMPLE );

	vector<DetectedObject> objects;
	for ( ; contours ; contours = contours->h_next )
	{
		CvRect rect = cvBoundingRect ( contours ) ;
		CvPoint pos = cvPoint (  rect.x + rect.width / 2, rect.y + rect.height / 2 );
		DetectedObject obj = { rect, pos };
		if ( rect.width >= sizethresh.width && rect.height >= sizethresh.height ) 
		{
			objects.push_back ( obj );
		}

		
	}

	return objects;

}
void testObjects ( const char* filename )
{
	MyCvImage img = cvLoadImage ( filename );
	CvRect rect;
	rect.width  = 10;
	rect.height = 10;
	vector<DetectedObject> objects = DetectnSeparateObjects ( img , 100 , rect);
	cout << objects.size () << endl;
	for ( vector<DetectedObject>::iterator iter = objects.begin () ; iter != objects.end (); iter ++ )
	{
		cout << "width : " << iter->rect.width << "\t" << "height : " << iter->rect.height << endl;
		MyCvImage temp = cvCreateImage ( cvGetSize ( img ), 8, 3 );
		cvZero ( temp );
		CvScalar color = CV_RGB ( rand () & 255, rand () & 255, rand () & 255 );
		cvRectangle ( temp, cvPoint ( iter->rect.x, iter->rect.y ), cvPoint ( iter->rect.x + iter->rect.width , iter->rect.y + iter->rect.height ), color, CV_FILLED );
		draw_cross(temp, (iter->pos), color, 3, 2 );
		stringstream strstream;
		strstream << "Object " << iter - objects.begin () + 1 ;
		DisplayImage ( strstream.str().c_str(), temp );


	}


}

MyCvImage DetectMotion ( MyCvImage thisframe, MyCvImage prevframe, int motionthresh )
{

	MyCvImage tempthis = cvCreateImage ( cvGetSize ( thisframe ) , 8, 3);
	MyCvImage tempprev = cvCreateImage ( cvGetSize ( prevframe ) , 8 , 3);
	
	MyCvImage frame_diff = cvCreateImage( cvGetSize(thisframe),IPL_DEPTH_8U, 3);
	MyCvImage frame_thresh = cvCreateImage( cvGetSize(thisframe),IPL_DEPTH_8U, 3);
	MyCvImage frame_restore = cvCreateImage( cvGetSize(thisframe),IPL_DEPTH_8U, 3);

	/*
	cvCvtColor ( thisframe, tempthis, CV_RGB2GRAY );
	cvCvtColor ( prevframe, tempprev, CV_RGB2GRAY );
	cvEqualizeHist ( tempthis, tempthis );
	cvEqualizeHist ( tempprev, tempprev );
	*/
	
	tempthis = thisframe;
	tempprev = prevframe;

	cvAbsDiff(tempthis,tempprev,frame_diff);
	cvThreshold(frame_diff,frame_thresh,motionthresh,1,CV_THRESH_BINARY);
	//cvDilate(frame_thresh,frame_thresh,NULL,2);		

	cvMul(tempthis,frame_thresh,frame_restore);
	return frame_restore;


}
struct DetectedObjectsContext
{
	vector<DetectedObject> objects;
	int num_objs;
	MyCvImage frame;
	MyCvImage motionframe;
	MyCvImage rectsframe;
	MyCvImage morphedframe;
};


void TrackMovingObjects ( CvCapture* capture, int pixthresh, CvRect sizethresh, int motionthresh )
{
}

void DetectMovingObjects ( CvCapture* capture, int pixthresh, CvRect sizethresh, int motionthresh )
{

	MyCvImage frame, prevframe, thisframe, tempframe;
	cvNamedWindow ( "Original Video", 0 );
	cvNamedWindow ( "Moving Objects", 0 );
	cvNamedWindow ( "Detected Objects", 0 );
	cvNamedWindow ( "Morphed Frame", 0 );

	MyCvImage MHI ;
	//vector< DetectedObjectsContext > objhistory;
	for ( int frameno = 1;; frameno ++ )
	{

		if (! cvGrabFrame ( capture )) break;
		frame = cvRetrieveFrame ( capture );
		if ( ! MHI.image )
		{

			 MHI = cvCreateImage ( cvGetSize ( frame ) , IPL_DEPTH_32F, 1 );
			 cvSetZero ( MHI );
		}


		tempframe = cvCloneImage ( frame );
		cvZero ( tempframe );
		if ( frame->origin == IPL_ORIGIN_TL )
			cvCopy ( frame, tempframe );
		else
			cvFlip ( frame, tempframe );
		
		if ( ! prevframe.image )
		{
			prevframe = cvCloneImage ( tempframe );
			cvCopy ( tempframe , prevframe );
			continue;

		}
		thisframe = cvCloneImage ( tempframe );
		MyCvImage motionframe = DetectMotion ( thisframe, prevframe, motionthresh );
		double timestamp = (double)clock ( );
		cvUpdateMotionHistory ( motionframe, MHI, timestamp, timestamp/CLOCKS_PER_SEC );
		/*
		MyCvImage mask = cvCreateImage
		cvCalcMotionGradient ( MHI, 
		*/
		vector<DetectedObject> objects = DetectnSeparateObjects ( motionframe, pixthresh, sizethresh ) ;
		MyCvImage rectsimg = cvCreateImage ( cvGetSize ( motionframe ) , 8 , 3 );
		cvZero ( rectsimg );
		for ( vector<DetectedObject>::iterator iter = objects.begin () ; iter != objects.end () ; iter ++ )
		{
			//for ( vector<DetectedObject>::iterator initer = prevobjs.begin () ; initer != objects.end () ; initer ++ )
			//{
			//	int dx = iter->pos.x - prevobj.pos.x;
			//	int dy = iter->pos.y - prevobj.pos.y;
			//	if ( dx <= motionthresh && dy <= motionthresh ) 
			//	{

			//	}

			//}
			CvScalar color = CV_RGB ( rand () & 255, rand () & 255, rand () & 255 );
			cvRectangle ( rectsimg, cvPoint ( iter->rect.x, iter->rect.y ), cvPoint ( iter->rect.x + iter->rect.width , iter->rect.y + iter->rect.height ), color, 1 );
			//draw_cross(rectsimg,iter->pos,color,3,1);
			//MyCvImage mask = cvCreateImage ( cvGetSize ( rectsimg ), 8, 3 );
			//cvAbsDiff ( rectsimg, prevrectsimg, mask );
			//cvThreshold ( mask, mask, 1, 1, CV_THRESH_TOZERO );
			//cvUpdateMotionHistory ( mask, MHI, frameno, 3);
			//MyCvImage segmask = cvCreateImage ( cvGetSize ( frame ), IPL_DEPTH_32F, 1 );
			//CvMemStorage* storage = cvCreateMemStorage ( );
			//CvSeq* segs = cvSegmentMotion ( MHI, segmask, storage, frameno, 3 );
			//for ( int i = 1 ; segs ; segs = segs->h_next , i ++)
			//{
			//	MyCvImage dst ( segmask );
			//	cvCmpS ( segmask, 1, dst, CV_CMP_EQ );
			//	
			//}
			
			//prevrectsimg = rectsimg;
			DetectedObjectsContext ctxt;
			
			//objhistory.push_back ( 
			
		}
		cout << objects.size () << " Object(s) in frame " << frameno << " at : " << endl;
		for ( vector<DetectedObject>::iterator iter = objects.begin () ; iter != objects.end () ; iter ++  )
		{
			cout << "(x,y) = (" << iter->pos.x << ", " << iter->pos.y << ")" << endl;

		}
		cvShowImage ( "Original Video", frame );
		cvShowImage ( "Moving Objects", motionframe );
		cvShowImage ( "Detected Objects" , rectsimg );
		if ( cvWaitKey ( 100 ) > 0 ) break;
		cvCopy ( thisframe, prevframe );
		

	}
	cvDestroyAllWindows ();
	
}
void testDetectMovingObjects ( )
{

	CvCapture* capture = 0;
	//cout << "Detecting Camera....\n";
	//capture = cvCaptureFromCAM(0);
	//if(capture)
	//{
	//	cout << "Camera Detected" << endl;
	//}
	//else
	//{
	//	cout << "Camera Not Detected" << endl;
	//	return;
	//	
	//}

	capture = cvCaptureFromAVI("viptraffic(uncomp).avi");
	//capture = cvCaptureFromAVI("car.avi");
	//capture = cvCaptureFromAVI("car2.avi");
	//capture = cvCaptureFromAVI ( "asif.avi" );
	CvRect sizethresh = { 0 , 0 , 10 , 10 } ;
	DetectMovingObjects ( capture, 60, sizethresh, 30 );

		
}

void testPyraSeg ( const char* filename )
{
	MyCvImage pimg (filename);
	pyra_segmentation(pimg);

}
bool getFrame ( CvCapture* capture, MyCvImage* frameout )
{

	if (! cvGrabFrame ( capture )) return false;
	MyCvImage frame = cvRetrieveFrame ( capture );
	MyCvImage tempframe = cvCloneImage ( frame );
	cvZero ( tempframe );
	if ( frame->origin == IPL_ORIGIN_TL )
		cvCopy ( frame, tempframe );
	else
		cvFlip ( frame, tempframe );
	
	*frameout = tempframe;
	return true;
}

void SubtractBG ( CvCapture* capture )
{
	MyCvImage frame;
	MyCvImage floatframe;
	MyCvImage mean;
	MyCvImage grayframe;
	MyCvImage sqsum;
	CvDisplayWnd fgwnd ( "Foreground" );
	CvDisplayWnd bgwnd ( "Background" );
	CvDisplayWnd framewnd ( "Frame" );
	for ( int frameno = 1;getFrame (capture, &frame); frameno ++  )
	{
		if ( mean.image == NULL )
		{
			mean.image = cvCreateImage ( cvGetSize(frame), IPL_DEPTH_32F, 1);
			cvZero ( mean );

		}
		if ( floatframe.image == NULL )
		{
			floatframe.image = cvCreateImage ( cvGetSize(frame), IPL_DEPTH_32F, 1 );
			cvZero ( floatframe );

		}
		if ( grayframe.image == NULL )
		{
			grayframe.image = cvCreateImage ( cvGetSize(frame), IPL_DEPTH_8U , 1 );
			cvZero ( grayframe );

		}
		
		if ( sqsum.image == NULL )
		{
			sqsum.image = cvCreateImage ( cvGetSize ( frame ), IPL_DEPTH_32F, 1 );
			cvZero ( sqsum );

		}
		
		cvCvtColor ( frame, grayframe, CV_RGB2GRAY );
		cvConvertScale ( grayframe, floatframe );		
		cvRunningAvg ( floatframe, mean, 0.05 );
		cvSquareAcc ( floatframe, sqsum );
		MyCvImage temp = sqsum;
		cvConvertScale ( sqsum, temp, 1./frameno );
		MyCvImage temp2 = mean;
		cvPow ( temp2, mean, 2 );
		MyCvImage stddev = mean ;
		cvSub ( temp, temp2, stddev );
		cvPow ( stddev, stddev, 0.5 );
		MyCvImage FGtemp =  mean ;
		cvAbsDiff ( mean, floatframe, FGtemp );
		cvConvertScale ( stddev, stddev, 3 );
		MyCvImage FG = cvCreateImage ( cvGetSize ( FGtemp ), IPL_DEPTH_8U, 1 );
		cvCmp ( FGtemp, stddev, FG, CV_CMP_GT );
		/*MyCvImage FG = cvCloneImage ( mean );
		cvZero ( FG );
		cvAbsDiff ( mean, floatframe, FG );
		MyCvImage FGNot = cvCreateImage (cvGetSize (FG), IPL_DEPTH_8U, 1);
		cvConvertScale ( FG, FGNot );
		cvNot ( FGNot, FGNot );
		cvThreshold ( FGNot, FGNot, 250, 255, CV_THRESH_BINARY );*/
		
		framewnd.ShowImage ( frame );
		//fgwnd.ShowImage ( FGNot );
		fgwnd.ShowImage ( FG );
		bgwnd.ShowImage ( mean );
		
		if ( cvWaitKey ( 10 ) > 0 ) break;

	}

}
void testDetectMotion ( CvCapture* capture )
{

	MyCvImage frame, prevframe, thisframe, tempframe;
	CvDisplayWnd origwnd ( "Original Video");
	CvDisplayWnd movwnd ( "Moving Objects" );

	for ( int frameno = 1;; frameno ++ )
	{

		if (! cvGrabFrame ( capture )) break;
		frame = cvRetrieveFrame ( capture );
		tempframe = cvCloneImage ( frame );
		cvZero ( tempframe );
		if ( frame->origin == IPL_ORIGIN_TL )
			cvCopy ( frame, tempframe );
		else
			cvFlip ( frame, tempframe );

		if ( !prevframe )
		{
			prevframe = cvCloneImage ( tempframe );
			cvCopy ( tempframe , prevframe );
			continue;

		}
		thisframe = cvCloneImage ( tempframe );
		MyCvImage motionframe = DetectMotion ( thisframe, prevframe, 40);
		origwnd.ShowImage ( frame );
		movwnd.ShowImage ( motionframe );	
		if ( cvWaitKey ( 100 ) > 0 ) break;
		cvCopy ( thisframe, prevframe );
		thisframe.Release ();
		tempframe.Release ();

	}

}
void logString ( CkalmanmfcDlg* parent, const char* str )
{
	USES_CONVERSION;
	parent->LogString ( A2W ( str ) );

}

void testBGthing ( CvCapture* cap, CkalmanmfcDlg* parent )
{

	parent->ClearLog ( );

	//MyCvImage pf, cf;
	//CvDisplayWnd wnd ( "fg" );
	//while ( cvGrabFrame ( cap ) )
	//{
	//	if ( pf.image == NULL )
	//	{
	//		pf = cvRetrieveFrame ( cap );
	//		continue;

	//	}
	//	cf = cvRetrieveFrame ( cap );
	//	if ( cf->origin == IPL_ORIGIN_BL ) 
	//		cvFlip ( cf );

	//	MyCvImage motion = DetectMotion ( cf, pf, 40 );
	//	wnd.ShowImage ( motion );
	//	if ( cvWaitKey ( 10 ) > 0 ) break;
	//	pf = cf;

	//}

	IplImage* tmp_frame = NULL;
	MyCvImage copy_frame; 
	//MyCvImage morphed_frame;
	cvNamedWindow("Original Video", 0);
	cvNamedWindow("Foreground", 0);
	//cvNamedWindow("morphed FG", 1);
	CvBGStatModel* bg_model = NULL;
	

	for( ;; )
	{

		if ( !cvGrabFrame ( cap ) ) break;
		tmp_frame = cvRetrieveFrame ( cap );
		if ( tmp_frame->origin == IPL_ORIGIN_BL ) cvFlip ( tmp_frame );
		if ( !bg_model ) 
		{
			bg_model = cvCreateFGDStatModel( tmp_frame );
			continue;
		}


		//double t = (double)cvGetTickCount();
		//cvFlip ( tmp_frame, copy_frame );
		//copy_frame->origin = IPL_ORIGIN_TL;
		//cvFlip ( copy_frame );
		cvUpdateBGStatModel( tmp_frame, bg_model );
		//t = (double)cvGetTickCount() - t;
		//printf( "%.1f\n", t/(cvGetTickFrequency()*1000.) );
		cvShowImage("Original Video", tmp_frame);
		//MyCvImage temp = bg_model->foreground;
		//cvThreshold ( temp, temp, 1, 255, CV_THRESH_BINARY );
		//cvFlip ( temp );
		//cvAnd ( copy_frame, temp, copy_frame );
		/*stringstream str ;
		str << bg_model->foreground->nChannels;
		logString ( parent, str.str().c_str ( ) );
		*/
		MyCvImage temp1 = bg_model->background;
		cvZero ( temp1 );
		MyCvImage temp2 = bg_model->background;
		cvZero ( temp2 );
		cvCvtPlaneToPix ( bg_model->foreground, bg_model->foreground, bg_model->foreground, 0, temp1 );
		cvAnd ( tmp_frame, temp1, temp2 );
		MyCvImage temp3 = bg_model->background;
		cvZero ( temp3 );
		cvSub ( temp2, bg_model->background, temp3 );
		//cvShowImage("Foreground", bg_model->foreground);
		cvShowImage("Foreground", temp3);
		//morphed_frame = temp;
		//cvDilate(morphed_frame, morphed_frame, 0, 3);
		//cvErode(morphed_frame, morphed_frame, 0, 3);
		//cvShowImage("morphed FG", morphed_frame);
		int k = cvWaitKey(1);
		if( k == 27 ) break;
		//printf("frame# %d \n", fr);
		//copy_frame.Release ( );
		//morphed_frame.Release ( );
		//temp.Release ();

	}

	cvReleaseBGStatModel( &bg_model );
	cvDestroyAllWindows ( );

}

void doKalmanTest(CvCapture* capture, CkalmanmfcDlg* parent)
{

	//testPyraSeg ( "blob_image03.jpg" );
	//doKalman();
	//doVideo();
	//motiondetect();
	//testThreshold();
	//testContours("testimg2.jpg");
	//testContours("segmentedcars_0002.jpg");
	//testObjects("segmentedcars_0001.jpg");
	//testDetectMovingObjects ( );
	//SubtractBG ( capture );
	//testDetectMotion ( capture );
	//testBGthing ( capture );

}

void testLogging ( CkalmanmfcDlg* parent )
{
	for ( int i = 0 ; i < 100 ; i ++ )
		parent->LogString ( _T("this is bad") );

}

void goodfeatures ( MyCvImage img )
{

	
	MyCvImage gray = cvCreateImage ( cvGetSize (img), 8, 1 );
	cvCvtColor ( img, gray, CV_BGR2GRAY );

	MyCvImage eig = cvCreateImage ( cvGetSize ( gray ), IPL_DEPTH_32F, 1 );
	MyCvImage temp_eig = eig;

	CvPoint2D32f* corners = (CvPoint2D32f*)cvAlloc ( sizeof ( CvPoint2D32f ) * 500 );
	int corner_count = 500;

	cvGoodFeaturesToTrack ( gray, eig, temp_eig, corners, &corner_count, 0.01, 10 );
	cvFindCornerSubPix ( gray, corners, corner_count, cvSize (5, 5), cvSize ( -1, -1 ), cvTermCriteria ( CV_TERMCRIT_ITER | CV_TERMCRIT_EPS, 10, 0.03 ) );


	for ( int i = 0 ; i < corner_count ; i ++ )
	{
		CvPoint pt = { corners[i].x, corners[i].y };
		cvCircle ( img, pt, 3, CV_RGB( 0, 255, 0 ), CV_FILLED );

	}
	
	CvDisplayWnd features( "features", 0 );
	features.ShowImage ( img );
	cvWaitKey ( );

}

void track ( vector<MyCvImage> imgs )
{

	CvDisplayWnd wnd ( "Image", 0 );

	bool firstimg = true;

	MyCvImage prev_gray, pyramid, prev_pyramid;
	CvPoint2D32f* points [ 2 ] = { NULL, NULL };

	const int MAX_COUNT = 500;
	int count = MAX_COUNT;

	char* status = NULL;

	int flags = 0;

	vector<MyCvImage>::iterator iter = imgs.begin ();
	for( ; iter != imgs.end(); iter ++ )
	{
				
		MyCvImage gray = cvCreateImage( cvGetSize(*iter), 8, 1 );
		cvCvtColor( *iter, gray, CV_BGR2GRAY );

		if ( firstimg )
		{

			prev_gray = cvCreateImage( cvGetSize(*iter), 8, 1 );
			pyramid = cvCreateImage ( cvGetSize ( *iter ) , 8, 1 );
			prev_pyramid = pyramid;

			points[0] = (CvPoint2D32f*)cvAlloc(MAX_COUNT*sizeof(CvPoint2D32f));
			points[1] = (CvPoint2D32f*)cvAlloc(MAX_COUNT*sizeof(CvPoint2D32f));

			status = (char*)cvAlloc ( MAX_COUNT * sizeof ( char ) );

			MyCvImage eig = cvCreateImage( cvGetSize(gray), 32, 1 );
			MyCvImage temp = cvCreateImage( cvGetSize(gray), 32, 1 );
		
			double quality = 0.01;
			double min_distance = 10;

			count = MAX_COUNT;
			cvGoodFeaturesToTrack( gray, eig, temp, points[1], &count, quality, min_distance, 0, 3, 0, 0.04 );
			cvFindCornerSubPix( gray, points[1], count, cvSize(5,5), cvSize(-1,-1), cvTermCriteria(CV_TERMCRIT_ITER|CV_TERMCRIT_EPS,20,0.03));

			firstimg = false;

		}
		else
		{

			cvCalcOpticalFlowPyrLK( prev_gray, gray, prev_pyramid, 
				pyramid, points[0], points[1], count, 
				cvSize(5,5), 3, status, 0, 
				cvTermCriteria(CV_TERMCRIT_ITER|CV_TERMCRIT_EPS,20,0.03), flags);

			
			if (!flags )
			{
				flags |= CV_LKFLOW_PYR_A_READY ;
			}

			

		}

		for( int i = 0; i < count; i++ )
		{
			if ( status [ i ] )
				cvCircle( *iter, cvPointFrom32f(points[1][i]), 3, CV_RGB(0,255,0), -1, 8,0);
		}

		prev_gray = gray;
		prev_pyramid = pyramid;

		for ( int i =  0 ; i < MAX_COUNT ; i ++ )
			points[0][i] = points[1][i];


		wnd.ShowImage ( *iter );
		cvWaitKey ( 0 );


	}

}
