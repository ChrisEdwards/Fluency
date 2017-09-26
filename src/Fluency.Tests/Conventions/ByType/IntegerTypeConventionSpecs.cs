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


namespace Fluency.Tests.Deprecated.Conventions.ByType
{
	public class IntegerTypeConventionsSpecs
	{
		public abstract class When_getting_the_default_value_for_a_property_having_an_integer_type_convention_applied
		{
			protected static IDefaultConvention< int > convention;
			protected static PropertyInfo propertyInfo;

			Because of = () => { convention = Convention.IntegerType(); };
		}


		[ Subject( typeof ( Convention ), "ByType<DateTime>" ) ]
		public class When_property_is_a_Integer_type : When_getting_the_default_value_for_a_property_having_an_integer_type_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {IntProperty = 1};
			                    		propertyInfo = person.PropertyInfoFor( x => x.IntProperty );
			                    	};

			It should_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.True();
			It should_return_a_random_integer = () => convention.DefaultValue( propertyInfo ).Should().Not.Be.EqualTo( 0 );
		}


		[ Subject( typeof ( Convention ), "ByType<DateTime>" ) ]
		public class When_property_is_not_an_Integer_type : When_getting_the_default_value_for_a_property_having_an_integer_type_convention_applied
		{
			Establish context = () =>
			                    	{
			                    		var person = new {NonIntegerProperty = "bob"};
			                    		propertyInfo = person.PropertyInfoFor( x => x.NonIntegerProperty );
			                    	};

			It should_not_apply = () => convention.AppliesTo( propertyInfo ).Should().Be.False();
			It should_return_the_zero = () => convention.DefaultValue( propertyInfo ).Should().Be.EqualTo( 0 );
		}
	}
}


// ReSharper restore InconsistentNaming