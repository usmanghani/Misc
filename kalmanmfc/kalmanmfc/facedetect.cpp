#include "stdafx.h"
#include "MyCvImage.h"
#include "kalmanmfcdlg.h"
#include "CvDisplayWnd.h"

void facedetect ( MyCvImage img, CvHaarClassifierCascade* cascade, CkalmanmfcDlg* parent )
{
    //USES_CONVERSION;
    //stringstream str;
    //str << ( int ) cascade;
    //parent->LogString ( A2W(str.str () .c_str ()) );

    if( cascade )
    {

        int scale = 1;
        CvPoint pt1;
        CvPoint pt2;
        //CvPoint pt3;
        //CvPoint pt4;
        //CvPoint pt5;
        //CvPoint pt6;
        //CvPoint mid_pt;
        //CvPoint pt7;
        //CvPoint center1;
        //CvPoint center2;
        //CvPoint pt8;
        //CvPoint pt9;

        int i;
        //int radius1;
        //int radius2;

        CvMemStorage* storage = cvCreateMemStorage ( );

        CvSeq* faces = cvHaarDetectObjects( img, cascade, storage, 1.1, 2, CV_HAAR_DO_CANNY_PRUNING, cvSize(40, 40) );

        for( i = 0; i < (faces ? faces->total : 0); i++ )
        {

            CvRect* r = (CvRect*)cvGetSeqElem( faces, i );

            pt1.x = r->x*scale;
            pt2.x = (r->x+r->width)*scale;
            pt1.y = r->y*scale;
            pt2.y = (r->y+r->height)*scale;

            cvRectangle( img, pt1, pt2, CV_RGB(0,255,0), 3, 8, 0 );

            //pt3.x = (pt1.x+pt2.x)/2;
            //pt4.x = (pt1.x+pt2.x)/2;
            //pt3.y = pt1.y;
            //pt4.y = pt2.y;
            //pt5.y = (pt1.y+pt2.y)/2;
            //pt6.y = (pt1.y+pt2.y)/2;
            //pt5.x = pt1.x;
            //pt6.x = pt2.x;

            //mid_pt.x = (pt1.x+pt2.x)/2;
            //mid_pt.y = (pt1.y+pt2.y)/2;

            //pt7.x = pt2.x;
            //pt7.y = pt1.y;

            //center1.x = (pt1.x + 2*mid_pt.x)/3;
            //center1.y = (pt1.y + 2*mid_pt.y)/3;
            //radius1 = (center1.x - pt1.x)/2;

            //cvCircle( img,center1,radius1,CV_RGB(255,0,0),1,8,0);

            //center2.x = (pt7.x + 2*mid_pt.x)/3;
            //center2.y = (pt7.y + 2*mid_pt.y)/3;
            //radius2 = (pt7.x - center2.x )/2;

            //cvCircle( img,center2,radius2,CV_RGB(255,0,0),1,8,0);

            //pt8.x = mid_pt.x - (pt2.x - pt1.x) * 0.25 ;
            //pt9.x = mid_pt.x + (pt2.x - pt1.x) * 0.25 ;
            //pt8.y = (5.5*mid_pt.y + 4.5*pt4.y)/10;
            //pt9.y = (mid_pt.y+9*pt4.y)/10;

            //cvRectangle( img, pt8, pt9, CV_RGB(0,255,255), 1, 8, 0 );

        }

        cvShowImage( "Face Detection", img );
        cvClearMemStorage ( storage );
    }

}

void facedetectwithbgsep ( CvCapture* capture, CvHaarClassifierCascade* cascade, CkalmanmfcDlg* parent )
{

    parent->ClearLog ( );
    MyCvImage tmp_frame;
    MyCvImage copy_frame; 
    MyCvImage temp1, temp2;
    cvNamedWindow("Face Detection", 0);
    CvBGStatModel* bg_model = NULL;


    for( ;; )
    {

        if ( !cvGrabFrame ( capture ) ) break;
        tmp_frame = cvRetrieveFrame ( capture );

        MyCvImage flipped = cvCreateImage ( cvGetSize ( tmp_frame ), tmp_frame->depth, tmp_frame->nChannels );
        if ( tmp_frame->origin == IPL_ORIGIN_BL )
            cvFlip ( tmp_frame, flipped );
        else
            cvCopy ( tmp_frame, flipped );

        if ( !bg_model )  
            bg_model = cvCreateFGDStatModel( flipped );


        cvUpdateBGStatModel( flipped, bg_model );
        temp1 = bg_model->background;
        cvZero ( temp1 );
        temp2 = bg_model->background;
        cvZero ( temp2 );
        cvCvtPlaneToPix ( bg_model->foreground, bg_model->foreground, bg_model->foreground, 0, temp1 );
        cvAnd ( flipped, temp1, temp2 );
        facedetect ( temp2, cascade, parent );
        if( cvWaitKey ( 1 ) == 27 ) break;

    }

    cvReleaseBGStatModel( &bg_model );
    cvDestroyAllWindows ( );


}

void facedetectwithoutbgsep ( CvCapture* capture, CvHaarClassifierCascade* cascade, CkalmanmfcDlg* parent )
{
    parent->ClearLog ( );

    MyCvImage tmp_frame;
    cvNamedWindow("Face Detection", 0);
    for( ;; )
    {

        if ( !cvGrabFrame ( capture ) ) break;
        tmp_frame = cvRetrieveFrame ( capture );
        MyCvImage flipped = cvCreateImage ( cvGetSize ( tmp_frame ), tmp_frame->depth, tmp_frame->nChannels );
        if ( tmp_frame->origin == IPL_ORIGIN_BL )
            cvFlip ( tmp_frame, flipped );
        else
            cvCopy ( tmp_frame, flipped );

        facedetect ( flipped, cascade, parent );
        if( cvWaitKey ( 1 ) == 27 ) break;

    }
    cvDestroyAllWindows ( );

}

void facedetectstill ( MyCvImage img, CvHaarClassifierCascade* cascade, CkalmanmfcDlg* parent )
{
    CvDisplayWnd wnd( "Face Detection");
    facedetect ( img, cascade, parent );
    cvWaitKey ( );

}
