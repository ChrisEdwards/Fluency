using System;
using System.Collections.Generic;
using System.Linq;


namespace FluentObjectBuilder
{
	public static class IEnumerableExtensions
	{
		public static IEnumerable< T > one_at_a_time< T >( this IEnumerable< T > items )
		{
			return items.Select( item => item );
		}


		public static void each< T >( this IEnumerable< T > items, Action< T > action )
		{
			foreach ( T item in items ) action( item );
		}


		public static IEnumerable< int > to( this int start, int end )
		{
			for ( int i = start; i <= end; i++ ) yield return i;
		}


		/// <summary>
		/// Adds the specified item to the collection so long as no other object with the same identity exists.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list">The list to add to.</param>
		/// <param name="itemToAdd">The item to add.</param>
		/// <param name="hasSameIdentityAsItemToAdd">The predicate to use to determine if any list item has same identity as item to add.</param>
		/// <returns></returns>
		public static bool AddIfUnique< T >( this ICollection< T > list, T itemToAdd, Func< T, bool > hasSameIdentityAsItemToAdd ) where T : class
		{
			if ( itemToAdd == null || list.Any( hasSameIdentityAsItemToAdd ) )
				return false;

			// Cache the object.
			list.Add( itemToAdd );
			return true;
		}
	}
}