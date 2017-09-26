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
	public class FirstNameConventionSpecs
	{
		public abstract class When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			protected static IDefaultConvention< string > convention;
			protected static PropertyInfo propertyInfo;

			Because of = () => { convention = Convention.FirstName(); };
		}


		[ Subject( typeof ( Convention ), "FirstName" ) ]
		public class When_property_name_is_lowercase_firstname : When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {firstname = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.firstname );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_first_name = () => convention.DefaultValue( propertyInfo ).Should().Not.Be.Empty();
		}


		[ Subject( typeof ( Convention ), "FirstName" ) ]
		public class When_property_name_is_mixed_case_FirstName : When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {FirstName = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.FirstName );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_first_name = () => convention.DefaultValue( propertyInfo ).Should().Not.Be.Empty();
		}


		[ Subject( typeof ( Convention ), "FirstName" ) ]
		public class When_property_name_contains_lowercase_firstname : When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {customerfirstname = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.customerfirstname );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_first_name = () => convention.DefaultValue( propertyInfo ).Should().Not.Be.Empty();
		}


		[ Subject( typeof ( Convention ), "FirstName" ) ]
		public class When_property_name_contains_mixed_case_FirstName : When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {CustomerFirstName = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.CustomerFirstName );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_first_name = () => convention.DefaultValue( propertyInfo ).Should().Not.Be.Empty();
		}


		[ Subject( typeof ( Convention ), "FirstName" ) ]
		public class When_given_a_property_with_name_other_than_firstname : When_getting_the_default_value_for_a_property_having_a_first_name_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {lastname = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.lastname );
			                    	};

			It should_not_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.False();
			It should_return_nothing = () => convention.DefaultValue( propertyInfo ).Should().Be.Null();
		}
	}
}


// ReSharper restore InconsistentNaming