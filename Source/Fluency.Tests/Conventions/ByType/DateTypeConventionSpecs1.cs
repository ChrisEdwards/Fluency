using System;
using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using Fluency.Conventions;


namespace Fluency.Tests.Conventions.ByType
{
	public class DateTypeConventionSpecs1
	{
		public abstract class concern : observations_for_a_sut_with_a_contract< IDefaultConvention<DateTime>, LambdaConvention<DateTime> >
		{
			protected static IDefaultConvention<DateTime> convention;
			protected static PropertyInfo property_info;

			because b = () => { convention = Convention.DateType(); };
		}


		public class when_given_a_property_of_type_date : concern
		{
			context c = () =>
			            	{
			            		var person = new {DateProperty = DateTime.Now};
			            		property_info = person.GetType().GetProperty( "DateProperty" );
			            	};

			it should_apply = () => convention.AppliesTo( property_info ).should_be_true();

			it should_return_a_random_date = () =>
			                                 	{
			                                 		object value = convention.DefaultValue( property_info );
			                                 		value.should_not_be_null();
			                                 		value.should_be_an_instance_of< DateTime >();
			                                 	};
		}


		public class when_given_a_property_of_type_other_than_date : concern
		{
			context c = () =>
			            	{
			            		var person = new {NonDateProperty = 7};
			            		property_info = person.GetType().GetProperty( "NonDateProperty" );
			            	};

			it should_not_apply = () => convention.AppliesTo( property_info ).should_be_false();

			it should_return_nothing = () =>
			                           	{
			                           		object value = convention.DefaultValue( property_info );
			                           		value.should_be_null();
			                           	};
		}
	}
}