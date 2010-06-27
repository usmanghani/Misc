struct ObjectTrackingData
{
    CvConnectedComp* comp;
    double angle;

};

class MHIObjectTracker
{

    // various tracking parameters (in seconds)
    const double MHI_DURATION ;
    const double MAX_TIME_DELTA ;
    const double MIN_TIME_DELTA ;
    // number of cyclic frame buffer used for motion detection
    // (should, probably, depend on FPS)
    const int N ;

    // ring image buffer
    IplImage **buf ;
    int last ;

    // temporary images
    IplImage *mhi ; // MHI
    IplImage *orient ; // orientation
    IplImage *mask ; // valid orientation mask
    IplImage *segmask ; // motion segmentation map
    CvMemStorage* storage ; // temporary storage



    // parameters:
    //  img - input video frame
    //  dst - resultant motion picture
    //  args - optional parameters
    vector<ObjectTrackingData> update_mhi( IplImage* img, IplImage* dst, int diff_threshold, CkalmanmfcDlg* parent, int frameno );

public:

    void doTracking(CvCapture* capture, CkalmanmfcDlg* parent);
    MHIObjectTracker ();

};
