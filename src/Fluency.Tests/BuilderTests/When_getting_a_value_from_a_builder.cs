// ReSharper disable InconsistentNaming

// TODO: Tests are not complete.
using System;
using Machine.Specifications;


namespace Fluency.Tests.BuilderTests
{
	public class When_getting_a_value_from_a_builder
	{
		protected class Foo
		{
			public int ValueProperty { get; set; }
			public Foo ReferenceProperty { get; set; } 
		}

		[ Subject( typeof ( FluentBuilder<> ), "GetValue" ) ]
		public class When_getting_a_property_value_from_a_builder_builder_before_calling_build
		{
			private static int _expectedValue;

			private Because of = () => {};

			private It SHOULD = () =>
			                    	{
			                    		_expectedValue = 5;
			                    		var builder = new DynamicFluentBuilder< Foo >()
											.Having( x => x.ValueProperty, _expectedValue );

			                    		var actualValue = builder.GetValue( x => x.ValueProperty );
			                    		actualValue.ShouldEqual( _expectedValue );
			                    	};

			It should_observation = () =>
			                        	{
											// Necessary to ensure a value is not assigned at all. Otherwise default conventions will supply one.
			                        		Fluency.Configuration.DefaultValueConventions.Clear();

			                        		var builder = new DynamicFluentBuilder< Foo >();

											var actualValue = builder.GetValue(x => x.ValueProperty);
											actualValue.ShouldEqual(_expectedValue);
			                        	};
		}

		// For a value type
		// Given a value was not supplied for the property -> should return null
		// Given a value was supplied for this property -> should return the value

		// For a reference type
		// Given neither a value nor a builder were supplied for the property -> should return null
		// Given a value was supplied -> should return value
		// Given 2 values were supplied -> should return the last supplied value (first is overwritten)
		// Given a builder was supplied -> should fail (suggest using GetBuilderFor())

		
	}
}


// ReSharper restore InconsistentNaming