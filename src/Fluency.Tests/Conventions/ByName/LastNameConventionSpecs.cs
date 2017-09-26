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

using System.Reflection;
using Fluency.Conventions;
using Fluency.Utils;
using Machine.Specifications;
using SharpTestsEx;

// ReSharper disable InconsistentNaming


namespace Fluency.Tests.Deprecated.Conventions.ByName
{
	public class LastNameConventionSpecs
	{
		public abstract class When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
		{
			protected static IDefaultConvention< string > convention;
			protected static PropertyInfo propertyInfo;

			Because of = () => { convention = Convention.LastName(); };
		}


		[ Subject( typeof ( Convention ), "LastName" ) ]
		public class When_property_name_is_lowercase_lastname : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {lastname = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.lastname );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_last_name = () => convention.DefaultValue( propertyInfo ).Should().Not.Be.Empty();
		}


		[ Subject( typeof ( Convention ), "LastName" ) ]
		public class When_property_name_is_mixed_case_LastName : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {LastName = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.LastName );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_last_name = () => convention.DefaultValue( propertyInfo ).Should().Not.Be.Empty();
		}


		[ Subject( typeof ( Convention ), "LastName" ) ]
		public class When_property_name_contains_lowercase_lastname : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {customerlastname = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.customerlastname );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_last_name = () => convention.DefaultValue( propertyInfo ).Should().Not.Be.Empty();
		}


		[ Subject( typeof ( Convention ), "LastName" ) ]
		public class When_property_name_contains_mixed_case_LastName : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {CustomerLastName = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.CustomerLastName );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_last_name = () => convention.DefaultValue( propertyInfo ).Should().Not.Be.Empty();
		}


		[ Subject( typeof ( Convention ), "LastName" ) ]
		public class When_given_a_property_with_name_other_than_lastname : When_getting_the_default_value_for_a_property_having_a_last_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {othername = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.othername );
			                    	};

			It should_not_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.False();
			It should_return_nothing = () => convention.DefaultValue( propertyInfo ).Should().Be.Null();
		}
	}
}


// ReSharper restore InconsistentNaming