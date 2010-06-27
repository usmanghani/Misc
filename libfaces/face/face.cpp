// This is the main DLL file.

#include "stdafx.h"
#include "facedetect.h"
#include "face.h"

namespace LibFaces
{
    void StringToCharPointer(String^ string, char* output, int size)
    {
        pin_ptr<const __wchar_t> p = PtrToStringChars(string);
        wcstombs_s(NULL, output, size, p, size);
    }

    vector<RECT> GetFaces(String^ fileName, CvHaarClassifierCascade* cascade)
    {
        char imageFileNameBuffer[MAX_PATH + 1];
        StringToCharPointer(fileName, imageFileNameBuffer, MAX_PATH);
        IplImage* image = cvLoadImage(imageFileNameBuffer);

        vector<RECT> faces = facedetect(image, cascade);

        cvReleaseImage(&image);

        return faces;
    }

    IplImage* BitmapToIplImage(Bitmap^ bitmap)
    {
        IplImage* iplImage = cvCreateImage(cvSize(bitmap->Width, bitmap->Height), IPL_DEPTH_32F, 3);
        System::Drawing::Imaging::BitmapData^ bData = bitmap->LockBits(System::Drawing::Rectangle(0, 0, bitmap->Width, bitmap->Height),
            System::Drawing::Imaging::ImageLockMode::ReadWrite,
            System::Drawing::Imaging::PixelFormat::Format24bppRgb);
        pin_ptr<char> bits = reinterpret_cast<char*>(bData->Scan0.ToPointer());
        iplImage->imageData = (char*)bits;
        IplImage* convertedImage = cvCreateImage(cvSize(bitmap->Width, bitmap->Height), IPL_DEPTH_8U, 3);
        cvConvertImage(iplImage, convertedImage);
        bitmap->UnlockBits(bData);
        return convertedImage;
    }

    FaceDetectorInternal::FaceDetectorInternal(String^ classifierFileName)
    {
        this->_classifierFileName = classifierFileName;

        char classifierFileNameBuffer[MAX_PATH + 1];
        StringToCharPointer(classifierFileName, classifierFileNameBuffer, MAX_PATH);
        this->cascade = (CvHaarClassifierCascade*)cvLoad(classifierFileNameBuffer);
    }

    FaceDetectorInternal::~FaceDetectorInternal()
    {
        if(this->cascade)
        {
            CvHaarClassifierCascade* ptr = cascade;
            cvReleaseHaarClassifierCascade(&ptr);
        }
    }

    int FaceDetectorInternal::CountFaces(String^ fileName)
    {
        vector<RECT> faces = GetFaces(fileName, this->cascade);
        return faces.size();
    }

    int FaceDetectorInternal::CountFaces(System::Drawing::Bitmap^ image)
    {
        IplImage* iplImage = BitmapToIplImage(image);
        vector<RECT> faces = facedetect(iplImage, this->cascade);
        cvReleaseImage(&iplImage);
        return faces.size();
    }

    List<System::Drawing::Rectangle>^ FaceDetectorInternal::FindFaces(String^ fileName)
    {
        vector<RECT> faceVector = GetFaces(fileName, this->cascade);
        List<System::Drawing::Rectangle>^ faces = gcnew List<System::Drawing::Rectangle>();
        for each ( RECT r in faceVector )
        {
            System::Drawing::Rectangle rect(r.left, r.top, r.right - r.left, r.bottom - r.top);
            faces->Add(rect);
        }
        return faces;
    }
    
    List<System::Drawing::Rectangle>^ FaceDetectorInternal::FindFaces(System::Drawing::Bitmap ^image)
    {
        IplImage* iplImage = BitmapToIplImage(image);
        vector<RECT> faces = facedetect(iplImage, this->cascade);
        cvReleaseImage(&iplImage);
        List<System::Drawing::Rectangle>^ faceList = gcnew List<System::Drawing::Rectangle>();
        for each ( RECT r in faces )
        {
            System::Drawing::Rectangle rect(r.left, r.top, r.right - r.left, r.bottom - r.top);
            faceList->Add(rect);
        }
        return faceList;
    }
}