using FluentAssertions;
using Xunit;

namespace Fluency.Tests.BuilderTests
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
        public class MyCustomType { }
        public class MyCustomTypeBuilder : FluentBuilder<MyCustomType> { }

        public class Given_a_custom_builder
        {
            protected readonly MyCustomTypeBuilder _builder;
            protected readonly MyCustomType _existingInstance = new MyCustomType();

            protected Given_a_custom_builder()
            {
                _builder = new MyCustomTypeBuilder();
            }
        }

        public class When_setting_the_builder_as_an_alias_for_an_existing_instance : Given_a_custom_builder
        {
            [Fact]
            public void should_return_the_existing_instance_as_the_build_result()
            {
                _builder.AliasFor(_existingInstance);

                var result = _builder.build();

                result.Should().BeSameAs(_existingInstance);
            }
        }

        public class
            When_setting_the_builder_as_an_alias_for_an_existing_instance_using_the_UsePrebuiltResult_alternative_syntax :
                Given_a_custom_builder
        {
            [Fact]
            public void should_return_the_existing_instance_as_the_build_result()
            {
                _builder.UsePreBuiltResult(_existingInstance);

                var result = _builder.build();

                result.Should().BeSameAs(_existingInstance);
            }
        }

        public class
            When_setting_the_builder_as_an_alias_for_an_existing_instance_then_setting_it_as_an_alias_for_a_different_instance
            : Given_a_custom_builder
        {
            [Fact]
            public void should_return_the_last_instance_it_was_set_as_an_alias_for()
            {
                MyCustomType differentInstance = new MyCustomType();
                _builder.AliasFor(_existingInstance);
                _builder.AliasFor(differentInstance);

                var result = _builder.build();

                result.Should().BeSameAs(differentInstance);
            }
        }
    }
}