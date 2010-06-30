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
using Lucene.Net.Analysis.Standard;

using DotFermion.Analysis;

namespace DotFermion.Searching
{
    public enum DefaultOperator
    {
        AND,
        OR
    }

    public class Searcher
    {
        int _fragmentSize = 250;
        IndexSearcher _indexSearcher = null;
        string _idxDirName = string.Empty;
        PerFieldAnalyzerWrapper _analyzer = new PerFieldAnalyzerWrapper(new StandardAnalyzer());
        DefaultOperator _operator = DefaultOperator.OR;
        List<string> _searchFields = new List<string>();
        List<string> _sortFields = new List<string>();
        //List<string> _diacriticFields = new List<string>();

        public Searcher()
        {
            

        }
        public Searcher(string idxPath)
            : this()
        {

            if (IndexReader.IndexExists(idxPath))
            {
                _idxDirName = idxPath;
                _indexSearcher = new IndexSearcher(idxPath);

            }

        }

        public Searcher(string idxPath, DefaultOperator oper)
            : this(idxPath)
        {
            this._operator = oper;

        }
        public void AddSearchField(string _searchField)
        {
            _searchFields.Add(_searchField);
        }
        public void AddSearchFieldRange(string[] _sf)
        {
            _searchFields.AddRange(_sf);
        }

        public void AddSortField(string _sortField)
        {
            _sortFields.Add(_sortField);

        }
        public void AddSortFieldRange(string[] _sf)
        {
            _sortFields.AddRange(_sf);
        }

        public void AddDiacriticField(string _fieldName)
        {
            //_diacriticFields.Add(_fieldName);
            _analyzer.AddAnalyzer(_fieldName, new DiacriticAnalyzer(DiacriticFilter.stopWords));
        }
        public void AddDiacriticFieldRange(string[] _fields)
        {
            foreach (string s in _fields)
            {
                _analyzer.AddAnalyzer(s, new DiacriticAnalyzer(DiacriticFilter.stopWords));
            }

        }

        public string IndexDirectory
        {
            get { return _idxDirName; }
            set { _idxDirName = value; }
        }

        public int FragmentSize
        {
            get { return _fragmentSize; }
            set { _fragmentSize = value; }

        }


        public DefaultOperator BooleanOperator
        {
            get
            {
                return _operator;

            }

            set
            {
                _operator = value;

            }

        }
        private QueryParser _createQueryParser()
        {

            string[] searchfields = null;
            if (_searchFields.Count == 0)
                searchfields = new string[] { "contents" };
            else
                searchfields = _searchFields.ToArray();

            MultiFieldQueryParser parser = new MultiFieldQueryParser(searchfields, _analyzer);

            switch (_operator)
            {
                case DefaultOperator.OR:
                    parser.SetDefaultOperator(QueryParser.OR_OPERATOR);
                    break;

                case DefaultOperator.AND:
                    parser.SetDefaultOperator(QueryParser.AND_OPERATOR);
                    break;

            }

            return parser;

        }

        private Hits _doSearch(string query)
        {
            if (query == string.Empty) return null;
            if (_indexSearcher == null && _idxDirName != string.Empty)
            {
                _indexSearcher = new IndexSearcher(_idxDirName);

            }

            QueryParser parser = _createQueryParser();
            //foreach (string s in _diacriticFields)
            //{
            //    _analyzer.AddAnalyzer(s, new DiacriticAnalyzer(DiacriticFilter.stopWords));
            //}

            
            Sort sort = new Sort(_sortFields.ToArray());
            Query q = parser.Parse(query);
            Hits hits = null;
            if (sort != null)
                hits = _indexSearcher.Search(q, sort);
            else
                hits = _indexSearcher.Search(q);

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
                sr.QueryParser = _createQueryParser();
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
            QueryParser parser = _createQueryParser();
            switch (_operator)
            {
                case DefaultOperator.OR:
                    parser.SetDefaultOperator(QueryParser.OR_OPERATOR);
                    break;

                case DefaultOperator.AND:
                    parser.SetDefaultOperator(QueryParser.AND_OPERATOR);
                    break;



            }

            Query q = parser.Parse(query);

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
                Highlighter hiliter = new Highlighter(formatter, new QueryScorer(_createQueryParser().Parse(query)));
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
                sr.QueryParser = _createQueryParser();
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
