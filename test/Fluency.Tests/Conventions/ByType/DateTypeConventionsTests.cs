using System;
using System.Reflection;
using Fluency.Conventions;
using Fluency.Utils;
using FluentAssertions;
using Xunit;

namespace Fluency.Tests.Conventions.ByType
{
    public class DateTypeConventionsTests
    {
        public abstract class When_getting_the_default_value_for_a_property_having_a_date_type_convention_applied
        {
            protected static IDefaultConvention<DateTime> convention;
            protected static PropertyInfo propertyInfo;

            public When_getting_the_default_value_for_a_property_having_a_date_type_convention_applied()
            {
                convention = Convention.DateType();
            }
        }

        public class When_property_is_a_DateTime_type : When_getting_the_default_value_for_a_property_having_a_date_type_convention_applied
        {
            public When_property_is_a_DateTime_type()
            {
                var person = new { DateProperty = DateTime.Now };
                propertyInfo = person.PropertyInfoFor(x => x.DateProperty);
            }

            [Fact]
            public void should_apply()
            {
                convention.AppliesTo(propertyInfo).Should().BeTrue();
            }

            [Fact]
            public void should_return_a_random_date()
            {
                convention.DefaultValue(propertyInfo).Should().NotBe(default(DateTime));
            }
        }

        public class When_property_is_not_a_DateTime_type : When_getting_the_default_value_for_a_property_having_a_date_type_convention_applied
        {
            public When_property_is_not_a_DateTime_type()
            {
                var person = new { NonDateProperty = "bob" };
                propertyInfo = person.PropertyInfoFor(x => x.NonDateProperty);

            }

            [Fact]
            public void should_not_apply()
            {
                convention.AppliesTo(propertyInfo).Should().BeFalse();
            }

            [Fact]
            public void should_return_the_default_datetime_value()
            {
                convention.DefaultValue(propertyInfo).Should().Be(default(DateTime));
            }
        }
    }
}