#include "MyCvImage.h"

void facedetect ( MyCvImage img, CvHaarClassifierCascade* cascade, CkalmanmfcDlg* parent );
void facedetectwithbgsep ( CvCapture* capture, CvHaarClassifierCascade* cascade, CkalmanmfcDlg* parent );
void facedetectwithoutbgsep ( CvCapture* capture, CvHaarClassifierCascade* cascade, CkalmanmfcDlg* parent );
void facedetectstill ( MyCvImage img, CvHaarClassifierCascade* cascade, CkalmanmfcDlg* parent );