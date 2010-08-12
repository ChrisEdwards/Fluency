using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using Fluency.Conventions;


namespace Fluency.Tests.Conventions.ByType
{
	public class IntegerTypeConventionSpecs
	{
		public abstract class concern : observations_for_a_sut_with_a_contract< IDefaultConvention<int>, LambdaConvention<int> >
		{
			protected static IDefaultConvention<int> convention;
			protected static PropertyInfo property_info;

			because b = () => { convention = Convention.IntegerType(); };
		}


		public class when_given_a_property_of_type_integer : concern
		{
			context c = () =>
			            	{
			            		var person = new {IntegerProperty = 1};
			            		property_info = person.GetType().GetProperty( "IntegerProperty" );
			            	};

			it should_apply = () => convention.AppliesTo( property_info ).should_be_true();

			it should_return_a_random_integer = () =>
			                                    	{
			                                    		object value = convention.DefaultValue( property_info );
			                                    		value.should_not_be_null();
			                                    		value.should_be_an_instance_of< int >();
			                                    	};
		}


		public class when_given_a_property_of_type_other_than_integer : concern
		{
			context c = () =>
			            	{
			            		var person = new {NonIntegerProperty = "somevalue"};
			            		property_info = person.GetType().GetProperty( "NonIntegerProperty" );
			            	};

			it should_apply = () => convention.AppliesTo( property_info ).should_be_false();

			it should_return_nothing = () =>
			                           	{
			                           		object value = convention.DefaultValue( property_info );
			                           		value.should_be_null();
			                           	};
		}
	}
}