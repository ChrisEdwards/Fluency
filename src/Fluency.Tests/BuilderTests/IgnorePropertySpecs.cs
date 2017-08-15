using Fluency.IdGenerators;
using Machine.Specifications;
using Rhino.Mocks.Constraints;
using SharpTestsEx;

namespace Fluency.Tests.BuilderTests
{
    public class IgnorePropertySpecs
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

        [Subject("FluencyIgnoreProperties")]
        public class When_builder_is_not_configured_to_ignore_any_property : IgnorePropertySpecs
        {
            Establish context = () => Fluency.Initialize(x => x.IdGeneratorIsConstructedBy(() => new DecrementingIdGenerator()));

            private It should_populate_all_properties = () =>
            {
                var builder = new FluentBuilder<AClassWithTwoProperties>();

                var instance = builder.build();

                instance.PropertyASetterCalled.Should().Be(true);
                instance.PropertyBSetterCalled.Should().Be(true);
            };
        }

        [Subject("FluencyIgnoreProperties")]
        public class When_builder_is_configured_to_ignore_a_property : IgnorePropertySpecs
        {
            Establish context = () => Fluency.Initialize(x => x.IdGeneratorIsConstructedBy(() => new DecrementingIdGenerator()));

            private It should_not_call_setter_for_ignored_property = () =>
            {
                var builder = new FluentBuilder<AClassWithTwoProperties>();
                builder.IgnoreProperty(x => x.PropertyB);

                var instance = builder.build();

                instance.PropertyASetterCalled.Should().Be(true);
                instance.PropertyBSetterCalled.Should().Be(false);
            };
        }
    }
}