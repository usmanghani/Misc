using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Lucene.Net.Documents;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Analysis;
using Lucene.Net.QueryParsers;
using Lucene.Net.Index;

namespace scratch
{
    public class ResultHighlighter
    {
        public static void Highlight(Document d, string query, Analyzer analyzer)
        {
            string contents = d.Get("contents");
            SimpleHTMLFormatter formatter = new SimpleHTMLFormatter("<span class=\"highlight\">", "</span>");
            //SpanGradientFormatter formatter = new SpanGradientFormatter(10.0f, null, null, "#F1FD9F", "#EFF413");
            //SimpleHTMLEncoder encoder = new SimpleHTMLEncoder();
            SimpleFragmenter fragmenter = new SimpleFragmenter(250);
            Highlighter hiliter = new Highlighter(formatter, new QueryScorer(QueryParser.Parse(query, "contents", analyzer)));
            hiliter.SetTextFragmenter(fragmenter);
            int numfragments = contents.Length / fragmenter.GetFragmentSize() + 1;// +1 ensures its never zero. More than the required number of fragments dont harm.
            StringBuilder result = new StringBuilder("<html><style>.highlight{background:yellow;}</style><head><title>Search Results - ");
            result.Append(d.Get("filename"));
            result.Append("</title></head><body><font face=Arial size=5>");
            TokenStream tokenstream = analyzer.TokenStream("contents", new StringReader(contents));
            TextFragment[] frags = hiliter.GetBestTextFragments(tokenstream, contents, false, numfragments);
            foreach (TextFragment frag in frags)
            {
                if (frag.GetScore() > 0)
                {
                    result.Append(frag.ToString() + "<br/><hr/><br/>");

                }

            }

            string contentspath = Path.Combine(Application.StartupPath, "contents.html");
            result.Append("</font><a target=_self href=\"file:///");
            result.Append(contentspath);
            result.Append("\">View Original Document...</a>");
            result.Append("</body></html>");
            result.Replace("\n", "<br/>");

            string resultspath = Path.Combine(Application.StartupPath, "results.html");
            File.WriteAllText(resultspath, result.ToString());
            //webBrowser1.Url = new Uri("file:///" + resultspath);

            Highlighter hiliter2 = new Highlighter(formatter, new QueryScorer(QueryParser.Parse(query, "contents", analyzer)));
            hiliter2.SetTextFragmenter(fragmenter);
            TokenStream tokstr = analyzer.TokenStream(new StringReader(contents));
            StringBuilder htmlcontents = new StringBuilder("<html><style>.highlight{background:yellow;}</style><body><font face=Arial size=5>");
            htmlcontents.Append(hiliter2.GetBestFragments(tokstr, contents, numfragments, "..."));
            htmlcontents.Append("</font></body></html>");
            htmlcontents.Replace("\n", "<br/>");
            System.IO.File.WriteAllText(contentspath, htmlcontents.ToString());

        }

    }
}
