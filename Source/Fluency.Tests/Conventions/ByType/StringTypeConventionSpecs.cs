using System.Reflection;
using Fluency.Conventions;
using Fluency.Utils;
using Machine.Specifications;
using SharpTestsEx;

// ReSharper disable InconsistentNaming


namespace Fluency.Tests.Conventions.ByType
{
	public class StringTypeConventionsSpecs
	{
		public abstract class When_getting_the_default_value_for_a_property_having_a_string_type_convention_applied
		{
			protected static IDefaultConvention< string > convention;
			protected static PropertyInfo propertyInfo;
			protected const int expectedLength = 10;

			Because of = () => { convention = Convention.String( expectedLength ); };
		}


		[ Subject( typeof ( Convention ), "ByType<DateTime>" ) ]
		public class When_property_is_a_String_type : When_getting_the_default_value_for_a_property_having_a_string_type_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {StringProperty = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.StringProperty );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_string_of_the_specified_length = () => convention.DefaultValue( propertyInfo ).Length.Should().Be.EqualTo( expectedLength );
		}


		[ Subject( typeof ( Convention ), "ByType<DateTime>" ) ]
		public class When_property_is_not_a_String_type : When_getting_the_default_value_for_a_property_having_a_string_type_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {NonStringProperty = 123};
			                    		propertyInfo = person.PropertyInfoFor( x => x.NonStringProperty );
			                    	};

			It should_not_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.False();
			It should_return_nothing = () => convention.DefaultValue( propertyInfo ).Should().Be.Null();
		}
	}
}


// ReSharper restore InconsistentNaming