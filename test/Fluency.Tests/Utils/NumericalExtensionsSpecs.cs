using System.Collections.Generic;
using System.Linq;
using Fluency.DataGeneration;
using Fluency.Utils;
using FluentAssertions;
using Xunit;

namespace Fluency.Tests.Utils
{
    public class NumericalExtensionsTests
    {
        public class When_repeating_a_function_5_times_using_the_of_extension
        {
            private IEnumerable< int > _results;

            public When_repeating_a_function_5_times_using_the_of_extension()
            {
                _results = 5.Of(() => ARandom.Int());
            }

            [Fact]
            public void should_return_5_elements() => _results.Count().Should().Be(5);

            [Fact]
            public void should_return_all_integers() => _results.Count(x => x > 0).Should().Be(5);            
        }
    }
}
