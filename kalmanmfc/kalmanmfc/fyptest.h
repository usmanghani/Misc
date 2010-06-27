
CvRect FindObject ( MyCvImage src, MyCvImage patt, double threshold );

void FindObjectStill ( MyCvImage src, MyCvImage patt, double threshold );

void FindObjectInVideo ( CvCapture* capture, MyCvImage target, double threshold );

MyCvImage FindObjectUI ( MyCvImage src );

MyCvImage FindObjectInVideoUI ( CvCapture* capture );

CvRect FindObjectWithPyramiding ( MyCvImage src, MyCvImage patt, double threshold/*[0,1]*/, int levels );

bool DetectSceneChange ( MyCvImage im1, MyCvImage im2, double threshold1, /*[0,1]*/double threshold2, int blksize );

MyCvImage RotateImage ( MyCvImage im, int angle );

vector<MyCvImage> GetExpandedImages (MyCvImage image, int levels );

vector<MyCvImage> GetReducedImages (MyCvImage image, int levels );