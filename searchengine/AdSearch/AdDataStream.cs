using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace AdSearch
{
    class AdDataStream
    {
        List<Advert> _adverts = new List<Advert>();
        //private int _currentPos = 0;
        private string _fileName = string.Empty;
        public AdDataStream(string fileName)
        {
            _fileName = fileName;

        }
        public void LoadData()
        {
            FileStream fs = new FileStream(_fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string contents = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            ExtractAds(contents);
            //_currentPos = 0;

        }
        private void ExtractAds(string contents)
        {

            if (contents.Trim() == string.Empty) return;

            

            const string STARTAD = "<<STARTAD>>";
            const string ENDAD = "<<ENDAD>>";

            int startindex = 0;
            int endindex = 0;
            while (startindex != -1)
            {
                startindex = contents.IndexOf("<<STARTAD>>", endindex + ENDAD.Length);
                endindex = contents.IndexOf("<<ENDAD>>", startindex + STARTAD.Length);
                if (startindex <= endindex)
                {
                    string ad = contents.Substring(startindex + STARTAD.Length, endindex - startindex);
                    string[] lines = ad.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    Advert advert = new Advert();
                    foreach (string s in lines)
                    {
                        string st = s.Trim();
                        if (st == string.Empty) continue;
                        string[] tokens = st.Split(":".ToCharArray(), 2);
                        //Debug.Assert(tokens.Length < 2);
                        if (tokens.Length < 2)
                        {
                            continue;

                        }
                        if (tokens[1].Trim() == string.Empty)
                        {
                            continue;

                        }

                        advert[tokens[0]] = tokens[1];

                    }
                    _adverts.Add(advert);
                }

            }

        }

        public IEnumerable<Advert> FetchAd()
        {
            int currentPos = 0;

            if (_adverts.Count == 0) yield break;
            for (int i = 0; i < _adverts.Count; i++)
            {
                yield return _adverts[currentPos++];
            }

        }

    }
}
