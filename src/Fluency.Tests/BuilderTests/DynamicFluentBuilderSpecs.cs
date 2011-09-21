// Copyright 2011 Chris Edwards
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System;
using System.Linq;
using Fluency.DataGeneration;
using Machine.Specifications;
using SharpTestsEx;

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
			public TestClass ReferenceProperty { get; set; }
		}


		[ Subject( typeof ( DynamicFluentBuilder< > ) ) ]
		public class When_building_the_object : Given_a_DynamicFluentBuilder_that_uses_default_value_conventions
		{
			static TestClass _result;

			Because of = () => _result = _builder.build();

			It should_use_default_convention_for_a_first_name_property = () => _result.FirstName.Satisfy( x => RandomData.FirstNames.Contains( x ) );
			It should_use_default_convention_for_a_last_name_property = () => _result.LastName.Satisfy( x => RandomData.LastNames.Contains( x ) );
			It should_use_default_convention_for_date_property = () => _result.DateTimeProperty.Should().Not.Be.EqualTo( default( DateTime ) );
			It should_use_default_convention_for_integer_property = () => _result.IntegerProperty.Should().Not.Be.EqualTo( 0 );
			It should_use_default_convention_for_string_property = () => _result.StringProperty.Length.Should().Be.EqualTo( 20 );
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

			It should_use_the_provided_date_time = () => _result.DateTimeProperty.Should().Be.EqualTo( _dateTime );
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

			It should_use_the_provided_date_time = () => _result.IntegerProperty.Should().Be.EqualTo( _expected );
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

			It should_use_the_provided_date_time = () => _result.StringProperty.Should().Be.EqualTo( _expected );
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

			It should_use_the_provided_date_time = () => _result.StringProperty.Should().Be.EqualTo( _expected );
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

			It should_use_the_provided_date_time = () => _result.StringProperty.Should().Be.EqualTo( _expected );
		}


		[ Subject( typeof ( DynamicFluentBuilder< > ) ) ]
		public class When_building_the_object_after_specifying_dynamic_property_builder_using_With : Given_a_DynamicFluentBuilder_that_uses_default_value_conventions
		{
			static TestClass _result;
			static TestClass _expectedValue;

			Establish context = () =>
			                    	{
			                    		// Create a builder that will return the expected value.
			                    		_expectedValue = new TestClass {FirstName = "Bob", LastName = "Smith"};
			                    		FluentBuilder< TestClass > propertyBuilder = new DynamicFluentBuilder< TestClass >().AliasFor( _expectedValue );

			                    		_builder.With( x => x.ReferenceProperty, propertyBuilder );
			                    	};

			Because of = () => _result = _builder.build();

			It should_use_the_provided_builder_to_build_the_result = () => _result.ReferenceProperty.Should().Be.EqualTo( _expectedValue );
		}


		[ Subject( typeof ( DynamicFluentBuilder< > ) ) ]
		public class When_building_the_object_after_specifying_dynamic_property_builder_using_For : Given_a_DynamicFluentBuilder_that_uses_default_value_conventions
		{
			static TestClass _result;
			static TestClass _expectedValue;

			Establish context = () =>
			                    	{
			                    		// Create a builder that will return the expected value.
			                    		_expectedValue = new TestClass {FirstName = "Bob", LastName = "Smith"};
			                    		FluentBuilder< TestClass > propertyBuilder = new DynamicFluentBuilder< TestClass >().AliasFor( _expectedValue );

			                    		_builder.For( x => x.ReferenceProperty, propertyBuilder );
			                    	};

			Because of = () => _result = _builder.build();

			It should_use_the_provided_builder_to_build_the_result = () => _result.ReferenceProperty.Should().Be.EqualTo( _expectedValue );
		}


		[ Subject( typeof ( DynamicFluentBuilder< > ) ) ]
		public class When_building_the_object_after_specifying_dynamic_property_builder_using_Having : Given_a_DynamicFluentBuilder_that_uses_default_value_conventions
		{
			static TestClass _result;
			static TestClass _expectedValue;

			Establish context = () =>
			                    	{
			                    		// Create a builder that will return the expected value.
			                    		_expectedValue = new TestClass {FirstName = "Bob", LastName = "Smith"};
			                    		FluentBuilder< TestClass > propertyBuilder = new DynamicFluentBuilder< TestClass >().AliasFor( _expectedValue );

			                    		_builder.Having( x => x.ReferenceProperty, propertyBuilder );
			                    	};

			Because of = () => _result = _builder.build();

			It should_use_the_provided_builder_to_build_the_result = () => _result.ReferenceProperty.Should().Be.EqualTo( _expectedValue );
		}
	}
}


// ReSharper restore InconsistentNaming