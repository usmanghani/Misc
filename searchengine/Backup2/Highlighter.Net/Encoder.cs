/// <summary> Copyright 2005 The Apache Software Foundation
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
/// Encodes original text. The Encoder works with the Formatter to generate the output.
/// </summary>
/// <author>  Nicko Cadell
/// </author>

using System;

namespace Lucene.Net.Search.Highlight
{
	public interface Encoder
	{
		/// <param name="strOriginalText">
		/// The section of text being output
		/// </param>
		string EncodeText(string strOriginalText);
	}
}