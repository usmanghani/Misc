using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;


namespace libfacestest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                Bitmap bmp = (Bitmap)Bitmap.FromStream(openFileDialog1.OpenFile());
                //pictureBox1.Image = bmp;
                Emgu.CV.HaarCascade cascade = new Emgu.CV.HaarCascade("haarcascade_frontalface_default.xml");
                Emgu.CV.HaarCascade eyeCascade = new Emgu.CV.HaarCascade("haarcascade_eye.xml");
                Emgu.CV.Image<Emgu.CV.Structure.Bgr, Byte> image = new Emgu.CV.Image<Emgu.CV.Structure.Bgr, Byte>(bmp);
                Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte> gray = image.Convert<Emgu.CV.Structure.Gray, Byte>();
                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
                Emgu.CV.Structure.MCvAvgComp[][] faces = gray.DetectHaarCascade(cascade,
                    1.1000, 1, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20,20));
                watch.Stop();
                float scalex = pictureBox1.Width / image.Width;
                float scaley = pictureBox1.Height / image.Height;

                //MessageBox.Show(string.Format("Found {0} Faces in {1} milliseconds", faces[0].Count().ToString(), watch.ElapsedMilliseconds.ToString()));
                foreach (var f in faces[0])
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(f.rect.ToString());
                    gray.ROI = f.rect;
                    Emgu.CV.Structure.MCvAvgComp[][] eyes = gray.DetectHaarCascade(eyeCascade,
                    1.1000, 1, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
                    gray.ROI = Rectangle.Empty;

                    if (eyes[0].Length < 2) continue;
                    sb.Append(" => ");
                    sb.Append(eyes[0].Length.ToString());
                    listBox1.Items.Add(sb.ToString());
                    //MessageBox.Show(eyes[0].Count().ToString());
                    Graphics g = Graphics.FromImage(bmp);
                    g.DrawRectangle(new Pen(Brushes.Red), f.rect.X,
                        f.rect.Y, f.rect.Width,
                        f.rect.Height);
                    foreach (var eye in eyes[0])
                    {
                        Rectangle r = new Rectangle(
                            eye.rect.X + f.rect.X,
                            eye.rect.Y + f.rect.Y,
                            eye.rect.Width, eye.rect.Height
                            );
                        g.DrawRectangle(new Pen(Brushes.Yellow), r);
                    }
                }

                pictureBox1.Image = bmp;
                //watch = System.Diagnostics.Stopwatch.StartNew();
                //openCV.Net.Haar.HaarClassifierCascade cascade2 = openCV.Net.Haar.HaarClassifierCascade.Parse(XDocument.Load("haarcascade_frontalface_default.xml"));
                //openCV.Net.Haar.Sequence<openCV.Net.Haar.AvgComp>
                //    seq = openCV.Net.FaceDetector.DetectFaces(bmp, cascade2);
                //watch.Stop();

                ////MessageBox.Show(string.Format("Found {0} Faces in {1} milliseconds", seq.Count.ToString(), watch.ElapsedMilliseconds.ToString()));
                //foreach (var a in seq)
                //{
                //    pictureBox1.CreateGraphics().DrawRectangle(new Pen(Brushes.Yellow), a.Rectangle.X * scalex,
                //        a.Rectangle.Y * scaley, a.Rectangle.Width * scalex,
                //        a.Rectangle.Height * scaley);
                //}

                //using (FaceDetector fd = new FaceDetector(LibFaces.ClassifierType.Front))
                //{
                //    var faces = fd.FindFaces(bmp);
                //    Graphics g = pictureBox1.CreateGraphics();
                //    g.DrawRectangles(new Pen(Brushes.Red), faces.ToArray());
                //}
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Bitmap bmp1 = Bitmap.FromStream(openFileDialog1.OpenFile()) as Bitmap;
            Bitmap bmp2 = new Bitmap(bmp1.Width / 3, bmp1.Height / 3);
            Graphics g = Graphics.FromImage(bmp2);
            GraphicsUnit unit = GraphicsUnit.Pixel;
            g.DrawImage(bmp1, Rectangle.Round(bmp2.GetBounds(ref unit)),
                new Rectangle(bmp1.Width / 3, bmp1.Height / 3, 2 * bmp1.Width / 3, 2 * bmp1.Height / 3),
                GraphicsUnit.Pixel);
            pictureBox1.Image = bmp1;
            pictureBox2.Image = bmp2;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo path = System.IO.Directory.GetParent(Assembly.GetExecutingAssembly().Location);

            DotFermion.LibFaces.FaceDetector detector = new DotFermion.LibFaces.FaceDetector(DotFermion.LibFaces.ClassifierType.Front, path);
            //MessageBox.Show(detector.ClassifierDir.ToString());
            //MessageBox.Show(detector.ClassifierType.ToString());
            openFileDialog1.ShowDialog();
            Bitmap image = Bitmap.FromStream(openFileDialog1.OpenFile()) as Bitmap;
            Graphics g = Graphics.FromImage(image);
            var faces = detector.FindFacesUnmanaged(image);
            foreach (var f in faces.Faces)
            {
                StringBuilder sb = new StringBuilder();
                if (f.Face != null)
                {
                    sb.Append(f.Face.ToString());
                    g.DrawRectangle(new Pen(Brushes.Red), f.Face);
                }
                if (f.Eyes != null && f.Eyes.Count > 0)
                {
                    sb.Append(" => ");
                    sb.Append(f.Eyes.Count.ToString());
                    g.DrawRectangles(new Pen(Brushes.Yellow), f.Eyes.ToArray());
                }
                listBox1.Items.Add(sb.ToString());
            }
            pictureBox1.Image = image;
        }
    }
}
