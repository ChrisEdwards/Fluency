using System.Collections.Generic;


namespace Shiloh.DataGeneration
{
	public class AnonymousValue
	{



		public T From<T>(IList<T> items)
		{
			return items[Anonymous.Int.BetweenExclusive(0, items.Count - 1)];
		}


		public T From<T>(params T[] items)
		{
			return From<T>(new List<T>(items));
		}
	}
}