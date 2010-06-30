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
	
	/// <summary> Adds to the score for a fragment based on its tokens</summary>
	public interface Scorer
	{
		/// <summary> Called when the highlighter has no more tokens for the current fragment - the scorer returns
		/// the weighting it has derived for the most recent fragment, typically based on the tokens
		/// passed to getTokenScore(). 
		/// 
		/// </summary>
		float FragmentScore
		{
			get;
		}
		/// <summary> called when a new fragment is started for consideration</summary>
		/// <param name="">newFragment
		/// </param>
		void StartFragment(TextFragment newFragment);
		
		/// <summary> Called for each token in the current fragment</summary>
		/// <param name="token">The token to be scored
		/// </param>
		/// <returns> a score which is passed to the Highlighter class to influence the mark-up of the text
		/// (this return value is NOT used to score the fragment)
		/// </returns>
		float GetTokenScore(Token token);
	}
}