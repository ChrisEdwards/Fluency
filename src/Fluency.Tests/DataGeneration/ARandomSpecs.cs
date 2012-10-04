using System;
using System.Collections.Generic;
using System.Linq;
using Fluency.DataGeneration;
using FluentNHibernate.Utils;
using Machine.Specifications;
using Shiloh.Utils;


namespace Fluency.Tests.DataGeneration
{
	public class ARandomSpecs
	{
		public abstract class When_generating_a_random_currency_amount_between_a_specified_max_and_min_amount
		{
			protected static int minAmount = 0;
			protected static int maxAmount = 100;
			protected static decimal result;

			private Because of = () => result = ARandom.CurrencyAmountBetween( minAmount, maxAmount );
		}


		[ Behaviors ]
		public class CurrencyIsBetweenMinAndMaxBehavior
		{
			protected static int minAmount;
			protected static int maxAmount;
			protected static decimal result;

			private It should_generate_a_number_that_is_greater_than_or_equal_to_the_min_amount = () => result.ShouldBeGreaterThanOrEqualTo( minAmount );
			private It should_generate_a_number_that_is_less_than_or_equal_to_the_max_amount = () => result.ShouldBeLessThanOrEqualTo( maxAmount );
		}


		[ Subject( typeof ( ARandom ), "CurrencyAmountBetween" ) ]
		public class When_generating_a_random_currency_amount_between_a_min_and_max_when_both_are_positive :
				When_generating_a_random_currency_amount_between_a_specified_max_and_min_amount
		{
			private Establish context = () =>
				{
					minAmount = 0;
					maxAmount = 100;
				};

			private Behaves_like< CurrencyIsBetweenMinAndMaxBehavior > currency_is_between_min_and_max;
		}


		[ Subject( typeof ( ARandom ), "CurrencyAmountBetween" ) ]
		public class When_generating_a_random_currency_amount_between_a_min_and_max_when_both_are_negative :
				When_generating_a_random_currency_amount_between_a_specified_max_and_min_amount
		{
			private Establish context = () =>
				{
					minAmount = -100;
					maxAmount = -1;
				};

			private Behaves_like< CurrencyIsBetweenMinAndMaxBehavior > currency_is_between_min_and_max;
		}


		public abstract class When_generating_a_random_currency_amount_less_than_a_specified_amount
		{
			protected static int maxAmount;
			protected static decimal result;

			private Because of = () => result = ARandom.CurrencyAmountLessThan( maxAmount );
		}


		[ Subject( typeof ( ARandom ), "CurrencyAmountLessThan" ) ]
		public class When_generating_a_random_currency_amount_less_than_a_positive_number : When_generating_a_random_currency_amount_less_than_a_specified_amount
		{
			private Establish context = () => maxAmount = 100;
			private It should_generate_an_amount_less_than_the_specified_max_amount = () => result.ShouldBeLessThanOrEqualTo( maxAmount );
		}


		[ Subject( typeof ( ARandom ), "CurrencyAmountLessThan" ) ]
		public class When_generating_a_random_currency_amount_less_than_a_negative_number : When_generating_a_random_currency_amount_less_than_a_specified_amount
		{
			private Establish context = () => maxAmount = 100;
			private It should_generate_an_amount_less_than_the_specified_max_amount = () => result.ShouldBeLessThanOrEqualTo( maxAmount );
		}


		[ Subject( typeof ( ARandom ), "StringFromCharacterSet" ) ]
		public class When_generating_a_random_string_constrained_to_a_specific_set_of_characters
		{
			private const string AllowedCharacters = "ABC123";
			private static string _string;

			private Because of = () => _string = ARandom.StringFromCharacterSet( 10, "ABC123" );

			private It should_generate_a_string_only_containing_chars_from_the_allowed_character_set = () => _string.ToCharArray().ShouldEachConformTo( character => AllowedCharacters.ToCharArray().Contains( character ) );
			private It should_fail_if_characterset_is_null = () => Catch.Exception( () => ARandom.StringFromCharacterSet( 10, null ) ).should_be_an_instance_of< ArgumentNullException >();
			private It should_fail_if_characterset_is_empty = () => Catch.Exception( () => ARandom.StringFromCharacterSet( 10, "" ) ).should_be_an_instance_of< ArgumentNullException >();
		}


		[ Subject( typeof ( ARandom ), "BirthDateForAge" ) ]
		public class When_generating_a_random_birthdate_for_a_person_10_years_old
		{
			private static DateTime result;

			private Because of = () => result = ARandom.BirthDateForAge( 10 );

			private It should_be_a_date_that_does_not_include_time_information = () => result.ShouldEqual( result.Date );
			private It should_be_less_than_or_equal_to_10_years_ago_today = () => result.should_be_less_than_or_equal_to( 10.YearsAgo().Date );
			private It should_be_greater_than_11_years_ago_today = () => result.should_be_greater_than( 11.YearsAgo().Date );
		}


		[ Subject( typeof ( ARandom ), "DateBetween" ) ]
		public class When_generating_a_random_date_between_two_datetimes
		{
			private It should_fail_if_no_date_boundary_exists_between_the_two_times = () => Catch.Exception( () => ARandom.DateBetween( DateTime.Parse( "1/1/2010 1:00:00 PM" ), DateTime.Parse( "1/1/2010 2:00:00 PM" ) ) ).should_be_an_instance_of< FluencyException >();
			private It should_fail_if_the_start_date_is_greater_than_the_end_date = () => Catch.Exception( () => ARandom.DateBetween( DateTime.Parse( "2/1/2010 1:00:00 PM" ), DateTime.Parse( "1/1/2010 2:00:00 PM" ) ) ).should_be_an_instance_of< ArgumentException >();
			private It should_be_a_date_with_no_time_information = () => ARandom.DateBetween( DateTime.Parse( "1/1/2010 2:00:00 PM" ), DateTime.Parse( "2/1/2010 2:00:00 PM" ) ).Hour.should_be_equal_to( 0 );
			private It should_be_greater_than_or_equal_to_the_min_date = () => ARandom.DateBetween( DateTime.Parse( "1/1/2010 2:00:00 PM" ), DateTime.Parse( "2/1/2010 2:00:00 PM" ) ).should_be_greater_than_or_equal_to( DateTime.Parse( "1/1/2010 2:00:00 PM" ) );
			private It should_be_less_than_or_equal_to_the_max_date = () => ARandom.DateBetween( DateTime.Parse( "1/1/2010 2:00:00 PM" ), DateTime.Parse( "2/1/2010 2:00:00 PM" ) ).should_be_less_than_or_equal_to( DateTime.Parse( "2/1/2010 2:00:00 PM" ) );
		}


		[ Subject( typeof ( ARandom ), "DateTimeBetween" ) ]
		public class When_generating_a_random_datetime_between_two_datetimes
		{
			private It should_fail_if_the_start_date_is_greater_than_the_end_date = () => Catch.Exception(() => ARandom.DateTimeBetween(DateTime.Parse("2/1/2010 1:00:00 PM"), DateTime.Parse("1/1/2010 2:00:00 PM"))).should_be_an_instance_of<ArgumentException>();
			private It should_be_greater_than_or_equal_to_the_min_date = () => ARandom.DateTimeBetween(DateTime.Parse("1/1/2010 2:00:00 PM"), DateTime.Parse("2/1/2010 2:00:00 PM")).should_be_greater_than_or_equal_to(DateTime.Parse("1/1/2010 2:00:00 PM"));
			private It should_be_less_than_or_equal_to_the_max_date = () => ARandom.DateTimeBetween(DateTime.Parse("1/1/2010 2:00:00 PM"), DateTime.Parse("2/1/2010 2:00:00 PM")).should_be_less_than_or_equal_to(DateTime.Parse("2/1/2010 2:00:00 PM"));
		}


		[ Subject( typeof ( ARandom ), "BirthDateForAge" ) ]
		public class When_generating_a_random_birth_date_given_a_persons_age
		{
			private It should_not_allow_an_age_below_1 = () => Catch.Exception( () => ARandom.BirthDateForAge( 0 ) ).should_be_an_instance_of< ArgumentOutOfRangeException >();
			private It should_not_allow_an_age_greater_than_1000 = () => Catch.Exception( () => ARandom.BirthDateForAge( 1001 ) ).should_be_an_instance_of< ArgumentOutOfRangeException >();
			private It should_return_a_date_earlier_than_the_age_number_of_years_ago = () => ARandom.BirthDateForAge( 10 ).should_be_less_than( 10.YearsAgo() );
			private It should_return_a_date_later_than_a_year_prior_to_the_age_number_of_years_ago = () => ARandom.BirthDateForAge( 10 ).should_be_greater_than( 11.YearsAgo() );
		}


		[ Subject( typeof ( ARandom ), "Age" ) ]
		public class When_generating_a_random_age
		{
			private static int result;

			private Because of = () => result = ARandom.Age();

			private It should_be_greater_than_or_equal_to_one = () => result.should_be_greater_than_or_equal_to( 1 );
			private It should_be_less_than_or_equal_to_100 = () => result.should_be_less_than_or_equal_to( 100 );
		}


		[ Subject( typeof ( ARandom ), "AdultAge" ) ]
		public class When_generating_a_random_adult_age
		{
			private static int result;

			private Because of = () => result = ARandom.AdultAge();

			private It should_be_greater_than_or_equal_to_21 = () => result.should_be_greater_than_or_equal_to( 21 );
			private It should_be_less_than_or_equal_to_65 = () => result.should_be_less_than_or_equal_to( 65 );
		}


		[ Subject( typeof ( ARandom ), "IntBetween" ) ]
		public class When_generating_a_random_integer_between_two_values
		{
			private It should_generate_value_greater_than_or_equal_to_min_value = () => ARandom.IntBetween( 1, 3 ).should_be_greater_than_or_equal_to( 1 );
			private It should_generate_value_less_than_or_equal_to_max_value = () => ARandom.IntBetween( 1, 3 ).should_be_less_than_or_equal_to( 3 );
			private It when_given_that_same_min_and_max_value_it_should_generate_that_number = () => ARandom.IntBetween( 1, 1 ).should_be_equal_to( 1 );
			private It should_accept_min_integer_value_as_min = () => ARandom.IntBetween( int.MinValue, 0 ).should_be_greater_than_or_equal_to( int.MinValue );
			private It should_accept_max_integer_value_as_max = () => ARandom.IntBetween( 0, int.MaxValue ).should_be_less_than_or_equal_to( int.MaxValue );
			private It should_accept_a_very_high_min_value = () => ARandom.IntBetween( int.MaxValue - 1, int.MaxValue ).ShouldBeGreaterThanOrEqualTo( int.MaxValue - 1 );
			private It should_accept_a_very_low_max_value = () => ARandom.IntBetween( int.MinValue, int.MinValue + 1 ).should_be_less_than_or_equal_to( int.MinValue + 1 );
			private It should_accept_min_and_max_int_values = () => ARandom.IntBetween( int.MinValue, int.MaxValue ).should_be_greater_than_or_equal_to( int.MinValue );
			private It should_fail_if_min_is_greater_than_max = () => Catch.Exception( () => ARandom.IntBetween( 10, 0 ) ).should_be_an_instance_of< ArgumentException >();
		}


		[ Subject( typeof ( ARandom ), "Title" ) ]
		public class When_generating_a_random_title
		{
			private It should_not_allow_max_chars_less_than_1 = () => Catch.Exception( () => ARandom.Title( 0 ) ).should_be_an_instance_of< ArgumentOutOfRangeException >();
			private It should_return_a_nonempty_string = () => ARandom.Title( 100 ).ShouldNotBeEmpty();
			private It should_generate_a_large_string = () => ARandom.Title( int.MaxValue ).ShouldNotBeEmpty();
			private It should_generate_a_small_string = () => ARandom.Title( 1 ).Length.should_be_equal_to( 1 );
		}


		[ Subject( typeof ( ARandom ), "Text" ) ]
		public class When_generating_a_random_text
		{
			private It should_not_allow_max_chars_less_than_1 = () => Catch.Exception( () => ARandom.Text( 0 ) ).should_be_an_instance_of< ArgumentOutOfRangeException >();
			private It should_return_a_nonempty_string = () => ARandom.Text( 100 ).ShouldNotBeEmpty();
//			private It should_generate_a_large_string = () => ARandom.Text( int.MaxValue ).ShouldNotBeEmpty();
			private It should_generate_a_small_string = () => ARandom.Text( 1 ).Length.should_be_equal_to( 1 );
		}


		// TODO: Need to remove the IList override since using List makes it fail.
		[Subject(typeof(ARandom), "ItemFrom<IList<T>>")]
		public class When_getting_a_random_item_from_a_list
		{
			private It should_fail_if_passed_a_null_list = () => Catch.Exception( () => ARandom.ItemFrom( (IList<int>)null ) ).should_be_an_instance_of< ArgumentNullException >();
			private It should_fail_if_passed_an_empty_list = () => Catch.Exception( () => ARandom.ItemFrom<int>( new List<int>() ) ).should_be_an_instance_of< ArgumentException >();
			private It should_return_one_of_the_values_passed_in_the_list = () => ARandom.ItemFrom<string>( new List< string >( new[] { "a", "b", "c" } ) ).In( "a", "b", "c" ).ShouldBeTrue();
		}


		[Subject(typeof(ARandom), "ItemFrom<T>")]
		public class When_getting_a_random_item_from_an_array_paramlist
		{
			private It should_fail_if_passed_a_null_list = () => Catch.Exception( () => ARandom.ItemFrom( (string[])null ) ).should_be_an_instance_of< ArgumentNullException >();
			private It should_fail_if_passed_an_empty_list = () => Catch.Exception( () => ARandom.ItemFrom( new string[]{} ) ).should_be_an_instance_of< ArgumentException >();
			private It should_return_one_of_the_values_passed_in_the_list = () => ARandom.ItemFrom( new[] { "a", "b", "c" } ).In( "a", "b", "c" ).ShouldBeTrue();
		}
	}
}