using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace DotFermion.Facer
{
    public partial class _Default : System.Web.UI.Page
    {
        public static DotFermion.LibFaces.FaceDetector FaceDetector = null;

        static _Default()
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(
                HttpContext.Current.Server.MapPath("Classifiers"));
            FaceDetector = new DotFermion.LibFaces.FaceDetector(DotFermion.LibFaces.ClassifierType.Front, dir);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString.HasKeys() && this.Request.QueryString["image"] != null)
            {
                object o = Application[this.Request.QueryString["image"]];
                if (o != null)
                {
                    Bitmap bmp = o as Bitmap;
                    System.IO.MemoryStream stream = new System.IO.MemoryStream();
                    bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Response.ContentType = "image/jpeg";
                    Response.BinaryWrite(stream.GetBuffer());
                    Response.Flush();
                    Response.End();
                }
            }

            if (this.IsPostBack)
            {
                if (FileUpload1.FileContent != null)
                {
                    Bitmap bmp = Bitmap.FromStream(FileUpload1.FileContent) as Bitmap;
                    if (FaceDetector != null)
                    {
                        var faces = FaceDetector.FindFacesUnmanaged(bmp);
                        FaceDetector.MarkFaces(bmp, faces);
                        string key = Guid.NewGuid().ToString();
                        Application.Add(key, bmp);
                        Image1.ImageUrl = string.Format("Default.aspx?image={0}", key);
                        //Image1.ImageUrl = HttpUtility.UrlEncode(string.Format("Default.aspx?image={0}", key));
                    }
                    //else
                    //{
                    //    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath("Classifiers"));
                    //    FaceDetector = new DotFermion.LibFaces.FaceDetector(DotFermion.LibFaces.ClassifierType.Front, dir);
                    //}
                }
            }
        }
    }
}
