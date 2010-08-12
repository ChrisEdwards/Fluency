using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit.standard.observations;
using Fluency.Conventions;
using FluentObjectBuilder;
using Machine.Specifications;


namespace Fluency.Tests.Conventions.ByName
{
	public class LastNameConventionSpecs1
	{
		public class When_gettin_the_default_value_from_a_last_name_convention
		{
			protected static IDefaultConvention convention;
			protected static PropertyInfo property_info;
			Because of = () => convention = Convention.LastName();
		}


		public abstract class concern : observations_for_a_sut_with_a_contract< IDefaultConvention, LambdaConvention >
		{
			protected static IDefaultConvention convention;
			protected static PropertyInfo property_info;

			because b = () => { convention = Convention.LastName(); };
		}


		[ Subject( typeof ( Convention ) ) ]
		public class when_given_a_property_with_name_of_LastName : When_gettin_the_default_value_from_a_last_name_convention
		{
			Establish context = () =>
			                    	{
			                    		var person = new {LastName = "bob"};
			                    		property_info = person.GetType().GetProperty( "LastName" );
			                    	};


			it should_apply = () => convention.AppliesTo( property_info ).should_be_true();

			it should_return_a_random_last_name = () =>
			                                      	{
			                                      		string value = convention.DefaultValue( property_info ).ToString();
			                                      		value.should_not_be_null();
			                                      		value.Length.should_be_greater_than( 0 );
			                                      	};
		}


		public class when_given_a_property_with_name_of_lastname : concern
		{
			context c = () =>
			            	{
			            		var person = new {lastname = "bob"};
			            		property_info = person.GetType().GetProperty( "lastname" );
			            	};

			it should_apply = () => convention.AppliesTo( property_info ).should_be_true();

			it should_return_a_random_last_name = () =>
			                                      	{
			                                      		string value = convention.DefaultValue( property_info ).ToString();
			                                      		value.should_not_be_null();
			                                      		value.Length.should_be_greater_than( 0 );
			                                      	};
		}


		public class when_given_a_property_with_name_other_than_lastname : concern
		{
			context c = () =>
			            	{
			            		var person = new {firstname = "bob"};
			            		property_info = person.GetType().GetProperty( "firstname" );
			            	};

			it should_not_apply = () => convention.AppliesTo( property_info ).should_be_false();

			it should_return_nothing = () => convention.DefaultValue( property_info ).should_be_null();
		}
	}
}