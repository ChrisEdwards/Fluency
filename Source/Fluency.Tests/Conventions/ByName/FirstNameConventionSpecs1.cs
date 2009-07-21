using System.Reflection;
using Fluency.Conventions;
using Fluency.DataGeneration;
using Fluency.Utils;
using FluentObjectBuilder;
using NUnit.Framework;
using SpecUnit;


namespace Fluency.Tests.Conventions.ByName
{
	[ TestFixture ]
	public class FirstNameConventionSpecs1
	{
		[ Concern( "FirstNameConvention" ) ]
		public class FirstNameConventionSpecs1Context : ContextSpecification
		{
			protected IDefaultConvention convention;
			protected PropertyInfo propertyInfo;


			protected override void SharedContext()
			{
				convention = Convention.FirstName();
			}
		}


		public class when_fluency_is_configured_to_use_the_first_name_default_convention
		{
			#region Where FirstName Convention Applies

			/// <summary>
			/// Observations when the convention should apply. 
			/// </summary>
			public abstract class firstname_convention_should_apply_and_generate_random_first_name : FirstNameConventionSpecs1Context
			{
				[ Observation ]
				public void convention_should_apply_to_the_property()
				{
					convention.AppliesTo( propertyInfo ).should_be_true();
				}


				[ Observation ]
				public void should_generate_a_random_first_name_for_the_property()
				{
					string value = convention.DefaultValue( propertyInfo ).ToString();
					value.should_not_be_null();
					value.Length.should_be_greater_than( 0 );
					RandomData.FirstNames.should_contain( value );
				}
			}


			public class when_property_name_is_lowercase_firstname : firstname_convention_should_apply_and_generate_random_first_name
			{
				protected override void Context()
				{
					var person = new {firstname = "bob"};
					propertyInfo = person.PropertyInfoFor( x => x.firstname );
				}
			}


			public class when_property_name_contains_lowercase_firstname : firstname_convention_should_apply_and_generate_random_first_name
			{
				protected override void Context()
				{
					var person = new {customerfirstname = "bob"};
					propertyInfo = person.PropertyInfoFor( x => x.customerfirstname );
				}
			}


			public class when_property_name_is_mixed_case_firstname : firstname_convention_should_apply_and_generate_random_first_name
			{
				protected override void Context()
				{
					var person = new {FirstName = "bob"};
					propertyInfo = person.PropertyInfoFor( x => x.FirstName );
				}
			}


			public class when_property_name_contains_mixed_case_firstname : firstname_convention_should_apply_and_generate_random_first_name
			{
				protected override void Context()
				{
					var person = new {EmployeeFirstName = "bob"};
					propertyInfo = person.PropertyInfoFor( x => x.EmployeeFirstName );
				}
			}

			#endregion


			#region Where FirstName Convention Does Not Apply

			/// <summary>
			/// Observations when the convention should not apply
			/// </summary>
			public abstract class firstname_convention_should_not_apply_and_shoud_not_generate_a_random_first_name : FirstNameConventionSpecs1Context
			{
				[ Observation ]
				public void convention_should_not_apply_to_the_property()
				{
					convention.AppliesTo( propertyInfo ).should_be_false();
				}


				[ Observation ]
				public void should_not_generate_a_random_first_name_for_the_property()
				{
					convention.DefaultValue( propertyInfo ).should_be_null();
				}
			}


			public class when_property_name_does_not_contain_firstname : firstname_convention_should_not_apply_and_shoud_not_generate_a_random_first_name
			{
				protected override void Context()
				{
					var person = new {othername = "bob"};
					propertyInfo = person.PropertyInfoFor( x => x.othername );
				}
			}

			#endregion
		}
	}
}