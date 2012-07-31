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
using System.Collections.Generic;
using Machine.Specifications;
using NUnit.Framework;
using Rhino.Mocks;
using SharpTestsEx;

// ReSharper disable InconsistentNaming


namespace Fluency.Tests.BuilderTests
{
	/// <summary>
	/// A FluentBuilder can work with properties that are generic lists. It does not yet support other types of collections though.
	/// </summary>
	public class Using_a_custom_builder_to_work_with_a_list_property
	{
		#region Target class and builder

		/// <summary>
		/// Target Class <see cref="Foo"/> has a list property of type <see cref="IList{T}"/> 
		/// </summary>
		public class Foo
		{
			/// <summary>
			/// Non-List Property.
			/// </summary>
			/// <value>The bar.</value>
			public Bar Bar { get; set; }

			/// <summary>
			/// List Property.
			/// </summary>
			/// <value>The bars.</value>
			public IList< Bar > Bars { get; set; }
		}


		/// <summary>
		/// Bar is simply a type that we are using in for the list property.
		/// </summary>
		public class Bar {}


		/// <summary>
		/// FooBuilder builds <see cref="Foo"/> and allows us to set the Bars list property 
		/// by passing in a <see cref="FluentListBuilder{Bar}"/> which is the point of this test.
		/// </summary>
		public class FooBuilder : FluentBuilder< Foo >
		{
			public FooBuilder()
			{
				//SetList( x => x.Bars, new FluentListBuilder< Bar >() );
			}
		}

		#endregion


		[ Subject( "FluentBuilder" ) ]
		public class Given_a_builder_for_a_target_type_having_a_list_property
		{
			public static FooBuilder _builder;
			public static Foo _buildResult;

			Establish context = () => _builder = new FooBuilder();

			Because of = () => _buildResult = _builder.build();
		}


		/// <summary>
		/// You can set a list property's value to be built by a <see cref="FluentListBuilder{T}"/>
		///  by using <code>SetList()</code> and passing the ListBuilder as the value for the property.
		/// The code here calls SetList on the builder, but you would call SetList from within your custom builder.
		/// </summary>
		public class By_passing_in_a_list_builder_to_build_the_list_for_the_property
		{
			[ Subject( "FluentBuilder" ) ]
			public class When_setting_the_list_property_to_an_instance_of_a_list_builder : Given_a_builder_for_a_target_type_having_a_list_property
			{
				public static IFluentListBuilder< Bar > _listBuilder;
				public static IList< Bar > _expectedList = new List< Bar >();

				Establish context = () =>
				                    	{
				                    		// Setup mock list builder.
				                    		_listBuilder = MockRepository.GenerateMock< FluentListBuilder< Bar > >();
				                    		_listBuilder.Stub( x => x.build() ).Return( _expectedList );

				                    		// Set the list builder.
				                    		_builder.SetList( x => x.Bars, _listBuilder );
				                    	};

				It should_invoke_the_list_builder_to_get_the_list_propertys_value = () => _listBuilder.AssertWasCalled( x => x.build() );
				It should_build_an_instance_having_the_list_property_set_to_the_result_of_the_list_builder = () => _buildResult.Bars.Should().Be.SameInstanceAs( _expectedList );
			}


			[ Subject( "FluentBuilder" ) ]
			public class When_setting_the_list_property_using_a_builder_that_is_not_a_list_builder : Given_a_builder_for_a_target_type_having_a_list_property
			{
				public static IFluentBuilder< Bar > _nonListBuilder;

				Establish context = () => _nonListBuilder = new FluentBuilder< Bar >();

				It should_fail = () => Assert.Throws< ArgumentException >( () => _builder.SetList( x => x.Bars, _nonListBuilder ) );
			}


			[ Subject( "FluentBuilder" ) ]
			public class When_trying_to_use_a_list_builder_to_build_a_property_that_is_not_a_list_property : Given_a_builder_for_a_target_type_having_a_list_property
			{
				static FluentListBuilder< Bar > _listBuilder;

				Establish context = () => _listBuilder = new FluentListBuilder< Bar >();

				It should_fail = () => Assert.Throws< ArgumentException >( () => _builder.SetList( x => x.Bar, _listBuilder ) );
			}
		}

		public class By_passing_in_a_prepopulated_list_for_the_list_property
		{
			[ Subject( "FluentBuilder" ) ]
			public class When_setting_a_list_property_to_an_existing_prepopulated_list : Given_a_builder_for_a_target_type_having_a_list_property
			{
				static IList< Bar > _expectedList;

				Establish context = () =>
				                    	{
				                    		_expectedList = new List< Bar >{ new Bar() };
				                    		_builder.SetProperty( x => x.Bars, _expectedList );
				                    	};

				It should_build_an_instance_having_the_list_set_to_the_prepopulated_list = () => _buildResult.Bars.Should().Be.SameInstanceAs( _expectedList );
			}
		}


		/// <summary>
		/// To add an item to a list from within a custom builder, you can call <code>AddListItem( x => x.ListProperty, item );</code>
		/// </summary>
		public class By_adding_an_item_to_the_list
		{
			[ Subject( "FluentBuilder" ) ]
			public class When_adding_a_list_item_to_a_list_property : Given_a_builder_for_a_target_type_having_a_list_property
			{
				static Bar _expectedListItem = new Bar();

				Establish context = () => _builder.AddListItem( x => x.Bars, _expectedListItem );

				It should_build_an_instance_whose_list_property_contains_the_new_item = () => _buildResult.Bars.Should().Contain( _expectedListItem );
			}
		}


		/// <summary>
		/// To add an item to a list from within a custom builder, you can call <code>AddListItem( x => x.ListProperty, item );</code>
		/// </summary>
		public class By_adding_a_builder_to_build_an_item_for_the_list
		{
			[ Subject( "FluentBuilder" ) ]
			public class When_adding_a_list_item_to_a_list_property : Given_a_builder_for_a_target_type_having_a_list_property
			{
				static Bar _expectedListItem = new Bar();
				static IFluentBuilder< Bar > _listItemBuilder;

				Establish context = () =>
				                    	{
				                    		// Setup mock builder to return the new item.
				                    		_listItemBuilder = MockRepository.GenerateMock< IFluentBuilder< Bar > >();
				                    		_listItemBuilder.Stub( x => x.build() ).Return( _expectedListItem );

				                    		// Add the new item by adding its builder.
				                    		_builder.AddListItem( x => x.Bars, _listItemBuilder );
				                    	};

				It should_invoke_the_builder_to_create_the_new_list_item = () => _listItemBuilder.AssertWasCalled( x => x.build() );
				It should_build_an_instance_whose_list_property_contains_the_new_item = () => _buildResult.Bars.Should().Contain( _expectedListItem );
			}
		}
	}
}


// ReSharper restore InconsistentNaming