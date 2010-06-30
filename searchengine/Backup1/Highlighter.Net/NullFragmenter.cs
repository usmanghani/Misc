/// <summary> 
/// Copyright 2005 The Apache Software Foundation
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
using Token = Lucene.Net.Analysis.Token;

namespace Lucene.Net.Search.Highlight
{
	/// <summary> {@link Fragmenter} implementation which does not fragment the text.
	/// This is useful for highlighting the entire content of a document or field.
	/// </summary>
	public class NullFragmenter : Fragmenter
	{
		public virtual void Start(string s)
		{}
		
		public virtual bool IsNewFragment(Token token)
		{
			return false;
		}
	}
}