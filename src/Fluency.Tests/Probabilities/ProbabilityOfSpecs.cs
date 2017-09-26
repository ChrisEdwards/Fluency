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

using System.Collections.Generic;
using System.Linq;
using Fluency.Probabilities;
using Machine.Specifications;
using Shiloh.Utils;

namespace Fluency.Tests.Deprecated.Probabilities
{
	public class ProbabilitySpecificationSpecs
	{
		[ Subject( typeof ( ProbabilitySpecification< > ) ) ]
		public class Getting_the_outcome_of_a_probability_with_a_single_100_percent_chance_outcome
		{
			Establish context = () => probability = new ProbabilitySpecification< int >().PercentOutcome( 100, expectedOutcome );

			Because of = () => outcome = probability.GetOutcome();

			It should_return_the_single_100_percent_outcome = () => outcome.should_be_equal_to( expectedOutcome );

			const int expectedOutcome = 1;
			static ProbabilitySpecification< int > probability;
			static int outcome;
		}


		[ Subject( typeof ( ProbabilitySpecification< > ) ) ]
		public class When_getting_10_outcomes_of_a_probability_having_two_50_50_outcomes
		{
			Establish context = () =>
			                    probability = new ProbabilitySpecification< int >()
			                                  		.PercentOutcome( 50, outcome1 )
			                                  		.PercentOutcome( 50, outcome2 );


			Because of = () => outcomes = 10.Times().Select( x => probability.GetOutcome() );

			It should_return_at_least_one_of_each_outcome = () =>
			                                                	{
			                                                		outcomes.should_contain( outcome1 );
			                                                		outcomes.should_contain( outcome2 );
			                                                	};

			const int outcome1 = 1;
			const int outcome2 = 2;
			static ProbabilitySpecification< int > probability;
			static IEnumerable< int > outcomes;
		}


		[ Subject( typeof ( ProbabilitySpecification< > ) ) ]
		public class When_getting_10_outcomes_of_a_probability_having_a_zero_percent_outcome
		{
			Establish context = () =>
			                    probability = new ProbabilitySpecification< int >().PercentOutcome( 0, outcome );


			Because of = () => outcomes = 10.Times().Select( x => probability.GetOutcome() );

			It should_never_return_the_zero_percent_outcome = () => outcomes.should_not_contain( outcome );

			const int outcome = 1;
			static ProbabilitySpecification< int > probability;
			static IEnumerable< int > outcomes;
		}
	}
}


// ReSharper restore InconsistentNaming