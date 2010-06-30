/*
 * Copyright 2004 The Apache Software Foundation
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
/// Modified to C# :: Joe Langley
/// joe_langley78@hotmail.com
/// EE 2005

using System;

namespace Lucene.Net.Search.Highlight
{
	/// <summary>Lightweight class to hold term and a weight value used for scoring this term </summary>
	/// <author>  Mark Harwood
	/// </author>
	public class WeightedTerm
	{
		internal float weight; // multiplier
		internal string term; //stemmed form
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="weight"></param>
		/// <param name="term"></param>
		public WeightedTerm(float weight, string term)
		{
			this.weight = weight;
			this.term = term;
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual String Term
		{
			get
			{
				return term;
			}
			
			set
			{
				this.term = value;
			}
			
		}

		public virtual float Weight
		{
			get
			{
				return weight;
			}
			
			set
			{
				this.weight = value;
			}
			
		}
	}
}