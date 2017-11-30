using System.Reflection;
using Fluency.Conventions;
using Fluency.Utils;
using FluentAssertions;
using Xunit;

namespace Fluency.Tests.Conventions.ByType
{
    public class IntegerTypeConventionTests
    {
        public abstract class When_getting_the_default_value_for_a_property_having_an_integer_type_convention_applied
        {
            protected static IDefaultConvention<int> convention;
            protected static PropertyInfo propertyInfo;

            public When_getting_the_default_value_for_a_property_having_an_integer_type_convention_applied()
            {
                convention = Convention.IntegerType();
            }

            public class When_property_is_a_Integer_type : When_getting_the_default_value_for_a_property_having_an_integer_type_convention_applied
            {
                public When_property_is_a_Integer_type()
                {
                    var person = new { IntProperty = 1 };
                    propertyInfo = person.PropertyInfoFor(x => x.IntProperty);
                }

                [Fact]
                public void should_apply()
                {
                    convention.AppliesTo(propertyInfo).Should().BeTrue();
                }

                [Fact]
                public void should_return_a_random_integer()
                {
                    convention.DefaultValue(propertyInfo).Should().NotBe(0);
                }
            }

            public class When_property_is_not_an_Integer_type : When_getting_the_default_value_for_a_property_having_an_integer_type_convention_applied
            {
                public When_property_is_not_an_Integer_type()
                {
                    var person = new { NonIntProperty = "bob" };
                    propertyInfo = person.PropertyInfoFor(x => x.NonIntProperty);
                }

                [Fact]
                public void should_not_apply()
                {
                    convention.AppliesTo(propertyInfo).Should().BeFalse();
                }

                [Fact]
                public void should_return_zero()
                {
                    convention.DefaultValue(propertyInfo).Should().Be(0);
                }
            }
        }
    }
}