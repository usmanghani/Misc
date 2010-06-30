using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DotFermion;

namespace testkanzul
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SearchResult[] results = null;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty) return;

            Searcher searcher = new Searcher("c:\\kanzuliman");

            results = searcher.FastSearch(textBox1.Text);

            listBox1.Items.Clear();

            label1.Text = results.Length.ToString() + " hit(s).";

            foreach (SearchResult result in results)
            {
                string para = result.GetDocProperty("pid");
                string sura = result.GetDocProperty("sid");
                string ayah = result.GetDocProperty("ayatno");

                string temp = "Para: " + para + ", Sura: " + sura + ", Ayah: " + ayah;

                listBox1.Items.Add(temp);
                
                
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            if (results == null) return;


            SearchResult result = results[listBox1.SelectedIndex];
            
            //Utils.GetFastSearchResultFragments(ref result);
            //string frags = Utils.FormatSearchResultAsHtml(result);
            //string path = System.IO.Path.Combine(Application.StartupPath, "results.html");
            //System.IO.File.WriteAllText(path, frags);

            string path = System.IO.Path.Combine(Application.StartupPath, "contents.html");
            string contents = Utils.GetOriginalHighlightedContents(result);
            System.IO.File.WriteAllText(path, contents);

            webBrowser1.Url = new Uri("file://" + path);
            webBrowser1.Document.Encoding = "utf-8";


        }
    }
}