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
		public class FooBuilder : FluentBuilder< Foo > {}

		#endregion


		[ Subject( "FluentBuilder" ) ]
		public class Given_a_builder_for_a_target_type_having_a_list_property
		{
			public static FooBuilder _builder;
			public static Foo _buildResult;

			private Establish context = () => _builder = new FooBuilder();

			private Because of = () => _buildResult = _builder.build();

			// Helper Method
			protected static IFluentBuilder< T > MockBuilderFor< T >( T itemToBuild )
			{
				var builder = MockRepository.GenerateMock< IFluentBuilder< T > >();
				builder.Stub( x => x.build() ).Return( itemToBuild );
				return builder;
			}
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

				private Establish context = () =>
				                            	{
				                            		// Setup mock list builder.
				                            		_listBuilder = MockRepository.GenerateMock< FluentListBuilder< Bar > >();
				                            		_listBuilder.Stub( x => x.build() ).Return( _expectedList );

				                            		// Set the list builder.
				                            		_builder.SetList( x => x.Bars, _listBuilder );
				                            	};

				private It should_invoke_the_list_builder_to_get_the_list_propertys_value = () => _listBuilder.AssertWasCalled( x => x.build() );
				private It should_build_an_instance_having_the_list_property_set_to_the_result_of_the_list_builder = () => _buildResult.Bars.Should().Be.SameInstanceAs( _expectedList );
			}


			[ Subject( "FluentBuilder" ) ]
			public class When_setting_the_list_property_using_a_builder_that_is_not_a_list_builder : Given_a_builder_for_a_target_type_having_a_list_property
			{
				public static IFluentBuilder< Bar > _nonListBuilder;

				private Establish context = () => _nonListBuilder = new FluentBuilder< Bar >();

				private It should_fail = () => Assert.Throws< ArgumentException >( () => _builder.SetList( x => x.Bars, _nonListBuilder ) );
			}


			[ Subject( "FluentBuilder" ) ]
			public class When_trying_to_use_a_list_builder_to_build_a_property_that_is_not_a_list_property : Given_a_builder_for_a_target_type_having_a_list_property
			{
				private static FluentListBuilder< Bar > _listBuilder;

				private Establish context = () => _listBuilder = new FluentListBuilder< Bar >();

				private It should_fail = () => Assert.Throws< ArgumentException >( () => _builder.SetList( x => x.Bar, _listBuilder ) );
			}
		}


		public class By_passing_in_a_prepopulated_list_for_the_list_property
		{
			[ Subject( "FluentBuilder" ) ]
			public class When_setting_a_list_property_to_an_existing_prepopulated_list : Given_a_builder_for_a_target_type_having_a_list_property
			{
				private static IList< Bar > _expectedList;

				private Establish context = () =>
				                            	{
				                            		_expectedList = new List< Bar > { new Bar() };
				                            		_builder.SetProperty( x => x.Bars, _expectedList );
				                            	};

				private It should_build_an_instance_having_the_list_set_to_the_prepopulated_list = () => _buildResult.Bars.Should().Be.SameInstanceAs( _expectedList );
			}
		}


		/// <summary>
		/// To add an item to a list from within a custom builder, you can call <code>AddToList( x => x.ListProperty, item );</code>
		/// </summary>
		public class By_adding_an_item_to_the_list
		{
			[ Subject( "FluentBuilder" ) ]
			public class When_adding_a_list_item_to_a_list_property_using_AddToList : Given_a_builder_for_a_target_type_having_a_list_property
			{
				private static Bar _expectedListItem = new Bar();

				private Establish context = () => _builder.AddToList( x => x.Bars, _expectedListItem );

				private It should_build_an_instance_whose_list_property_contains_the_new_item = () => _buildResult.Bars.Should().Contain( _expectedListItem );
			}


			[ Subject( "FluentBuilder" ) ]
			public class When_adding_a_list_item_to_a_list_property_using_deprecated_AddListItem_method : Given_a_builder_for_a_target_type_having_a_list_property
			{
				private static Bar _expectedListItem = new Bar();

				private Establish context = () => _builder.AddListItem( x => x.Bars, _expectedListItem );

				private It should_build_an_instance_whose_list_property_contains_the_new_item = () => _buildResult.Bars.Should().Contain( _expectedListItem );
			}
		}


		/// <summary>
		/// To add multiple items to a list from within a custom builder, you can call <code>AddToList( x => x.ListProperty, item1, item2, item3 ..., itemN );</code>
		/// </summary>
		public class By_adding_multiple_items_to_the_list
		{
			[ Subject( "FluentBuilder" ) ]
			public class When_adding_multiple_list_items_to_a_list_property : Given_a_builder_for_a_target_type_having_a_list_property
			{
				private static Bar _expectedListItem1 = new Bar();
				private static Bar _expectedListItem2 = new Bar();

				private Establish context = () => _builder.AddToList( x => x.Bars, _expectedListItem1, _expectedListItem2 );

				private It should_build_an_instance_whose_list_property_contains_the_first_item = () => _buildResult.Bars.Should().Contain( _expectedListItem1 );
				private It should_build_an_instance_whose_list_property_contains_the_second_item = () => _buildResult.Bars.Should().Contain( _expectedListItem2 );
			}
		}


		/// <summary>
		/// To add an item to a list from within a custom builder, you can call <code>AddListItem( x => x.ListProperty, item );</code>
		/// </summary>
		public class By_adding_a_builder_to_build_an_item_for_the_list
		{
			[ Subject( "FluentBuilder" ) ]
			public class When_adding_a_list_item_to_a_list_property_using_deprecated_AddListItem_method : Given_a_builder_for_a_target_type_having_a_list_property
			{
				private static Bar _expectedListItem = new Bar();
				private static IFluentBuilder< Bar > _listItemBuilder;

				private Establish context = () =>
				                            	{
				                            		_listItemBuilder = MockBuilderFor( _expectedListItem );
				                            		_builder.AddListItem( x => x.Bars, _listItemBuilder );
				                            	};

				private It should_invoke_the_builder_to_create_the_new_list_item = () => _listItemBuilder.AssertWasCalled( x => x.build() );
				private It should_build_an_instance_whose_list_property_contains_the_new_item = () => _buildResult.Bars.Should().Contain( _expectedListItem );
			}


			[ Subject( "FluentBuilder" ) ]
			public class When_adding_a_builder_for_a_list_item_to_a_list_property_using_AddToList : Given_a_builder_for_a_target_type_having_a_list_property
			{
				private static Bar _expectedListItem = new Bar();
				private static IFluentBuilder< Bar > _listItemBuilder;

				private Establish context = () =>
				                            	{
				                            		_listItemBuilder = MockBuilderFor( _expectedListItem );
				                            		_builder.AddToList( x => x.Bars, _listItemBuilder );
				                            	};

				private It should_invoke_the_builder_to_create_the_new_list_item = () => _listItemBuilder.AssertWasCalled( x => x.build() );
				private It should_build_an_instance_whose_list_property_contains_the_new_item = () => _buildResult.Bars.Should().Contain( _expectedListItem );
			}


			[ Subject( "FluentBuilder" ) ]
			public class When_adding_multiple_builders_for_list_items_to_a_list_property_using_AddToList : Given_a_builder_for_a_target_type_having_a_list_property
			{
				private static Bar _expectedListItem1 = new Bar();
				private static IFluentBuilder< Bar > _listItemBuilder1;

				private static Bar _expectedListItem2 = new Bar();
				private static IFluentBuilder< Bar > _listItemBuilder2;

				private Establish context = () =>
				                            	{
				                            		_listItemBuilder1 = MockBuilderFor( _expectedListItem1 );
				                            		_listItemBuilder2 = MockBuilderFor( _expectedListItem2 );

				                            		_builder.AddToList( x => x.Bars, _listItemBuilder1, _listItemBuilder2 );
				                            	};

				private It should_invoke_the_builder_to_create_the_first_list_item = () => _listItemBuilder1.AssertWasCalled( x => x.build() );
				private It should_build_an_instance_whose_list_property_contains_the_first_item = () => _buildResult.Bars.Should().Contain( _expectedListItem1 );

				private It should_invoke_the_builder_to_create_the_second_list_item = () => _listItemBuilder2.AssertWasCalled( x => x.build() );
				private It should_build_an_instance_whose_list_property_contains_the_second_item = () => _buildResult.Bars.Should().Contain( _expectedListItem2 );
			}
		}
	}
}


// ReSharper restore InconsistentNaming