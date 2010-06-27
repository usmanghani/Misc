#include "stdafx.h"
#include "kalmantest.h"
#include "MyCvImage.h"
#include "CvDisplayWnd.h"
#include "kalmanmfcdlg.h"
#include "drawhist.h"
#include "MHIObjectTracker.h"


vector<ObjectTrackingData> MHIObjectTracker::update_mhi( IplImage* img, IplImage* dst, int diff_threshold, CkalmanmfcDlg* parent, int frameno )
{
	USES_CONVERSION;
	double timestamp = (double)clock()/CLOCKS_PER_SEC; // get current time in seconds
	CvSize size = cvSize(img->width,img->height); // get current frame size
	int i, idx1 = last, idx2;
	IplImage* silh;
	CvSeq* seq;
	CvRect comp_rect;
	double count;
	double angle;
	CvPoint center;
	double magnitude;          
	CvScalar color;
	vector<ObjectTrackingData> objects;

	// allocate images at the beginning or
	// reallocate them if the frame size is changed
	if( !mhi || mhi->width != size.width || mhi->height != size.height ) 
	{
		if( buf == 0 ) 
		{
			buf = (IplImage**)malloc(N*sizeof(buf[0]));
			memset( buf, 0, N*sizeof(buf[0]));
		}

		for( i = 0; i < N; i++ ) 
		{
			cvReleaseImage( &buf[i] );
			buf[i] = cvCreateImage( size, IPL_DEPTH_8U, 1 );
			cvZero( buf[i] );
		}
		cvReleaseImage( &mhi );
		cvReleaseImage( &orient );
		cvReleaseImage( &segmask );
		cvReleaseImage( &mask );

		mhi = cvCreateImage( size, IPL_DEPTH_32F, 1 );
		cvZero( mhi ); // clear MHI at the beginning
		orient = cvCreateImage( size, IPL_DEPTH_32F, 1 );
		segmask = cvCreateImage( size, IPL_DEPTH_32F, 1 );
		mask = cvCreateImage( size, IPL_DEPTH_8U, 1 );
	}

	cvCvtColor( img, buf[last], CV_BGR2GRAY ); // convert frame to grayscale

	idx2 = (last + 1) % N; // index of (last - (N-1))th frame
	last = idx2;

	silh = buf[idx2];
	cvAbsDiff( buf[idx1], buf[idx2], silh ); // get difference between frames

	cvThreshold( silh, silh, diff_threshold, 1, CV_THRESH_BINARY ); // and threshold it
	cvUpdateMotionHistory( silh, mhi, timestamp, MHI_DURATION ); // update MHI

	// convert MHI to blue 8u image
	cvCvtScale( mhi, mask, 255./MHI_DURATION, (MHI_DURATION - timestamp)*255./MHI_DURATION );
	cvZero( dst );
	cvCvtPlaneToPix( mask, 0, 0, 0, dst );

	// calculate motion gradient orientation and valid orientation mask
	cvCalcMotionGradient( mhi, mask, orient, MAX_TIME_DELTA, MIN_TIME_DELTA, 3 );

	if( !storage )
		storage = cvCreateMemStorage(0);
	else
		cvClearMemStorage(storage);

	// segment motion: get sequence of motion components
	// segmask is marked motion components map. It is not used further
	seq = cvSegmentMotion( mhi, segmask, storage, timestamp, MAX_TIME_DELTA );

	// iterate through the motion components,
	// One more iteration (i == -1) corresponds to the whole image (global motion)
	stringstream tempstr;
	tempstr << "Total " << seq->total << " objects in frame " << frameno << ".";
	parent->LogString  ( A2W ( tempstr.str().c_str() ) );
	for( i = 0; i < seq->total; i++ ) 
	{

		if( i < 0 ) 
		{ // case of the whole image
			comp_rect = cvRect( 0, 0, size.width, size.height );
			color = CV_RGB(255,255,255);
			magnitude = 100;
		}
		else 
		{ // i-th motion component

			comp_rect = ((CvConnectedComp*)cvGetSeqElem( seq, i ))->rect;

			stringstream str;
			str << i + 1 << "th object value : " << 
				((CvConnectedComp*)cvGetSeqElem ( seq, i ))->value.val[0];
			str << " ROI : x = " << comp_rect.x << 
				", y = " << comp_rect.y << 
				", width = " << comp_rect.width << 
				", height = " << comp_rect.height;

			parent->LogString ( A2W(str.str().c_str()) );

			if( comp_rect.width + comp_rect.height < 50 ) // reject very small components
				continue;
			color = CV_RGB(255,0,0);
			magnitude = 30;

		}

		// select component ROI
		cvSetImageROI( silh, comp_rect );
		cvSetImageROI( mhi, comp_rect );
		cvSetImageROI( orient, comp_rect );
		cvSetImageROI( mask, comp_rect );

		// calculate orientation
		angle = cvCalcGlobalOrientation( orient, mask, mhi, timestamp, MHI_DURATION);
		angle = 360.0 - angle;  // adjust for images with top-left origin

		if ( i >= 0 )
		{

			ObjectTrackingData tracker = { (CvConnectedComp*)cvGetSeqElem ( seq, i ), angle };
			objects.push_back ( tracker );
		}


		count = cvNorm( silh, 0, CV_L1, 0 ); // calculate number of points within silhouette ROI

		cvResetImageROI( mhi );
		cvResetImageROI( orient );
		cvResetImageROI( mask );
		cvResetImageROI( silh );

		// check for the case of little motion
		if( count < comp_rect.width*comp_rect.height * 0.01 )
			continue;

		// draw a clock with arrow indicating the direction
		center = cvPoint( (comp_rect.x + comp_rect.width/2),
			(comp_rect.y + comp_rect.height/2) );

		cvCircle( dst, center, cvRound(magnitude*1.2), color, 1, CV_AA, 0 );
		cvLine( dst, center, cvPoint( cvRound( center.x + magnitude*cos(angle*CV_PI/180)),
			cvRound( center.y - magnitude*sin(angle*CV_PI/180))), color, 1, CV_AA, 0 );

	}

	return objects;

}


void MHIObjectTracker::doTracking(CvCapture* capture, CkalmanmfcDlg* parent)
{

	parent->ClearLog  ( );
	USES_CONVERSION;

	IplImage* motion = 0;

	if( capture )
	{
		MyCvImage tmp_frame = cvQueryFrame(capture);
		if ( !tmp_frame.image )
		{
			MessageBox ( NULL, _T("BAAAD VIDEO !!"), _T("Kalman Tracker"), 0 );
			return;

		}

		const float A[] = { 
			1, 1, 0, 0, 0, 0,
			0, 1, 0, 0, 0, 0,
			0, 0, 1, 1, 0, 0,
			0, 0, 0, 1, 0, 0,
			0, 0, 0, 0, 1, 1,
			0, 0, 0, 0, 0, 1 
		};

		MyCvImage kalmanimg = tmp_frame;//cvCreateImage( cvGetSize ( tmp_frame ), 8, 3 );
		cvZero ( kalmanimg );
		CvKalman* kalman = cvCreateKalman( 6, 3, 0 );
		/* state is (phi, delta_phi, x, delta_x, y, delta_y) - angle and angle increment */
		//CvMat* state = cvCreateMat( 2, 1, CV_32FC1 );
		CvMat* process_noise = cvCreateMat( 6, 1, CV_32FC1 );
		/* only phi (angle, x, y) is measured */
		CvMat* measurement = cvCreateMat( 3, 1, CV_32FC1 );
		CvRandState rng;
		int code = -1;

		cvRandInit( &rng, 0, 1, -1, CV_RAND_UNI );

		cvZero( measurement );
		cvNamedWindow( "Motion Tracking", 0 );

		fstream datafile( "datafile.txt", ios_base::in | ios_base::out | ios_base::trunc );
		vector<int> ptsx;
		vector<int> ptsy;

		cvRandSetRange( &rng, 0, 0.1, 0 );
		rng.disttype = CV_RAND_NORMAL;

		//cvRand( &rng, state );
		memcpy( kalman->transition_matrix->data.fl, A, sizeof(A));
		cvSetIdentity( kalman->measurement_matrix, cvRealScalar(1) );
		cvSetIdentity( kalman->process_noise_cov, cvRealScalar(1e-5) );
		cvSetIdentity( kalman->measurement_noise_cov, cvRealScalar(0.1) );
		cvSetIdentity( kalman->error_cov_post, cvRealScalar(1));
		/* choose random initial state */
		cvRand( &rng, kalman->state_post );

		rng.disttype = CV_RAND_NORMAL;

		cvZero ( kalmanimg ) ;
		//DrawLegend ( kalmanimg );
		ptsx.clear ( );
		ptsy.clear ();

		parent->ClearLog ();



		CvBGStatModel* bg_model = cvCreateFGDStatModel( tmp_frame );

		cvNamedWindow( "Motion History & Orientation", 0 );
		CvDisplayWnd originalvidwnd("Original Video");
		cvNamedWindow ( "Foreground", 0 );

		MyCvImage tmp1, tmp2;
		// the simulation begins here.
		for(int fr = 1 ; ;  fr ++)
		{


			IplImage* image;
			if( !cvGrabFrame( capture ))
				break;
			image = cvRetrieveFrame( capture );

			if( image )
			{
				if( !motion )
				{
					motion = cvCreateImage( cvSize(image->width,image->height), 8, 3 );
					cvZero( motion );
					motion->origin = image->origin;
				}
			}

			double t = (double)cvGetTickCount();

			MyCvImage copy_frame = image;

			cvUpdateBGStatModel( copy_frame, bg_model );

			t = (double)cvGetTickCount() - t;

			tmp1 = bg_model->background ;
			tmp2 = bg_model->background;
			cvCvtPlaneToPix ( bg_model->foreground, bg_model->foreground, bg_model->foreground, 0, tmp1 );
			cvAnd ( copy_frame, tmp1, tmp2 );

			MyCvImage morphed_frame = bg_model->foreground;

			cvErode(morphed_frame, morphed_frame, 0, 3);

			cvDilate(morphed_frame, morphed_frame, 0, 3);

			MyCvImage img = cvCloneImage ( image );

			cvCvtColor ( morphed_frame, img, CV_GRAY2BGR );

			//Update the motion history image and segment motion components.
			//returns the motion direction of each component.
			vector<ObjectTrackingData> objects = update_mhi( img, motion, 30, parent, fr );

			if ( objects.size ( ) > 0 )
			{
				/* predict point position */
				const CvMat* prediction = cvKalmanPredict( kalman, 0 );
				float predict_angle = CV_MAT_ELEM ( (*prediction), float, 0, 0 );//prediction->data.fl[0];
				CvPoint predict_pt = cvPoint (
					CV_MAT_ELEM ( (*prediction), float, 1, 0 ), 
					CV_MAT_ELEM ( (*prediction), float, 2, 0 )
					);


				//measurement->data.fl[0] = (float)objects[0].angle;
				CV_MAT_ELEM ( (*measurement), float, 0, 0 ) = ( float )objects[0].angle;

				float measurement_angle;
				measurement_angle = measurement->data.fl[0];
				CvRect rect = objects[0].comp->rect;
				CvPoint measurement_pt = cvPoint ( rect.x + rect.width / 2, rect.y + rect.height / 2 );

				CV_MAT_ELEM ( (*measurement), float, 1, 0 ) = ( float )measurement_pt.x;
				CV_MAT_ELEM ( (*measurement), float, 2, 0 ) = ( float ) measurement_pt.y;

				draw_cross(kalmanimg, measurement_pt, CV_RGB(255,0,0), 3, 1 );
				//draw_cross(kalmanimg, predict_pt, CV_RGB(0,255,0), 3, 1 );

				//cvLine( kalmanimg, measurement_pt, predict_pt, CV_RGB(255,255,0), 1, 0 );

				/* adjust Kalman filter state */
				cvKalmanCorrect( kalman, measurement );

				int deltax = abs(predict_pt.x - measurement_pt.x) ;
				int deltay = abs(predict_pt.y - measurement_pt.y) ;

				if ( deltax > 50 || deltay > 50 ) 
				{
					//ShowNotice ( kalmanimg );

					cvShowImage( "Motion Tracking", kalmanimg );
					//MessageBeep ( MB_ICONASTERISK );
					//cvWaitKey  ( 500 );
					//EraseNotice ( kalmanimg );	
					//cvShowImage ( "Motion Tracking", kalmanimg );
					char timetemp [ 80 ] = { 0 };
					char datetemp [ 80 ] = { 0 };
					_strtime_s ( timetemp, 80 );
					_strdate_s ( datetemp, 80 );
					stringstream log;
					log << "Deviation at : (" << measurement_pt.x << ", " << measurement_pt.y << "). DeltaX = " << deltax << ", DeltaY = " << deltay << ", " << timetemp << ", " << datetemp << endl;
					parent->LogString ( A2W(log.str().c_str() ) ) ;
					datafile << "Deviation at : (" << measurement_pt.x << ", " << measurement_pt.y << "). DeltaX = " << deltax << ", DeltaY = " << deltay << ", " << timetemp << ", " << datetemp << endl;
					ptsx.push_back ( deltax );
					ptsy.push_back ( deltay );

				} 
				else
				{
					cvShowImage( "Motion Tracking", kalmanimg );
				}
			}
			originalvidwnd.ShowImage ( copy_frame );
			cvShowImage( "Motion History & Orientation", motion );
			cvShowImage ( "Foreground", tmp2 );

			copy_frame.Release();
			morphed_frame.Release();

			code = cvWaitKey( 50 );

			if( code > 0 ) /* break current simulation by pressing a key */
			{

				int resp = IDNO; //MessageBox ( NULL, _T("Beginning new simulation !! All data related to the previous simulation will be erased !! \n Do you want to save it now ?") , _T("Kalman"), MB_YESNOCANCEL | MB_ICONWARNING );
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


					cvZero ( kalmanimg );
					//DrawLegend ( kalmanimg );
					//DrawHistogram ( ptsx, ptsy );
					break;

				}
				else if ( resp == IDNO )
				{
					datafile.close () ;
					datafile.open ( "datafile.txt", ios_base::in | ios_base::out | ios_base::trunc );
					cvZero ( kalmanimg );
					//DrawLegend ( kalmanimg );
					//DrawHistogram ( ptsx, ptsy );
					break;

				}

				else if ( resp == IDCANCEL )
				{

				}


			}

			/*if( cvWaitKey(50) >= 0 )
			break;*/

		}
		cvReleaseBGStatModel( &bg_model );
		cvDestroyAllWindows ( );

	}

}

MHIObjectTracker::MHIObjectTracker ( ):
	MHI_DURATION(1),
	MAX_TIME_DELTA(0.5),
	MIN_TIME_DELTA(0.05),
	N(4),
	buf(0),
	last(0),
	mhi(0),
	orient(0),
	mask(0),
	segmask(0),
	storage(0)
{}
