using System;
using System.Collections.Generic;
using System.Text;

namespace AdSearch
{
    class Advert
    {
        Dictionary<string, string> _map = new Dictionary<string,string>();
        public Advert()
        {
        }
        public string this[string attrib]
        {
            get
            {
                if (_map.ContainsKey(attrib))
                    return _map[attrib];
                else
                    return string.Empty;

            }
            set
            {
                if (_map.ContainsKey(attrib)) _map[attrib] = value as string;
                else _map.Add(attrib, value as string);

            }

        }

        public Dictionary<string, string> GetDictionary()
        {
            return _map;
        }

        public override string ToString()
        {
            if (_map.ContainsKey("Title"))
            {
                return _map["Title"];

            }
            else
            {
                string[] keys = new string[_map.Keys.Count];
                _map.Keys.CopyTo(keys, 0);
                return _map[keys[0]];

            }

        }

    }
}
