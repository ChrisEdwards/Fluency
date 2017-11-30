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
using System.Linq;
using Fluency.Probabilities;
using FluentAssertions;
using Xunit;

namespace Fluency.Tests.Probabilities
{
    public class ProbabilityTests
    {
        public class Defining_a_probability_with_percent_chances_totalling_greater_than_100
        {
            [Fact]
            public void should_fail() =>
                Catch.Exception(() =>
                    Probability.Of<int>()
                        .PercentOutcome(50, 0)
                        .PercentOutcome(51, 1))
                        .Should().BeOfType<ArgumentException>();            
        }

        public class Defining_a_probability_with_percent_chances_totalling_100
        {
            public Defining_a_probability_with_percent_chances_totalling_100()
            {
                result =
                    Probability.Of<int>()
                        .PercentOutcome(50, 0)
                        .PercentOutcome(50, 1);
            }
            
            [Fact]
            public void should_return_a_valid_probability_specification() => 
                result.Should().BeOfType<ProbabilitySpecification<int>>();

            [Fact]
            public void should_contain_each_of_the_specified_percent_chance_specifications() =>
                result.Outcomes.Count().Should().Be(2);

            private readonly ProbabilitySpecification<int> result;
        }
    }
}
