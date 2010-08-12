using System.Reflection;
using Fluency.Conventions;
using Fluency.Utils;
using Machine.Specifications;
using SharpTestsEx;

// ReSharper disable InconsistentNaming


namespace Fluency.Tests.Conventions.ByName
{
	public class LastNameConventionSpecs
	{
		public abstract class When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
		{
			protected static IDefaultConvention< string > convention;
			protected static PropertyInfo propertyInfo;
			protected static object defaultValue;

			Because of = () =>
			             	{
			             		convention = Convention.LastName();
			             		defaultValue = convention.DefaultValue( propertyInfo );
			             	};
		}


		[ Subject( typeof ( Convention ), "LastName" ) ]
		public class When_property_name_is_lowercase_lastname : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {lastname = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.lastname );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_last_name = () => defaultValue.ToString().Length.Should().Be.GreaterThan( 0 );
		}


		[ Subject( typeof ( Convention ), "LastName" ) ]
		public class When_property_name_is_mixed_case_LastName : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {LastName = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.LastName );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_last_name = () => defaultValue.ToString().Length.Should().Be.GreaterThan( 0 );
		}


		[ Subject( typeof ( Convention ), "LastName" ) ]
		public class When_property_name_contains_lowercase_lastname : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {customerlastname = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.customerlastname );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_first_name = () => defaultValue.ToString().Length.Should().Be.GreaterThan( 0 );
		}


		[ Subject( typeof ( Convention ), "LastName" ) ]
		public class When_property_name_contains_mixed_case_LastName : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {CustomerLastName = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.CustomerLastName );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_first_name = () => defaultValue.ToString().Length.Should().Be.GreaterThan( 0 );
		}


		[ Subject( typeof ( Convention ), "LastName" ) ]
		public class When_given_a_property_with_name_other_than_lastname : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {othername = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.othername );
			                    	};

			It should_not_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.False();
			It should_return_nothing = () => defaultValue.Should().Be.Null();
		}
	}
}


// ReSharper restore InconsistentNaming