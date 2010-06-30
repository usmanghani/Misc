using System;
using System.Text;

namespace Lucene.Net.Search.Highlight
{
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
	
	/// <summary> Simple {@link Formatter} implementation to highlight terms with a pre and post tag</summary>
	/// <author>  MAHarwood
	/// 
	/// </author>
	public class SimpleHTMLFormatter : Formatter
	{
		internal string preTag;
		internal string postTag;

		public SimpleHTMLFormatter(string preTag, string postTag)
		{
			this.preTag = preTag;
			this.postTag = postTag;
		}
		
		/// <summary> 
		/// Default constructor uses HTML: &lt;B&gt; tags to markup terms
		/// </summary>
		public SimpleHTMLFormatter()
		{
			this.preTag = "<B>";
			this.postTag = "</B>";
		}
		
		public virtual String HighlightTerm(string originalText, TokenGroup tokenGroup)
		{
			StringBuilder returnBuffer;
			if (tokenGroup.TotalScore > 0)
			{
				returnBuffer = new StringBuilder();
				returnBuffer.Append(preTag);
				returnBuffer.Append(originalText);
				returnBuffer.Append(postTag);
				return returnBuffer.ToString();
			}
			return originalText;
		}
	}
}