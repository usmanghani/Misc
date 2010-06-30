//System imports
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


//Lucene imports
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Store;
using Lucene.Net.Util;


namespace scratch
{
    public partial class MainForm : Form
    {
        //Analyzer analyzer = new StandardAnalyzer();
        Analyzer analyzer = new DiacriticAnalyzer(FilterData.stopWords);
        string _indexTarget = string.Empty;

        public MainForm()
        {
            //MessageBox.Show(Properties.Settings.Default["data"].ToString());
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string indexedPath = string.Empty;
            DialogResult response = folderBrowserDialog1.ShowDialog();
            if (response == DialogResult.OK)
            {
                try
                {
                    indexedPath = folderBrowserDialog1.SelectedPath;
                    textBox3.Text = indexedPath;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else
            {
                return;
            }
            if (textBox5.Text == string.Empty) return;
            _indexTarget = textBox5.Text;
            Lucene.Net.Store.Directory indexDir = null;
            if (System.IO.Directory.Exists(_indexTarget))
            {

                indexDir = FSDirectory.GetDirectory(_indexTarget, false);
                listBox3.Items.Add("Index Directory exists.");
            }

            else
            {

                indexDir = FSDirectory.GetDirectory(_indexTarget, true);
                listBox3.Items.Add("Creating new index directory.");
            }
            //MessageBox.Show(indexDir.ToString());
            Application.DoEvents();

            IndexWriter writer = null;
            if (IndexReader.IndexExists(indexDir))
            {

                writer = new IndexWriter(indexDir, analyzer, false);
                listBox3.Items.Add("Using previous index.");
            }
            else
            {

                writer = new IndexWriter(indexDir, analyzer, true);
                writer.SetUseCompoundFile(false);
                listBox3.Items.Add("Creating new index.");
            }

            Application.DoEvents();

            string[] files = FSDirectory.GetDirectory(indexedPath, false).List();
            int deleted = 0;
            
            writer.Close();
            IndexReader reader = IndexReader.Open(indexDir);
            foreach (string s in files)
            {

                if (/*s.ToLower().EndsWith(".txt")*/IFilter.DefaultParser.IsParseable(s))
                {
                    int thisdel = 0;
                    listBox3.Items.Add("Looking for a previous instance of " + s);
                    
                    thisdel = reader.Delete(new Term("filename", s));
                    deleted += thisdel;
                    if (thisdel == 0)
                    {
                        listBox3.Items.Add("No docs found with a filename matching " + s);
                        
                    }

                    if (thisdel > 0)
                    {
                        listBox3.Items.Add("Deleting: " + s);
                        
                    }

                }
                Application.DoEvents();

            }
            reader.Close();
            writer = new IndexWriter(indexDir, analyzer, false);
            writer.Optimize();                    
            listBox3.Items.Add(deleted + " document(s) deleted.");
            Application.DoEvents();

            foreach (string s in files)
            {
                if (/*s.ToLower().EndsWith(".txt")*/IFilter.DefaultParser.IsParseable(s))
                {
                    string contents = IFilter.DefaultParser.Extract(s);
                    Document doc = new Document();
                    doc.Add(Field.Keyword("filename", s));
                    doc.Add(Field.Text("FileNameIndexed", s));
                    doc.Add(Field.Text("contents", contents));
                    writer.AddDocument(doc);
                    listBox3.Items.Add("Added: " + s);
                    
                }
                Application.DoEvents();
            }

            writer.Optimize();
            writer.Close();

            IndexReader rdr = IndexReader.Open(indexDir);
            listBox3.Items.Add("Total docs indexed = " + rdr.NumDocs());
            rdr.Close();
            Application.DoEvents();

        }

        IndexSearcher searcher = null;
        Hits hits = null;

        private void button2_Click(object sender, EventArgs e)
        {
            if (_indexTarget == string.Empty)
                return;

            if (textBox4.Text == string.Empty)
            {
                return;
            }
            listBox4.Items.Clear();
            if (IndexReader.IndexExists(_indexTarget))
            {
                searcher = new IndexSearcher(_indexTarget);
                Query q = QueryParser.Parse(textBox4.Text, "contents", analyzer);
                hits = searcher.Search(q);

                if (hits.Length() == 0)
                {
                    label8.Text = "No Hits :P";
                    return;
                }
                label8.Text = hits.Length() + " hit(s).";
                for (int i = 0; i < hits.Length(); i++)
                {
                    Document d = hits.Doc(i);
                    listBox4.Items.Add(d.Get("filename"));

                }

            }

        }

        private void listBox4_DoubleClick(object sender, EventArgs e)
        {

            if (listBox4.SelectedItems.Count == 0)
                return;

            int idx = listBox4.SelectedIndex; //subtract 1 because the first item reports the number of hits.
            Document d = hits.Doc(idx);
            ResultHighlighter.Highlight(d, textBox4.Text, analyzer);
            string resultspath = Path.Combine(Application.StartupPath, "results.html");
            webBrowser1.Url = new Uri("file:///" + resultspath);

            //string contents = d.Get("contents");            
            //SimpleHTMLFormatter formatter = new SimpleHTMLFormatter("<span class=\"highlight\">", "</span>");
            ////SpanGradientFormatter formatter = new SpanGradientFormatter(10.0f, null, null, "#F1FD9F", "#EFF413");
            ////SimpleHTMLEncoder encoder = new SimpleHTMLEncoder();
            //SimpleFragmenter fragmenter = new SimpleFragmenter(250);
            //Highlighter hiliter = new Highlighter(formatter, new QueryScorer(QueryParser.Parse(textBox4.Text, "contents", analyzer)));
            //hiliter.SetTextFragmenter(fragmenter);
            //int numfragments = contents.Length / fragmenter.GetFragmentSize() + 1;// +1 ensures its never zero. More than the required number of fragments dont harm.
            //StringBuilder result = new StringBuilder("<html><style>.highlight{background:yellow;}</style><head><title>Search Results - ");
            //result.Append(d.Get("filename"));
            //result.Append("</title></head><body><font face=Arial size=5>");
            //TokenStream tokenstream = analyzer.TokenStream("contents", new StringReader(contents));
            //TextFragment[] frags = hiliter.GetBestTextFragments(tokenstream, contents, false, numfragments);
            //foreach (TextFragment frag in frags)
            //{
            //    if (frag.GetScore() > 0)
            //    {
            //        result.Append(frag.ToString() + "<br/><hr/><br/>");

            //    }

            //}

            //string contentspath = Path.Combine(Application.StartupPath, "contents.html");
            //result.Append("</font><a target=_self href=\"file:///");
            //result.Append(contentspath);
            //result.Append("\">View Original Document...</a>");
            //result.Append("</body></html>");
            //result.Replace("\n", "<br/>");

            //string resultspath = Path.Combine(Application.StartupPath, "results.html");
            //File.WriteAllText(resultspath, result.ToString());
            //webBrowser1.Url = new Uri("file:///" + resultspath);

            //Highlighter hiliter2 = new Highlighter(formatter, new QueryScorer(QueryParser.Parse(textBox4.Text, "contents", analyzer)));
            //hiliter2.SetTextFragmenter(fragmenter);
            //TokenStream tokstr = analyzer.TokenStream(new StringReader(contents));
            //StringBuilder htmlcontents = new StringBuilder("<html><style>.highlight{background:yellow;}</style><body><font face=Arial size=5>");
            //htmlcontents.Append(hiliter2.GetBestFragments(tokstr, contents, numfragments, "..."));
            //htmlcontents.Append("</font></body></html>");
            //htmlcontents.Replace("\n", "<br/>");
            //System.IO.File.WriteAllText(contentspath, htmlcontents.ToString());



        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.CompareTo('\n') == 0)
            {
                button2_Click(sender, e);

            }

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FilterData.PrepareCharMap();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK && folderBrowserDialog1.SelectedPath != string.Empty)
            {
                _indexTarget = folderBrowserDialog1.SelectedPath;
                textBox5.Text = _indexTarget;
                button3.Enabled = true;
                button4.Enabled = true;

            }

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox4.SelectedItems.Count == 0)
                return;

            int idx = listBox4.SelectedIndex; //subtract 1 because the first item reports the number of hits.
            Document d = hits.Doc(idx);
            ResultHighlighter.Highlight(d, textBox4.Text, analyzer);
            string resultspath = Path.Combine(Application.StartupPath, "results.html");
            webBrowser1.Url = new Uri("file:///" + resultspath);

        }

    }

}