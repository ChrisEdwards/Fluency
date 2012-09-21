using System;
using System.Linq;
using Fluency.DataGeneration;
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

			private It should_generate_a_string_only_containing_chars_from_the_allowed_character_set =
					() => _string.ToCharArray().ShouldEachConformTo( character => AllowedCharacters.ToCharArray().Contains( character ) );
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
			private static DateTime result;
			private static DateTime _minDate;
			private static DateTime _maxDate;

			private Establish context = () =>
				{
					_minDate = ARandom.DateTime();
					_maxDate = ARandom.DateTimeAfter( _minDate );
				};

			private Because of = () => result = ARandom.DateBetween( _minDate, _maxDate );

			private It should_be_a_date_with_no_time_information = () => result.should_be_equal_to( result.Date );
			private It should_be_greater_than_or_equal_to_the_min_date = () => result.should_be_greater_than_or_equal_to( _minDate );
			private It should_be_less_than_or_equal_to_the_max_date = () => result.should_be_less_than_or_equal_to( _maxDate );
		}


		[ Subject( typeof ( ARandom ), "DateBetween" ) ]
		public class When_generating_a_random_date_between_two_times_on_the_same_day_that_dont_include_midnight
		{
			private static Exception exception;

			private Because of = () => exception = Catch.Exception( () => ARandom.DateBetween( DateTime.Parse( "1/1/2010 1:00:00 PM" ), DateTime.Parse( "1/1/2010 2:00:00 PM" ) ) );

			private It should_fail = () => exception.should_be_an_instance_of< FluencyException >();
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
	}
}