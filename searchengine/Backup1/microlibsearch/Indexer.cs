using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Lucene.Net;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace DotFermion
{
    public delegate void IndexerUpdateCallback ( string info );
    public class Indexer
    {
        private IndexerUpdateCallback _updateCallback = null;
        private string _indexDirName = string.Empty;
        private FSDirectory _indexDir = null;
        private IndexWriter _indexWriter = null;
        private Analyzer _analyzer = new DiacriticAnalyzer(FilterData.stopWords);

        private void _updateDirectory()
        {
            if (System.IO.Directory.Exists(_indexDirName))
                _indexDir = FSDirectory.GetDirectory(_indexDirName, false);
            else
                _indexDir = FSDirectory.GetDirectory(_indexDirName, true);
        }
        public IndexerUpdateCallback UpdateCallback
        {
            get { return _updateCallback; }
            set { _updateCallback = value; }

        }

        public string IndexDir
        {
            get
            {
                return _indexDirName;
            }
            set
            {
                _indexDirName = value;
                _updateDirectory();

            }

        }
        public Indexer()
        {
            //FilterData.PrepareCharMap();

        }
        public Indexer(string idxDir)
        {
            //FilterData.PrepareCharMap();
            this._indexDirName = idxDir;
            _updateDirectory();
            _updateWriter();
        }

        public Indexer(string idxDir, bool createNewIdx)
        {
            //FilterData.PrepareCharMap();
            this._indexDirName = idxDir;
            _updateDirectory();
            _updateWriterNew();
            
        }
        public Indexer(string idxDir, IndexerUpdateCallback callback)
        {
            //FilterData.PrepareCharMap();
            this._indexDirName = idxDir;
            _updateDirectory();
            _updateWriter();
            _updateCallback = callback;

        }

        private void _updateWriter()
        {
            if (IndexReader.IndexExists(_indexDir))
            {
                _indexWriter = new IndexWriter(_indexDir, _analyzer, false);

            }
            else
            {
                //_indexWriter = new IndexWriter(_indexDir, _analyzer, true);
                //_indexWriter.SetUseCompoundFile(true);

                _updateWriterNew();
            }


        }
        private void _updateWriterNew()
        {
            _indexWriter = new IndexWriter(_indexDir, _analyzer, true);
            _indexWriter.SetUseCompoundFile(true);
        }

        public void IndexDirectory(string path)
        {
            if (_indexWriter == null && _indexDirName != string.Empty)
            {
                _updateWriter();
            }

            string[] files = FSDirectory.GetDirectory(path, false).List();
            StringBuilder logdata = new StringBuilder(string.Empty);
            StringWriter logger = new StringWriter(logdata);

            foreach (string s in files)
            {
                Document doc = new Document();
                try
                {
                    //XFilter.TextFilter filter = new XFilter.TextFilter(s);
                    //filter.ProcessInSTAThread();
                    //string contents = filter.DocumentText.ToString();
                    string filename = s;
                    string contents = string.Empty;
                    if (IFilter.DefaultParser.IsParseable(s))
                    {
                        contents = IFilter.DefaultParser.Extract(s);
                        logger.WriteLine(contents);
                        doc.Add(Field.Keyword("filename", filename));
                        doc.Add(Field.Text("contents", contents));
                        _indexWriter.AddDocument(doc);
                        _updateCallback("Added document : " + s);

                    }

                }
                catch (Exception ex)
                {
                    logger.WriteLine("Exception : " + ex.Message);

                }

            }

            logger.Close();

            if (!File.Exists("c:\\indexlog.txt"))
                File.Create("c:\\indexlog.txt");

            File.AppendAllText("C:\\indexlog.txt", logdata.ToString());

            _indexWriter.Optimize();
            _indexWriter.Close();
        }

    }
}
