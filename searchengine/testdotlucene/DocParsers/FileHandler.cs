using System;
using System.Collections.Generic;
using System.Text;

using Lucene.Net.Documents;

namespace DotFermion.DocParsers
{
    public interface FileHandler
    {
        Document GetDocument(System.IO.FileInfo fi);
    }


}
