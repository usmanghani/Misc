using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Lucene.Net.Store;
using Lucene.Net.Documents;

namespace DotFermion.DocSources
{
    public class FileSystemDocumentSource : DocumentSource
    {
        //private FSDirectory _directory = null;
        private DirectoryInfo di = null;
        public FileSystemDocumentSource(DirectoryInfo dir)
        {
            di = dir;
        }

        #region IEnumerable<Document> Members

        public IEnumerator<Document> GetEnumerator()
        {
            foreach (FileInfo fi in di.GetFiles())
            {
                yield return _buildDocument(fi);

            }

        }

        #endregion


        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (FileInfo fi in di.GetFiles())
            {
                yield return _buildDocument(fi);

            }

        }

        #endregion

        private Document _buildDocument(FileInfo fi)
        {
            DocParsers.FileHandler handler = new DocParsers.ExtensionFileHandler();
            return handler.GetDocument(fi);

        }

    }


}
