using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace FluentObjectBuilder
{
	public static class AssertionExtensions
	{
		public static void force_traversal< T >( this IEnumerable< T > items )
		{
			items.Count();
		}


		public static void should_be_null( this object item )
		{
			Assert.IsNull( item );
		}


		public static void should_have_item_count_of< T >( this IEnumerable< T > items, int count )
		{
			Assert.AreEqual( count, items.Count() );
		}


		public static void should_have_same_item_count_as< T >( this IEnumerable< T > items, IEnumerable< T > expected )
		{
			Assert.AreEqual( expected.Count(), items.Count() );
		}


		public static void should_contain_no_items< T >( this IEnumerable< T > items )
		{
			items.should_have_item_count_of( 0 );
		}


		public static void should_contain_item_matching< T >( this IEnumerable< T > items, Predicate< T > condition )
		{
			Assert.IsTrue( new List< T >( items ).Exists( condition ) );
		}


		public static void should_contain< T >( this IEnumerable< T > items, T item )
		{
			Assert.IsTrue( new List< T >( items ).Contains( item ) );
		}


		public static void should_contain< T >( this IEnumerable< T > items, params T[] items_that_should_be_found )
		{
			items_that_should_be_found.each( x => should_contain( items, x ) );
		}


		public static void should_not_contain< T >( this IEnumerable< T > items, params T[] items_that_should_not_be_found )
		{
			var list = new List< T >( items );
			foreach ( T item in items_that_should_not_be_found ) Assert.IsFalse( list.Contains( item ) );
		}


		public static void should_be_greater_than< T >( this T item, T other ) where T : IComparable< T >
		{
			( item.CompareTo( other ) > 0 ).should_be_true();
		}


		public static void should_not_be_equal_to< T >( this T item, T other )
		{
			Assert.AreNotEqual( item, other );
		}


		public static void should_be_the_type< T >( this Type type )
		{
			type.should_be_equal_to( typeof ( T ) );
		}


		public static void should_be_equal_ignoring_case( this string item, string other )
		{
			Assert.AreEqual( other.ToLower(), item.ToLower() );
		}


		public static void should_only_contain< T >( this IEnumerable< T > items, params T[] itemsToFind )
		{
			var results = new List< T >( items );
			itemsToFind.Length.should_be_equal_to( items.Count() );
			foreach ( T itemToFind in itemsToFind )
				results.Contains( itemToFind ).should_be_true();
		}


		public static void should_only_contain_in_order< T >( this IEnumerable< T > items, params T[] itemsToFind )
		{
			var results = new List< T >( items );
			itemsToFind.Length.should_be_equal_to( items.Count() );
			for ( int i = 0; i < itemsToFind.Count(); i++ )
				itemsToFind[i].should_be_equal_to( results[i] );
		}


		public static void should_be_true( this bool item )
		{
			item.should_be_equal_to( true );
		}


		public static void should_be_false( this bool item )
		{
			item.should_be_equal_to( false );
		}


		public static void should_be_same_day_as( this DateTime actual, DateTime expected )
		{
			var expectedDay = new DateTime( expected.Year, expected.Month, expected.Day );
			var actualDay = new DateTime( actual.Year, actual.Month, actual.Day );
			Assert.AreEqual( expectedDay, actualDay );
		}


		public static void should_be_same_day_as( this DateTime? actual, DateTime? expected )
		{
			if ( actual.HasValue && expected.HasValue )
			{
				actual.Value.should_be_same_day_as( expected.Value );
				return;
			}

			// If one has a value but the other is null
			if ( actual.HasValue || expected.HasValue )
				Assert.Fail( "One of the dates passed in was null and the other was not." );
		}


		public static void should_be_equal_to< T >( this T actual, T expected )
		{
			Assert.AreEqual( expected, actual );
		}


		public static void should_be< T >( this T actual, T expected )
		{
			Assert.AreSame( expected, actual );
		}


		public static void should_not_throw_any_exceptions( this Action work_to_perform )
		{
			work_to_perform();
		}


		public static Type should_be_an_instance_of< Type >( this object item )
		{
			Assert.That( item, Is.InstanceOf( typeof ( Type ) ) );
			return (Type)item;
		}


		public static void should_not_be_null( this object item )
		{
			Assert.IsNotNull( item );
		}


		public static void should_be_greater_than_or_equal_to< T >( this T item, T other ) where T : IComparable< T >
		{
			( item.CompareTo( other ) >= 0 ).should_be_true();
		}


		public static void should_be_less_than< T >( this T item, T other ) where T : IComparable< T >
		{
			( item.CompareTo( other ) < 0 ).should_be_true();
		}


		public static void should_be_less_than_or_equal_to< T >( this T item, T other ) where T : IComparable< T >
		{
			( item.CompareTo( other ) <= 0 ).should_be_true();
		}
	}
}