using System.Reflection;
using Fluency.Conventions;
using Fluency.Utils;
using FluentAssertions;
using Xunit;

namespace Fluency.Tests.Conventions.ByName
{
    // TODO: Rewrite these tests using Theory
    public class LastNameConventionTests
    {
        public abstract class When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
        {
            protected static IDefaultConvention<string> convention;
            protected static PropertyInfo propertyInfo;

            public When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied()
            {
                convention = Convention.LastName();
            }
        }

        public class When_property_name_is_lowercase_lastname : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
        {
            public When_property_name_is_lowercase_lastname()
            {
                var person = new { lastname = "bob" };
                propertyInfo = person.PropertyInfoFor(x => x.lastname);
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

        public class WhenPropertyNameIsMixedCaseLastName : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
        {
            public WhenPropertyNameIsMixedCaseLastName()
            {
                // BUG: original test values, to fix
                var person = new { LastName = "bob" };
                propertyInfo = person.PropertyInfoFor(x => x.LastName);
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

        public class
            When_property_name_contains_lowercase_lastname :
                When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
        {
            public When_property_name_contains_lowercase_lastname()
            {
                var person = new { customerlastname = "bob" };
                propertyInfo = person.PropertyInfoFor(x => x.customerlastname);
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

        public class WhenPropertyNameContainsMixedCaseLastName : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
        {
            public WhenPropertyNameContainsMixedCaseLastName()
            {
                var person = new { CustomerLastName = "bob" };
                propertyInfo = person.PropertyInfoFor(x => x.CustomerLastName);
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

        public class When_given_a_property_with_name_other_than_lastname : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
        {
            public When_given_a_property_with_name_other_than_lastname()
            {
                var person = new { firstname = "bob" };
                propertyInfo = person.PropertyInfoFor(x => x.firstname);
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