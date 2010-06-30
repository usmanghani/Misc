using System;
using System.Collections.Generic;
using System.Text;

using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Store;
using Lucene.Net.Util;


namespace AdSearch
{
    class AdIndexer
    {

        private string _idxDir = string.Empty;

        public AdIndexer(string idxDir)
        {
            _idxDir = idxDir;
        }

        public void IndexFile(string filePath)
        {
            PropertyDescriptors descriptors = new PropertyDescriptors();
            descriptors.LoadData(System.Windows.Forms.Application.StartupPath + "\\PropertyDescriptors.xml");
            Analyzer a = new Lucene.Net.Analysis.Standard.StandardAnalyzer();
            bool create = !(System.IO.Directory.Exists(_idxDir) && IndexReader.IndexExists(_idxDir));
            IndexWriter iw = new IndexWriter(_idxDir, a, create);
            iw.SetUseCompoundFile(true);

            AdDataStream adStream = new AdDataStream(filePath);
            adStream.LoadData();
            foreach (Advert ad in adStream.FetchAd())
            {
                Document doc = new Document();
                foreach (string s in ad.GetDictionary().Keys)
                {
                    string temp = descriptors.GetIndexableFormat(descriptors[s], ad[s]);
                    doc.Add(Field.Text(s, temp));

                }
                iw.AddDocument(doc);
                if (_updateCallback != null)
                {
                    _updateCallback("Added Document: " + ad["Title"]);

                }
            }
            iw.Optimize();
            iw.Close();

        }
        public delegate void AdIndexerUpdateCallback(string message);
        private AdIndexerUpdateCallback _updateCallback = null;
        public void SetUpdateCallback(AdIndexerUpdateCallback cb)
        {
            _updateCallback = cb;

        }

    }
}
