using System.Collections.Generic;
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
        }
    }
}