using System;
using System.Collections.Generic;
using System.Text;

using Lucene.Net.QueryParsers;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.Index;

namespace IndexDBTables
{
    public class MyQueryParser : MultiFieldQueryParser
    {
        public MyQueryParser(string field, Lucene.Net.Analysis.Analyzer analyzer)
            : base(field, analyzer)
        {

        }
        public MyQueryParser(string[] fields, Lucene.Net.Analysis.Analyzer analyzer)
            : base(fields, analyzer)
        {

        }

        protected override Lucene.Net.Search.Query GetRangeQuery(string field, Lucene.Net.Analysis.Analyzer analyzer, string part1, string part2, bool inclusive)
        {
            if (field.ToLower() == "sid" || field.ToLower() == "pid" || field.ToLower() == "ayatno")
            {
                try
                {
                    long num1 = long.Parse(part1);
                    long num2 = long.Parse(part2);
                    return new RangeQuery(new Term(field, NumberTools.LongToString(num1)), new Term(field, NumberTools.LongToString(num2)), inclusive);

                }
                catch (Exception ex)
                {
                    throw new ParseException(ex.Message);
                }

            }

            return base.GetRangeQuery(field, analyzer, part1, part2, inclusive);
        }
    }
}
