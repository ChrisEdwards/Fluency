using System;
using System.Collections.Generic;
using System.Linq;


namespace Fluency.Utils
{
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Perform the specified action on each item in the enumerable.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The items.</param>
		/// <param name="action">The action.</param>
		public static void each< T >( this IEnumerable< T > items, Action< T > action )
		{
			foreach ( T item in items ) action( item );
		}


		/// <summary>
		/// Yields an <see cref="IEnumerable{int}" />  containing the sequence of integers in the specified range.
		/// </summary>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <returns></returns>
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