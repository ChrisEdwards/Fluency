using System;
using Fluency.DataGeneration;
using FluentObjectBuilder;
using Machine.Specifications;
using Should.Assertions;

// ReSharper disable InconsistentNaming


namespace Fluency.Tests.BuilderTests
{
	public class DynamicFluentBuilderSpecs
	{
		[ Subject( typeof ( DynamicFluentBuilder< > ) ) ]
		public class Given_a_DynamicFluentBuilder_that_uses_default_value_conventions
		{
			public static DynamicFluentBuilder< TestClass > _builder;

			Establish context = () =>
			                    	{
			                    		Fluency.Initialize( x => x.UseDefaultValueConventions() );
			                    		_builder = new DynamicFluentBuilder< TestClass >();
			                    	};
		}


		public class TestClass
		{
			public string StringProperty { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public int IntegerProperty { get; set; }
			public DateTime DateTimeProperty { get; set; }
		}


		[ Subject( typeof ( DynamicFluentBuilder< > ) ) ]
		public class When_building_the_object : Given_a_DynamicFluentBuilder_that_uses_default_value_conventions
		{
			static TestClass _result;

			Because of = () => _result = _builder.build();

			It should_use_default_convention_for_a_first_name_property = () => Assert.Contains( _result.FirstName, RandomData.FirstNames );
			It should_use_default_convention_for_a_last_name_property = () => Assert.Contains( _result.LastName, RandomData.LastNames );
			It should_use_default_convention_for_date_property = () => _result.DateTimeProperty.should_not_be_equal_to( new DateTime() );
			It should_use_default_convention_for_integer_property = () => _result.IntegerProperty.should_not_be_equal_to( 0 );
			It should_use_default_convention_for_string_property = () => _result.StringProperty.Length.should_be_equal_to( 20 );
		}


		[ Subject( typeof ( DynamicFluentBuilder< > ) ) ]
		public class When_building_the_object_after_specifying_dynamic_date_time_property_value : Given_a_DynamicFluentBuilder_that_uses_default_value_conventions
		{
			static TestClass _result;
			static DateTime _dateTime;

			Establish context = () =>
			                    	{
			                    		_dateTime = ARandom.DateTime();
			                    		_builder.With( x => x.DateTimeProperty, _dateTime );
			                    	};

			Because of = () => _result = _builder.build();

			It should_use_the_provided_date_time = () => _result.DateTimeProperty.ShouldEqual( _dateTime );
		}


		[ Subject( typeof ( DynamicFluentBuilder< > ) ) ]
		public class When_building_the_object_after_specifying_dynamic_integer_property_value : Given_a_DynamicFluentBuilder_that_uses_default_value_conventions
		{
			static TestClass _result;
			static int _expected;

			Establish context = () =>
			                    	{
			                    		_expected = ARandom.Int();
			                    		_builder.With( x => x.IntegerProperty, _expected );
			                    	};

			Because of = () => _result = _builder.build();

			It should_use_the_provided_date_time = () => _result.IntegerProperty.ShouldEqual( _expected );
		}


		[ Subject( typeof ( DynamicFluentBuilder< > ) ) ]
		public class When_building_the_object_after_specifying_dynamic_string_property_value : Given_a_DynamicFluentBuilder_that_uses_default_value_conventions
		{
			static TestClass _result;
			static string _expected;

			Establish context = () =>
			                    	{
			                    		_expected = ARandom.String( 20 );
			                    		_builder.With( x => x.StringProperty, _expected );
			                    	};

			Because of = () => _result = _builder.build();

			It should_use_the_provided_date_time = () => _result.StringProperty.ShouldEqual( _expected );
		}


		[ Subject( typeof ( DynamicFluentBuilder< > ) ) ]
		public class When_building_the_object_after_specifying_dynamic_property_value_using_For : Given_a_DynamicFluentBuilder_that_uses_default_value_conventions
		{
			static TestClass _result;
			static string _expected;

			Establish context = () =>
			                    	{
			                    		_expected = ARandom.String( 20 );
			                    		_builder.For( x => x.StringProperty, _expected );
			                    	};

			Because of = () => _result = _builder.build();

			It should_use_the_provided_date_time = () => _result.StringProperty.ShouldEqual( _expected );
		}


		[ Subject( typeof ( DynamicFluentBuilder< > ) ) ]
		public class When_building_the_object_after_specifying_dynamic_property_value_using_Having : Given_a_DynamicFluentBuilder_that_uses_default_value_conventions
		{
			static TestClass _result;
			static string _expected;

			Establish context = () =>
			                    	{
			                    		_expected = ARandom.String( 20 );
			                    		_builder.Having( x => x.StringProperty, _expected );
			                    	};

			Because of = () => _result = _builder.build();

			It should_use_the_provided_date_time = () => _result.StringProperty.ShouldEqual( _expected );
		}
	}
}


// ReSharper restore InconsistentNaming