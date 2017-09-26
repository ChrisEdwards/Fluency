using System.Collections.Generic;
using System.Linq;
using Fluency.DataGeneration;
using Fluency.Utils;
using Machine.Specifications;

namespace Fluency.Tests.Deprecated.Utils
{
	public class NumericalExtensionsSpecs
	{
		[ Subject(typeof(NumericalExtensions), "Of")]
		public class When_repeating_a_function_5_times_using_the_of_extension
		{
			private static IEnumerable< int > results;

			private Because of = () => results = 5.Of( () => ARandom.Int() );

			private It should_return_5_elements = () => results.Count().ShouldEqual( 5 );
			private It should_return_all_integers = () => results.ShouldEachConformTo( item => item > 0 );
		}
	}
}
