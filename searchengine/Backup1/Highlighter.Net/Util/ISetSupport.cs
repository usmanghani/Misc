using System;
using System.Collections;

namespace Lucene.Net.Search.Highlight.Util
{
	public interface ISetSupport : ICollection, IList
	{
		new bool Add(object obj);
		bool AddAll(ICollection c);
	}
}
