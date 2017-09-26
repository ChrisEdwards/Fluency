using Machine.Specifications;
using SharpTestsEx;

namespace Fluency.Tests.Deprecated.BuilderTests
{
    public class IgnoreNonPublicSetters
    {
        private class AClassWithNonPublicSetters
        {
            public string PropertyWithPrivateSetter { get; private set; }
            public string PropertyWithProtectedSetter { get; protected set; }
            public string PropertyWithInternalSetter { get; internal set; }
            public string PropertyWithPublicSetter { get; set; }
        }

        [Subject("FluencyIgnoreProperties")]
        public class When_builder_is_configured_to_ignore_non_public_setters : IgnorePropertySpecs
        {
            private It should_not_populate_private_setters = () =>
            {
                var builder = new FluentBuilder<AClassWithNonPublicSetters>();
                builder.IgnoreNonPublicSetters();

                var instance = builder.build();

                instance.PropertyWithPrivateSetter.Should().Be(null);
            };

            private It should_not_populate_protected_setters = () =>
            {
                var builder = new FluentBuilder<AClassWithNonPublicSetters>();
                builder.IgnoreNonPublicSetters();

                var instance = builder.build();

                instance.PropertyWithProtectedSetter.Should().Be(null);
            };

            private It should_not_populate_internal_setters = () =>
            {
                var builder = new FluentBuilder<AClassWithNonPublicSetters>();
                builder.IgnoreNonPublicSetters();

                var instance = builder.build();

                instance.PropertyWithInternalSetter.Should().Be(null);
            };

            private It should_still_call_public_setters = () =>
            {
                var builder = new FluentBuilder<AClassWithNonPublicSetters>();
                builder.IgnoreNonPublicSetters();

                var instance = builder.build();

                instance.PropertyWithPublicSetter.Should().Not.Be(null);
                instance.PropertyWithPublicSetter.Should().Not.Be(string.Empty);
            };
        }
    }
}