using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Shiloh.DataGeneration;


namespace Shiloh.Utils.Tests
{
	public class NumericalExtensionsSpecs
	{
		[ Subject( typeof ( NumericalExtensions ), "Of" ) ]
		public class When_repeating_a_function_5_times_using_the_of_extension
		{
			private static IEnumerable< int > results;

			private Because of = () => results = 5.Of( () => Anonymous.Integer.GreaterThan( 0 ) );

			private It should_return_5_elements = () => results.Count().ShouldEqual( 5 );
			private It should_return_all_integers = () => results.ShouldEachConformTo( item => item > 0 );
		}
	}
}