///
/// TODO :: Fix this method to use .NET HashTable completely
///


/// <summary> 
/// Copyright 2002-2004 The Apache Software Foundation
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
/// 
/// http://www.apache.org/licenses/LICENSE-2.0
/// 
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
/// </summary>
/// 
using System;
using System.Collections;
using System.IO;

using  Lucene.Net.Search.Highlight.Util;

using IndexReader = Lucene.Net.Index.IndexReader;
using Term = Lucene.Net.Index.Term;
using BooleanClause = Lucene.Net.Search.BooleanClause;
using BooleanQuery = Lucene.Net.Search.BooleanQuery;
using PhraseQuery = Lucene.Net.Search.PhraseQuery;
using Query = Lucene.Net.Search.Query;
using TermQuery = Lucene.Net.Search.TermQuery;
//using SpanNearQuery = org.apache.lucene.search.spans.SpanNearQuery;
using Lucene.Net.Search.Spans;

namespace Lucene.Net.Search.Highlight
{
	
	/// <summary> Utility class used to extract the terms used in a query, plus any weights.
	/// This class will not find terms for MultiTermQuery, RangeQuery and PrefixQuery classes
	/// so the caller must pass a rewritten query (see Query.rewrite) to obtain a list of 
	/// expanded terms. 
	/// 
	/// </summary>
	public sealed class QueryTermExtractor
	{
		/// <summary> 
		/// Extracts all terms texts of a given Query into an array of WeightedTerms
		/// </summary>
		/// <param name="query">Query to extract term texts from</param>
		/// <returns> an array of the terms used in a query, plus their weights.</returns>
		public static WeightedTerm[] GetTerms(Query query)
		{
			return GetTerms(query, false);
		}
		
		/// <summary> Extracts all terms texts of a given Query into an array of WeightedTerms
		/// 
		/// </summary>
		/// <param name="query">     Query to extract term texts from
		/// </param>
		/// <param name="reader">used to compute IDF which can be used to a) score selected fragments better 
		/// b) use graded highlights eg chaning intensity of font color
		/// </param>
		/// <param name="fieldName">the field on which Inverse Document Frequency (IDF) calculations are based
		/// </param>
		/// <returns> an array of the terms used in a query, plus their weights.
		/// </returns>
		public static WeightedTerm[] GetIdfWeightedTerms(Query query, IndexReader reader, string fieldName)
		{
			WeightedTerm[] terms = GetTerms(query, false, fieldName);
			int totalNumDocs = reader.NumDocs();
			for (int i = 0; i < terms.Length; i++)
			{
				try
				{
					int docFreq = reader.DocFreq(new Term(fieldName, terms[i].term));
					//IDF algorithm taken from DefaultSimilarity class
					float idf = (float) (System.Math.Log((float) totalNumDocs / (double) (docFreq + 1)) + 1.0);
					terms[i].weight *= idf;
				}
				catch (IOException e)
				{
					//ignore 
					throw e;
				}
			}
			return terms;
		}
		
		/// <summary> Extracts all terms texts of a given Query into an array of WeightedTerms
		/// 
		/// </summary>
		/// <param name="query">     Query to extract term texts from
		/// </param>
		/// <param name="prohibited"><code>true</code> to extract "prohibited" terms, too
		/// </param>
		/// <param name="fieldName"> The fieldName used to filter query terms
		/// </param>
		/// <returns> an array of the terms used in a query, plus their weights.
		/// </returns>
		public static WeightedTerm[] GetTerms(Query query, bool prohibited, string fieldName)
		{
			HashSetSupport terms = new HashSetSupport();
			if (fieldName != null)
			{
				fieldName = String.Intern(fieldName);
			}
			GetTerms(query, terms, prohibited, fieldName);
			//return (WeightedTerm[]) ICollectionSupport.ToArray(terms, new WeightedTerm[0]);
			return (WeightedTerm[]) terms.ToArray(typeof(WeightedTerm));
		}
		
		/// <summary> Extracts all terms texts of a given Query into an array of WeightedTerms
		/// 
		/// </summary>
		/// <param name="query">Query to extract term texts from</param>
		/// <param name="prohibited"><code>true</code> to extract "prohibited" terms, too</param>
		/// <returns> an array of the terms used in a query, plus their weights.
		/// </returns>
		public static WeightedTerm[] GetTerms(Query query, bool prohibited)
		{
			return GetTerms(query, prohibited, null);
		}
		
		private static void GetTerms(Query query, HashSetSupport terms, bool prohibited, string fieldName)
		{
			if (query is BooleanQuery)
				GetTermsFromBooleanQuery((BooleanQuery) query, terms, prohibited, fieldName);
			else if (query is PhraseQuery)
				GetTermsFromPhraseQuery((PhraseQuery) query, terms, fieldName);
			else if (query is TermQuery)
				GetTermsFromTermQuery((TermQuery) query, terms, fieldName);
			else if (query is SpanNearQuery)
				GetTermsFromSpanNearQuery((SpanNearQuery) query, terms, fieldName);
		}
		
		private static void GetTermsFromBooleanQuery(BooleanQuery query, HashSetSupport terms, bool prohibited, string fieldName)
		{
			BooleanClause[] queryClauses = query.GetClauses();
			int i;
			
			for (i = 0; i < queryClauses.Length; i++)
			{
				if (prohibited || !queryClauses[i].prohibited)
					GetTerms(queryClauses[i].query, terms, prohibited, fieldName);
			}
		}
		
		private static void GetTermsFromPhraseQuery(PhraseQuery query, HashSetSupport terms, string fieldName)
		{
			Term[] queryTerms = query.GetTerms();
			int i;
			
			for (i = 0; i < queryTerms.Length; i++)
			{
				if ((fieldName == null) || (queryTerms[i].Field() == (string)(object)fieldName))
				{
					terms.Add(new WeightedTerm(query.GetBoost(), queryTerms[i].Text()));
				}
			}
		}
		
		private static void GetTermsFromTermQuery(TermQuery query, HashSetSupport terms, string fieldName)
		{
			if ((fieldName == null) || (query.GetTerm().Field() == (string)(object)fieldName))
			{
				terms.Add(new WeightedTerm(query.GetBoost(), query.GetTerm().Text()));
			}
		}
		
		private static void GetTermsFromSpanNearQuery(SpanNearQuery query, HashSetSupport terms, string fieldName)
		{
			ICollection queryTerms = query.GetTerms();
			
			for (IEnumerator iterator = queryTerms.GetEnumerator(); iterator.MoveNext(); )
			{		
				// break it out for debugging.	
				Term term = (Term) iterator.Current;

				string text = term.Text();				
				if ((fieldName == null) || (term.Field() == (string)(object)fieldName))
				{
					terms.Add(new WeightedTerm(query.GetBoost(), text));
				}
			}
		}
	}
}