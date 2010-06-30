/// <summary> Copyright 2002-2005 The Apache Software Foundation
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
/// EE 2005

using System;
using System.Collections;
using System.IO;
using System.Text;
using Analyzer = Lucene.Net.Analysis.Analyzer;
using TokenStream = Lucene.Net.Analysis.TokenStream;
using PriorityQueue = Lucene.Net.Util.PriorityQueue;

namespace Lucene.Net.Search.Highlight
{
	
	/// <summary> Class used to markup highlighted terms found in the best sections of a 
	/// text, using configurable {@link Fragmenter}, {@link Scorer}, {@link Formatter}, 
	/// {@link Encoder} and tokenizers.
	/// </summary>
	/// <author>  mark@searcharea.co.uk
	/// </author>
	public class Highlighter
	{
		/// <returns> the maximum number of bytes to be tokenized per doc 
		/// </returns>
		/// <param name="byteCount">the maximum number of bytes to be tokenized per doc
		/// (This can improve performance with large documents)
		/// </param>
		public virtual int MaxDocBytesToAnalyze
		{
			get
			{
				return maxDocBytesToAnalyze;
			}
			
			set
			{
				maxDocBytesToAnalyze = value;
			}
			
		}

		public virtual Fragmenter TextFragmenter
		{
			get
			{
				return textFragmenter;
			}
			
			set
			{
				textFragmenter = value;
			}
			
		}

		public virtual Scorer FragmentScorer
		{
			get
			{
				return fragmentScorer;
			}
			
			set
			{
				fragmentScorer = value;
			}
			
		}
		public virtual Encoder Encoder
		{
			get
			{
				return encoder;
			}
			
			set
			{
				this.encoder = value;
			}
			
		}
		
		public const int DEFAULT_MAX_DOC_BYTES_TO_ANALYZE = 50 * 1024;
		private int maxDocBytesToAnalyze = DEFAULT_MAX_DOC_BYTES_TO_ANALYZE;
		private Formatter formatter;
		private Encoder encoder;
		private Fragmenter textFragmenter = new SimpleFragmenter();
		private Scorer fragmentScorer = null;
		
		public Highlighter(Scorer fragmentScorer):this(new SimpleHTMLFormatter(), fragmentScorer)
		{}
		
		
		public Highlighter(Formatter formatter, Scorer fragmentScorer):this(formatter, new DefaultEncoder(), fragmentScorer)
		{}
		
		public Highlighter(Formatter formatter, Encoder encoder, Scorer fragmentScorer)
		{
			this.formatter = formatter;
			this.encoder = encoder;
			this.fragmentScorer = fragmentScorer;
		}
		
		/// <summary> Highlights chosen terms in a text, extracting the most relevant section.
		/// This is a convenience method that calls
		/// {@link #getBestFragment(TokenStream, String)}
		/// 
		/// </summary>
		/// <param name="analyzer">  the analyzer that will be used to split <code>text</code>
		/// into chunks  
		/// </param>
		/// <param name="text">text to highlight terms in
		/// </param>
		/// <param name="fieldName">Name of field used to influence analyzer's tokenization policy 
		/// 
		/// </param>
		/// <returns> highlighted text fragment or null if no terms found
		/// </returns>
		public String GetBestFragment(Analyzer analyzer, string fieldName, string text)
		{
			TokenStream tokenStream = analyzer.TokenStream(fieldName, new StringReader(text));
			return GetBestFragment(tokenStream, text);
		}
		
		/// <summary> 
		/// Highlights chosen terms in a text, extracting the most relevant section.
		/// The document text is analysed in chunks to record hit statistics
		/// across the document. After accumulating stats, the fragment with the highest score
		/// is returned
		/// </summary>
		/// <param name="tokenStream">  a stream of tokens identified in the text parameter, including offset information. 
		/// This is typically produced by an analyzer re-parsing a document's 
		/// text. Some work may be done on retrieving TokenStreams more efficently 
		/// by adding support for storing original text position data in the Lucene
		/// index but this support is not currently available (as of Lucene 1.4 rc2).  
		/// </param>
		/// <param name="text">text to highlight terms in
		/// 
		/// </param>
		/// <returns> highlighted text fragment or null if no terms found
		/// </returns>
		public String GetBestFragment(TokenStream tokenStream, string text)
		{
			String[] results = GetBestFragments(tokenStream, text, 1);
			if (results.Length > 0)
			{
				return results[0];
			}
			return null;
		}
		
		/// <summary> Highlights chosen terms in a text, extracting the most relevant sections.
		/// This is a convenience method that calls
		/// {@link #getBestFragments(TokenStream, String, int)}
		/// 
		/// </summary>
		/// <param name="analyzer">  the analyzer that will be used to split <code>text</code> into chunks </param>
		/// <param name="text">text to highlight terms in</param>
		/// <param name="maxNumFragments"> the maximum number of fragments.
		/// 
		/// </param>
		/// <returns> highlighted text fragments (between 0 and maxNumFragments number of fragments)
		/// </returns>
		public String[] GetBestFragments(Analyzer analyzer, string text, int maxNumFragments)
		{
			TokenStream tokenStream = analyzer.TokenStream("field", new StringReader(text));
			return GetBestFragments(tokenStream, text, maxNumFragments);
		}
		
		/// <summary>
		/// Highlights chosen terms in a text, extracting the most relevant sections.
		/// The document text is analysed in chunks to record hit statistics
		/// across the document. After accumulating stats, the fragments with the highest scores
		/// are returned as an array of strings in order of score (contiguous fragments are merged into 
		/// one in their original order to improve readability)
		/// </summary>
		/// <param name="tokenStream"></param>
		/// <param name="text">text to highlight terms in</param>
		/// <param name="maxNumFragments">the maximum number of fragments.</param>
		/// <returns>highlighted text fragments (between 0 and maxNumFragments number of fragments)</returns>
		public String[] GetBestFragments(TokenStream tokenStream, string text, int maxNumFragments)
		{
			maxNumFragments = System.Math.Max(1, maxNumFragments); //sanity check
			
			TextFragment[] frag = GetBestTextFragments(tokenStream, text, true, maxNumFragments);
			
			//Get text
			ArrayList fragTexts = new ArrayList();
			for (int i = 0; i < frag.Length; i++)
			{
				if ((frag[i] != null) && (frag[i].GetScore() > 0))
				{
					fragTexts.Add(frag[i].ToString());
				}
			}
			//return (String[]) ICollectionSupport.ToArray(fragTexts, new String[0]);
			return (String[]) fragTexts.ToArray(typeof(String));
		}
		
		
		/// <summary> Low level api to get the most relevant (formatted) sections of the document.
		/// This method has been made public to allow visibility of score information held in TextFragment objects.
		/// Thanks to Jason Calabrese for help in redefining the interface.  
		/// </summary>
		/// <param name="">tokenStream
		/// </param>
		/// <param name="">text
		/// </param>
		/// <param name="">maxNumFragments
		/// </param>
		/// <param name="">mergeContiguousFragments
		/// </param>
		/// <throws>  IOException </throws>
		public TextFragment[] GetBestTextFragments(TokenStream tokenStream, string text, bool mergeContiguousFragments, int maxNumFragments)
		{
			ArrayList docFrags = new ArrayList();
			StringBuilder newText = new StringBuilder();
			
			TextFragment currentFrag = new TextFragment(newText, newText.Length, docFrags.Count);
			fragmentScorer.StartFragment(currentFrag);
			docFrags.Add(currentFrag);
			
			FragmentQueue fragQueue = new FragmentQueue(maxNumFragments);
			
			try
			{
				Lucene.Net.Analysis.Token token;
				string tokenText;
				int startOffset;
				int endOffset;
				int lastEndOffset = 0;
				textFragmenter.Start(text);
				
				TokenGroup tokenGroup = new TokenGroup();
				
				while ((token = tokenStream.Next()) != null)
				{
					if ((tokenGroup.numTokens > 0) && (tokenGroup.IsDistinct(token)))
					{
						//the current token is distinct from previous tokens - 
						// markup the cached token group info
						startOffset = tokenGroup.startOffset;
						endOffset = tokenGroup.endOffset;
						tokenText = text.Substring(startOffset, (endOffset) - (startOffset));
						string markedUpText = formatter.HighlightTerm(encoder.EncodeText(tokenText), tokenGroup);
						//store any whitespace etc from between this and last group
						if (startOffset > lastEndOffset)
							newText.Append(encoder.EncodeText(text.Substring(lastEndOffset, (startOffset) - (lastEndOffset))));
						newText.Append(markedUpText);
						lastEndOffset = endOffset;
						tokenGroup.Clear();
						
						//check if current token marks the start of a new fragment						
						if (textFragmenter.IsNewFragment(token))
						{
							currentFrag.SetScore(fragmentScorer.FragmentScore);
							//record stats for a new fragment
							currentFrag.textEndPos = newText.Length;
							currentFrag = new TextFragment(newText, newText.Length, docFrags.Count);
							fragmentScorer.StartFragment(currentFrag);
							docFrags.Add(currentFrag);
						}
					}
					
					tokenGroup.AddToken(token, fragmentScorer.GetTokenScore(token));
					
					if (lastEndOffset > maxDocBytesToAnalyze)
					{
						break;
					}
				}
				currentFrag.SetScore(fragmentScorer.FragmentScore);
				
				if (tokenGroup.numTokens > 0)
				{
					//flush the accumulated text (same code as in above loop)
					startOffset = tokenGroup.startOffset;
					endOffset = tokenGroup.endOffset;
					tokenText = text.Substring(startOffset, (endOffset) - (startOffset));
					string markedUpText = formatter.HighlightTerm(encoder.EncodeText(tokenText), tokenGroup);
					//store any whitespace etc from between this and last group
					if (startOffset > lastEndOffset)
						newText.Append(encoder.EncodeText(text.Substring(lastEndOffset, (startOffset) - (lastEndOffset))));
					newText.Append(markedUpText);
					lastEndOffset = endOffset;
				}
				
				// append text after end of last token
				//			if (lastEndOffset < text.length())
				//				newText.append(encoder.encodeText(text.substring(lastEndOffset)));
				
				currentFrag.textEndPos = newText.Length;
				
				//sort the most relevant sections of the text
				for (IEnumerator i = docFrags.GetEnumerator(); i.MoveNext(); )
				{
					currentFrag = (TextFragment) i.Current;
					
					//If you are running with a version of Lucene before 11th Sept 03
					// you do not have PriorityQueue.insert() - so uncomment the code below					
					/*
					if (currentFrag.getScore() >= minScore)
					{
					fragQueue.put(currentFrag);
					if (fragQueue.size() > maxNumFragments)
					{ // if hit queue overfull
					fragQueue.pop(); // remove lowest in hit queue
					minScore = ((TextFragment) fragQueue.top()).getScore(); // reset minScore
					}
					
					
					}
					*/
					//The above code caused a problem as a result of Christoph Goller's 11th Sept 03
					//fix to PriorityQueue. The correct method to use here is the new "insert" method
					// USE ABOVE CODE IF THIS DOES NOT COMPILE!
					fragQueue.Insert(currentFrag);
				}
				
				//return the most relevant fragments
				TextFragment[] frag = new TextFragment[fragQueue.Size()];
				for (int i = frag.Length - 1; i >= 0; i--)
				{
					frag[i] = (TextFragment) fragQueue.Pop();
				}
				
				//merge any contiguous fragments to improve readability
				if (mergeContiguousFragments)
				{
					MergeContiguousFragments(frag);
					ArrayList fragTexts = new ArrayList();
					for (int i = 0; i < frag.Length; i++)
					{
						if ((frag[i] != null) && (frag[i].GetScore() > 0))
						{
							fragTexts.Add(frag[i]);
						}
					}
					//frag = (TextFragment[]) ICollectionSupport.ToArray(fragTexts, new TextFragment[0]);
					frag = (TextFragment[]) fragTexts.ToArray(typeof(TextFragment));
				}
				
				return frag;
			}
			finally
			{
				if (tokenStream != null)
				{
					try
					{
						tokenStream.Close();
					}
					catch (Exception e)
					{
						throw e;
					}
				}
			}
		}
		
		
		/// <summary>Improves readability of a score-sorted list of TextFragments by merging any fragments 
		/// that were contiguous in the original text into one larger fragment with the correct order.
		/// This will leave a "null" in the array entry for the lesser scored fragment. 
		/// 
		/// </summary>
		/// <param name="frag">An array of document fragments in descending score
		/// </param>
		private void MergeContiguousFragments(TextFragment[] frag)
		{
			bool mergingStillBeingDone;
			if (frag.Length > 1)
				do 
				{
					mergingStillBeingDone = false; //initialise loop control flag
					//for each fragment, scan other frags looking for contiguous blocks
					for (int i = 0; i < frag.Length; i++)
					{
						if (frag[i] == null)
						{
							continue;
						}
						//merge any contiguous blocks 
						for (int x = 0; x < frag.Length; x++)
						{
							if (frag[x] == null)
							{
								continue;
							}
							if (frag[i] == null)
							{
								break;
							}
							TextFragment frag1 = null;
							TextFragment frag2 = null;
							int frag1Num = 0;
							int frag2Num = 0;
							int bestScoringFragNum;
							int worstScoringFragNum;
							//if blocks are contiguous....
							if (frag[i].Follows(frag[x]))
							{
								frag1 = frag[x];
								frag1Num = x;
								frag2 = frag[i];
								frag2Num = i;
							}
							else if (frag[x].Follows(frag[i]))
							{
								frag1 = frag[i];
								frag1Num = i;
								frag2 = frag[x];
								frag2Num = x;
							}
							//merging required..
							if (frag1 != null)
							{
								if (frag1.GetScore() > frag2.GetScore())
								{
									bestScoringFragNum = frag1Num;
									worstScoringFragNum = frag2Num;
								}
								else
								{
									bestScoringFragNum = frag2Num;
									worstScoringFragNum = frag1Num;
								}
								frag1.Merge(frag2);
								frag[worstScoringFragNum] = null;
								mergingStillBeingDone = true;
								frag[bestScoringFragNum] = frag1;
							}
						}
					}
				}
				while (mergingStillBeingDone);
		}
		
		
		/// <summary> Highlights terms in the  text , extracting the most relevant sections
		/// and concatenating the chosen fragments with a separator (typically "...").
		/// The document text is analysed in chunks to record hit statistics
		/// across the document. After accumulating stats, the fragments with the highest scores
		/// are returned in order as "separator" delimited strings.
		/// 
		/// </summary>
		/// <param name="strText">       text to highlight terms in
		/// </param>
		/// <param name="maxNumFragments"> the maximum number of fragments.
		/// </param>
		/// <param name="separator"> the separator used to intersperse the document fragments (typically "...")
		/// 
		/// </param>
		/// <returns> highlighted text
		/// </returns>
		public String GetBestFragments(TokenStream tokenStream, string strText, int maxNumFragments, string separator)
		{
			String[] sections = GetBestFragments(tokenStream, strText, maxNumFragments);
			StringBuilder result = new System.Text.StringBuilder();
			for (int i = 0; i < sections.Length; i++)
			{
				if (i > 0)
				{
					result.Append(separator);
				}
				result.Append(sections[i]);
			}
			return result.ToString();
		}
	}
	class FragmentQueue : PriorityQueue
	{
		public FragmentQueue(int size)
		{
			Initialize(size);
		}
		
		public override bool LessThan(object a, object b)
		{
			TextFragment fragA = (TextFragment) a;
			TextFragment fragB = (TextFragment) b;
			if (fragA.GetScore() == fragB.GetScore())
				return fragA.fragNum > fragB.fragNum;
			else
				return fragA.GetScore() < fragB.GetScore();
		}
	}
}