using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace xmlreadertest
{
    class Program
    {
        static void Main(string[] args)
        {
            string city = string.Empty;
            string country = string.Empty;
            string latitude = string.Empty;
            string longitude = string.Empty;
            string la_dir = string.Empty;
            string lo_dir = string.Empty;
            string green = string.Empty;
            SortedDictionary<string, XmlNode> dic = new SortedDictionary<string, XmlNode>();
            XmlDataDocument doc = new XmlDataDocument();
            doc.Load("locdata.xml");
            XmlNodeList nodes = doc.GetElementsByTagName("loctest");
            foreach (XmlNode nd in nodes)
            {
                dic.Add(nd.ChildNodes[0].InnerText, nd);

            }
            XmlNodeList mainnodes = doc.GetElementsByTagName("DataSet1");
            mainnodes[0].RemoveAll();
            foreach (string s in dic.Keys)
            {
                mainnodes[0].AppendChild(dic[s]);

            }

            doc.Save("locdata2.xml");
            bool readloc = false;
            StreamWriter writer = new StreamWriter("pagal.txt");
            XmlTextReader reader = new XmlTextReader("locdata2.xml");
            while (reader.Read())
            {
                
                if ((reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement))
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "loctest")
                    {
                        readloc = true;
                    }
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "loctest")
                    {
                        readloc = false;
                        
                    }
                    if (readloc == false && reader.Name == "loctest")
                    {
                        writer.WriteLine(city + " - " + country + " - " + latitude + " - " + longitude + " - " + green);
                        continue;
                    }

                    switch (reader.Name)
                    {
                        case "city":
                            city = reader.ReadInnerXml();
                            break;
                        case "country":
                            country = reader.ReadInnerXml();
                            break;
                        case "green":
                            green = reader.ReadInnerXml();
                            break;
                        case "latitude":
                            latitude = reader.ReadInnerXml();
                            break;
                        case "longitude":
                            longitude = reader.ReadInnerXml();
                            break;
                        case "la_dir":
                            la_dir = reader.ReadInnerXml();
                            break;
                        case "lo_dir":
                            lo_dir = reader.ReadInnerXml();
                            break;

                    }

                }

            }
            reader.Close();
            writer.Close();

        }
    }
}
