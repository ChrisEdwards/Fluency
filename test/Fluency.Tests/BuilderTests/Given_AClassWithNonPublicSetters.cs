using FluentAssertions;
using Xunit;

namespace Fluency.Tests.BuilderTests
{
    public class Given_AClassWithNonPublicSetters
    {
        private class AClassWithNonPublicSetters
        {
            public string PropertyWithPrivateSetter { get; private set; }
            public string PropertyWithProtectedSetter { get; protected set; }
            public string PropertyWithInternalSetter { get; internal set; }
            public string PropertyWithPublicSetter { get; set; }
        }

        public class When_builder_is_configured_to_ignore_non_public_setters : Given_AClassWithNonPublicSetters
        {
            [Fact]
            public void should_not_populate_private_setters()
            {
                var builder = new FluentBuilder<AClassWithNonPublicSetters>();
                builder.IgnoreNonPublicSetters();

                var instance = builder.build();

                instance.PropertyWithPrivateSetter.Should().Be(null);
            }

            [Fact]
            public void should_not_populate_protected_setters()
            {
                var builder = new FluentBuilder<AClassWithNonPublicSetters>();
                builder.IgnoreNonPublicSetters();

                var instance = builder.build();

                instance.PropertyWithProtectedSetter.Should().Be(null);
            }

            [Fact]
            public void should_not_populate_internal_setters()
            {
                var builder = new FluentBuilder<AClassWithNonPublicSetters>();
                builder.IgnoreNonPublicSetters();

                var instance = builder.build();

                instance.PropertyWithInternalSetter.Should().Be(null);
            }

            [Fact]
            public void should_still_call_public_setters()
            {
                var builder = new FluentBuilder<AClassWithNonPublicSetters>();
                builder.IgnoreNonPublicSetters();

                var instance = builder.build();

                instance.PropertyWithPublicSetter.Should().NotBe(null);
                instance.PropertyWithPublicSetter.Should().NotBe(string.Empty);
            }
        }
    }
}