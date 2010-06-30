using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;


namespace AdSearch
{
    class AdSearcher
    {
        private IndexSearcher _searcher = null;
        Lucene.Net.Analysis.Standard.StandardAnalyzer _analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer();
        private string _idxDir = string.Empty;

        public AdSearcher(string idxDir)
        {
            _idxDir = idxDir;
            bool success = System.IO.Directory.Exists(idxDir) && IndexReader.IndexExists(idxDir);
            if (success)
            {
                _searcher = new IndexSearcher(idxDir);

            }


        }
        
        public List<Advert> SearchAds(string query)
        {
            if (_searcher == null) return null;

            ICollection fields = _searcher.Reader.GetFieldNames(IndexReader.FieldOption.ALL);
            List<string> fldList = new List<string>();
            foreach (DictionaryEntry f in fields)
            {
                fldList.Add(f.Key.ToString());

            }
            List<Advert> adverts = new List<Advert>();
            MultiFieldQueryParser parser = new MultiFieldQueryParser(fldList.ToArray(), _analyzer);
            Query q = parser.Parse(query);
            Hits hits = _searcher.Search(q);
            PropertyDescriptors desc = new PropertyDescriptors();
            desc.LoadData(System.Windows.Forms.Application.StartupPath + "\\PropertyDescriptors.xml");
            for (int i = 0; i < hits.Length(); i++)
            {
                Advert ad = new Advert();
                Document doc = hits.Doc(i);
                foreach (Field f in doc.Fields())
                {

                    string temp = desc.GetDisplayableFormat(f.Name(), f.StringValue());
                    ad[f.Name()] = temp;

                }
                adverts.Add(ad);
            }

            return adverts;

        }
        public void Close()
        {
            if (_searcher != null) _searcher.Close();
        }

    }
}
