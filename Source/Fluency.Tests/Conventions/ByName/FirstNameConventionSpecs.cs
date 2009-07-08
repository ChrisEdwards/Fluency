using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit.standard.observations;
using Fluency.Conventions;
using FluentObjectBuilder;


namespace Fluency.Tests.Conventions
{
	public class FirstNameConventionSpecs
	{
		public abstract class concern : observations_for_a_sut_with_a_contract< IDefaultConvention, LambdaConvention >
		{
			protected static IDefaultConvention convention;
			protected static PropertyInfo propertyInfo;

			because b = () =>
			            {
			            	convention = Convention.FirstName();
			            };
		}


		public class when_given_a_property_with_name_of_FirstName : concern
		{
			context c = () =>
			            {
			            	var person = new {FirstName = "bob"};
			            	propertyInfo = person.GetType().GetProperty( "FirstName" );
			            };

			it should_apply = () => convention.AppliesTo( propertyInfo ).should_be_true();

			it should_return_a_random_first_name = () =>
			                                       {
			                                       	string value = convention.DefaultValue( propertyInfo ).ToString();
			                                       	value.should_not_be_null();
			                                       	value.Length.should_be_greater_than( 0 );
			                                       };
		}


		public class when_given_a_property_with_name_of_firstname : concern
		{
			context c = () =>
			            {
			            	var person = new {firstname = "bob"};
			            	propertyInfo = person.GetType().GetProperty( "firstname" );
			            };

			it should_apply = () => convention.AppliesTo( propertyInfo ).should_be_true();

			it should_return_a_random_first_name = () =>
			                                       {
			                                       	string value = convention.DefaultValue( propertyInfo ).ToString();
			                                       	value.should_not_be_null();
			                                       	value.Length.should_be_greater_than( 0 );
			                                       };
		}


		public class when_given_a_property_with_name_other_than_firstname : concern
		{
			context c = () =>
			            {
			            	var person = new {lastname = "bob"};
			            	propertyInfo = person.GetType().GetProperty( "lastname" );
			            };

			it should_not_apply = () => convention.AppliesTo( propertyInfo ).should_be_false();

			it should_return_nothing = () => convention.DefaultValue( propertyInfo ).should_be_null();
		}
	}


}