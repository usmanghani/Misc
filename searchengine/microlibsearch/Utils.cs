using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Lucene.Net.Documents;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Analysis;
using Lucene.Net.QueryParsers;

using DotFermion.Searching;

namespace DotFermion.Highlighting
{
    public class Utils
    {
        public static string FormatSearchResultAsHtml(SearchResult sr)
        {

            StringBuilder html = new StringBuilder("<html><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><style>.highlight{background:yellow;}</style><body><font face=Arial size=5>");
            foreach (string s in sr.GetFragments())
            {
                html.Append(s);
                html.Append("<br/><hr/><br/>");

            }
            html.Append("</font><a href=\"contents.html\">View Original Document...</a></body></html>");
            html.Replace("\n", "<br/>");
            return html.ToString();

        }

        public static string GetOriginalHighlightedContents(SearchResult sr)
        {

            StringBuilder result = new StringBuilder("<html><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><style>.highlight{background:yellow;}</style><body><font face=Arial size=5>");
            string contents = sr.GetDocContents();
            SimpleHTMLFormatter formatter = new SimpleHTMLFormatter("<span class=\"highlight\">", "</span>");
            SimpleFragmenter fragmenter = new SimpleFragmenter(sr.FragmentSize);
            Highlighter hiliter = new Highlighter(formatter, new QueryScorer(sr.QueryParser.Parse(sr.Query)));
            hiliter.SetTextFragmenter(fragmenter);
            int numfragments = contents.Length / fragmenter.GetFragmentSize() + 1;
            TokenStream tokenstream = sr.Analyzer.TokenStream("contents", new StringReader(contents));
            result.Append(hiliter.GetBestFragments(tokenstream, contents, numfragments, "..."));
            result.Append("</font></body></html>");
            result.Replace("\n", "<br/>");
            return result.ToString();

        }

        public static string GetHilitedContentsWithoutHeaders(SearchResult sr)
        {
            StringBuilder result = new StringBuilder("<font face=Arial size=5>");
            string contents = sr.GetDocContents();
            SimpleHTMLFormatter formatter = new SimpleHTMLFormatter("<span class=\"highlight\">", "</span>");
            SimpleFragmenter fragmenter = new SimpleFragmenter(sr.FragmentSize);
            Highlighter hiliter = new Highlighter(formatter, new QueryScorer(sr.QueryParser.Parse(sr.Query)));
            hiliter.SetTextFragmenter(fragmenter);
            int numfragments = contents.Length / fragmenter.GetFragmentSize() + 1;
            TokenStream tokenstream = sr.Analyzer.TokenStream("contents", new StringReader(contents));
            result.Append(hiliter.GetBestFragments(tokenstream, contents, numfragments, "..."));
            result.Append("</font>");
            result.Replace("\n", "<br/>");
            return result.ToString();
        }

        public static SearchResult GetFastSearchResultFragments(ref SearchResult sr)
        {
            Document doc = sr.Document;
            string contents = doc.Get("contents");
            SimpleHTMLFormatter formatter = new SimpleHTMLFormatter("<span class=\"highlight\">", "</span>");
            SimpleFragmenter fragmenter = new SimpleFragmenter(sr.FragmentSize);
            Highlighter hiliter = new Highlighter(formatter, new QueryScorer(sr.QueryParser.Parse(sr.Query)));
            hiliter.SetTextFragmenter(fragmenter);
            int numfragments = contents.Length / fragmenter.GetFragmentSize() + 1;
            TokenStream tokenstream = sr.Analyzer.TokenStream("contents", new StringReader(contents));
            TextFragment[] frags = hiliter.GetBestTextFragments(tokenstream, contents, false, numfragments);
            //SearchResult sr = new SearchResult(doc, _analyzer, query, _fragmentSize);
            foreach (TextFragment frag in frags)
            {

                if (frag.GetScore() > 0)
                    sr.AddFragment(frag.ToString());


            }
            return sr;

        }
    }
}
