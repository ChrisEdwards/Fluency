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
			public FooBuilder SetBarUsingBuilder( IFluentBuilder builder )
			{
				SetProperty( x => x.Bar, builder );
				return this;
			}


			public FooBuilder SetBarsListUsingBuilder( IFluentBuilder barListBuilder )
			{
				SetList( x => x.Bars, barListBuilder );
				return this;
			}
		}

		#endregion


		/// <summary>
		/// You can set a list property's value to be built by a <see cref="FluentListBuilder{T}"/>
		///  by using <code>SetList()</code> and passing the ListBuilder as the value for the property.
		/// <example>
		///		<code>_builder.SetList( x => x.MyList, new FluentListBuilder&lt;MyList&gt;() );</code>
		/// </example>
		/// </summary>
		public class By_passing_in_a_list_builder_to_build_the_list_for_the_property
		{
			[ Subject( "FluentBuilder" ) ]
			public class Given_a_builder_for_a_target_type_having_a_list_property
			{
				public static FooBuilder _builder;
				public static Foo _buildResult;

				Establish context = () => _builder = new FooBuilder();

				Because of = () => _buildResult = _builder.build();
			}


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
				                    		_builder.SetBarsListUsingBuilder( _listBuilder );
				                    	};

				It should_invoke_the_list_builder_to_get_the_list_propertys_value = () => _listBuilder.AssertWasCalled( x => x.build() );
				It should_build_an_instance_having_the_list_property_set_to_the_result_of_the_list_builder = () => _buildResult.Bars.Should().Be.SameInstanceAs( _expectedList );
			}


			[ Subject( "FluentBuilder" ) ]
			public class When_setting_the_list_property_using_a_builder_that_is_not_a_list_builder : Given_a_builder_for_a_target_type_having_a_list_property
			{
				public static IFluentBuilder< Bar > _nonListBuilder;

				Establish context = () => _nonListBuilder = new FluentBuilder< Bar >();

				It should_fail = () => Assert.Throws< ArgumentException >( () => _builder.SetBarsListUsingBuilder( _nonListBuilder ) );
			}


			[ Subject( "FluentBuilder" ) ]
			public class When_trying_to_use_a_list_builder_to_build_a_property_that_is_not_a_list_property : Given_a_builder_for_a_target_type_having_a_list_property
			{
				static FluentListBuilder< Bar > _listBuilder;

				Establish context = () => _listBuilder = new FluentListBuilder< Bar >();

				It should_fail = () => Assert.Throws< ArgumentException >( () => _builder.SetBarUsingBuilder( _listBuilder ) );
			}
		}


		[ Subject( typeof ( FluentBuilder< > ) ) ]
		public class Given_a_builder_for_a_class_having_a_list_property
		{
			public static ClassWithListPropertyBuilder _builder;

			Establish context = () => { _builder = new ClassWithListPropertyBuilder(); };
		}


		public class ClassWithListProperty
		{
			public IList< DynamicFluentBuilderSpecs.TestClass > TestClasses { get; set; }
		}


		public class ClassWithListPropertyBuilder : FluentBuilder< ClassWithListProperty >
		{
			public ClassWithListPropertyBuilder()
			{
				SetList( x => x.TestClasses, new FluentListBuilder< DynamicFluentBuilderSpecs.TestClass >() );
			}


			public ClassWithListPropertyBuilder Having( DynamicFluentBuilderSpecs.TestClass item )
			{
				AddListItem( x => x.TestClasses, item );
				return this;
			}
		}


		[ Subject( typeof ( FluentBuilder< > ) ) ]
		public class When_adding_an_item_to_the_list_builder : Given_a_builder_for_a_class_having_a_list_property
		{
			static ClassWithListProperty _result;
			static DynamicFluentBuilderSpecs.TestClass _expectedListItem;

			Establish context = () =>
			                    	{
			                    		_expectedListItem = new DynamicFluentBuilderSpecs.TestClass();

			                    		// Calls "AddItem" on the list builder.
			                    		_builder.Having( _expectedListItem );
			                    	};

			Because of = () => _result = _builder.build();

			It should_build_a_list_with_the_expected_list_item = () => _result.TestClasses.Should().Contain( _expectedListItem );
		}
	}
}


// ReSharper restore InconsistentNaming