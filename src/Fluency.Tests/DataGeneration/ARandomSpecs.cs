using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluency.DataGeneration;
using Machine.Specifications;
using Shiloh.Utils;

namespace Fluency.Tests.Deprecated.DataGeneration
{
	public class ARandomSpecs
	{
		#region Strings

		[ Subject( typeof ( ARandom ), "Text" ) ]
		public class When_generating_a_random_text
		{
			It should_not_allow_max_chars_less_than_1 = () => Catch.Exception( () => ARandom.Text( 0 ) ).should_be_an_instance_of< ArgumentOutOfRangeException >();
			It should_return_a_nonempty_string = () => ARandom.Text( 100 ).ShouldNotBeEmpty();
//			private It should_generate_a_large_string = () => ARandom.Text( int.MaxValue ).ShouldNotBeEmpty();
			It should_generate_a_small_string = () => ARandom.Text( 1 ).Length.should_be_equal_to( 1 );
		}


		[ Subject( typeof ( ARandom ), "Title" ) ]
		public class When_generating_a_random_title
		{
			It should_not_allow_max_chars_less_than_1 = () => Catch.Exception( () => ARandom.Title( 0 ) ).should_be_an_instance_of< ArgumentOutOfRangeException >();
			It should_return_a_nonempty_string = () => ARandom.Title( 100 ).ShouldNotBeEmpty();
			It should_generate_a_large_string = () => ARandom.Title( int.MaxValue ).ShouldNotBeEmpty();
			It should_generate_a_small_string = () => ARandom.Title( 1 ).Length.should_be_equal_to( 1 );
		}


		[ Subject( typeof ( ARandom ), "StringFromCharacterSet" ) ]
		public class When_generating_a_random_string_constrained_to_a_specific_set_of_characters
		{
			const string AllowedCharacters = "ABC123";
			static string _string;

			Because of = () => _string = ARandom.StringFromCharacterSet( 10, "ABC123" );

			It should_generate_a_string_only_containing_chars_from_the_allowed_character_set = () => _string.ToCharArray().ShouldEachConformTo( character => AllowedCharacters.ToCharArray().Contains( character ) );
			It should_fail_if_characterset_is_null = () => Catch.Exception( () => ARandom.StringFromCharacterSet( 10, null ) ).should_be_an_instance_of< ArgumentNullException >();
			It should_fail_if_characterset_is_empty = () => Catch.Exception( () => ARandom.StringFromCharacterSet( 10, "" ) ).should_be_an_instance_of< ArgumentNullException >();
		}


		[ Subject( typeof ( ARandom ), "StringPattern" ) ]
		public class When_generating_a_random_string_for_a_specified_pattern
		{
			It should_fail_if_pattern_is_null = () => Catch.Exception( () => ARandom.StringPattern( null ) ).should_be_an_instance_of< ArgumentNullException >();
			It should_generate_empty_string_if_pattern_is_empty = () => ARandom.StringPattern( "" ).should_be( "" );
			It should_generate_a_number_for_each_9_found_in_the_pattern = () => ARandom.StringPattern( "999" ).ShouldMatch( "\\d\\d\\d" );
			It should_generate_a_number_for_each_pound_sign_in_the_pattern = () => ARandom.StringPattern( "###" ).ShouldMatch( "\\d\\d\\d" );
			It should_leave_other_characters_in_the_pattern_untouched = () => ARandom.StringPattern( ".##-Abc##xyZ" ).ShouldMatch( "\\.\\d\\d-Abc\\d\\dxyZ" );
		}

		#endregion


		#region Integers

		[ Subject( typeof ( ARandom ), "Int" ) ]
		public class When_generating_a_random_integer
		{
			It should_generate_a_integer = () => ARandom.Int().should_be_an_instance_of< Int32 >();
			It should_generate_a_positive_nonzero_number = () => ARandom.Int().should_be_greater_than( 0 );
		}


		[ Subject( typeof ( ARandom ), "PositiveInt" ) ]
		public class When_generating_a_random_positive_integer
		{
			It should_generate_an_integer = () => ARandom.PositiveInt().should_be_an_instance_of< Int32 >();
			It should_generate_a_positive_nonzero_number = () => ARandom.PositiveInt().should_be_greater_than( 0 );
		}


		[ Subject( typeof ( ARandom ), "IntBetween" ) ]
		public class When_generating_a_random_integer_between_two_values
		{
			It should_generate_an_integer = () => ARandom.IntBetween( 0, 100 ).should_be_an_instance_of< Int32 >();
			It should_generate_value_greater_than_or_equal_to_min_value = () => ARandom.IntBetween( 1, 3 ).should_be_greater_than_or_equal_to( 1 );
			It should_generate_value_less_than_or_equal_to_max_value = () => ARandom.IntBetween( 1, 3 ).should_be_less_than_or_equal_to( 3 );
			It when_given_that_same_min_and_max_value_it_should_generate_that_number = () => ARandom.IntBetween( 1, 1 ).should_be_equal_to( 1 );
			It should_accept_min_value_as_min = () => ARandom.IntBetween( int.MinValue, 0 ).should_be_greater_than_or_equal_to( int.MinValue );
			It should_accept_max_value_as_max = () => ARandom.IntBetween( 0, int.MaxValue ).should_be_less_than_or_equal_to( int.MaxValue );
			It should_accept_a_very_high_min_value = () => ARandom.IntBetween( int.MaxValue - 1, int.MaxValue ).ShouldBeGreaterThanOrEqualTo( int.MaxValue - 1 );
			It should_accept_a_very_low_max_value = () => ARandom.IntBetween( int.MinValue, int.MinValue + 1 ).should_be_less_than_or_equal_to( int.MinValue + 1 );
			It should_accept_min_and_max_values = () => ARandom.IntBetween( int.MinValue, int.MaxValue ).should_be_greater_than_or_equal_to( int.MinValue );
			It should_fail_if_min_is_greater_than_max = () => Catch.Exception( () => ARandom.IntBetween( 10, 0 ) ).should_be_an_instance_of< ArgumentException >();
            It should_eventually_generate_the_min_and_max_value = () =>
            {
                var generatedMin = false;
                var generatedMax = false;

                for (int i = 0; i < 100; i++)
                {
                    var result = ARandom.IntBetween(1, 3);
                    if (result == 1) generatedMin = true;
                    if (result == 3) generatedMax = true;
                }

                generatedMin.should_be_true();
                generatedMax.should_be_true();
            };
		}

		#endregion


		#region Doubles

		[Subject( typeof(ARandom), "Double")]
		public class When_generating_a_random_double
		{
			It should_generate_a_double = () => ARandom.Double().should_be_an_instance_of< Double >();
		}


		[ Subject( typeof ( ARandom ), "DoubleBetween" ) ]
		public class When_generating_a_random_double_between_two_values
		{
			It should_generate_a_double = () => ARandom.DoubleBetween( 0, 100 ).should_be_an_instance_of< Double >();
			It should_generate_value_greater_than_or_equal_to_min_value = () => ARandom.DoubleBetween( 0, double.Epsilon ).should_be_greater_than_or_equal_to( 0 );
			It should_generate_value_less_than_or_equal_to_max_value = () => ARandom.DoubleBetween( 0, double.Epsilon ).should_be_less_than_or_equal_to(  double.Epsilon );
			It when_given_that_same_min_and_max_value_it_should_generate_that_number = () => ARandom.DoubleBetween( 1.0001, 1.0001 ).should_be_equal_to( 1.0001 );
			It should_accept_min_value_as_min = () => ARandom.DoubleBetween( double.MinValue, 0 ).should_be_greater_than_or_equal_to( double.MinValue );
			It should_accept_max_value_as_max = () => ARandom.DoubleBetween( 0, double.MaxValue ).should_be_less_than_or_equal_to( double.MaxValue );
			It should_accept_a_very_high_min_value = () => ARandom.DoubleBetween( double.MaxValue - double.Epsilon, double.MaxValue ).ShouldBeGreaterThanOrEqualTo( double.MaxValue - double.Epsilon );
			It should_accept_a_very_low_max_value = () => ARandom.DoubleBetween( double.MinValue, double.MinValue + double.Epsilon ).should_be_less_than_or_equal_to( double.MinValue + double.Epsilon );
			It should_accept_min_and_max_values = () => ARandom.DoubleBetween( double.MinValue, double.MaxValue ).should_be_greater_than_or_equal_to( double.MinValue );
			It should_fail_if_min_is_greater_than_max = () => Catch.Exception( () => ARandom.DoubleBetween( 1.0001, 0 ) ).should_be_an_instance_of< ArgumentException >();
		}

		#endregion Doubles


		#region Floats (Singles)

		[Subject( typeof(ARandom), "Float")]
		public class When_generating_a_random_single
		{
			It should_generate_a_double = () => ARandom.Float().should_be_an_instance_of< float >();
		}


		[ Subject( typeof ( ARandom ), "FloatBetween" ) ]
		public class When_generating_a_random_float_between_two_values
		{
			It should_generate_a_double = () => ARandom.FloatBetween( 0, 100 ).should_be_an_instance_of< float >();
			It should_generate_value_greater_than_or_equal_to_min_value = () => ARandom.FloatBetween( 0, float.Epsilon ).should_be_greater_than_or_equal_to( 0 );
			It should_generate_value_less_than_or_equal_to_max_value = () => ARandom.FloatBetween( 0, float.Epsilon ).should_be_less_than_or_equal_to(  float.Epsilon );
			It when_given_that_same_min_and_max_value_it_should_generate_that_number = () => ARandom.DoubleBetween( 1.0001, 1.0001 ).should_be_equal_to( 1.0001 );
			It should_accept_min_value_as_min = () => ARandom.FloatBetween( float.MinValue, 0 ).should_be_greater_than_or_equal_to( float.MinValue );
			It should_accept_max_value_as_max = () => ARandom.FloatBetween( 0, float.MaxValue ).should_be_less_than_or_equal_to( float.MaxValue );
			It should_accept_a_very_high_min_value = () => ARandom.FloatBetween( float.MaxValue - float.Epsilon, float.MaxValue ).ShouldBeGreaterThanOrEqualTo( float.MaxValue - float.Epsilon );
			It should_accept_a_very_low_max_value = () => ARandom.FloatBetween( float.MinValue, float.MinValue + float.Epsilon ).should_be_less_than_or_equal_to( float.MinValue + float.Epsilon );
			It should_accept_min_and_max_values = () => ARandom.FloatBetween( float.MinValue, float.MaxValue ).should_be_greater_than_or_equal_to( float.MinValue );
			It should_fail_if_min_is_greater_than_max = () => Catch.Exception( () => ARandom.FloatBetween( float.Epsilon, 0 ) ).should_be_an_instance_of< ArgumentException >();
		}

		#endregion 


		#region Currency (Decimal)

		public abstract class When_generating_a_random_currency_amount_between_a_specified_max_and_min_amount
		{
			protected static int minAmount = 0;
			protected static int maxAmount = 100;
			protected static decimal result;

			Because of = () => result = ARandom.CurrencyAmountBetween( minAmount, maxAmount );
		}


		[ Behaviors ]
		public class CurrencyIsBetweenMinAndMaxBehavior
		{
			protected static int minAmount;
			protected static int maxAmount;
			protected static decimal result;

			It should_generate_a_number_that_is_greater_than_or_equal_to_the_min_amount = () => result.ShouldBeGreaterThanOrEqualTo( minAmount );
			It should_generate_a_number_that_is_less_than_or_equal_to_the_max_amount = () => result.ShouldBeLessThanOrEqualTo( maxAmount );
		}


		[ Subject( typeof ( ARandom ), "CurrencyAmountBetween" ) ]
		public class When_generating_a_random_currency_amount_between_a_min_and_max_when_both_are_positive :
				When_generating_a_random_currency_amount_between_a_specified_max_and_min_amount
		{
			Establish context = () =>
				{
					minAmount = 0;
					maxAmount = 100;
				};

			Behaves_like< CurrencyIsBetweenMinAndMaxBehavior > currency_is_between_min_and_max;
		}


		[ Subject( typeof ( ARandom ), "CurrencyAmountBetween" ) ]
		public class When_generating_a_random_currency_amount_between_a_min_and_max_when_both_are_negative :
				When_generating_a_random_currency_amount_between_a_specified_max_and_min_amount
		{
			Establish context = () =>
				{
					minAmount = -100;
					maxAmount = -1;
				};

			Behaves_like< CurrencyIsBetweenMinAndMaxBehavior > currency_is_between_min_and_max;
		}


		public abstract class When_generating_a_random_currency_amount_less_than_a_specified_amount
		{
			protected static int maxAmount;
			protected static decimal result;

			Because of = () => result = ARandom.CurrencyAmountLessThan( maxAmount );
		}


		[ Subject( typeof ( ARandom ), "CurrencyAmountLessThan" ) ]
		public class When_generating_a_random_currency_amount_less_than_a_positive_number : When_generating_a_random_currency_amount_less_than_a_specified_amount
		{
			Establish context = () => maxAmount = 100;
			It should_generate_an_amount_less_than_the_specified_max_amount = () => result.ShouldBeLessThanOrEqualTo( maxAmount );
		}


		[ Subject( typeof ( ARandom ), "CurrencyAmountLessThan" ) ]
		public class When_generating_a_random_currency_amount_less_than_a_negative_number : When_generating_a_random_currency_amount_less_than_a_specified_amount
		{
			Establish context = () => maxAmount = 100;
			It should_generate_an_amount_less_than_the_specified_max_amount = () => result.ShouldBeLessThanOrEqualTo( maxAmount );
		}

		#endregion


		
		[Subject(typeof(ARandom), "EnumValue")]
		public class When_selecting_a_random_enum_value
		{
			enum TestEnum
			{
				Value1,
				Value2
			}


			It should_return_the_specified_enum_type = () => ARandom.EnumValue< TestEnum >().should_be_an_instance_of< TestEnum >();
		}

		#region DateTimes

		[Subject(typeof(ARandom), "DateTime")]
		public class When_generating_a_random_datetime
		{
			It should_return_a_value_of_type_datetime = () => ARandom.DateTime().should_be_an_instance_of< DateTime >();
		}

		[Subject(typeof(ARandom), "DateTimeBefore")]
		public class When_generating_a_random_datetime_before_a_specified_datetime
		{
			It should_return_a_value_of_type_datetime = () => ARandom.DateTimeBefore( DateTime.Parse( "1/1/2010 2:00:00 PM" ) ).should_be_an_instance_of< DateTime >();
			It should_return_a_datetime_prior_to_the_specified_datetime = () => ARandom.DateTimeBefore( DateTime.Parse( "1/1/2010 2:00:00 PM" ) ).should_be_less_than( DateTime.Parse( "1/1/2010 2:00:00 PM" ) );
		}

		[Subject(typeof(ARandom), "DateTimeAfter")]
		public class When_generating_a_random_datetime_after_a_specified_datetime
		{
			It should_return_a_value_of_type_datetime = () => ARandom.DateTimeAfter( DateTime.Parse( "1/1/2010 2:00:00 PM" ) ).should_be_an_instance_of< DateTime >();
			It should_return_a_datetime_greater_than_the_specified_datetime = () => ARandom.DateTimeAfter( DateTime.Parse( "1/1/2010 2:00:00 PM" ) ).should_be_greater_than( DateTime.Parse( "1/1/2010 2:00:00 PM" ) );
		}

		[Subject(typeof(ARandom), "DateTimeInPast")]
		public class When_generating_a_random_datetime_in_the_past
		{
			It should_return_a_value_of_type_datetime = () => ARandom.DateTimeInPast().should_be_an_instance_of< DateTime >();
			It should_return_a_datetime_prior_to_now = () => ARandom.DateTimeInPast().should_be_less_than( DateTime.Now );
		}

		[Subject(typeof(ARandom), "DateTimeInPastSince")]
		public class When_generating_a_random_datetime_in_the_past_since_a_specified_date
		{
			It should_fail_if_the_specified_data_is_not_in_the_past = () => Catch.Exception( () => ARandom.DateTimeInPastSince( 2.DaysFromNow() ) ).should_be_an_instance_of< ArgumentException >();
			It should_return_a_value_of_type_datetime = () => ARandom.DateTimeInPastSince( 2.MonthsAgo() ).should_be_an_instance_of< DateTime >();
			It should_return_a_datetime_greater_than_the_specified_since_datetime = () => ARandom.DateTimeInPastSince( DateTime.Parse( "1/1/2010 2:00:00 PM" ) ).should_be_greater_than( DateTime.Parse( "1/1/2010 2:00:00 PM" ) );
			It should_return_a_datetime_prior_to_now = () => ARandom.DateTimeInPastSince( DateTime.Parse( "1/1/2010 2:00:00 PM" ) ).should_be_less_than( DateTime.Now );
		}

		[Subject(typeof(ARandom), "DateTimeInPastYear")]
		public class When_generating_a_random_datetime_in_the_past_year
		{
			It should_return_a_value_of_type_datetime = () => ARandom.DateTimeInPastYear().should_be_an_instance_of< DateTime >();
			It should_return_a_datetime_greater_than_the_specified_since_datetime = () => ARandom.DateTimeInPastYear().should_be_greater_than( 1.YearsAgo() );
			It should_return_a_datetime_prior_to_now = () => ARandom.DateTimeInPastYear().should_be_less_than( DateTime.Now );
		}

		[Subject(typeof(ARandom), "DateTimeInFuture")]
		public class When_generating_a_random_datetime_in_the_future
		{
			It should_return_a_value_of_type_datetime = () => ARandom.DateTimeInFuture().should_be_an_instance_of< DateTime >();
			It should_return_a_datetime_after_now = () => ARandom.DateTimeInFuture().should_be_greater_than( DateTime.Now );
		}

		[ Subject( typeof ( ARandom ), "DateTimeBetween" ) ]
		public class When_generating_a_random_datetime_between_two_datetimes
		{
			It should_fail_if_the_start_date_is_greater_than_the_end_date = () => Catch.Exception( () => ARandom.DateTimeBetween( DateTime.Parse( "2/1/2010 1:00:00 PM" ), DateTime.Parse( "1/1/2010 2:00:00 PM" ) ) ).should_be_an_instance_of< ArgumentException >();
			It should_be_greater_than_or_equal_to_the_min_date = () => ARandom.DateTimeBetween( DateTime.Parse( "1/1/2010 2:00:00 PM" ), DateTime.Parse( "2/1/2010 2:00:00 PM" ) ).should_be_greater_than_or_equal_to( DateTime.Parse( "1/1/2010 2:00:00 PM" ) );
			It should_be_less_than_or_equal_to_the_max_date = () => ARandom.DateTimeBetween( DateTime.Parse( "1/1/2010 2:00:00 PM" ), DateTime.Parse( "2/1/2010 2:00:00 PM" ) ).should_be_less_than_or_equal_to( DateTime.Parse( "2/1/2010 2:00:00 PM" ) );
		}

		#endregion

		#region Dates (No Time)

		[Subject(typeof(ARandom), "Date")]
		public class When_generating_a_random_date
		{
			It should_return_a_value_of_type_datetime = () => ARandom.Date().should_be_an_instance_of< DateTime >();
			It should_not_have_a_time_component = () => ARandom.Date().TimeOfDay.ShouldEqual( TimeSpan.FromTicks( 0 ) );
		}

		[Subject(typeof(ARandom), "DateAfter")]
		public class When_generating_a_random_date_after_a_specified_datetime
		{
			It should_return_a_value_of_type_datetime = () => ARandom.DateAfter( DateTime.Now ).should_be_an_instance_of< DateTime >();
			It should_not_have_a_time_component = () => ARandom.DateAfter( DateTime.Now ).TimeOfDay.ShouldEqual( TimeSpan.FromTicks( 0 ) );
			It should_not_include_the_specified_lower_bound_date = () => ARandom.DateAfter( DateTime.MaxValue.Date - 1.Days() ).ShouldEqual( DateTime.MaxValue.Date );
		}

		
		[ Subject( typeof ( ARandom ), "DateBetween" ) ]
		public class When_generating_a_random_date_between_two_datetimes
		{
			It should_fail_if_no_date_boundary_exists_between_the_two_times = () => Catch.Exception( () => ARandom.DateBetween( DateTime.Parse( "1/1/2010 1:00:00 PM" ), DateTime.Parse( "1/1/2010 2:00:00 PM" ) ) ).should_be_an_instance_of< FluencyException >();
			It should_fail_if_the_start_date_is_greater_than_the_end_date = () => Catch.Exception( () => ARandom.DateBetween( DateTime.Parse( "2/1/2010 1:00:00 PM" ), DateTime.Parse( "1/1/2010 2:00:00 PM" ) ) ).should_be_an_instance_of< ArgumentException >();
			It should_be_a_date_with_no_time_information = () => ARandom.DateBetween( DateTime.Parse( "1/1/2010 2:00:00 PM" ), DateTime.Parse( "2/1/2010 2:00:00 PM" ) ).Hour.should_be_equal_to( 0 );
			It should_be_greater_than_or_equal_to_the_min_date = () => ARandom.DateBetween( DateTime.Parse( "1/1/2010 2:00:00 PM" ), DateTime.Parse( "2/1/2010 2:00:00 PM" ) ).should_be_greater_than_or_equal_to( DateTime.Parse( "1/1/2010 2:00:00 PM" ) );
			It should_be_less_than_or_equal_to_the_max_date = () => ARandom.DateBetween( DateTime.Parse( "1/1/2010 2:00:00 PM" ), DateTime.Parse( "2/1/2010 2:00:00 PM" ) ).should_be_less_than_or_equal_to( DateTime.Parse( "2/1/2010 2:00:00 PM" ) );
		}

		#endregion

		#region Ages and Birth Dates

		[ Subject( typeof ( ARandom ), "Age" ) ]
		public class When_generating_a_random_age
		{
			static int result;

			Because of = () => result = ARandom.Age();

			It should_be_greater_than_or_equal_to_one = () => result.should_be_greater_than_or_equal_to( 1 );
			It should_be_less_than_or_equal_to_100 = () => result.should_be_less_than_or_equal_to( 100 );
		}


		[ Subject( typeof ( ARandom ), "AdultAge" ) ]
		public class When_generating_a_random_adult_age
		{
			static int result;

			Because of = () => result = ARandom.AdultAge();

			It should_be_greater_than_or_equal_to_21 = () => result.should_be_greater_than_or_equal_to( 21 );
			It should_be_less_than_or_equal_to_65 = () => result.should_be_less_than_or_equal_to( 65 );
		}


		[ Subject( typeof ( ARandom ), "BirthDateForAge" ) ]
		public class When_generating_a_random_birth_date_given_a_persons_age
		{
			It should_not_allow_an_age_below_1 = () => Catch.Exception( () => ARandom.BirthDateForAge( 0 ) ).should_be_an_instance_of< ArgumentOutOfRangeException >();
			It should_not_allow_an_age_greater_than_1000 = () => Catch.Exception( () => ARandom.BirthDateForAge( 1001 ) ).should_be_an_instance_of< ArgumentOutOfRangeException >();
			It should_return_a_date_earlier_than_the_age_number_of_years_ago = () => ARandom.BirthDateForAge( 10 ).should_be_less_than( 10.YearsAgo() );
			It should_return_a_date_later_than_a_year_prior_to_the_age_number_of_years_ago = () => ARandom.BirthDateForAge( 10 ).should_be_greater_than( 11.YearsAgo() );
		}


		[ Subject( typeof ( ARandom ), "BirthDateForAge" ) ]
		public class When_generating_a_random_birthdate_for_a_person_10_years_old
		{
			static DateTime result;

			Because of = () => result = ARandom.BirthDateForAge( 10 );

			It should_be_a_date_that_does_not_include_time_information = () => result.ShouldEqual( result.Date );
			It should_be_less_than_or_equal_to_10_years_ago_today = () => result.should_be_less_than_or_equal_to( 10.YearsAgo().Date );
			It should_be_greater_than_11_years_ago_today = () => result.should_be_greater_than( 11.YearsAgo().Date );
		}

		#endregion


		// TODO: Need to remove the IList override since using List makes it fail.
		[ Subject( typeof ( ARandom ), "ItemFrom<IList<T>>" ) ]
		public class When_getting_a_random_item_from_a_list
		{
			It should_fail_if_passed_a_null_list = () => Catch.Exception( () => ARandom.ItemFrom( (IList< int >)null ) ).should_be_an_instance_of< ArgumentNullException >();
			It should_fail_if_passed_an_empty_list = () => Catch.Exception( () => ARandom.ItemFrom< int >( new List< int >() ) ).should_be_an_instance_of< ArgumentException >();
			It should_return_one_of_the_values_passed_in_the_list = () => ARandom.ItemFrom< string >( new List< string >( new[] { "a", "b", "c" } ) ).In( "a", "b", "c" ).ShouldBeTrue();
		}


		[ Subject( typeof ( ARandom ), "ItemFrom<T>" ) ]
		public class When_getting_a_random_item_from_an_array_paramlist
		{
			It should_fail_if_passed_a_null_list = () => Catch.Exception( () => ARandom.ItemFrom( (string[])null ) ).should_be_an_instance_of< ArgumentNullException >();
			It should_fail_if_passed_an_empty_list = () => Catch.Exception( () => ARandom.ItemFrom( new string[] { } ) ).should_be_an_instance_of< ArgumentException >();
			It should_return_one_of_the_values_passed_in_the_list = () => ARandom.ItemFrom( new[] { "a", "b", "c" } ).In( "a", "b", "c" ).ShouldBeTrue();
		}


		[ Subject( typeof ( ARandom ), "Int" ) ]
		public class When_getting_a_random_int_from_multiple_threads_at_the_same_time
		{
		    It should_return_all_unique_values = () =>
		    {
		        BlockingCollection<int> values = new BlockingCollection<int>();
                Parallel.For( 0, 100,
			                                 new ParallelOptions { MaxDegreeOfParallelism = 10 },
			                                 ( i, loop ) => values.Add( ARandom.IntBetween( int.MinValue, int.MaxValue ) ) );
                
		        values.Distinct().Count().should_be_equal_to(100);
		    };
		}


		[ Subject( typeof ( ARandom ), "AddressLine1" ) ]
		public class When_getting_a_random_address_line_1
		{
			It should_return_a_non_empty_string = () => ARandom.AddressLine1().ShouldNotBeEmpty();
			It should_start_with_a_number = () => ARandom.AddressLine1().ShouldMatch( "^\\d+" );
		}


		[ Subject( typeof ( ARandom ), "StreetName" ) ]
		public class When_getting_a_random_street_name
		{
			It should_return_a_non_empty_string = () => ARandom.StreetName().ShouldNotBeEmpty();
		}
	}
}