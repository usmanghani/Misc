using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Lucene.Net.Documents;

namespace DotFermion.DocParsers
{
    public abstract class DocumentParser
    {

        public abstract string GetContents();
        public abstract string GetTitle();
        public abstract string GetAuthor();
        public abstract string GetProperty(string prop);


        public static DocumentParser Create(FileInfo fi)
        {
            if (!Configuration.ConfigurationSettings.Initialized)
            {
                Configuration.ConfigurationSettings.LoadConfigurationInfo();

            }
            Configuration.ParserInfo pi = Configuration.ConfigurationSettings.GetParserInfo();
            
            string ext = fi.Extension;

            if (pi.ExtExists(ext.Remove(0,1)))
            {
                System.Runtime.Remoting.ObjectHandle handle = Activator.CreateInstanceFrom(pi[ext].AssemblyFile, pi[ext].TypeName);
                DocumentParser parser = (DocumentParser)handle.Unwrap();
                if (parser != null)
                {
                    return parser;

                }

            }
            return null;
            
        }

    }


}
