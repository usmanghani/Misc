using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Lucene.Net.Index;
using Lucene.Net.Analysis;
using Lucene.Net.Store;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis.Standard;


namespace IndexDBTables
{
    public partial class Form1 : Form
    {

        static string indexpath = "c:\\kanzulimanindex8";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FilterData.PrepareCharMap();

            StringBuilder log = new StringBuilder();
            StringWriter logwriter = new StringWriter(log);

            long start = DateTime.Now.Ticks;
            int total = this.databaseDataSet.trans.Count;
            long end = DateTime.Now.Ticks;

            logwriter.WriteLine("Fetched total rowcount in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                        
            int counter = 1;
            string fsPath = indexpath;

            if (!System.IO.Directory.Exists(fsPath)) System.IO.Directory.CreateDirectory(fsPath);
            if (IndexReader.IndexExists(fsPath)) return;
            RAMDirectory dir = new RAMDirectory();
            PerFieldAnalyzerWrapper analyzer = new PerFieldAnalyzerWrapper(new StandardAnalyzer());
            analyzer.AddAnalyzer("ayat_arabic", new DiacriticAnalyzer(FilterData.stopWords));
            //analyzer.AddAnalyzer("contents", new DiacriticAnalyzer(FilterData.stopWords));
            IndexWriter ramWriter = new IndexWriter(dir, analyzer, true);
            IndexWriter fsWriter = new IndexWriter(fsPath, analyzer, !IndexReader.IndexExists(fsPath));
            ramWriter.SetUseCompoundFile(false);
            fsWriter.SetUseCompoundFile(false);

            //this.transTableAdapter.Fill(databaseDataSet.trans);
            

            foreach (DataRow row in this.databaseDataSet.trans.Rows)
            {

                Document doc = new Document();

                start = DateTime.Now.Ticks;
                string pid = row[this.databaseDataSet.trans.pidColumn].ToString();
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Fetched pid " + pid + " in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                string sid = row[this.databaseDataSet.trans.sidColumn].ToString();
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Fetched sid " + sid + " in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                string ayatno = row[this.databaseDataSet.trans.ayatnoColumn].ToString();
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Fetched ayatno " + ayatno + " in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                string arabic = row[this.databaseDataSet.trans.ayat_arabicColumn].ToString();
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Fetched ayat_arabic in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                string urdu = row[this.databaseDataSet.trans.ayat_urduColumn].ToString();
                end = DateTime.Now.Ticks;
                
                logwriter.WriteLine("Fetched ayat_urdu in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                string english = row[this.databaseDataSet.trans.ayat_descColumn].ToString();
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Fetched ayat_desc in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                doc.Add(Field.Keyword("pid", pid));
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Added field pid in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                doc.Add(Field.Keyword("sid", sid));
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Added field sid in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                doc.Add(Field.Keyword("ayatno", ayatno));
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Added field ayatno in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                doc.Add(Field.Text("ayat_desc", english));
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Added field ayat_desc in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");


                start = DateTime.Now.Ticks;
                doc.Add(Field.Text("ayat_arabic", arabic));
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Added field ayat_arabic in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                doc.Add(Field.Text("ayat_urdu", urdu));
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Added field ayat_urdu in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");


                start = DateTime.Now.Ticks;
                doc.Add(Field.Text("contents", arabic + Environment.NewLine + urdu + Environment.NewLine + english));
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Added field contents in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");


                start = DateTime.Now.Ticks;
                ramWriter.AddDocument(doc);
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Added document in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                int percent = counter * 100 / total;
                this.progressBar1.Value = percent;
                label1.Text = percent.ToString() + "%";
                counter++;
                Application.DoEvents();

            }
            ramWriter.Optimize();
            fsWriter.AddIndexes(new Lucene.Net.Store.Directory[] { dir });
            ramWriter.Close();
            fsWriter.Close();

            logwriter.Close();
            File.WriteAllText("c:\\indexinglog.txt", log.ToString());

            MessageBox.Show("Done Indexing!");

        }

        private void transBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.transBindingSource.EndEdit();
            this.transTableAdapter.Update(this.databaseDataSet.trans);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'databaseDataSet.trans' table. You can move, or remove it, as needed.
            this.transTableAdapter.Fill(this.databaseDataSet.trans);

        }
        Hits hits = null;
        IndexSearcher searcher = null;
        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            searcher = new IndexSearcher(new RAMDirectory(indexpath));
            PerFieldAnalyzerWrapper analyzer = new PerFieldAnalyzerWrapper(new StandardAnalyzer());
            analyzer.AddAnalyzer("ayat_arabic", new DiacriticAnalyzer(FilterData.stopWords));
            Query q = MultiFieldQueryParser.Parse(textBox1.Text, new string[] { "ayat_desc", "ayat_urdu", "ayat_arabic" }, analyzer);
            //Query q = QueryParser.Parse(textBox1.Text, "contents", new DiacriticAnalyzer(FilterData.stopWords));
            //Query q = QueryParser.Parse(textBox1.Text, "ayat_desc", new DiacriticAnalyzer(FilterData.stopWords));
            hits = searcher.Search(q);
            label2.Text = string.Format("{0} hit(s).", hits.Length().ToString());
            Application.DoEvents();
            
            //for (int i = 0; i < hits.Length(); i++)
            //{
            //    foreach (Field f in hits.Doc(i).Fields())
            //    {
            //        listBox1.Items.Add(f.Name());

            //    }
            //}

            for (int i = 0; i < hits.Length(); i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Para: ").Append(hits.Doc(i).Get("pid"));
                sb.Append(", Surat: ").Append(hits.Doc(i).Get("sid"));
                sb.Append(", Verse: ").Append(hits.Doc(i).Get("ayatno"));
                listBox1.Items.Add(sb.ToString());

            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                ResultHighlighter.Highlight(hits.Doc(listBox1.SelectedIndex), textBox1.Text, new DiacriticAnalyzer(FilterData.stopWords));
                string resultspath = System.IO.Path.Combine(Application.StartupPath, "results.html");
                webBrowser1.Url = new Uri("file:///" + resultspath);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //if (!System.IO.File.Exists("c:\\kanzuldata.txt"))
            //{
            //    System.IO.File.Create("C:\\kanzuldata.txt");

            //}
            
            FileStream stream = new FileStream("c:\\kanzuldata2.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            long starttime = DateTime.Now.Ticks;
                        
            foreach (DataRow row in this.databaseDataSet.trans.Rows)
            {
                string pid = row[this.databaseDataSet.trans.pidColumn].ToString();
                string sid = row[this.databaseDataSet.trans.sidColumn].ToString();
                string ayatno = row[this.databaseDataSet.trans.ayatnoColumn].ToString();

                string arabic = row[this.databaseDataSet.trans.ayat_arabicColumn].ToString();
                string urdu = row[this.databaseDataSet.trans.ayat_urduColumn].ToString();
                string english = row[this.databaseDataSet.trans.ayat_descColumn].ToString();

                writer.WriteLine("Para: " + pid);
                writer.WriteLine("Surat: " + sid);
                writer.WriteLine("Ayat: " + ayatno);

                writer.WriteLine(arabic);
                writer.WriteLine(urdu);
                writer.WriteLine(english);
                writer.WriteLine();
            }

            long endtime = DateTime.Now.Ticks;

            TimeSpan span = TimeSpan.FromTicks(endtime - starttime);

            MessageBox.Show(span.TotalMilliseconds.ToString());

            writer.Close();
            stream.Close();

            MessageBox.Show("Done!!");
            
        }

    //    private void button3_Click(object sender, EventArgs e)
    //    {
    //        Hits hitsurdu = searcher.Search(QueryParser.Parse(textBox2.Text, "ayat_urdu", new DiacriticAnalyzer(FilterData.stopWords)));
    //        label2.Text = (hitsurdu.Length().ToString());
    //        for (int i = 0; i < hitsurdu.Length(); i++)
    //        {
                
    //        }
    //    }

    //    private void button4_Click(object sender, EventArgs e)
    //    {
    //        Hits hitsarabic = searcher.Search(QueryParser.Parse(textBox3.Text, "ayat_arabic", new DiacriticAnalyzer(FilterData.stopWords)));
    //        label2.Text = (hitsarabic.Length().ToString());
    //        for (int i = 0; i < hitsarabic.Length(); i++)
    //        {

    //        }
    //    }
    }
}