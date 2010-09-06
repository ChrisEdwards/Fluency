using System.Collections.Generic;
using Machine.Specifications;
using SharpTestsEx;

// ReSharper disable InconsistentNaming


namespace Fluency.Tests.BuilderTests
{
	public class When_a_builder_populates_a_property_that_is_a_list
	{
		[ Subject( typeof ( FluentBuilder< > ) ) ]
		public class Given_a_builder_for_a_class_having_a_list_property
		{
			public static ClassWithListPropertyBuilder _builder;

			Establish context = () =>
			                    	{
			                    		Fluency.Initialize( x => x.UseDefaultValueConventions() );
			                    		_builder = new ClassWithListPropertyBuilder();
			                    	};
		}


		public class ClassWithListProperty
		{
			public IList< string > Strings { get; set; }
			public IList<DynamicFluentBuilderSpecs.TestClass> TestClasses { get; set; }
		}


		public class ClassWithListPropertyBuilder : FluentBuilder< ClassWithListProperty >
		{
			public ClassWithListPropertyBuilder()
			{
				SetList( x => x.Strings, new FluentListBuilder< DynamicFluentBuilderSpecs.TestClass >() );
			}


			public ClassWithListPropertyBuilder Having(DynamicFluentBuilderSpecs.TestClass item)
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

			Because of = () =>  _result = _builder.build();

			It should_build_a_list_with_the_expected_list_item = () => _result.TestClasses.Should().Contain( _expectedListItem );
		}
	}
}


// ReSharper restore InconsistentNaming