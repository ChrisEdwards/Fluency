using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluency.DataGeneration;
using Fluency.Utils;
using FluentAssertions;
using Xunit;

namespace Fluency.Tests.DataGeneration
{
    public class ARandomTests
    {
        public class When_generating_a_random_text
        {
            [Fact]
            public void should_not_allow_max_chars_less_than_1()
            {
                Action randomTextOfZero = () => { ARandom.Text(0); };
                randomTextOfZero.ShouldThrow<ArgumentOutOfRangeException>();
            }

            [Fact]
            public void should_return_a_nonempty_string()
            {
                ARandom.Text(100).Should().NotBeEmpty();
            }

            [Theory]
            [InlineData(2)]
            [InlineData(100)]
            public void should_generate_a_string_of_the_specified_size_string(int length)
            {
                ARandom.Text(length).Length.Should().Be(length);
            }
        }

        public class When_generating_a_random_title
        {
            [Fact]
            public void should_not_allow_max_chars_less_than_1()
            {
                Action randomTitleOfZero = () => { ARandom.Title(0); };
                randomTitleOfZero.ShouldThrow<ArgumentOutOfRangeException>();
            }

            [Fact]
            public void should_return_a_nonempty_string()
            {
                ARandom.Title(100).Should().NotBeEmpty();
            }

            [Theory]
            [InlineData(2)]
            [InlineData(100)]
            public void should_generate_a_string_of_up_to_the_specified_size_string(int length)
            {
                ARandom.Title(length).Length.Should().BeInRange(1, length);
            }
        }

        public class When_generating_a_random_string_constrained_to_a_specific_set_of_characters
        {
            const string AllowedCharacters = "ABC123";
            static string _string;

            public When_generating_a_random_string_constrained_to_a_specific_set_of_characters()
            {
                _string = ARandom.StringFromCharacterSet(10, "ABC123");
            }

            [Fact]
            public void should_generate_a_string_only_containing_chars_from_the_allowed_character_set()
            {
                _string.ToCharArray().Should().BeSubsetOf(AllowedCharacters);
            }

            [Fact]
            public void should_fail_if_characterset_is_null()
            {
                Catch.Exception(() => ARandom.StringFromCharacterSet(10, null)).Should()
                    .BeOfType<ArgumentNullException>();
            }

            [Fact]
            public void should_fail_if_characterset_is_empty()
            {
                Catch.Exception(() =>
                    ARandom.StringFromCharacterSet(10, "")).Should().BeOfType<ArgumentNullException>();
            }
        }

        public class When_generating_a_random_string_for_a_specified_pattern
        {
            [Fact]
            public void should_fail_if_pattern_is_null()
            {
                Catch.Exception(() => ARandom.StringPattern(null)).Should().BeOfType<ArgumentNullException>();
            }

            [Fact]
            public void should_generate_empty_string_if_pattern_is_empty()
            {
                ARandom.StringPattern("").Should().Be("");
            }

            [Fact]
            public void should_generate_a_number_for_each_9_found_in_the_pattern()
            {
                ARandom.StringPattern("999").Should().MatchRegex("\\d\\d\\d");
            }

            [Fact]
            public void should_generate_a_number_for_each_pound_sign_in_the_pattern()
            {
                ARandom.StringPattern("###").Should().MatchRegex("\\d\\d\\d");
            }

            [Fact]
            public void should_leave_other_characters_in_the_pattern_untouched()
            {
                ARandom.StringPattern(".##-Abc##xyZ").Should().MatchRegex("\\.\\d\\d-Abc\\d\\dxyZ");
            }
        }

        public class When_generating_a_random_integer
        {
            [Fact]
            public void should_generate_a_positive_nonzero_number()
                => ARandom.Int().Should().BeGreaterThan(0);
        }

        public class When_generating_a_random_positive_integer
        {
            [Fact]
            public void should_generate_a_positive_nonzero_number() =>
                ARandom.PositiveInt().Should().BeGreaterThan(0);
        }

        public class When_generating_a_random_integer_between_two_values
        {
            [Fact]
            public void should_generate_value_greater_than_or_equal_to_min_value() =>
                ARandom.IntBetween(1, 3).Should().BeGreaterOrEqualTo(1);

            [Fact]
            public void should_generate_value_less_than_or_equal_to_max_value() =>
                ARandom.IntBetween(1, 3).Should().BeLessOrEqualTo(3);

            [Fact]
            public void when_given_that_same_min_and_max_value_it_should_generate_that_number() =>
                ARandom.IntBetween(3, 3).Should().Be(3);

            [Fact]
            public void should_accept_a_very_high_min_value() =>
                ARandom.IntBetween(Int32.MaxValue - 1, Int32.MaxValue).Should().BeGreaterOrEqualTo(Int32.MaxValue - 1);

            [Fact]
            public void should_accept_a_very_low_max_value() =>
                ARandom.IntBetween(Int32.MinValue, Int32.MinValue + 1).Should().BeLessOrEqualTo(Int32.MinValue + 1);

            [Fact]
            public void should_fail_if_min_is_greater_than_max() =>
                Catch.Exception(() => ARandom.IntBetween(10, 0)).Should().BeOfType<ArgumentException>();

            [Fact]
            public void should_eventually_generate_the_min_and_max_value()
            {
                var generatedMin = false;
                var generatedMax = false;

                while (!generatedMin || !generatedMax)
                {
                    var result = ARandom.IntBetween(1, 3);
                    if (result == 1) generatedMin = true;
                    if (result == 3) generatedMax = true;
                }

                generatedMin.Should().BeTrue();
                generatedMax.Should().BeTrue();
            }
        }

        public class When_generating_a_random_double
        {
            [Fact]
            public void should_generate_a_double() => ARandom.Double().Should().BeGreaterOrEqualTo(0D);        
        }
        
        public class When_generating_a_random_double_between_two_values
        {
            [Fact]
            public void should_generate_value_greater_than_or_equal_to_min_value() =>
                ARandom.DoubleBetween(0, double.Epsilon).Should().BeGreaterOrEqualTo(0);
            
            [Fact]
            public void should_generate_value_less_than_or_equal_to_max_value() =>
                ARandom.DoubleBetween(0, double.Epsilon).Should().BeLessOrEqualTo(double.Epsilon);

            [Fact]
            public void when_given_that_same_min_and_max_value_it_should_generate_that_number() =>
                ARandom.DoubleBetween(15, 15).Should().BeLessOrEqualTo(15);

            [Fact]
            public void should_accept_min_value_as_min() =>
                ARandom.DoubleBetween(double.MinValue, 15).Should().BeGreaterOrEqualTo(double.MinValue);

            [Fact]
            public void should_accept_max_value_as_max() =>
                ARandom.DoubleBetween(0, double.MaxValue).Should().BeLessOrEqualTo(double.MaxValue);

            [Fact]
            public void should_accept_a_very_high_min_value() =>
                ARandom.DoubleBetween(double.MaxValue - double.Epsilon, double.MaxValue).Should().BeGreaterOrEqualTo(double.MaxValue - double.Epsilon);

            [Fact]
            public void should_accept_a_very_low_max_value() =>
                ARandom.DoubleBetween(double.MinValue, double.MinValue + double.Epsilon).Should().BeLessOrEqualTo(double.MinValue + double.Epsilon);

            [Fact]
            public void should_accept_min_and_max_values() =>
                ARandom.DoubleBetween(double.MinValue, double.MaxValue).Should().BeGreaterOrEqualTo(double.MinValue);

            [Fact]
            public void should_fail_if_min_is_greater_than_max() =>
                Catch.Exception(() => ARandom.DoubleBetween(1.0001, 0)).Should().BeOfType<ArgumentException>();
        }
        
        public class When_generating_a_random_single
        {
            [Fact]
            public void should_generate_a_float() =>
                ARandom.Float().Should().BeOfType(typeof(float));                 
        }

        public class When_generating_a_random_float_between_two_values
        {
            [Fact]
            public void should_generate_a_float() =>
                ARandom.FloatBetween(0, 100).Should().BeOfType(typeof(float));

            [Fact]
            public void should_generate_value_greater_than_or_equal_to_min_value() =>
                ARandom.FloatBetween(0, float.Epsilon).Should().BeGreaterOrEqualTo(0);

            [Fact]
            public void should_generate_value_less_than_or_equal_to_max_value() =>
                ARandom.FloatBetween(0, float.Epsilon).Should().BeLessOrEqualTo(float.Epsilon);

            [Fact]
            public void when_given_that_same_min_and_max_value_it_should_generate_that_number() =>
                ARandom.DoubleBetween(1.0001, 1.0001).Should().Be(1.0001);

            [Fact]
            public void should_accept_min_and_max_values() =>
                ARandom.FloatBetween(float.MinValue, float.MaxValue).Should().BeGreaterOrEqualTo(float.MinValue);

            [Fact]
            public void should_fail_if_min_is_greater_than_max() =>
                Catch.Exception(() => ARandom.FloatBetween(float.Epsilon, 0)).Should().BeOfType<ArgumentException>();            
        }

        public class When_generating_a_random_currency_amount_between_a_specified_max_and_min_amount
        {
            [Theory]
            [InlineData(0, 100)]
            [InlineData(-100, -1)]
            public void should_generate_a_number_that_is_greater_than_or_equal_to_the_min_amount(
                int minAmount, int maxAmount)
            {
                ARandom.CurrencyAmountBetween(minAmount, maxAmount)
                    .Should().BeGreaterOrEqualTo(minAmount)
                    .And.BeLessOrEqualTo(maxAmount);
            }            
        }

        public class When_generating_a_random_currency_amount_less_than_a_specified_amount
        {
            [Fact]
            public void when_max_amount_is_positive_should_generate_an_amount_less_than_the_specified_max_amount() =>
                ARandom.CurrencyAmountLessThan(100).Should().BeLessThan(100);
        
            [Fact]
            public void when_max_amount_is_negative_should_throw_an_argument_exception() =>
                Catch.Exception(() => ARandom.CurrencyAmountLessThan(-100)).Should().BeOfType<ArgumentException>();
        }

        public class When_selecting_a_random_enum_value
        {
            enum TestEnum
            {
                Value1,
                Value2
            }

            [Fact]
            public void should_return_the_specified_enum_type() =>
                ARandom.EnumValue<TestEnum>().Should().BeOfType<TestEnum>();
        }

        public class When_generating_a_random_datetime_before_a_specified_datetime
        {
            [Fact]
            public void should_return_a_datetime_prior_to_the_specified_datetime() =>
                ARandom.DateTimeBefore(DateTime.Parse("1/1/2010 2:00:00 PM"))
                .Should().BeBefore(DateTime.Parse("1/1/2010 2:00:00 PM"));
        }
        
        public class When_generating_a_random_datetime_after_a_specified_datetime
        {
            [Fact]
            public void should_return_a_datetime_greater_than_the_specified_datetime() =>
                ARandom.DateTimeAfter(DateTime.Parse("1/1/2010 2:00:00 PM"))
                .Should().BeAfter(DateTime.Parse("1/1/2010 2:00:00 PM"));
        }
        
        public class When_generating_a_random_datetime_in_the_past
        {
            [Fact]
            public void should_return_a_datetime_prior_to_now() =>
                ARandom.DateTimeInPast().Should().BeBefore(DateTime.Now);
        }
        
        public class When_generating_a_random_datetime_in_the_past_since_a_specified_date
        {
            [Fact]
            public void should_fail_if_the_specified_data_is_not_in_the_past() =>
                Catch.Exception(() => ARandom.DateTimeInPastSince(2.DaysFromNow()))
                .Should().BeOfType<ArgumentException>();

            [Fact]
            public void should_return_a_datetime_greater_than_the_specified_since_datetime() =>
                ARandom.DateTimeInPastSince(DateTime.Parse("1/1/2010 2:00:00 PM"))
                .Should().BeAfter(DateTime.Parse("1/1/2010 2:00:00 PM"));

            [Fact]
            public void should_return_a_datetime_prior_to_now() =>
                ARandom.DateTimeInPastSince(DateTime.Parse("1/1/2010 2:00:00 PM"))
                .Should().BeBefore(DateTime.Now);
        }
        
        public class When_generating_a_random_datetime_in_the_past_year
        {
            [Fact]
            public void should_return_a_datetime_greater_than_the_specified_since_datetime() =>
                ARandom.DateTimeInPastYear().Should().BeAfter(1.YearsAgo());
            
            [Fact]
            public void should_return_a_datetime_prior_to_now () =>
                ARandom.DateTimeInPastYear().Should().BeBefore(DateTime.Now);
        }
        
        public class When_generating_a_random_datetime_in_the_future
        {
            [Fact]
            public void should_return_a_datetime_after_now() =>
                ARandom.DateTimeInFuture().Should().BeAfter(DateTime.Now);
        }
        
        public class When_generating_a_random_datetime_between_two_datetimes
        {
            [Fact]
            public void should_fail_if_the_start_date_is_greater_than_the_end_date() =>
                Catch.Exception(() => ARandom.DateTimeBetween(DateTime.Parse("2/1/2010 1:00:00 PM"), 
                    DateTime.Parse("1/1/2010 2:00:00 PM"))).Should().BeOfType<ArgumentException>();

            [Fact]
            public void should_be_greater_than_or_equal_to_the_min_date() =>
                ARandom.DateTimeBetween(
                    DateTime.Parse("1/1/2010 2:00:00 PM"), 
                    DateTime.Parse("2/1/2010 2:00:00 PM"))
                .Should().BeOnOrAfter(DateTime.Parse("1/1/2010 2:00:00 PM"));

            [Fact]
            public void should_be_less_than_or_equal_to_the_max_date() =>
                ARandom.DateTimeBetween(
                    DateTime.Parse("1/1/2010 2:00:00 PM"), 
                    DateTime.Parse("2/1/2010 2:00:00 PM"))
                .Should().BeOnOrBefore(DateTime.Parse("2/1/2010 2:00:00 PM"));
        }

        public class When_generating_a_random_date
        {
            [Fact]
            public void should_not_have_a_time_component() => 
                ARandom.Date().TimeOfDay.Should().Be(TimeSpan.FromTicks(0));
        }

        public class When_generating_a_random_date_after_a_specified_datetime
        {
            [Fact]
            public void should_not_have_a_time_component() =>
                ARandom.DateAfter(DateTime.Now).TimeOfDay.Should().Be(TimeSpan.FromTicks(0));

            [Fact]
            public void should_not_include_the_specified_lower_bound_date() =>
                ARandom.DateAfter(DateTime.MaxValue.Date - 1.Days()).Should().Be(DateTime.MaxValue.Date);
        }

        public class When_generating_a_random_date_between_two_datetimes
        {
            [Fact]
            public void should_fail_if_no_date_boundary_exists_between_the_two_times() =>
                Catch.Exception(() => ARandom.DateBetween(
                    DateTime.Parse("1/1/2010 1:00:00 PM"), 
                    DateTime.Parse("1/1/2010 2:00:00 PM")))
                .Should().BeOfType<FluencyException>();

            [Fact]
            public void should_fail_if_the_start_date_is_greater_than_the_end_date() =>
                Catch.Exception(() => ARandom.DateBetween(
                    DateTime.Parse("2/1/2010 1:00:00 PM"), 
                    DateTime.Parse("1/1/2010 2:00:00 PM")))
                .Should().BeOfType<ArgumentException>();

            [Fact]
            public void should_be_a_date_with_no_time_information() =>
                ARandom.DateBetween(
                    DateTime.Parse("1/1/2010 2:00:00 PM"), 
                    DateTime.Parse("2/1/2010 2:00:00 PM")).Hour.Should().Be(0);

            [Fact]
            public void should_be_greater_than_or_equal_to_the_min_date() =>
                ARandom.DateBetween(
                    DateTime.Parse("1/1/2010 2:00:00 PM"), 
                    DateTime.Parse("2/1/2010 2:00:00 PM"))
                .Should().BeOnOrAfter(DateTime.Parse("1/1/2010 2:00:00 PM"));

            [Fact]
            public void should_be_less_than_or_equal_to_the_max_date() =>
                ARandom.DateBetween(
                    DateTime.Parse("1/1/2010 2:00:00 PM"), 
                    DateTime.Parse("2/1/2010 2:00:00 PM"))
                .Should().BeOnOrBefore(DateTime.Parse("2/1/2010 2:00:00 PM"));
        }

        public class When_generating_a_random_age
        {
            [Fact]
            public void should_be_greater_than_or_equal_to_one() => 
                ARandom.Age().Should().BeGreaterOrEqualTo(1);

            [Fact]
            public void should_be_less_than_or_equal_to_100() =>
                ARandom.Age().Should().BeLessOrEqualTo(100);            
        }

        public class When_generating_a_random_adult_age
        {
            [Fact]
            public void should_be_greater_than_or_equal_to_21() =>
                ARandom.AdultAge().Should().BeGreaterOrEqualTo(21);

            [Fact]
            public void should_be_less_than_or_equal_to_65() =>
                ARandom.AdultAge().Should().BeLessOrEqualTo(65);            
        }

        public class When_generating_a_random_birth_date_given_a_persons_age
        {
            [Fact]
            public void should_not_allow_an_age_below_1 () =>
                Catch.Exception(() => ARandom.BirthDateForAge(0)).Should().BeOfType<ArgumentOutOfRangeException>();

            [Fact]
            public void should_not_allow_an_age_greater_than_1000() =>
                Catch.Exception(() => ARandom.BirthDateForAge(1001)).Should().BeOfType<ArgumentOutOfRangeException>();
            
            [Fact]
            public void should_return_a_date_earlier_than_the_age_number_of_years_ago() =>
                ARandom.BirthDateForAge(10).Should().BeBefore(10.YearsAgo());

            [Fact]
            public void should_return_a_date_later_than_a_year_prior_to_the_age_number_of_years_ago() =>
                ARandom.BirthDateForAge(10).Should().BeAfter(11.YearsAgo());
        }

        public class When_generating_a_random_birthdate_for_a_person_10_years_old
        {
            [Fact]
            public void should_be_a_date_that_does_not_include_time_information() =>
                ARandom.BirthDateForAge(10).TimeOfDay.Should().Be(TimeSpan.FromTicks(0));
            
            [Fact]
            public void should_be_less_than_or_equal_to_10_years_ago_today() =>
                ARandom.BirthDateForAge(10).Should().BeOnOrBefore(10.YearsAgo().Date);

            [Fact]
            public void should_be_greater_than_11_years_ago_today() =>
                ARandom.BirthDateForAge(10).Should().BeAfter(11.YearsAgo().Date);
        }

        // TODO: Need to remove the IList override since using List makes it fail.
        public class When_getting_a_random_item_from_a_list
        {
            [Fact]
            public void should_fail_if_passed_a_null_list() =>
                Catch.Exception(() => ARandom.ItemFrom((IList<int>)null)).Should().BeOfType<ArgumentNullException>();

            [Fact]
            public void should_fail_if_passed_an_empty_list() =>
                Catch.Exception(() => ARandom.ItemFrom<int>(new List<int>())).Should().BeOfType<ArgumentException>();

            [Fact]
            public void should_return_one_of_the_values_passed_in_the_list() =>
                ARandom.ItemFrom<string>(new List<string>(new[] {"a", "b", "c"}))
                    .Should().BeOneOf("a", "b", "c");
        }

        
        public class When_getting_a_random_item_from_an_array_paramlist
        {
            [Fact]
            public void should_fail_if_passed_a_null_list() =>
                Catch.Exception(() => ARandom.ItemFrom((string[])null)).Should().BeOfType<ArgumentNullException>();
            
            [Fact]
            public void should_fail_if_passed_an_empty_list() =>
                Catch.Exception(() => ARandom.ItemFrom(new string[] { })).Should().BeOfType<ArgumentException>();

            [Fact]
            public void should_return_one_of_the_values_passed_in_the_list() =>
                ARandom.ItemFrom(new[] {"a", "b", "c"}).Should().BeOneOf(new[] {"a", "b", "c"});
        }
        
        public class When_getting_a_random_int_from_multiple_threads_at_the_same_time
        {
            [Fact]
            public void should_return_mostly_unique_values()
            {
                BlockingCollection<int> values = new BlockingCollection<int>();
                Parallel.For(0, 100, 
                    new ParallelOptions { MaxDegreeOfParallelism = 10 },
                    (i, loop) => values.Add(ARandom.IntBetween(int.MinValue, int.MaxValue)));

                // Allow for occasional duplicate
                values.Distinct().Count().Should().BeInRange(99, 100);
            }
        }

        public class When_getting_a_random_address_line_1
        {
            [Fact]
            public void should_return_a_non_empty_string() =>
                ARandom.AddressLine1().Should().NotBeEmpty();

            [Fact]
            public void should_start_with_a_number() => 
                ARandom.AddressLine1().Should().MatchRegex("^\\d+");
        }

        public class When_getting_a_random_street_name
        {
            [Fact]
            public void should_return_a_non_empty_string() =>
                ARandom.StreetName().Should().NotBeEmpty();
        }
    }
}
