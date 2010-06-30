using System;
using System.IO;
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
	
	/// <summary> 
	/// Formats text with different color intensity depending on the score of the term.
	/// </summary>
	/// <author> maharwood </author>
	/// Modified to C# :: Joe Langley
	/// joe_langley78@hotmail.com
	/// EE 2005
	public class GradientFormatter : Formatter
	{
		private float maxScore;
		internal int fgRMin, fgGMin, fgBMin;
		internal int fgRMax, fgGMax, fgBMax;
		protected internal bool bHighlightForeground;
		internal int bgRMin, bgGMin, bgBMin;
		internal int bgRMax, bgGMax, bgBMax;
		protected internal bool bHighlightBackground;
		
		/// <summary> Sets the color range for the IDF scores
		/// 
		/// </summary>
		/// <param name="">maxScore
		/// The score (and above) displayed as maxColor (See QueryScorer.getMaxWeight 
		/// which can be used to callibrate scoring scale)
		/// </param>
		/// <param name="">strMinForegroundColor
		/// The hex color used for representing IDF scores of zero eg
		/// #FFFFFF (white) or null if no foreground color required
		/// </param>
		/// <param name="">strMaxForegroundColor
		/// The largest hex color used for representing IDF scores eg
		/// #000000 (black) or null if no foreground color required
		/// </param>
		/// <param name="">strMinBackgroundColor
		/// The hex color used for representing IDF scores of zero eg
		/// #FFFFFF (white) or null if no background color required
		/// </param>
		/// <param name="">strMaxBackgroundColor
		/// The largest hex color used for representing IDF scores eg
		/// #000000 (black) or null if no background color required
		/// </param>
		public GradientFormatter(float maxScore, string strMinForegroundColor, string strMaxForegroundColor, string strMinBackgroundColor, string strMaxBackgroundColor)
		{
			bHighlightForeground = (strMinForegroundColor != null) && (strMaxForegroundColor != null);
			if (bHighlightForeground)
			{
				if (strMinForegroundColor.Length != 7)
				{
					throw new ArgumentException("strMinForegroundColor is not 7 bytes long eg a hex " + "RGB value such as #FFFFFF");
				}
				if (strMaxForegroundColor.Length != 7)
				{
					throw new ArgumentException("strMinForegroundColor is not 7 bytes long eg a hex " + "RGB value such as #FFFFFF");
				}
				fgRMin = HexToInt(strMinForegroundColor.Substring(1, (3) - (1)));
				fgGMin = HexToInt(strMinForegroundColor.Substring(3, (5) - (3)));
				fgBMin = HexToInt(strMinForegroundColor.Substring(5, (7) - (5)));
				
				fgRMax = HexToInt(strMaxForegroundColor.Substring(1, (3) - (1)));
				fgGMax = HexToInt(strMaxForegroundColor.Substring(3, (5) - (3)));
				fgBMax = HexToInt(strMaxForegroundColor.Substring(5, (7) - (5)));
			}
			
			bHighlightBackground = (strMinBackgroundColor != null) && (strMaxBackgroundColor != null);
			if (bHighlightBackground)
			{
				if (strMinBackgroundColor.Length != 7)
				{
					throw new ArgumentException("strMinBackgroundColor is not 7 bytes long eg a hex " + "RGB value such as #FFFFFF");
				}
				if (strMaxBackgroundColor.Length != 7)
				{
					throw new ArgumentException("strMinBackgroundColor is not 7 bytes long eg a hex " + "RGB value such as #FFFFFF");
				}
				bgRMin = HexToInt(strMinBackgroundColor.Substring(1, (3) - (1)));
				bgGMin = HexToInt(strMinBackgroundColor.Substring(3, (5) - (3)));
				bgBMin = HexToInt(strMinBackgroundColor.Substring(5, (7) - (5)));
				
				bgRMax = HexToInt(strMaxBackgroundColor.Substring(1, (3) - (1)));
				bgGMax = HexToInt(strMaxBackgroundColor.Substring(3, (5) - (3)));
				bgBMax = HexToInt(strMaxBackgroundColor.Substring(5, (7) - (5)));
			}
			//        this.corpusReader = corpusReader;
			this.maxScore = maxScore;
			//        totalNumDocs = corpusReader.numDocs();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="strOriginalText"></param>
		/// <param name="tokenGroup"></param>
		/// <returns></returns>
		public virtual String HighlightTerm(string strOriginalText, TokenGroup tokenGroup)
		{
			if (tokenGroup.TotalScore == 0)
				return strOriginalText;
			float score = tokenGroup.TotalScore;
			if (score == 0)
			{
				return strOriginalText;
			}
			StringBuilder sb = new StringBuilder();
			sb.Append("<font ");
			if (bHighlightForeground)
			{
				sb.Append("color=\"");
				sb.Append(GetForegroundColorString(score));
				sb.Append("\" ");
			}
			if (bHighlightBackground)
			{
				sb.Append("bgcolor=\"");
				sb.Append(GetBackgroundColorString(score));
				sb.Append("\" ");
			}
			sb.Append(">");
			sb.Append(strOriginalText);
			sb.Append("</font>");
			return sb.ToString();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="score"></param>
		/// <returns></returns>
		protected internal virtual String GetForegroundColorString(float score)
		{
			int rVal = GetColorVal(fgRMin, fgRMax, score);
			int gVal = GetColorVal(fgGMin, fgGMax, score);
			int bVal = GetColorVal(fgBMin, fgBMax, score);
			StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("#");
			sb.Append(IntToHex(rVal));
			sb.Append(IntToHex(gVal));
			sb.Append(IntToHex(bVal));
			return sb.ToString();
		}
		
		protected internal virtual String GetBackgroundColorString(float score)
		{
			int rVal = GetColorVal(bgRMin, bgRMax, score);
			int gVal = GetColorVal(bgGMin, bgGMax, score);
			int bVal = GetColorVal(bgBMin, bgBMax, score);
			StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("#");
			sb.Append(IntToHex(rVal));
			sb.Append(IntToHex(gVal));
			sb.Append(IntToHex(bVal));
			return sb.ToString();
		}
		
		private int GetColorVal(int colorMin, int colorMax, float score)
		{
			if (colorMin == colorMax)
			{
				return colorMin;
			}
			float scale = System.Math.Abs(colorMin - colorMax);
			float relScorePercent = System.Math.Min(maxScore, score) / maxScore;
			float colScore = scale * relScorePercent;
			return System.Math.Min(colorMin, colorMax) + (int) colScore;
		}
		
		private static char[] hexDigits = new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
		
		private static String IntToHex(int i)
		{
			return "" + hexDigits[(i & 0xF0) >> 4] + hexDigits[i & 0x0F];
		}
		
		/// <summary> Converts a hex string into an int. Integer.parseInt(hex, 16) assumes the
		/// input is nonnegative unless there is a preceding minus sign. This method
		/// reads the input as twos complement instead, so if the input is 8 bytes
		/// long, it will correctly restore a negative int produced by
		/// Integer.toHexString() but not neccesarily one produced by
		/// Integer.toString(x,16) since that method will produce a string like '-FF'
		/// for negative integer values.
		/// 
		/// </summary>
		/// <param name="">hex
		/// A string in capital or lower case hex, of no more then 16
		/// characters.
		/// </param>
		/// <throws>  NumberFormatException </throws>
		/// <summary>             if the string is more than 16 characters long, or if any
		/// character is not in the set [0-9a-fA-f]
		/// </summary>
		public static int HexToInt(string hex)
		{
            return Convert.ToInt32(hex, 16);

            //int len = hex.Length;
            //if (len > 16)
            //    throw new System.FormatException();
			
            //int l = 0;
            //for (int i = 0; i < len; i++)
            //{
            //    l <<= 4;
            //    Convert.ToInt32(
            //    int c = (int)System.Char.GetNumericValue(hex[i]);
            //    if (c < 0)
            //        throw new System.FormatException();
            //    l |= c;
            //}
            //return l;
		}
	}
}