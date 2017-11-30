using FluentAssertions;
using Xunit;

namespace Fluency.Tests.BuilderTests
{
    public class when_setting_a_reference_property
    {
        public class ReferenceType
        {
            public string Name { get; set; }
        }


        public class ClassWithReferenceProperty
        {
            public ReferenceType ReferenceProperty { get; set; }
        }


        protected static ReferenceType _expectedValue;

        public class when_setting_a_reference_property_with_no_default_builder : when_setting_a_reference_property
        {
            public class BuilderWithReferenceProperty_WithNoDefaultBuilder : FluentBuilder<ClassWithReferenceProperty>
            {
                public BuilderWithReferenceProperty_WithNoDefaultBuilder With(ReferenceType value)
                {
                    SetProperty(x => x.ReferenceProperty, value);
                    return this;
                }
            }

            [Fact]
            public void should_return_the_same_reference ()
            {
                var builder = new BuilderWithReferenceProperty_WithNoDefaultBuilder();
                ClassWithReferenceProperty instance = builder.With(_expectedValue).build();

                instance.ReferenceProperty.Should().BeSameAs(_expectedValue);
            }
        }
    }

    public class when_setting_a_reference_property_with_a_default_builder : when_setting_a_reference_property
    {
        public class BuilderWithReferenceProperty_WithDefaultBuilder : FluentBuilder<ClassWithReferenceProperty>
        {
            protected override void SetupDefaultValues()
            {
                SetProperty(x => x.ReferenceProperty, new FluentBuilder<ReferenceType>());
            }


            public BuilderWithReferenceProperty_WithDefaultBuilder With(ReferenceType value)
            {
                SetProperty(x => x.ReferenceProperty, value);
                return this;
            }
        }

        [Fact]
        public void should_return_the_same_reference()
        {
            var builder = new BuilderWithReferenceProperty_WithDefaultBuilder();
            ClassWithReferenceProperty instance = builder.With(_expectedValue).build();
            instance.ReferenceProperty.Should().BeSameAs(_expectedValue);
        }
    }
}