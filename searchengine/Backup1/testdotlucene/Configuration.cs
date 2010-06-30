using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.Specialized;

namespace DotFermion.Configuration
{
    public class ConfigurationSettings
    {
        //FileStream configStream = null;
        static Dictionary<string, string[]> configHeaderInfo = new Dictionary<string, string[]>();
        static ParserInfo pi = new ParserInfo();
        static LoggingInfo li = new LoggingInfo();

        static bool _initialized = false;

        public static bool Initialized
        {
            get { return _initialized; }
        }

        public static ParserInfo GetParserInfo()
        {
            return pi;

        }

        public static LoggingInfo GetLoggingInfo()
        {
            return li;
        }

        public static void LoadConfigurationInfo()
        {

            _initialized = false;

            configHeaderInfo.Clear();
            pi.Clear();
            li.Clear();
            
            //this.configStream = new FileStream(configFilePath, FileMode.Open, FileAccess.Read);
            //StreamReader reader = new StreamReader(configStream);
            string configFilePath = "Config.ini";
            string[] lines = File.ReadAllLines(configFilePath);
            LoadConfigHeaderInfo(lines);
            LoadParserInfo();
            LoadLoggingInfo();

            _initialized = true;


        }
        static void LoadConfigHeaderInfo(string[] lines)
        {
            
            string currentHeaderName = string.Empty;
            List<string> currentHeaderData = new List<string>();
            //string currentHeaderData = string.Empty;

            foreach (string l in lines)
            {
                string line = l.Trim();
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    if (currentHeaderName != string.Empty)
                    {
                        string[] headerData = currentHeaderData.ToArray();
                        configHeaderInfo.Add(currentHeaderName, headerData);
                        currentHeaderData.Clear();
                    }
                    
                    currentHeaderName = line.Replace("[", "").Replace("]", "");

                }
                else if (line.StartsWith("#") || (line == string.Empty))
                {
                    //this line is a comment
                    continue;
                }
                else
                {
                    currentHeaderData.Add(line);

                }

            }


        }

        static void LoadParserInfo()
        {
            string[] lines = configHeaderInfo["Parsers"];
            foreach (string line in lines)
            {
                string[] tokens = line.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string ext = tokens[0];
                string[] toks = tokens[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string assembly = toks[0];
                string type = toks[1];

                pi.AddEntry(ext, assembly, type);
                                
            }

        }
        static void LoadLoggingInfo()
        {

        }

    }
    public struct ParserTypeInfo
    {
        public ParserTypeInfo(string assembly, string type)
        {
            this.AssemblyFile = assembly;
            this.TypeName = type;

        }
        public string AssemblyFile;
        public string TypeName;

    }

    public class ParserInfo
    {
        Dictionary<string, ParserTypeInfo> extMap = new Dictionary<string, ParserTypeInfo>();
        
        public void AddEntry(string ext, string assemblyFile, string typeName)
        {
            extMap.Add(ext, new ParserTypeInfo(assemblyFile, typeName));
   
        }

        public ParserTypeInfo this[string ext]
        {
            get { return extMap[ext];  }
            set { extMap[ext] = value; }
        }
	    
        public ParserTypeInfo GetTypeInfoForExt(string ext)
        {
            return extMap[ext];

        }
        public void Clear () 
        {
            extMap.Clear();
        }

        public bool ExtExists(string ext)
        {
            return extMap.ContainsKey(ext);
        }

    }

    public class LoggingInfo
    {
        public void Clear()
        {
 	        throw new Exception("The method or operation is not implemented.");
        }
    }

}
