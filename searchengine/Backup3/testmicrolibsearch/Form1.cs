using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace testmicrolibsearch
{
    using DotFermion;
    public partial class Form1 : Form
    {

        string _indexTarget = string.Empty;

        SearchResult[] results = null;
        Searcher isearcher = null;

        public Form1()
        {
            InitializeComponent();
        }
        private void UpdateIndexingInfo(string info)
        {
            listBox2.Items.Add(info);
            Application.DoEvents();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isearcher != null) isearcher.Close();
            isearcher = null;

            folderBrowserDialog1.ShowNewFolderButton = false;
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK || folderBrowserDialog1.SelectedPath == string.Empty)
            {
                return;

            }
            textBox1.Text = folderBrowserDialog1.SelectedPath;
            Indexer indexer = new Indexer(_indexTarget);
            indexer.UpdateCallback = new IndexerUpdateCallback(this.UpdateIndexingInfo);
            indexer.IndexDirectory(folderBrowserDialog1.SelectedPath);

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\empty.html";
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path);

            }

            webBrowser1.Url = new Uri("file:///" + path);
            listBox1.Items.Clear();
            label1.Text = "Searching...";

            if (isearcher == null) isearcher = new Searcher(_indexTarget);

            results = isearcher.FastSearch(textBox2.Text);

            label1.Text = results.Length.ToString() + " hit(s).";

            foreach (SearchResult result in results)
            {
                listBox1.Items.Add(result.GetDocTitle());

            }

        }

        private void _displayResults()
        {

            if (listBox1.SelectedItems.Count == 0) return;
            if (results == null) return;

            SearchResult r = results[listBox1.SelectedIndex];
            Utils.GetFastSearchResultFragments(ref r);
            string htmlresult = Utils.FormatSearchResultAsHtml(r);
            string path = Application.StartupPath + "\\results.html";
            System.IO.File.WriteAllText(path, htmlresult);
            webBrowser1.Url = new Uri("file:///" + path);
            webBrowser1.Document.Encoding = "UTF-8";
            webBrowser1.Refresh(WebBrowserRefreshOption.Completely);
            string contentspath = Application.StartupPath + "\\contents.html";
            System.IO.File.WriteAllText(contentspath, Utils.GetOriginalHighlightedContents(r));

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            _displayResults();
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (_indexTarget == string.Empty) return;

            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK || folderBrowserDialog1.SelectedPath == string.Empty)
            {
                return;

            }
            textBox4.Text = _indexTarget = folderBrowserDialog1.SelectedPath;
            button1.Enabled = true;
            button2.Enabled = true;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Searcher isearcher = new Searcher(_indexTarget);
            string [] docs = isearcher.SearchDocs(textBox2.Text);
            label1.Text = docs.Length.ToString() + " hit(s).";
            listBox1.Items.AddRange(docs);
            isearcher.Close();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _displayResults();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isearcher != null) isearcher.Close();
        }

    }
}