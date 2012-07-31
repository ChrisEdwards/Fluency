// ReSharper disable InconsistentNaming


using System;
using Fluency.DataGeneration;
using Machine.Specifications;
using System.Linq;


namespace Fluency.Tests.DataGeneration
{
	public class ARandomSpecs
	{
		[ Subject( typeof ( ARandom ) ) ]
		public class When_generating_a_random_string_constrained_to_a_specific_set_of_characters
		{
			const string AllowedCharacters = "ABC123";
			private static string _string;

			private Because of = () => _string = ARandom.StringFromCharacterSet( 10, "ABC123" );

			private It should_generate_a_string_only_containing_chars_from_the_allowed_character_set = () => _string.ToCharArray().ShouldEachConformTo( character => AllowedCharacters.ToCharArray().Contains( character ) );
		}
	}
}


// ReSharper restore InconsistentNaming