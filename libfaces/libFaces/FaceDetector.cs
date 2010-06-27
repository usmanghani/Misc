using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;

namespace DotFermion.LibFaces
{
    //OpenCV Haar
    using Emgu.CV;
    using Emgu.CV.Structure;
    using Emgu.Util;

    //Managed Haar
    using openCV.Net;
    using openCV.Net.Haar;
    using System.Xml.Linq;

    public enum ClassifierType
    {
        Eyes,
        Front,
        ProfileFace,
        UpperBody,
        LowerBody,
        FullBody,
    }

    public struct CountFacesResponse
    {
        public int TotalFaces;
        public int FacesWithEyes;
    }

    public struct FaceStructure
    {
        public Rectangle Face;
        /// <summary>
        /// Rectangles for the eyes. The locations for these rectangles
        /// are relative to the image and not the face rectangle i.e. Directly drawing eyes
        /// will result in their being drawn at the correct place.
        /// </summary>
        public List<Rectangle> Eyes;
    }

    public struct FindFacesResponse
    {
        public int TotalFaces;
        public int FacesWithEyes;
        public List<FaceStructure> Faces;
    }

    public class FaceDetector:IDisposable
    {
        HaarCascade unmanagedHaarFace = null;
        HaarCascade unmanagedHaarEyes = null;
        HaarClassifierCascade managedHaarFace = null;
        HaarClassifierCascade managedHaarEyes = null;

        private ClassifierType classifierType = ClassifierType.Front;
        public ClassifierType ClassifierType
        {
            get { return classifierType; }
            set { InitClassifiers(value, this.ClassifierDir); }
        }

        public System.IO.DirectoryInfo ClassifierDir { get; set; }

        public FaceDetector(ClassifierType classifierType, System.IO.DirectoryInfo classifierDir)
        {
            this.classifierType = classifierType;
            this.ClassifierDir = classifierDir;
            InitClassifiers(classifierType, classifierDir);
        }

        public CountFacesResponse CountFacesUnmanaged(Bitmap bitmap)
        {
            CountFacesResponse response = new CountFacesResponse();
            Image<Bgr, Byte> image = new Image<Bgr, Byte>(bitmap);
            Image<Gray, Byte> gray = image.Convert<Gray,byte>();
            MCvAvgComp[][] faces = gray.DetectHaarCascade(this.unmanagedHaarFace, 1.1, 3, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DEFAULT, new System.Drawing.Size(20,20));

            response.TotalFaces = faces[0].Length;
            response.FacesWithEyes = 0;

            foreach (var f in faces[0])
            {
                gray.ROI = f.rect;
                MCvAvgComp[][] eyes = gray.DetectHaarCascade(this.unmanagedHaarEyes, 1.1, 3, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DEFAULT, new System.Drawing.Size(20,20));
                gray.ROI = Rectangle.Empty;
                if (eyes[0].Length > 0)
                    response.FacesWithEyes++;
            }

            return response;
        }

        public CountFacesResponse CountFacesManaged(Bitmap bitmap)
        {
            CountFacesResponse response = new CountFacesResponse();
            var seq = openCV.Net.FaceDetector.DetectFaces(bitmap, managedHaarFace, new Size(20, 20), 1.1);
            response.TotalFaces = seq.Count;
            response.FacesWithEyes = 0;

            foreach (var f in seq)
            {
                Bitmap eyeImage = ExtractFaceImage(bitmap, f);
                var eyeSeq = openCV.Net.FaceDetector.DetectFaces(eyeImage, managedHaarEyes);
                int ignore = eyeSeq.Count > 0 ? ++response.FacesWithEyes : 0;
            }

            return response;
        }

        public FindFacesResponse FindFacesUnmanaged(Bitmap bitmap)
        {
            FindFacesResponse response = new FindFacesResponse();

            Image<Bgr, Byte> image = new Image<Bgr, Byte>(bitmap);
            Image<Gray, Byte> gray = image.Convert<Gray, byte>();
            MCvAvgComp[][] faces = gray.DetectHaarCascade(this.unmanagedHaarFace, 1.1, 1, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new System.Drawing.Size(20, 20));

            response.TotalFaces = faces[0].Length;
            response.FacesWithEyes = 0;
            response.Faces = new List<FaceStructure>();

            foreach (var f in faces[0])
            {
                FaceStructure face = new FaceStructure();
                face.Face = f.rect;
                face.Eyes = new List<Rectangle>();

                gray.ROI = f.rect;
                MCvAvgComp[][] eyes = gray.DetectHaarCascade(this.unmanagedHaarEyes, 1.1, 1, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new System.Drawing.Size(20, 20));
                gray.ROI = Rectangle.Empty;

                int ignore = eyes[0].Length > 0 ? ++ response.FacesWithEyes : 0;
                foreach (var eye in eyes[0])
                {
                    Rectangle eyeRect = new Rectangle();
                    eyeRect.X = f.rect.X + eye.rect.X;
                    eyeRect.Y = f.rect.Y + eye.rect.Y;
                    eyeRect.Width = eye.rect.Width;
                    eyeRect.Height = eye.rect.Height;
                    //eye.rect.Offset(f.rect.X, f.rect.Y);
                    face.Eyes.Add(eyeRect);
                }

                response.Faces.Add(face);
            }

            return response;
        }

        public FindFacesResponse FindFacesManaged(Bitmap bitmap)
        {
            FindFacesResponse response = new FindFacesResponse();
            var seq = openCV.Net.FaceDetector.DetectFaces(bitmap, managedHaarFace, new Size(20, 20), 1.1);
            response.TotalFaces = seq.Count;
            response.FacesWithEyes = 0;
            response.Faces = new List<FaceStructure>();

            foreach (var f in seq)
            {
                FaceStructure face = new FaceStructure();
                face.Face = f.Rectangle;
                face.Eyes = new List<Rectangle>();

                Bitmap eyeImage = ExtractFaceImage(bitmap, f);
                var eyeSeq = openCV.Net.FaceDetector.DetectFaces(eyeImage, managedHaarEyes);
                int ignore = eyeSeq.Count > 0 ? ++response.FacesWithEyes : 0;

                foreach (var eye in eyeSeq)
                {
                    Rectangle eyeRect = new Rectangle();
                    eyeRect.X = f.Rectangle.X + eye.Rectangle.X;
                    eyeRect.Y = f.Rectangle.Y + eye.Rectangle.Y;
                    eyeRect.Width = eye.Rectangle.Width;
                    eyeRect.Height = eye.Rectangle.Height;
                    face.Eyes.Add(eyeRect);
                }

                response.Faces.Add(face);
            }

            return response;

        }

        public void MarkFaces(Bitmap bitmap, FindFacesResponse faces)
        {
            Graphics g = Graphics.FromImage(bitmap);
            if (faces.Faces != null)
            {
                foreach (var f in faces.Faces)
                {
                    g.DrawRectangle(new Pen(Brushes.Red), f.Face);
                    if (f.Eyes.Count > 0)
                    {
                        g.DrawRectangles(new Pen(Brushes.Yellow), f.Eyes.ToArray());
                    }
                }
            }
            g.Dispose();
        }

        public void Dispose()
        {
            unmanagedHaarEyes.Dispose();
            unmanagedHaarFace.Dispose();
        }

        private void InitClassifiers(ClassifierType classifierType, System.IO.DirectoryInfo classifierDir)
        {
            string faceClassifierFileName = GetFileNameFromClassifierType(classifierType);
            string eyesClassifierFileName = GetFileNameFromClassifierType(ClassifierType.Eyes);
            managedHaarFace = HaarClassifierCascade.Parse(XDocument.Load(faceClassifierFileName));
            managedHaarEyes = HaarClassifierCascade.Parse(XDocument.Load(eyesClassifierFileName));
            unmanagedHaarFace = new HaarCascade(faceClassifierFileName);
            unmanagedHaarEyes = new HaarCascade(eyesClassifierFileName);
        }

        private string GetFileNameFromClassifierType(ClassifierType classifierType)
        {
            string classifierFileName = string.Empty;
            switch (this.ClassifierType)
            {
                case ClassifierType.Eyes:
                    classifierFileName = "haarcascade_eye.xml";
                    break;
                case ClassifierType.Front:
                    classifierFileName = "haarcascade_frontalface_default.xml";
                    break;
                case ClassifierType.ProfileFace:
                    classifierFileName = "haarcascade_profileface.xml";
                    break;
                case ClassifierType.FullBody:
                    classifierFileName = "haarcascade_fullbody.xml";
                    break;
                case ClassifierType.UpperBody:
                    classifierFileName = "haarcascade_upperbody.xml";
                    break;
                case ClassifierType.LowerBody:
                    classifierFileName = "haarcascade_lowerbody.xml";
                    break;
            }
            classifierFileName = System.IO.Path.Combine(this.ClassifierDir.FullName, classifierFileName);
            return classifierFileName;
        }

        private Bitmap ExtractFaceImage(Bitmap bitmap, AvgComp f)
        {
            Bitmap eyeImage = new Bitmap(f.Rectangle.Width, f.Rectangle.Height);
            Graphics g = Graphics.FromImage(eyeImage);
            GraphicsUnit unit = GraphicsUnit.Pixel;
            g.DrawImage(bitmap,
                Rectangle.Round(eyeImage.GetBounds(ref unit)),
                f.Rectangle,
                GraphicsUnit.Pixel);
            return eyeImage;
        }

    }
}
