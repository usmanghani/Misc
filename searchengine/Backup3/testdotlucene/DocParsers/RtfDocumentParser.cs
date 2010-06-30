using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DotFermion.DocParsers
{
    public class RtfDocumentParser : DocumentParser
    { 
        
        IFilters.TextFilter filter = null;

        public RtfDocumentParser(FileInfo fi)
        {
            if (fi.Extension == ".rtf")
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


    }
}
