// face.h

#pragma once

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Drawing;

namespace LibFaces {

    public ref class FaceDetectorInternal:public IDisposable
    {
        CvHaarClassifierCascade* cascade;
        String^ _classifierFileName;
    public:
        FaceDetectorInternal(String^ classifierFileName);
        ~FaceDetectorInternal();
        int CountFaces(String^ fileName);
        List<System::Drawing::Rectangle>^ FindFaces(String^ fileName);
        int CountFaces(Bitmap^ image);
        List<System::Drawing::Rectangle>^ FindFaces(Bitmap^ image);
    };
}
