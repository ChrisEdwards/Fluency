using Fluency.IdGenerators;
using FluentAssertions;
using Xunit;

namespace Fluency.Tests.BuilderTests
{
    public class WhenBuildingTwoConsecutiveObjects
    {
        public class ClassWithId
        {
            public int Id { get; set; }
        }


        public class DifferentClassWithId
        {
            public int Id { get; set; }
        }


        public class BuilderWithId : FluentBuilder<ClassWithId>
        {
            protected override void SetupDefaultValues()
            {
                SetProperty(x => x.Id, GenerateNewId());
            }
        }


        public class DifferentBuilderWithId : FluentBuilder<DifferentClassWithId>
        {
            protected override void SetupDefaultValues()
            {
                SetProperty(x => x.Id, GenerateNewId());
            }
        }

        public WhenBuildingTwoConsecutiveObjects()
        {
            Fluency.Initialize(x => x.IdGeneratorIsConstructedBy(() => new DecrementingIdGenerator(-1)));
        }

        public class when_the_two_objects_are_of_the_same_type : WhenBuildingTwoConsecutiveObjects
        {
            [Fact]
            public void should_return_unique_ids()
            {
                ClassWithId object1 = new BuilderWithId().build();
                ClassWithId object2 = new BuilderWithId().build();

                object1.Id.Should().NotBe(object2.Id);
            }
        }

        public class when_the_two_objects_are_of_different_types : WhenBuildingTwoConsecutiveObjects
        {
            [Fact]
            public void should_not_return_unique_ids()
            {
                ClassWithId object1 = new BuilderWithId().build();
                DifferentClassWithId object2 = new DifferentBuilderWithId().build();

                object1.Id.Should().Be(object2.Id);
            }
        }
    }
}