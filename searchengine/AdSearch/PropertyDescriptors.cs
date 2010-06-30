using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AdSearch
{
    class PropertyDescriptors
    {
        
        Dictionary<string, string> _map = new Dictionary<string, string>();
        public PropertyDescriptors()
        {
        }

        public string this[string propertyName]
        {
            get
            {
                if (_map.ContainsKey(propertyName))
                    return _map[propertyName];
                else
                    return string.Empty;

            }
            set
            {

                if (_map.ContainsKey(propertyName))
                {
                    _map[propertyName] = value;

                }
                else
                    _map.Add(propertyName, value as string);

            }

        }

        public void LoadData(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.ChildNodes;
            foreach (XmlNode node in nodes)
            {
                string name = string.Empty;
                foreach (XmlNode child in node.ChildNodes)
                {
                    
                    if (child.Name.CompareTo("Name") == 0)
                    {
                        name = child.InnerText.Trim();

                    }
                    if (child.Name.CompareTo("Type") == 0)
                    {
                        if (name != string.Empty && name != null)
                        {
                            _map[name] = child.InnerText.Trim();

                        }

                    }


                }

            }


        }
        public Dictionary<string, string> GetDictionary()
        {
            return _map;
        }
        public string GetIndexableFormat(string propertyName, string value)
        {
            if (!_map.ContainsKey(propertyName)) return value;
            string type = _map[propertyName];
            string retval = value;
            switch (type)
            {
                case "Date":
                    System.Globalization.DateTimeFormatInfo dfi = new System.Globalization.DateTimeFormatInfo();
                    //dfi.ShortDatePattern = "YYYYMMDD";
                    DateTime dt = DateTime.Parse(value);
                    //retval = dt.ToString("s");
                    //retval = _convertDateStringToIndexable(dt.ToShortDateString());
                    retval = dt.ToString(dfi.SortableDateTimePattern);
                    break;
                case "Number":
                    long l = long.Parse(value);
                    retval = l.ToString("000000000000");
                    break;
                default:
                    break;
            }
            return retval;

        }
        public string GetDisplayableFormat(string propertyName, string value)
        {
            if (!_map.ContainsKey(propertyName)) return value;
            string type = _map[propertyName];
            string retval = value;
            switch (type)
            {
                case "Date":
                    //int year = int.Parse(value.Substring(0, 4));
                    //int month = int.Parse(value.Substring(4, 2));
                    //int day = int.Parse(value.Substring(6, 2));
                    DateTime dt = DateTime.Parse(value);
                    retval = dt.ToString("MM-dd-yyyy");
                    //retval = _convertDateStringToDisplayable(year, month, day);
                    break;
                case "Number":
                    long l = long.Parse(value);
                    retval = l.ToString();
                    break;
                default:
                    break;
            }
            return retval;
            
        }

        private string _convertDateStringToDisplayable(int year, int month, int day)
        {
            return day.ToString("00") + "/" + month.ToString("00") + "/" + year.ToString("0000");

        }

        private string _convertDateStringToIndexable(string p)
        {
            string[] tokens = p.Split("/".ToCharArray());
            Array.Reverse(tokens);
            return string.Join("", tokens);

        }
        private string _strReverse(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);

        }
        
    }
}
