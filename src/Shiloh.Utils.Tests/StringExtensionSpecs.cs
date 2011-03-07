// ReSharper disable InconsistentNaming


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
	}
}


// ReSharper restore InconsistentNaming