using System.Reflection;
using Fluency.Conventions;
using Fluency.Utils;
using Machine.Specifications;
using SharpTestsEx;

// ReSharper disable InconsistentNaming


namespace Fluency.Tests.Conventions.ByName
{
	public class FirstNameConventionSpecs
	{
		public abstract class When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			protected static IDefaultConvention convention;
			protected static PropertyInfo propertyInfo;
			protected static object defaultValue;

			Because of = () =>
			             	{
			             		convention = Convention.FirstName();
			             		defaultValue = convention.DefaultValue( propertyInfo );
			             	};
		}
		
		public abstract class FirstName_convention_should_apply_and_generate_random_first_name: When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			public It should_apply = () => convention.AppliesTo(propertyInfo).Should().Be.True();
			public It should_return_a_random_first_name = () => defaultValue.ToString().Length.Should().Be.GreaterThan(0);
		}


		[ Subject( typeof ( FirstNameConvention ) ) ]
		public class When_property_name_is_lowercase_firstname : FirstName_convention_should_apply_and_generate_random_first_name
		{
			Establish context = () =>
			                    	{
			                    		var person = new {firstname = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.firstname );
			                    	};

		}


		[ Subject( typeof ( FirstNameConvention ) ) ]
		public class When_property_name_is_mixed_case_FirstName : When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {FirstName = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.FirstName );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_first_name = () => defaultValue.ToString().Length.Should().Be.GreaterThan( 0 );
		}


		[ Subject( typeof ( FirstNameConvention ) ) ]
		public class When_property_name_contains_lowercase_firstname : When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {customerfirstname = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.customerfirstname );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_first_name = () => defaultValue.ToString().Length.Should().Be.GreaterThan( 0 );
		}


		[ Subject( typeof ( FirstNameConvention ) ) ]
		public class When_property_name_contains_mixed_case_FirstName : When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {CustomerFirstName = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.CustomerFirstName );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_first_name = () => defaultValue.ToString().Length.Should().Be.GreaterThan( 0 );
		}


		[ Subject( typeof ( FirstNameConvention ) ) ]
		public class When_given_a_property_with_name_other_than_firstname : When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {lastname = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.lastname );
			                    	};

			It should_not_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.False();
			It should_return_nothing = () => defaultValue.Should().Be.Null();
		}
	}
}


// ReSharper restore InconsistentNaming