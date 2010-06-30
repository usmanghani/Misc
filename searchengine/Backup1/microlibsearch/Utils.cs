using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Lucene.Net.Documents;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Analysis;
using Lucene.Net.QueryParsers;


namespace DotFermion
{
    public class Utils
    {
        public static string FormatSearchResultAsHtml(SearchResult sr)
        {

            StringBuilder html = new StringBuilder("<html><style>.highlight{background:yellow;}</style><body><font face=Arial size=5>");
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
            
            StringBuilder result = new StringBuilder("<html><style>.highlight{background:yellow;}</style><body><font face=Arial size=5>");
            string contents = sr.GetDocContents();
            SimpleHTMLFormatter formatter = new SimpleHTMLFormatter("<span class=\"highlight\">", "</span>");
            SimpleFragmenter fragmenter = new SimpleFragmenter(sr.FragmentSize);
            Highlighter hiliter = new Highlighter(formatter, new QueryScorer(QueryParser.Parse(sr.Query, "contents", sr.Analyzer)));
            hiliter.SetTextFragmenter(fragmenter);
            int numfragments = contents.Length / fragmenter.GetFragmentSize() + 1;
            TokenStream tokenstream = sr.Analyzer.TokenStream("contents", new StringReader(contents));
            result.Append(hiliter.GetBestFragments(tokenstream, contents, numfragments, "..."));
            result.Append("</font></body></html>");
            result.Replace("\n", "<br/>");
            return result.ToString();

        }
    }
}
