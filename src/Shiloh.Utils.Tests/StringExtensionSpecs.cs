// Copyright 2011 Chris Edwards
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using Machine.Specifications;


namespace Shiloh.Utils.Tests
{
	public class StringExtensionSpecs
	{
		#region ToFixedWidth() Specs

		[ Subject( typeof ( StringExtensions ), "ToFixedWidth" ) ]
		public class When_converting_a_string_to_a_fixed_width
		{
			protected static string result;
		}


		[ Subject( typeof ( StringExtensions ), "ToFixedWidth" ) ]
		public class When_the_specified_width_is_larger_than_the_input_string : When_converting_a_string_to_a_fixed_width
		{
			Because of = () => result = "bob".ToFixedWidth( 10 );
			It should_expand_the_string_to_the_specified_width = () => result.Length.ShouldEqual( 10 );
			It should_expand_the_string_by_padding_the_left_with_spaces = () => result.ShouldEqual( "       bob" );
		}


		[ Subject( typeof ( StringExtensions ), "ToFixedWidth" ) ]
		public class When_the_specified_width_is_shorter_than_the_input_string : When_converting_a_string_to_a_fixed_width
		{
			Because of = () => result = "1234567890abcdefghij".ToFixedWidth( 10 );
			It should_truncate_the_string_to_the_specified_width = () => result.Length.ShouldEqual( 10 );
			It should_truncate_the_rightmost_chars_of_the_string = () => result.ShouldEqual( "1234567890" );
		}


		[ Subject( typeof ( StringExtensions ), "ToFixedWidth" ) ]
		public class When_the_specified_width_is_the_same_length_as_the_input_string : When_converting_a_string_to_a_fixed_width
		{
			Because of = () => result = "bob".ToFixedWidth( 3 );
			It should_return_the_string_as_is = () => result.ShouldEqual( "bob" );
		}

		#endregion


		#region Left Specs

		[ Subject( typeof ( StringExtensions ), "Left" ) ]
		public class When_getting_the_leftmost_chars_of_a_string
		{
			protected static string result;
		}


		[ Subject( typeof ( StringExtensions ), "Left" ) ]
		public class When_the_requested_number_of_chars_is_greater_than_the_length_of_input_string : When_getting_the_leftmost_chars_of_a_string
		{
			Because of = () => result = "bob".Left( 10 );
			It should_simply_return_the_string_unchanged = () => result.ShouldEqual( "bob" );
		}


		[ Subject( typeof ( StringExtensions ), "Left" ) ]
		public class When_the_requested_number_of_chars_is_fewer_than_the_length_of_the_input_string : When_getting_the_leftmost_chars_of_a_string
		{
			Because of = () => result = "1234567890abcdefghij".Left( 10 );
			It should_return_the_number_of_requested_chars = () => result.Length.ShouldEqual( 10 );
			It should_return_the_leftmost_chars_of_the_string = () => result.ShouldEqual( "1234567890" );
		}

		#endregion


		#region ToCamelCase Specs

		[ Subject( typeof ( StringExtensions ), "ToCamelCase" ) ]
		public class When_converting_a_string_to_camel_case
		{
			protected static string result;

			private It should_change_the_first_char_to_lower_case = () => "SomeValue".ToCamelCase().ShouldEqual( "someValue" );
			private It should_not_change_consecutive_upper_case_chars_unless_its_the_very_first_char = () => "SHOUT".ToCamelCase().ShouldEqual( "sHOUT" );
			private It should_return_an_empty_string_if_given_an_empty_string = () => "".ToCamelCase().ShouldEqual( "" );
		}

		#endregion


		#region WithinBrackets Specs

		[ Subject( typeof ( StringExtensions ), "WithinBrackets" ) ]
		public class When_surrounding_a_string_with_brackets
		{
			protected static string result;

			private It should_prepend_an_open_bracket_to_the_front_of_the_string_and_append_a_close_bracket_to_the_end = () => "Test".WithinBrackets().ShouldEqual( "[Test]" );
			private It should_not_affect_the_string_if_it_is_already_surrounded_with_brackets = () => "[Test]".WithinBrackets().ShouldEqual( "[Test]" );
			private It should_return_open_and_close_brackests_only_if_given_an_empty_string = () => "".WithinBrackets().ShouldEqual( "[]" );
		}

		#endregion


		#region StripQuotes Specs

		[ Subject( typeof ( StringExtensions ), "StripQuotes" ) ]
		public class When_stripping_quotes_from_a_string
		{
			protected static string result;

			private It should_remove_double_quotations_that_are_the_first_and_last_char_of_the_string = () => "\"Test\"".StripQuotes().ShouldEqual( "Test" );
			private It should_not_affect_the_string_if_it_is_not_surrounded_with_double_quotes = () => "Test".StripQuotes().ShouldEqual( "Test" );
			private It should_not_affect_double_quotes_that_are_inside_the_string = () => "Te\"st".StripQuotes().ShouldEqual( "Test" );
			private It should_only_multiple_double_quotation_from_the_front_and_back_if_there_are_multiples = () => "\"\"Test\"\"".StripQuotes().ShouldEqual( "Test" );
			private It should_return_an_empty_string_if_given_an_empty_string = () => "".StripQuotes().ShouldEqual( "" );
		}

		#endregion


		#region StripWhitespace Specs

		[ Subject( typeof ( StringExtensions ), "StripQuotes" ) ]
		public class When_stripping_whitespace_from_a_string
		{
			protected static string result;

			private It should_remove_spaces_from_the_string = () => "  Te  st ".StripWhitespace().ShouldEqual( "Test" );
			private It should_remove_tabs_from_the_string = () => "\t\tTe\t\tst\t\t".StripWhitespace().ShouldEqual( "Test" );
			private It should_remove_newlines_from_the_string = () => "\nTe\n\nst\n".StripWhitespace().ShouldEqual( "Test" );
			private It shoule_remove_carriage_returns_from_the_string = () => "\rTe\r\rst\r\r".StripWhitespace().ShouldEqual( "Test" );
			private It should_return_empty_string_if_given_an_empty_string = () => "".StripWhitespace().ShouldEqual( "" );
		}

		#endregion
	}
}


// ReSharper restore InconsistentNaming