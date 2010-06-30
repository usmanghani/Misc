using System;
using System.Collections;

namespace Lucene.Net.Search.Highlight.Util
{
	[Serializable]
	public class HashSetSupport : ArrayList, ISetSupport
	{
		public HashSetSupport() : base()
		{}

		public HashSetSupport(ICollection c) 
		{
			this.AddAll(c);
		}

		public HashSetSupport(int capacity) : base(capacity)
		{}

		new public virtual bool Add(object obj)
		{
			bool inserted;

			if ((inserted = this.Contains(obj)) == false)
			{
				base.Add(obj);
			}

			return !inserted;
		}

		public bool AddAll(ICollection c)
		{
			IEnumerator e = new ArrayList(c).GetEnumerator();
			bool added = false;

			while (e.MoveNext() == true)
			{
				if (this.Add(e.Current) == true)
					added = true;
			}

			return added;
		}
	
		public override object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}