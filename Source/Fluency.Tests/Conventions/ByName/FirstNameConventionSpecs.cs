using System.Reflection;
using Fluency.Conventions;
using FluentObjectBuilder;
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

			Because of = () => { convention = Convention.FirstName(); };
		}


		public class When_given_a_property_with_name_of_FirstName : When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {FirstName = "bob"};
			                    		propertyInfo = person.GetType().GetProperty( "FirstName" );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).should_be_true();

			It should_return_a_random_first_name = () =>
			                                       	{
			                                       		string value = convention.DefaultValue( propertyInfo ).ToString();
			                                       		value.Should().Not.Be.Null();
			                                       		value.Length.Should().Be.GreaterThan( 0 );
			                                       	};
		}


		public class When_given_a_property_with_name_of_firstname : When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {firstname = "bob"};
			                    		propertyInfo = person.GetType().GetProperty( "firstname" );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();

			It should_return_a_random_first_name = () =>
			                                       	{
			                                       		string value = convention.DefaultValue( propertyInfo ).ToString();
			                                       		value.Should().Not.Be.Null();
			                                       		value.Length.Should().Be.GreaterThan( 0 );
			                                       	};
		}


		public class When_given_a_property_with_name_other_than_firstname : When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {lastname = "bob"};
			                    		propertyInfo = person.GetType().GetProperty( "lastname" );
			                    	};

			It should_not_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.False();

			It should_return_nothing = () => convention.DefaultValue( propertyInfo ).Should().Be.Null();
		}
	}
}


// ReSharper restore InconsistentNaming