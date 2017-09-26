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

using Machine.Specifications;
using SharpTestsEx;

// ReSharper disable InconsistentNaming


namespace Fluency.Tests.Deprecated.BuilderTests
{
	/// <summary>
	/// A FluentBuilder can be set as an 'alias' for an existing instance. 
	/// This means it will return the existing instance rather than build a new one.
	/// <example>
	///		_builder.AliasFor( _existingInstance );
	/// </example>
	/// </summary>
	public class Using_a_custom_builder_as_an_alias_for_an_existing_instance
	{
		[ Subject( "FluentBuilder" ) ]
		public class Given_a_custom_builder
		{
			public static MyCustomTypeBuilder _builder;
			public static MyCustomType _existingInstance = new MyCustomType();
			public static MyCustomType _buildResult;

			Establish context = () => _builder = new MyCustomTypeBuilder();

			Because of = () => _buildResult = _builder.build();
		}


		[ Subject( "FluentBuilder" ) ]
		public class When_setting_the_builder_as_an_alias_for_an_existing_instance : Given_a_custom_builder
		{
			Establish context = () => _builder.AliasFor( _existingInstance );

			It should_return_the_existing_instance_as_the_build_result = () => _buildResult.Should().Be.SameInstanceAs( _existingInstance );
		}


		[ Subject( "FluentBuilder" ) ]
		public class When_setting_the_builder_as_an_alias_for_an_existing_instance_using_the_UsePrebuiltResult_alternative_syntax : Given_a_custom_builder
		{
			Establish context = () => _builder.UsePreBuiltResult( _existingInstance );

			It should_return_the_existing_instance_as_the_build_result = () => _buildResult.Should().Be.SameInstanceAs( _existingInstance );
		}


		[ Subject( "FluentBuilder" ) ]
		public class When_setting_the_builder_as_an_alias_for_an_existing_instance_then_setting_it_as_an_alias_for_a_different_instance : Given_a_custom_builder
		{
			public static MyCustomType _differentInstance = new MyCustomType();

			Establish context = () =>
			                    	{
			                    		_builder.AliasFor( _existingInstance );
			                    		_builder.AliasFor( _differentInstance );
			                    	};

			It should_return_the_last_instance_it_was_set_as_an_alias_for = () => _buildResult.Should().Be.SameInstanceAs( _differentInstance );
		}


		#region Builder and Target Types Used In This Test

		public class MyCustomType {}


		public class MyCustomTypeBuilder : FluentBuilder< MyCustomType > {}

		#endregion
	}
}


// ReSharper restore InconsistentNaming