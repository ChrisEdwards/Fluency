using System.Reflection;
using Fluency.Conventions;
using Fluency.Utils;
using FluentAssertions;
using Xunit;

namespace Fluency.Tests.Conventions.ByName
{
    public class FirstNameConventionTests
    {
        public abstract class When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
        {
            protected static IDefaultConvention<string> convention;
            protected static PropertyInfo propertyInfo;

            public When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied()
            {
                convention = Convention.FirstName();
            }
        }

        public class When_property_name_is_lowercase_firstname : When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
        {
            public When_property_name_is_lowercase_firstname()
            {
                var person = new { firstname = "bob" };
                propertyInfo = person.PropertyInfoFor(x => x.firstname);
            }

            [Fact]
            public void should_apply()
            {
                convention.AppliesTo(propertyInfo).Should().BeTrue();
            }

            [Fact]
            public void should_return_a_random_first_name()
            {
                convention.DefaultValue(propertyInfo).Should().NotBeEmpty();
            }
        }
    }
}