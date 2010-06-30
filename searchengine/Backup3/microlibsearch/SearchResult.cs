using System;
using System.Collections.Generic;
using System.Text;

using Lucene.Net.Documents;
using Lucene.Net.Analysis;
using Lucene.Net.QueryParsers;

namespace DotFermion
{
    public class SearchResult
    {
        Document _document;
        Analyzer _analyzer;
        string _query;
        int _fragmentSize;
        QueryParser _parser;
        List<string> _fragments = new List<string>();

        public SearchResult()
        {
        }

        public SearchResult(Document doc, Analyzer analyzer, string query, int fragSize)
        {
            _document = doc;
            _analyzer = analyzer;
            _query = query;
            _fragmentSize = fragSize;
            _fragments.Clear();

        }

        public string Query
        {
            get { return _query; }
            set { _query = value; }

        }

        public Analyzer Analyzer
        { 
            get { return _analyzer; }
            set { _analyzer = value; }


        }

        public Document Document
        {
            get { return _document; }
            set { _document = value; }

        }

        public int FragmentSize
        {
            get { return _fragmentSize; }
            set { _fragmentSize = value; }
        }

        public QueryParser QueryParser
        {
            get { return _parser; }
            set { _parser = value; }

        }


        public void AddFragment(string frag)
        {
            _fragments.Add(frag);

        }
        public string[] GetFragments()
        {
            return _fragments.ToArray();

        }
        public string GetDocFileName()
        {
            return _document.Get("filename");
        }

        public string GetDocTitle()
        {
            return _document.Get("filename");

        }
        public string GetDocContents()
        {
            return _document.Get("contents");

        }
        public string GetDocProperty(string propName)
        {
            return _document.Get(propName);

        }


    }
}
