using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Lucene.Net.Documents;

namespace DotFermion.DocParsers
{
    public class ExtensionFileHandler : FileHandler
    {
        public ExtensionFileHandler()
        {
        }

        #region FileHandler Members

        Document FileHandler.GetDocument(FileInfo fi)
        {

            DocParsers.DocumentParser parser = DocParsers.DocumentParser.Create(fi);
            Document doc = new Document();
            string fileName = fi.Name;
            string title = fi.Name;
            string modified = DateTools.TimeToString((fi.LastWriteTime.Ticks - 621355968000000000) / 10000, DateTools.Resolution.SECOND);
            string originalContents = new StreamReader(fi.OpenRead()).ReadToEnd();
            string contents = parser.GetContents();
            
            doc.Add(new Field("FileName", fileName, Field.Store.YES, Field.Index.UN_TOKENIZED));
            doc.Add(new Field("Title", fileName, Field.Store.YES, Field.Index.TOKENIZED));
            doc.Add(new Field("Modified", modified, Field.Store.YES, Field.Index.UN_TOKENIZED));
            doc.Add(new Field("OriginalContents", originalContents, Field.Store.NO, Field.Index.NO));
            doc.Add(new Field("Contents", contents, Field.Store.YES, Field.Index.TOKENIZED));

            return doc;

        }

        #endregion
    }


}
