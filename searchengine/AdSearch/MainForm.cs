using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Lucene.Net.Search;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;

namespace AdSearch
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void _indexerUpdate(string message)
        {
            listBox1.Items.Add(message);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //IndexSearcher searcher = new IndexSearcher("sampleindex");
           //ICollection fields = searcher.Reader.GetFieldNames();
            //Hits hits = searcher.Search(QueryParser.Parse("kanal", "Area", new Lucene.Net.Analysis.Standard.StandardAnalyzer()));
            //MessageBox.Show(hits.Length().ToString());
            //foreach (Lucene.Net.Documents.Field f in hits.Doc(0).Fields())
            //{

            //    listBox1.Items.Add(f.Name() + ": " + f.StringValue());
            //}

            ////MessageBox.Show(hits.Doc(0).Fields().GetType().ToString());
            //searcher.Close();
            ////foreach (DictionaryEntry f in fields)
            //{
            //    listBox1.Items.Add(f.Key.ToString() + "=>" + f.Value.ToString());
                

            //}
            
            //if (textBox1.Text == string.Empty) return;
            //if (System.IO.File.Exists(textBox1.Text) == false) return;
            //listBox1.Items.Clear();

            if (textBox1.Text == string.Empty) return;
            if (!System.IO.File.Exists(textBox1.Text)) return;

            AdIndexer indexer = new AdIndexer("sampleindex");
            indexer.SetUpdateCallback(_indexerUpdate);
            indexer.IndexFile(textBox1.Text);


            //int propCount = 0;
            //PropertyDescriptors descriptors = new PropertyDescriptors();
            //descriptors.LoadData(Application.StartupPath + "\\" + textBox1.Text);
            //foreach (string s in descriptors.GetDictionary().Keys)
            //{
            //    listBox1.Items.Add(s + ": " + descriptors[s]);
            //    propCount++;
            //}
            //MessageBox.Show(descriptors.GetIndexableFormat("Date", "11/20/2007"));
            //MessageBox.Show(string.Format("Total {0} property descriptors loaded.", propCount));
            //AdDataStream stream = new AdDataStream(textBox1.Text);
            //stream.LoadData();
            //int adCount = 0;
            //foreach (Advert ad in stream.FetchAd())
            //{
            //    foreach (string s in ad.GetDictionary().Keys)
            //    {
            //        listBox1.Items.Add(s + ": " + ad[s]);
            //    }

            //    listBox1.Items.Add(">>>>>>>>>>");
            //    adCount++;
            //}
            //MessageBox.Show(string.Format("Total {0} ads loaded.", adCount));


        }

        private void button2_Click(object sender, EventArgs e)
        {

            //System.Globalization.DateTimeFormatInfo dfi = new System.Globalization.DateTimeFormatInfo();
            //dfi.ShortDatePattern = "yyyyMMdd";
            //DateTime dt = DateTime.Parse("12/05/2007");
            //string strDate = dt.ToString(dfi.SortableDateTimePattern);
            //DateTime dt2 = DateTime.Parse(strDate);
            //MessageBox.Show(strDate);
            //MessageBox.Show(dt2.ToString());
            //return;

            if (textBox2.Text == string.Empty) return;
            listBox2.DataSource = null;
            listBox2.Items.Clear();
            AdSearcher searcher = new AdSearcher("sampleindex");
            List<Advert> adverts = searcher.SearchAds(textBox2.Text);
            listBox2.DataSource = adverts;
            label1.Text = adverts.Count + " Hit(s).";
            label1.Visible = true;

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex == -1) return;
            Advert ad = listBox2.SelectedItem as Advert;
            AdFormatter formatter = new AdFormatter();
            string result = formatter.Format(ad);
            //System.IO.File.WriteAllText(Application.StartupPath + "\\result.html", result);
            //webBrowser1.Url = new Uri("file:///" + Application.StartupPath + "/result.html");
            webBrowser1.DocumentText = result;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(Application.StartupPath + "/sampleindex"))
                System.IO.Directory.Delete(Application.StartupPath + "/sampleindex", true);

        }
    }
}