using Fluency.IdGenerators;
using FluentAssertions;
using Xunit;

namespace Fluency.Tests.BuilderTests
{
    public class Given_AClassWithMultipleProperties
    {
        private class AClassWithTwoProperties
        {
            public bool PropertyASetterCalled;
            public bool PropertyBSetterCalled;
            private string _propertyA;
            private string _propertyB;

            public string PropertyA
            {
                get { return _propertyA; }
                set
                {
                    PropertyASetterCalled = true;
                    _propertyA = value;
                }
            }

            public string PropertyB
            {
                get { return _propertyB; }
                set
                {
                    PropertyBSetterCalled = true;
                    _propertyB = value;
                }
            }
        }

        public class When_builder_is_not_configured_to_ignore_any_property : Given_AClassWithMultipleProperties
        {
            [Fact]
            public void should_populate_all_properties ()
            {
                var builder = new FluentBuilder<AClassWithTwoProperties>();

                var instance = builder.build();

                instance.PropertyASetterCalled.Should().Be(true);
                instance.PropertyBSetterCalled.Should().Be(true);
            }
        }

        public class When_builder_is_configured_to_ignore_a_property : Given_AClassWithMultipleProperties
        {
            [Fact]
            public void should_not_call_setter_for_ignored_property()
            {
                var builder = new FluentBuilder<AClassWithTwoProperties>();
                builder.IgnoreProperty(x => x.PropertyB);

                var instance = builder.build();

                instance.PropertyASetterCalled.Should().Be(true);
                instance.PropertyBSetterCalled.Should().Be(false);
            }
        }

        public class When_builder_is_configured_to_ignore_multiple_properties : Given_AClassWithMultipleProperties
        {
            [Fact]
            public void should_not_call_setter_for_any_property()
            {
                var builder = new FluentBuilder<AClassWithTwoProperties>();
                builder.IgnoreProperty(x => x.PropertyA);
                builder.IgnoreProperty(x => x.PropertyB);

                var instance = builder.build();

                instance.PropertyASetterCalled.Should().Be(false);
                instance.PropertyBSetterCalled.Should().Be(false);
            }
        }

        public class When_builder_is_configured_to_ignore_all_properties : Given_AClassWithMultipleProperties
        {
            [Fact]
            public void should_not_call_setter_for_any_property()
            {
                var builder = new FluentBuilder<AClassWithTwoProperties>();
                builder.IgnoreAllProperties();

                var instance = builder.build();

                instance.PropertyASetterCalled.Should().Be(false);
                instance.PropertyBSetterCalled.Should().Be(false);
            }
        }
    }
}