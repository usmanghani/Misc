using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Security.Cryptography;

namespace encrypto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Rijndael r = Rijndael.Create();
        private void button1_Click(object sender, EventArgs e)
        {

            string text = File.ReadAllText(@"c:\atlog.txt");
            FileStream fs = new FileStream(@"c:\outenc.txt", FileMode.OpenOrCreate, FileAccess.Write);
            CryptoStream cs = new CryptoStream(fs, r.CreateEncryptor(r.Key, r.IV), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cs);
            writer.WriteLine(text);
            writer.Close();
            cs.Close();
            fs.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"c:\outenc.txt", FileMode.OpenOrCreate, FileAccess.Read);
            CryptoStream cs = new CryptoStream(fs, r.CreateDecryptor(r.Key, r.IV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            string text = sr.ReadLine();
            MessageBox.Show(text);
            sr.Close();
            cs.Close();
            fs.Close();

        }
    }
}