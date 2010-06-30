/// <summary> Copyright 2002-2004 The Apache Software Foundation
/// 
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
using System;
using System.Collections;

using Token = Lucene.Net.Analysis.Token;
using IndexReader = Lucene.Net.Index.IndexReader;
using Query = Lucene.Net.Search.Query;

namespace Lucene.Net.Search.Highlight
{
	
	/// <summary> {@link Scorer} implementation which scores text fragments by the number of unique query terms found.
	/// This class uses the {@link QueryTermExtractor} class to process determine the query terms and 
	/// their boosts to be used. 
	/// </summary>
	/// <author>  mark@searcharea.co.uk
	/// </author>
	//TODO: provide option to boost score of fragments near beginning of document 
	// based on fragment.getFragNum()
	public class QueryScorer : Scorer
	{
		virtual public float FragmentScore
		{
			get
			{
				return totalScore;
			}
		}

		/// <summary> </summary>
		/// <returns> The highest weighted term (useful for passing to GradientFormatter to set
		/// top end of coloring scale.  
		/// </returns>
		virtual public float MaxTermWeight
		{
			get
			{
				return maxTermWeight;
			}
			
		}
		internal TextFragment currentTextFragment = null;
		internal Hashtable uniqueTermsInFragment;
		internal float totalScore = 0;
		internal float maxTermWeight = 0;
		private Hashtable termsToFind;
		
		
		/// <summary> </summary>
		/// <param name="query">a Lucene query (ideally rewritten using query.rewrite 
		/// before being passed to this class and the searcher)
		/// </param>
		public QueryScorer(Query query):this(QueryTermExtractor.GetTerms(query))
		{}
		
		/// <summary> </summary>
		/// <param name="query">a Lucene query (ideally rewritten using query.rewrite 
		/// before being passed to this class and the searcher)
		/// </param>
		/// <param name="fieldName">the Field name which is used to match Query terms
		/// </param>
		public QueryScorer(Query query, string fieldName):this(QueryTermExtractor.GetTerms(query, false, fieldName))
		{}
		
		/// <summary> </summary>
		/// <param name="query">a Lucene query (ideally rewritten using query.rewrite 
		/// before being passed to this class and the searcher)
		/// </param>
		/// <param name="reader">used to compute IDF which can be used to a) score selected fragments better 
		/// b) use graded highlights eg set font color intensity
		/// </param>
		/// <param name="fieldName">the field on which Inverse Document Frequency (IDF) calculations are based
		/// </param>
		public QueryScorer(Query query, IndexReader reader, string fieldName):this(QueryTermExtractor.GetIdfWeightedTerms(query, reader, fieldName))
		{}
		
		public QueryScorer(WeightedTerm[] weightedTerms)
		{
			termsToFind = new Hashtable();
			for (int i = 0; i < weightedTerms.Length; i++)
			{
				WeightedTerm existingTerm = (WeightedTerm) termsToFind[weightedTerms[i].term];
				if ((existingTerm == null) || (existingTerm.weight < weightedTerms[i].weight))
				{
					//if a term is defined more than once, always use the highest scoring weight
					termsToFind[weightedTerms[i].term] = weightedTerms[i];
					maxTermWeight = System.Math.Max(maxTermWeight, weightedTerms[i].Weight);
				}
			}
		}
		
		public virtual void StartFragment(TextFragment newFragment)
		{
			//uniqueTermsInFragment = new HashSetSupport();
			uniqueTermsInFragment = new Hashtable();
			currentTextFragment = newFragment;
			totalScore = 0;
		}

		public virtual float GetTokenScore(Token token)
		{
			string termText = token.TermText();
			
			WeightedTerm queryTerm = (WeightedTerm) termsToFind[termText];
			if (queryTerm == null)
			{
				//not a query term - return
				return 0;
			}
			//found a query term - is it unique in this doc?
			if (!uniqueTermsInFragment.Contains(termText))
			{
				totalScore += queryTerm.Weight;
				uniqueTermsInFragment.Add(termText, termText);
			}
			return queryTerm.Weight;
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void AllFragmentsProcessed()
		{}
	}
}