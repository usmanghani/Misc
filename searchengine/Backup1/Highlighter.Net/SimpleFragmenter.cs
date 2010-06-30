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
using Token = Lucene.Net.Analysis.Token;

namespace Lucene.Net.Search.Highlight
{
	/// <summary> {@link Fragmenter} implementation which breaks text up into same-size 
	/// fragments with no concerns over spotting sentence boundaries.
	/// </summary>
	/// <author>  mark@searcharea.co.uk
	/// </author>
	public class SimpleFragmenter : Fragmenter
	{
		/// <returns> size in bytes of each fragment
		/// </returns>
		/// <param name="size">size in bytes of each fragment
		/// </param>
		virtual public int FragmentSize
		{
			get
			{
				return fragmentSize;
			}
			
			set
			{
				fragmentSize = value;
			}
			
		}
		private const int DEFAULT_FRAGMENT_SIZE = 100;
		private int currentNumFrags;
		private int fragmentSize;
		
		
		public SimpleFragmenter():this(DEFAULT_FRAGMENT_SIZE)
		{}
		
		/// <summary> </summary>
		/// <param name="fragmentSize">size in bytes of each fragment
		/// </param>
		public SimpleFragmenter(int fragmentSize)
		{
			this.fragmentSize = fragmentSize;
		}
		
		public virtual void Start(string originalText)
		{
			currentNumFrags = 1;
		}
		
		public virtual bool IsNewFragment(Token token)
		{
			bool isNewFrag = token.EndOffset() >= (fragmentSize * currentNumFrags);
			if (isNewFrag)
			{
				currentNumFrags++;
			}
			return isNewFrag;
		}
	}
}