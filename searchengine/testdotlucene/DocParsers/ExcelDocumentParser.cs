using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DotFermion.DocParsers
{
    public class ExcelDocumentParser : DocumentParser
    {
        IFilters.TextFilter filter = null;

        public ExcelDocumentParser(FileInfo fi)
        {
            if (fi.Extension == ".xls")
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
