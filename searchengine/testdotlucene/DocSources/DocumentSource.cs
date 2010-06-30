using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Lucene.Net.Documents;
using Lucene.Net.Store;

using FSDirectory = Lucene.Net.Store.Directory;

namespace DotFermion.DocSources
{
        public interface DocumentSource:IEnumerable<Document>
        {
        }

}
