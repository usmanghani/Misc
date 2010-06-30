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
    public enum DefaultOperator
    {
        AND,
        OR
    }

    public class Searcher
    {
        IndexSearcher _indexSearcher = null;
        string _idxDirName = string.Empty;
        Analyzer _analyzer = new DiacriticAnalyzer(FilterData.stopWords);
        DefaultOperator _operator = DefaultOperator.OR;
        QueryParser _parser = null;


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
            _parser = new QueryParser("contents", _analyzer);

        }
        public Searcher(string idxPath):this()
        {

            if (IndexReader.IndexExists(idxPath))
            {
                _idxDirName = idxPath;
                _indexSearcher = new IndexSearcher(idxPath);
                
            }

        }
        public Searcher(string idxPath, DefaultOperator oper):this(idxPath)
        {
            this._operator = oper;

        }

        private Hits _doSearch(string query)
        {
            if (query == string.Empty) return null;
            if (_indexSearcher == null && _idxDirName != string.Empty)
            {
                _indexSearcher = new IndexSearcher(_idxDirName);

            }
            
            switch (_operator)
            {
                case DefaultOperator.OR:
                    _parser.SetDefaultOperator(QueryParser.OR_OPERATOR);
                    break;

                case DefaultOperator.AND :
                    _parser.SetDefaultOperator(QueryParser.AND_OPERATOR);
                    break;



            }

            Query q = _parser.Parse(query);
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
        /// <summary>
        /// Fast Search works like Search but the SearchResults are not complete.
        /// They require further processing. Call Utils.GetFastSearchResultFragments.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SearchResult[] FastSearch(string query)
        {
            Hits hits = _doSearch(query);
            return _prepareSearchResultsFromHits(query, hits);
        }

        private SearchResult[] _prepareSearchResultsFromHits(string query, Hits hits)
        {
            List<SearchResult> results = new List<SearchResult>();
            for (int i = 0; i < hits.Length(); i++)
            {
                SearchResult sr = new SearchResult(hits.Doc(i), _analyzer, query, _fragmentSize);
                sr.QueryParser = _parser;
                results.Add(sr);

            }
            return results.ToArray();
        }
        public SearchResult[] FastSearch(string query, string[] sortFields)
        {
            Hits hits = _doSearchWithSort(query, sortFields);
            return _prepareSearchResultsFromHits(query, hits);


        }

        private Hits _doSearchWithSort(string query, string[] sortFields)
        {
            if (query == string.Empty) return null;
            if (_indexSearcher == null && _idxDirName != string.Empty)
            {
                _indexSearcher = new IndexSearcher(_idxDirName);

            }

            switch (_operator)
            {
                case DefaultOperator.OR:
                    _parser.SetDefaultOperator(QueryParser.OR_OPERATOR);
                    break;

                case DefaultOperator.AND:
                    _parser.SetDefaultOperator(QueryParser.AND_OPERATOR);
                    break;



            }

            Query q = _parser.Parse(query);

            List<SortField> fieldList = new List<SortField>();
            foreach (string f in sortFields)
            {
                fieldList.Add(new SortField(f));

            }
            SortField[] fields = fieldList.ToArray();
            Sort sorter = new Sort(fields);
            Hits hits = _indexSearcher.Search(q, sorter);

            return hits;

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
                Highlighter hiliter = new Highlighter(formatter, new QueryScorer(_parser.Parse(query)));
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
                sr.QueryParser = _parser;
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
