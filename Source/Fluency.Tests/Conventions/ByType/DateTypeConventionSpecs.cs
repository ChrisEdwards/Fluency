using System;
using System.Reflection;
using Fluency.Conventions;
using Fluency.Utils;
using Machine.Specifications;
using SharpTestsEx;

// ReSharper disable InconsistentNaming


namespace Fluency.Tests.Conventions.ByType
{
	public class DateTypeConventionsSpecs
	{
		public abstract class When_getting_the_default_value_for_a_property_having_a_date_type_convention_applied
		{
			protected static IDefaultConvention< DateTime > convention;
			protected static PropertyInfo propertyInfo;

			Because of = () => { convention = Convention.DateType(); };
		}


		[ Subject( typeof ( Convention ), "ByType<DateTime>" ) ]
		public class When_property_is_a_DateTime_type : When_getting_the_default_value_for_a_property_having_a_date_type_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {DateProperty = DateTime.Now};
			                    		propertyInfo = person.PropertyInfoFor( x => x.DateProperty );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_last_name = () => convention.DefaultValue( propertyInfo ).Should().Not.Be.EqualTo( default( DateTime ) );
		}


		[ Subject( typeof ( Convention ), "ByType<DateTime>" ) ]
		public class When_property_is_not_a_DateTime_type : When_getting_the_default_value_for_a_property_having_a_date_type_convention_applied
		{
			Establish context = () =>
			                    	{
										var person = new { NonDateProperty = "bob" };
			                    		propertyInfo = person.PropertyInfoFor( x => x.NonDateProperty);
			                    	};

			It should_not_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.False();
			It should_return_the_default_datetime_value = () => convention.DefaultValue( propertyInfo ).Should().Be.EqualTo( default( DateTime ) );
		}
	}
}


// ReSharper restore InconsistentNaming