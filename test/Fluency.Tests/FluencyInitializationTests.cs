using Fluency.IdGenerators;
using FluentAssertions;
using Xunit;

namespace Fluency.Tests
{
    public class FluencyInitializationTests
    {
        #region Test Builder

        public class TestItem
        {
            public int Id { get; set; }
        }


        public class TestItemBuilder : FluentBuilder<TestItem>
        {
            protected override void SetupDefaultValues()
            {
                SetProperty(x => x.Id, GenerateNewId());
            }
        }

        #endregion


        public class FluencyInitializationBaseSpecs
        {
            protected TestItem _item;     
        }
        
        public class When_Fluency_is_configured_to_use_decrementing_ids : FluencyInitializationBaseSpecs
        {
            public When_Fluency_is_configured_to_use_decrementing_ids()
            {
                Fluency.Initialize(x => x.IdGeneratorIsConstructedBy(() => new DecrementingIdGenerator()));
                _item = new TestItemBuilder().build();
            }

            [Fact]
            public void should_generate_a_negative_id_value() => _item.Id.Should().BeLessThan(0);
        }


        public class When_Fluency_is_configured_to_use_zero_for_ids : FluencyInitializationBaseSpecs
        {
            public When_Fluency_is_configured_to_use_zero_for_ids()
            {
                Fluency.Initialize(x => x.IdGeneratorIsConstructedBy(() => new StaticValueIdGenerator(0)));
                _item = new TestItemBuilder().build();
            }

            [Fact]
            public void should_generate_a_zero_id_value() => _item.Id.Should().Be(0);
        }

        public class When_no_id_generator_is_specified_for_fluency : FluencyInitializationBaseSpecs
        {
            public When_no_id_generator_is_specified_for_fluency()
            {
                _item = new TestItemBuilder().build();
            }

            [Fact]
            public void should_use_zero_id_value_generator_by_default() =>
                _item.Id.Should().Be(0);
        }

        public class When_no_default_value_conventions_are_specified : FluencyInitializationBaseSpecs
        {
            public When_no_default_value_conventions_are_specified()
            {
                _item = new TestItemBuilder().build();
            }

            [Fact]
            public void should_use_default_set_of_default_value_conventions() =>
                Fluency.Configuration.DefaultValueConventions.Count.Should().BeGreaterThan(0);
        }

        public class When_default_value_conventions_are_specified : FluencyInitializationBaseSpecs
        {
            public When_default_value_conventions_are_specified()
            {
                Fluency.Initialize(x => x.UseDefaultValueConventions());
                _item = new TestItemBuilder().build();
            }
            
            [Fact]
            public void the_default_conventions_should_be_used() => 
                Fluency.Configuration.DefaultValueConventions.Count.Should().BeGreaterThan(0);
        }
    }
}