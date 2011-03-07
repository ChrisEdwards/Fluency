// ReSharper disable InconsistentNaming


using System;
using Fluency.Probabilities;
using Machine.Specifications;
using System.Linq;


namespace Fluency.Tests.Probabilities
{
	public class ProbabilitySpecs
	{
		[ Subject( typeof ( Probability ) ) ]
		public class Defining_a_probability_with_percent_chances_totalling_greater_than_100
		{
			static Exception _exception;

			Because of = () => _exception = Catch.Exception( () =>
			                                                 Probability.Of< int >()
			                                                 		.PercentOutcome( 50, 0 )
			                                                 		.PercentOutcome( 55, 1 )
			                                		);

			It should_fail = () => _exception.ShouldBeOfType< ArgumentException >();
		}


		[ Subject( typeof ( Probability ) ) ]
		public class Defining_a_probability_with_percent_chances_totalling_100
		{
			static ProbabilitySpecification< int > result;

			Because of = () => result =
			             Probability.Of<int>()
			             		.PercentOutcome( 50, 0 )
			             		.PercentOutcome( 50, 1 ) ;

			It should_return_a_valid_probability_specification = () => result.should_be_an_instance_of< ProbabilitySpecification< int > >();
			It should_contain_each_of_the_specified_percent_chance_specifications = () => result.Outcomes.Count().should_be_equal_to( 2 );
		}
	}
}


// ReSharper restore InconsistentNaming