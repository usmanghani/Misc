using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Lucene.Net;
using Lucene.Net.Analysis;
using Lucene.Net.Search;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Index;
using Lucene.Net.Search.Highlight;


namespace DotFermion
{
    public class Searcher
    {
        IndexSearcher _indexSearcher = null;
        string _idxDirName = string.Empty;
        Analyzer _analyzer = new DiacriticAnalyzer(FilterData.stopWords);

        public string IndexDirectory
        {
            get { return _idxDirName; }
            set { _idxDirName = value; }
        }

        int _fragmentSize = 250;
        public int FragmentSize
        {
            get { return _fragmentSize; }
            set { _fragmentSize = value; }

        }

        
        public Searcher()
        {
            //FilterData.PrepareCharMap();
        }
        public Searcher(string idxPath)
        {
            //FilterData.PrepareCharMap();
            if (IndexReader.IndexExists(idxPath))
            {
                _idxDirName = idxPath;
                _indexSearcher = new IndexSearcher(idxPath);
                
            }

        }
        private Hits _doSearch(string query)
        {
            if (query == string.Empty) return null;

            if (_indexSearcher == null && _idxDirName != string.Empty)
            {
                _indexSearcher = new IndexSearcher(_idxDirName);

            }

            Query q = QueryParser.Parse(query, "contents", _analyzer);
            Hits hits = _indexSearcher.Search(q);
            return hits;

        }

        public string[] SearchDocs(string query)
        {
            Hits hits = _doSearch(query);
            List<string> results = new List<string>();
            for (int i = 0; i < hits.Length(); i++)
            {
                results.Add(hits.Doc(i).Get("filename"));

            }
            return results.ToArray();

        }

        public SearchResult[] Search(string query)
        {

            Hits hits = _doSearch(query);
            
            List<SearchResult> results = new List<SearchResult>();
            for (int i = 0; i < hits.Length(); i++)
            {
                Document doc = hits.Doc(i);
                string contents = doc.Get("contents");
                SimpleHTMLFormatter formatter = new SimpleHTMLFormatter("<span class=\"highlight\">", "</span>");
                SimpleFragmenter fragmenter = new SimpleFragmenter(_fragmentSize);
                Highlighter hiliter = new Highlighter(formatter, new QueryScorer(QueryParser.Parse(query, "contents", _analyzer)));
                hiliter.SetTextFragmenter(fragmenter);
                int numfragments = contents.Length / fragmenter.GetFragmentSize() + 1;
                TokenStream tokenstream = _analyzer.TokenStream("contents", new StringReader(contents));
                TextFragment[] frags = hiliter.GetBestTextFragments(tokenstream, contents, false, numfragments);
                SearchResult sr = new SearchResult(doc, _analyzer, query, _fragmentSize);
                foreach (TextFragment frag in frags)
                {

                    if (frag.GetScore() > 0)
                        sr.AddFragment(frag.ToString());


                }
                results.Add(sr);

            }

            return results.ToArray();

                        
        }

        public void Close()
        {
            _indexSearcher.Close();
        }


    }
    
}
