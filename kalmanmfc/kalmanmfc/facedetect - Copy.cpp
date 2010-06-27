#include "stdafx.h"

vector<RECT> facedetect (string imagefilename, string classifierfilename)
{
    CvHaarClassifierCascade* cascade = (CvHaarClassifierCascade*)cvLoad(classifierfilename.c_str());
    IplImage* image = cvLoadImage(imagefilename);
    CvMemStorage* storage = cvCreateMemStorage ( );
    vector<RECT> list;

    if( cascade && image && storage )
    {

        CvSeq* faces = cvHaarDetectObjects( img, cascade, storage, 1.1, 2, CV_HAAR_DO_CANNY_PRUNING, cvSize(40, 40) );

        for( int i = 0; i < (faces ? faces->total : 0); i++ )
        {

            CvRect* r = (CvRect*)cvGetSeqElem( faces, i );

            RECT rect ;
            rect.left = r->x;
            rect.right = r->x + r->width;
            rect.top = r->y;
            rect.bottom = r->y + r->height;

            list.push_back(rect);
        }

    }

    cvReleaseMemStorage(&storage);
    cvReleaseImage(&image);
    cvReleaseHaarClassifierCascade(&cascade);

    return 
}


