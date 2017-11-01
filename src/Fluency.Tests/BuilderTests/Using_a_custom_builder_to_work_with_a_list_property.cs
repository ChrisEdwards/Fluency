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


namespace Fluency.Tests.Deprecated.BuilderTests
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
        /// To add an item to a list from within a custom builder, you can call <code>AddListItem( x => x.ListProperty, item );</code>
        /// </summary>
        public class By_adding_a_builder_to_build_an_item_for_the_list
        {
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