using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using Fluency.Conventions;


namespace Fluency.Tests.Conventions.ByType
{
	public class TypeStringConventionSpecs
	{
		public abstract class concern : observations_for_a_sut_with_a_contract< IDefaultConvention<string>, LambdaConvention<string> >
		{
			protected static IDefaultConvention<string> convention;
			protected static PropertyInfo property_info;

			because b = () => { convention = Convention.String( 20 ); };
		}


		public class when_given_a_property_of_type_string : concern
		{
			context c = () =>
			            	{
			            		var person = new {StringProperty = "string"};
			            		property_info = person.GetType().GetProperty( "StringProperty" );
			            	};

			it should_apply = () => convention.AppliesTo( property_info ).should_be_true();

			it should_return_a_random_string = () =>
			                                   	{
			                                   		string value = convention.DefaultValue( property_info ).ToString();
			                                   		value.should_not_be_null();
			                                   		value.Length.should_be_greater_than( 0 );
			                                   	};
		}


		public class when_given_a_property_of_type_other_than_string : concern
		{
			context c = () =>
			            	{
			            		var person = new {BoolProperty = false};
			            		property_info = person.GetType().GetProperty( "BoolProperty" );
			            	};

			it should_apply = () => convention.AppliesTo( property_info ).should_be_false();

			it should_return_nothing = () => convention.DefaultValue( property_info ).should_be_null();
		}
	}
}