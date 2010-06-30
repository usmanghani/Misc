using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

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

        string _indexTarget = string.Empty;
        string _dbpath = string.Empty;
        bool _completed = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void _doIndexing()
        {

            _completed = false;


            StringBuilder log = new StringBuilder();
            StringWriter logwriter = new StringWriter(log);

            int counter = 1;
            string fsPath = _indexTarget;
            
            if (!System.IO.Directory.Exists(fsPath)) System.IO.Directory.CreateDirectory(fsPath);
            if (IndexReader.IndexExists(fsPath))
            {
                DialogResult result = MessageBox.Show("Index already exists.\nDo you want to overwrite?", "True Quran Database Indexer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;
                

            }
            
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\testingthings\searchengine\IndexDBTables\Database.mdb";
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _dbpath + ";Persist Security Info=True";
            OleDbConnection conn = new OleDbConnection(connString);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select * from trans", conn);
            OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            DatabaseDataSet dataset = new DatabaseDataSet();
            DataTable table = dataset.Tables["trans"];
            table.Load(reader);


            RAMDirectory dir = new RAMDirectory();
            PerFieldAnalyzerWrapper analyzer = new PerFieldAnalyzerWrapper(new StandardAnalyzer());
            analyzer.AddAnalyzer("ayat_arabic", new DiacriticAnalyzer(FilterData.stopWords));
            analyzer.AddAnalyzer("contents", new DiacriticAnalyzer(FilterData.stopWords));
            IndexWriter ramWriter = new IndexWriter(dir, analyzer, true);
            IndexWriter fsWriter = new IndexWriter(fsPath, analyzer, true);
            ramWriter.SetUseCompoundFile(false);
            fsWriter.SetUseCompoundFile(false);


            long start = DateTime.Now.Ticks;
            int total = table.Rows.Count;
            long end = DateTime.Now.Ticks;

            logwriter.WriteLine("Fetched total rowcount in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

            foreach (DataRow row in table.Rows)
            {

                Document doc = new Document();

                start = DateTime.Now.Ticks;
                string pid = row[table.Columns["PID"]].ToString();
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Fetched pid " + pid + " in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                string sid = row[table.Columns["SID"]].ToString();
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Fetched sid " + sid + " in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                string ayatno = row[table.Columns["ayatno"]].ToString();
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Fetched ayatno " + ayatno + " in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                string arabic = row[table.Columns["ayat_arabic"]].ToString();
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Fetched ayat_arabic in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                string urdu = row[table.Columns["ayat_urdu"]].ToString();
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Fetched ayat_urdu in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                string english = row[table.Columns["ayat_desc"]].ToString();
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Fetched ayat_desc in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                doc.Add(Field.Keyword("pid", long.Parse(pid).ToString("00000")));
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Added field pid in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                doc.Add(Field.Keyword("sid", long.Parse(sid).ToString("00000")));
                end = DateTime.Now.Ticks;

                logwriter.WriteLine("Added field sid in " + TimeSpan.FromTicks(end - start).TotalMilliseconds + " milliseconds.");

                start = DateTime.Now.Ticks;
                doc.Add(Field.Keyword("ayatno", long.Parse(ayatno).ToString("00000")));
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
                counter++;
                backgroundWorker1.ReportProgress(percent);

            }
            ramWriter.Optimize();
            fsWriter.AddIndexes(new Lucene.Net.Store.Directory[] { dir });
            ramWriter.Close();
            fsWriter.Close();

            logwriter.Close();
            File.WriteAllText("c:\\indexinglog.txt", log.ToString());

            //MessageBox.Show("Done Indexing!");
        }



        private void btnDBpath_Click(object sender, EventArgs e)
        {
            DialogResult response = openFileDialog1.ShowDialog();
            if (response == DialogResult.OK && openFileDialog1.FileName != string.Empty)
            {
                _dbpath = openFileDialog1.FileName;
                txtDBpath.Text = _dbpath;

            }


        }

        private void btnTargetPath_Click(object sender, EventArgs e)
        {
            DialogResult response = folderBrowserDialog1.ShowDialog();
            if (response == DialogResult.OK && folderBrowserDialog1.SelectedPath != string.Empty)
            {
                _indexTarget = folderBrowserDialog1.SelectedPath;
                txtTargetPath.Text = _indexTarget;

            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _doIndexing();

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgIndexing.Value = e.ProgressPercentage;
            Application.DoEvents();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done Indexing!!!", "True Quran Database Indexer");
            _completed = true;

        }


        Hits hits = null;
        IndexSearcher searcher = null;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lstResults.Items.Clear();
            searcher = new IndexSearcher(new RAMDirectory(_indexTarget));
            PerFieldAnalyzerWrapper analyzer = new PerFieldAnalyzerWrapper(new StandardAnalyzer());
            analyzer.AddAnalyzer("ayat_arabic", new DiacriticAnalyzer(FilterData.stopWords));
            //MyQueryParser parser = new MyQueryParser(new string[] { "ayat_desc", "ayat_urdu", "ayat_arabic" }, analyzer);
            //parser.SetDefaultOperator(QueryParser.Operator.AND);
            //Query q = parser.Parse(txtSearch.Text);
            //Query q = new TermQuery(new Term("ayatno", NumberTools.LongToString(long.Parse(txtSearch.Text))));
            BooleanQuery q = new BooleanQuery();
            long l1 = 1; long l2 = 500; long l3 = 1; long l4 = 1;
            //RangeQuery rq = new RangeQuery(new Term("ayatno", l1.ToString("00000")), new Term("ayatno", l2.ToString("00000")), true);
            //q.Add(rq, true, false);
            q.Add(new TermQuery(new Term("sid", l3.ToString("00000"))), true, false);
            q.Add(new TermQuery(new Term("ayatno", l4.ToString("00000"))), true, false);
            MessageBox.Show(q.ToString());
            Sort sort = new Sort(new string[] { "pid", "sid", "ayatno" });
            hits = searcher.Search(q, sort);
            lblHits.Text = hits.Length() + " hit(s).";
            Application.DoEvents();

            for (int i = 0; i < hits.Length(); i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Para: ").Append(hits.Doc(i).Get("pid"));
                sb.Append(", Surat: ").Append(hits.Doc(i).Get("sid"));
                sb.Append(", Verse: ").Append(hits.Doc(i).Get("ayatno"));
                lstResults.Items.Add(sb.ToString());

            }


        }

        private void btnIndex_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            _completed = false;

        }

        private void lstResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstResults.SelectedIndex != -1)
            {
                ResultHighlighter.Highlight(hits.Doc(lstResults.SelectedIndex), txtSearch.Text, new DiacriticAnalyzer(FilterData.stopWords));
                string resultspath = System.IO.Path.Combine(Application.StartupPath, "contents.html");
                webBrowser1.Url = new Uri("file:///" + resultspath);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_completed)
            {
                DialogResult response = MessageBox.Show("The indexing has not yet finished.\n Do you want to exit?", "True Quran Database Indexer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response == DialogResult.Yes)
                {
                    backgroundWorker1.CancelAsync();
                    e.Cancel = false;

                }
                else
                {
                    e.Cancel = true;

                }
            }

        }

        private void txtTargetPath_TextChanged(object sender, EventArgs e)
        {
            _indexTarget = txtTargetPath.Text;
        }

    }
}