using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace DotFermion.DocParsers
{
    using HtmlAgilityPack;
    using Lucene.Net.Demo;
    using Lucene.Net.Documents;

    public class HtmlDocumentParser : DocumentParser
    {

        IFilters.TextFilter filter = null;

        public HtmlDocumentParser(FileInfo fi)
        {
            if (fi.Extension == ".htm" || fi.Extension == ".html")
            {
                IFilters.TextFilter filter = new DotFermion.IFilters.TextFilter(fi.FullName);
                filter.Process();

            }

        }

        public override string GetContents()
        {
            if (filter == null) return null;
            return filter.DocumentText.ToString();

        }

        public override string GetTitle()
        {
            return this.GetProperty("title");

        }

        public override string GetAuthor()
        {
            return this.GetProperty("author");
        }

        public override string GetProperty(string prop)
        {
            if (filter == null) return null;
            if (filter.DocumentProperties.Contains(prop as object))
            {
                return filter.DocumentProperties[prop as object] as string;

            }
            return null;

        }

        #region Implementation using HtmlAgilityPack
        string ExtractContentFromChildren(HtmlNode node)
        {
            StringBuilder builder = new StringBuilder(string.Empty);

            foreach (HtmlNode child in node.ChildNodes)
            {
                if (child.NodeType == HtmlNodeType.Text)
                {
                    builder.Append((child as HtmlTextNode).Text);

                }
                else if (child.NodeType == HtmlNodeType.Element)
                {
                    if (child.Name.ToLower() == "p")
                    {
                        builder.Append("\r\n");

                    }

                    if (child.HasChildNodes)
                    {
                        builder.Append(ExtractContentFromChildren(child));

                    }

                }

            }
            return builder.ToString();


        }

        Dictionary<string, string> GetContent(Stream inputStream)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(inputStream);

            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (HtmlNode child in doc.DocumentNode.ChildNodes)
            {
                if (child.Name.ToLower() == "title")
                {
                    result.Add("title", (child as HtmlTextNode).Text);

                }
                if (child.Name.ToLower() == "body")
                {
                    StringBuilder builder = new StringBuilder(string.Empty);
                    if (child.NodeType == HtmlNodeType.Text)
                    {
                        builder.Append((child as HtmlTextNode).Text);

                    }
                    builder.Append(ExtractContentFromChildren(child));
                    result.Add("contents", builder.ToString());

                }

            }

            return result;

        }
        #endregion
    }

}
