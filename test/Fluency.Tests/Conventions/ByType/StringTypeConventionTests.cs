using System.Reflection;
using Fluency.Conventions;
using Fluency.Utils;
using FluentAssertions;
using Xunit;

namespace Fluency.Tests.Conventions.ByType
{
    public class StringTypeConventionTests
    {
        public abstract class When_getting_the_default_value_for_a_property_having_a_string_type_convention_applied
        {
            protected static IDefaultConvention<string> convention;
            protected static PropertyInfo propertyInfo;
            protected const int expectedLength = 10;

            public When_getting_the_default_value_for_a_property_having_a_string_type_convention_applied()
            {
                convention = Convention.String(expectedLength);
            }
        }

        public class When_property_is_a_String_type : When_getting_the_default_value_for_a_property_having_a_string_type_convention_applied
        {
            public When_property_is_a_String_type()
            {
                var person = new { StringProperty = "bob" };
                propertyInfo = person.PropertyInfoFor(x => x.StringProperty);
            }

            [Fact]
            public void should_apply()
            {
                convention.AppliesTo(propertyInfo).Should().BeTrue();
            }

            [Fact]
            public void should_return_a_random_string_of_the_specified_length()
            {
                convention.DefaultValue(propertyInfo).Length.Should().Be(expectedLength);
            }
        }

        public class When_property_is_not_a_String_type : When_getting_the_default_value_for_a_property_having_a_string_type_convention_applied
        {
            public When_property_is_not_a_String_type()
            {
                var person = new { NonStringProperty = 123 };
                propertyInfo = person.PropertyInfoFor(x => x.NonStringProperty);
            }

            [Fact]
            public void should_not_apply()
            {
                convention.AppliesTo(propertyInfo).Should().BeFalse();
            }

            [Fact]
            public void should_return_nothing()
            {
                convention.DefaultValue(propertyInfo).Should().BeNull();
            }
        }
    }
}