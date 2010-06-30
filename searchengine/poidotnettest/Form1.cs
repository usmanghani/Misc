using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace poidotnettest
{
    using Worder = Poi.Net.HWPF;
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream docstream = new FileStream("c:\\test.doc", FileMode.Open, FileAccess.Read);
            Poi.Net.PoiFS.FileSystem.POIFSFileSystem filesystem = new Poi.Net.PoiFS.FileSystem.POIFSFileSystem(docstream);
            Worder.HWPFDocument doc = new Poi.Net.HWPF.HWPFDocument(filesystem);
            FileStream txtstream = new FileStream("c:\\testtext.txt", FileMode.OpenOrCreate, FileAccess.Write);
            doc.Write(txtstream);
            docstream.Close();
            txtstream.Close();
            
        }
    }
}