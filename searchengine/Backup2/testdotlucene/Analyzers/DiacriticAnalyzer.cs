using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;

using Lucene.Net.Analysis;

namespace DotFermion.Analyzers
{
    public class FilterData
    {
        public static string[] stopChars = { "\u061F", "\u061B", "\u060C", "\u064B", "\u064C", "\u064D", "\u064E", "\u064F", "\u0650", "\u0651", "\u0652", "\u0670", "\u06D6", "\u06D7", "\u06D8", "\u06D9", "\u06DA", "\u06DB", "\u06DC", "\u06DD", "\u06DE", "\u06Df", "\u06e0", "\u06e1", "\u06e2", "\u06e3", "\u06e4", "\u06e5", "\u06e6", "\u06e7", "\u06e8", "\u06e9", "\u06ea", "\u06eb", "\u06ec", "\u06ed", "\ufc5e", "\ufc5f", "\ufc60", "\ufc61", "\ufc62", "\ufd3e", "\ufd3f" };
        public static string[] stopWords = { "فی", "في", "من", "و", "عن", "حتی", "ثم", "علی" };
        public static bool IsPrepared = false;
        public static StringDictionary charMap = new StringDictionary();


        public static void PrepareCharMap()
        {

            charMap.Add("آ", "ا");
            charMap.Add("أ", "ا");
            charMap.Add("إ", "ا");
            charMap.Add("ٲ", "ا");
            charMap.Add("ٳ", "ا");
            charMap.Add("ٵ", "ا");
            charMap.Add("ﺁ", "ا");
            charMap.Add("ﺂ", "ﺎ");
            charMap.Add("ﺄ", "ﺎ");
            charMap.Add("ﺃ", "ا");
            charMap.Add("ﺇ", "ا");
            charMap.Add("ﺈ", "ﺎ");
            charMap.Add("ﺍ", "ا"); //replace \uFE8D with \u0627
            //charMap.Add("", "");
            //charMap.Add("", "");
            //charMap.Add("", "");
            //charMap.Add("", "");
            //charMap.Add("", "");
            //charMap.Add("", "");
            //charMap.Add("", "");
            //charMap.Add("", ""); 

            IsPrepared = true;


        }

    }

    public class DiacriticFilter : Lucene.Net.Analysis.TokenFilter
    {
        public DiacriticFilter(TokenStream stream)
            : base(stream)
        { }
        public DiacriticFilter()
            : base()
        { }

        public override Token Next()
        {
            Token t = this.input.Next();
            if (t != null)
            {
                t = new Token(_replaceDiacritics(t.TermText()), t.StartOffset(), t.EndOffset()/*, "DiacriticFiltered"*/);

            }
            return t;

        }
        private string _replaceDiacritics(string tok)
        {
            foreach (string s in FilterData.stopChars)
            {
                tok = tok.Replace(s, "");

            }
            return tok;

        }

    }
    public class HamzaFilter : Lucene.Net.Analysis.TokenFilter
    {
        public HamzaFilter(TokenStream stream)
            : base(stream)
        { }
        public HamzaFilter()
            : base()
        {
        }
        public override Token Next()
        {
            Token t = this.input.Next();
            if (!FilterData.IsPrepared)
            {
                return t;
            }

            if ( t != null )
            {
                t = new Token(_replaceHamza(t.TermText()), t.StartOffset(), t.EndOffset());

            }
            return t;



        }
        private string _replaceHamza(string tok)
        {
            foreach (string s in FilterData.charMap.Keys)
            {
                tok = tok.Replace(s, FilterData.charMap[s]);

            }
            return tok;
        }

    }

    public class DiacriticAnalyzer : Lucene.Net.Analysis.Analyzer
    {
        string[] _stopWords = new string[] { };
        public DiacriticAnalyzer()
            : base()
        {
        }

        public DiacriticAnalyzer(string[] stopWords)
            : base()
        {
            _stopWords = stopWords;
        }

        public override TokenStream TokenStream(string field, TextReader reader)
        {
            TokenStream stdStream = new Lucene.Net.Analysis.Standard.StandardAnalyzer().TokenStream(reader);
            return new StopFilter(new DiacriticFilter(new HamzaFilter(stdStream)), _stopWords);

        }

    }
}
