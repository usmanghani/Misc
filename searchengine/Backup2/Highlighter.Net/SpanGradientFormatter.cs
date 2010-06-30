/// <summary> 
/// Copyright 2002-2004 The Apache Software Foundation
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
	
/// <summary> 
/// Formats text with different color intensity depending on the score of the
/// term using the span tag.  GradientFormatter uses a bgcolor argument to the font tag which
/// doesn't work in Mozilla, thus this class.
/// </summary>
/// <seealso cref="GradientFormatter">
/// </seealso>
/// <author>  David Spencer dave@searchmorph.com
/// </author>

using System;
using System.Text;

namespace Lucene.Net.Search.Highlight
{	
	public class SpanGradientFormatter:GradientFormatter
	{
		public SpanGradientFormatter(float maxScore, string strMinForegroundColor, string strMaxForegroundColor, string strMinBackgroundColor, string strMaxBackgroundColor):base(maxScore, strMinForegroundColor, strMaxForegroundColor, strMinBackgroundColor, strMaxBackgroundColor)
		{}
		
		public override String HighlightTerm(string originalText, TokenGroup tokenGroup)
		{
			if (tokenGroup.TotalScore == 0)
				return originalText;
			float score = tokenGroup.TotalScore;
			if (score == 0)
			{
				return originalText;
			}
			
			// try to size sb correctly
			StringBuilder sb = new System.Text.StringBuilder(originalText.Length + EXTRA);
			
			sb.Append("<span style=\"");
			if (bHighlightForeground)
			{
				sb.Append("color: ");
				sb.Append(GetForegroundColorString(score));
				sb.Append("; ");
			}
			if (bHighlightBackground)
			{
				sb.Append("background: ");
				sb.Append(GetBackgroundColorString(score));
				sb.Append("; ");
			}
			sb.Append("\">");
			sb.Append(originalText);
			sb.Append("</span>");
			return sb.ToString();
		}
		
		// guess how much extra text we'll add to the text we're highlighting to try to avoid a  StringBuffer resize
		private const String TEMPLATE = "<span style=\"background: #EEEEEE; color: #000000;\">...</span>";
		private static readonly int EXTRA = TEMPLATE.Length;
	}
}