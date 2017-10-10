using System;
using System.Collections.Generic;
using System.Security.Permissions;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Fluency.Tests.BuilderTests
{
    /// <summary>
    /// A FluentBuilder can work with properties that are generic lists. 
    /// It does not yet support other types of collections though.
    /// </summary>
    public class Using_a_custom_builder_to_work_with_a_list_property
    {
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
            public IList<Bar> Bars { get; set; }
        }


        /// <summary>
        /// Bar is simply a type that we are using in for the list property.
        /// </summary>
        public class Bar { }
        
        public class FooBuilder : FluentBuilder<Foo> { }
        
        public class Given_a_builder_for_a_target_type_having_a_list_property
        {
            protected FooBuilder _builder;

            protected Given_a_builder_for_a_target_type_having_a_list_property()
            {
                _builder = new FooBuilder();
            }
        }

        /// <summary>
        /// You can set a list property's value to be built by a <see cref="FluentListBuilder{T}"/>
        ///  by using <code>SetList()</code> and passing the ListBuilder as the value for the property.
        /// The code here calls SetList on the builder, but you would call SetList from within your custom builder.
        /// </summary>
        public class By_passing_in_a_list_builder_to_build_the_list_for_the_property
        {
            public class
                When_setting_the_list_property_to_an_instance_of_a_list_builder :
                    Given_a_builder_for_a_target_type_having_a_list_property
            {
                [Fact]
                public void should_invoke_the_list_builder_to_get_the_list_propertys_value()
                {
                    // TODO: Rewrite tests to not use mocks
                    IFluentListBuilder<Bar> listBuilder = Substitute.For<IFluentListBuilder<Bar>>();
                    IList<Bar> _expectedList = new List<Bar>();

                    listBuilder.build().Returns(_expectedList);

                    _builder.SetList(x => x.Bars, listBuilder);

                    var buildResult = _builder.build();

                    buildResult.Bars.Should().BeSameAs(_expectedList);
                }
            }

            public class When_setting_the_list_property_using_a_builder_that_is_not_a_list_builder :
                Given_a_builder_for_a_target_type_having_a_list_property
            {
                [Fact]
                public void should_fail()
                {
                    IFluentBuilder<Bar> nonListBuilder = new FluentBuilder<Bar>();

                    Action setList = () => _builder.SetList(x => x.Bars, nonListBuilder);

                    setList.ShouldThrow<ArgumentException>();
                }
            }

            public class When_trying_to_use_a_list_builder_to_build_a_property_that_is_not_a_list_property :
                Given_a_builder_for_a_target_type_having_a_list_property
            {
                [Fact]
                public void should_fail()
                {
                    FluentListBuilder<Bar> _listBuilder = new FluentListBuilder<Bar>();

                    Action setProperty = () => _builder.SetList(x => x.Bar, _listBuilder);

                    setProperty.ShouldThrow<ArgumentException>();
                }
            }           
        }

        public class By_passing_in_a_prepopulated_list_for_the_list_property
        {
            public class When_setting_a_list_property_to_an_existing_prepopulated_list :
                Given_a_builder_for_a_target_type_having_a_list_property
            {
                [Fact]
                public void should_build_an_instance_having_the_list_set_to_the_prepopulated_list()
                {
                    var expectedList = new List<Bar> { new Bar() };

                    _builder.SetProperty(x => x.Bars, expectedList);
                    var buildResult = _builder.build();

                    buildResult.Bars.Should().BeSameAs(expectedList);
                }
            }
        }

        /// <summary>
        /// To add an item to a list from within a custom builder, you can call <code>AddToList( x => x.ListProperty, item );</code>
        /// </summary>
        public class By_adding_an_item_to_the_list
        {
            public class When_adding_a_list_item_to_a_list_property_using_AddToList :
                Given_a_builder_for_a_target_type_having_a_list_property
            {
                [Fact]
                public void should_build_an_instance_whose_list_property_contains_the_new_item()
                {
                    Bar expectedListItem = new Bar();
                    _builder.AddToList(x => x.Bars, expectedListItem);

                    var buildResult = _builder.build();

                    buildResult.Bars.Should().Contain(expectedListItem);
                }
            }

            public class When_adding_a_list_item_to_a_list_property_using_deprecated_AddListItem_method :
                Given_a_builder_for_a_target_type_having_a_list_property
            {
                [Fact]
                public void should_build_an_instance_whose_list_property_contains_the_new_item()
                {
                    Bar expectedListItem = new Bar();

                    _builder.AddListItem(x => x.Bars, expectedListItem);

                    var buildResult = _builder.build();

                    buildResult.Bars.Should().Contain(expectedListItem);
                }
            }
        }

        /// <summary>
        /// To add multiple items to a list from within a custom builder, you can call <code>AddToList( x => x.ListProperty, item1, item2, item3 ..., itemN );</code>
        /// </summary>
        public class By_adding_multiple_items_to_the_list
        {
            public class
                When_adding_multiple_list_items_to_a_list_property :
                    Given_a_builder_for_a_target_type_having_a_list_property
            {
                [Fact]
                public void should_build_an_instance_whose_list_property_contains_all_items()
                {
                    Bar expectedListItem1 = new Bar();
                    Bar expectedListItem2 = new Bar();

                    _builder.AddToList(x => x.Bars, expectedListItem1, expectedListItem2);

                    var buildResult = _builder.build();

                    buildResult.Bars.Should().BeEquivalentTo(expectedListItem1, expectedListItem2);
                }
            }
        }

    }
}