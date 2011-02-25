using System.Collections.Generic;
using System.Linq;


namespace Shiloh.DataGeneration
{
	public class AnonymousValue
	{
		public T From< T >( IList< T > items )
		{
			return items[Anonymous.Int.Between( 0, items.Count - 1 )];
		}


		public T From< T >( IEnumerable< T > items )
		{
			int randomItemPosition = Anonymous.Int.Between(0, items.Count() - 1 );
			return items.Skip( randomItemPosition - 1 ).First();
		}


		public T From< T >( params T[] items )
		{
			return From< T >( new List< T >( items ) );
		}
	}
}