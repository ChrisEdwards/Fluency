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