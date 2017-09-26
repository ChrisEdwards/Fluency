using System;
using System.Collections.Generic;
using Fluency.DataGeneration;
using FluentAssertions;
using Xunit;

namespace Fluency.Tests.BuilderTests
{
    public class Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
    {
        protected readonly DynamicFluentBuilder<TestClass> Sut;

        protected class TestClass
        {
            public string StringProperty { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int IntegerProperty { get; set; }
            public DateTime DateTimeProperty { get; set; }
            public TestClass ReferenceProperty { get; set; }
            public IList<TestClass> ListProperty { get; set; }
        }

        protected Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions()
        {
            Fluency.Initialize(x => x.UseDefaultValueConventions());
            Sut = new DynamicFluentBuilder<TestClass>();
        }

        public class WhenBuildingTheObject : Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
        {
            private readonly TestClass _result;

            public WhenBuildingTheObject()
            {
                _result = Sut.build();
            }

            [Fact]
            public void It_Should_UseDefaultConventionFor_FirstNameProperty() =>
                _result.FirstName.Should().BeOneOf(RandomData.FirstNames);

            [Fact]
            public void It_Should_UseDefaultConventionFor_LastNameProperty() =>
                _result.LastName.Should().BeOneOf(RandomData.LastNames);

            [Fact]
            public void It_Should_UseDefaultConventionFor_DateTimeProperty() =>
                _result.DateTimeProperty.Should().NotBe(default(DateTime));

            [Fact]
            public void It_Should_UseDefaultConventionFor_IntegerProperty() =>
                _result.IntegerProperty.Should().NotBe(0);

            [Fact]
            public void It_Should_UseDefaultConventionFor_StringProperty() =>
                _result.StringProperty.Length.Should().Be(20);
        }

        public class
            WhenBuildingTheObject_WithProvidedDateTime : Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
        {
            private readonly TestClass _result;
            private readonly DateTime _providedDateTime = DateTime.Parse("1-Jan-2000 10:00");

            public WhenBuildingTheObject_WithProvidedDateTime()
            {
                _result = Sut.With(x => x.DateTimeProperty, _providedDateTime).build();
            }

            [Fact]
            public void It_Should_UseProvidedDateTime() => 
                _result.DateTimeProperty.Should().Be(_providedDateTime);
        }

        public class
            WhenBuildingTheObject_WithProvidedInteger : Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
        {
            private readonly TestClass _result;
            private readonly int _providedInteger = 1234;

            public WhenBuildingTheObject_WithProvidedInteger()
            {
                _result = Sut.With(x => x.IntegerProperty, _providedInteger).build();
            }

            [Fact]
            public void It_Should_UseProvidedInteger() =>
                _result.IntegerProperty.Should().Be(_providedInteger);
        }

        public class
            WhenBuildingTheObject_WithProvidedString : Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
        {
            private readonly TestClass _result;
            private readonly string _providedString = "1234";

            public WhenBuildingTheObject_WithProvidedString()
            {
                _result = Sut.With(x => x.StringProperty, _providedString).build();
            }

            [Fact]
            public void It_Should_UseProvidedString() =>
                _result.StringProperty.Should().Be(_providedString);
        }

        public class
            WhenBuildingTheObject_WithProvidedValueUsingFor : Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
        {
            private readonly TestClass _result;
            private readonly string _providedString = "1234";

            public WhenBuildingTheObject_WithProvidedValueUsingFor()
            {
                _result = Sut.For(x => x.StringProperty, _providedString).build();
            }

            [Fact]
            public void It_Should_UseProvidedValue() =>
                _result.StringProperty.Should().Be(_providedString);
        }

        public class
            WhenBuildingTheObject_WithProvidedValueUsingHaving : Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
        {
            private readonly TestClass _result;
            private readonly string _providedString = "1234";

            public WhenBuildingTheObject_WithProvidedValueUsingHaving()
            {
                _result = Sut.Having(x => x.StringProperty, _providedString).build();
            }

            [Fact]
            public void It_Should_UseProvidedValue() =>
                _result.StringProperty.Should().Be(_providedString);
        }

        public class
            WhenBuildingTheObject_AfterSpecifyingDynamicPropertyBuilder_UsingWith : Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
        {
            private readonly TestClass _result;
            private readonly TestClass _expectedValue;

            public WhenBuildingTheObject_AfterSpecifyingDynamicPropertyBuilder_UsingWith()
            {
                _expectedValue = new TestClass { FirstName = "Bob", LastName = "Smith" };
                FluentBuilder<TestClass> propertyBuilder = new DynamicFluentBuilder<TestClass>().AliasFor(_expectedValue);

                _result = Sut.With(x => x.ReferenceProperty, propertyBuilder).build();
            }

            [Fact]
            public void It_Should_UseProvidedValue() =>
                _result.ReferenceProperty.Should().Be(_expectedValue);
        }

        public class
            WhenBuildingTheObject_AfterSpecifyingDynamicPropertyBuilder_UsingFor : Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
        {
            private readonly TestClass _result;
            private readonly TestClass _expectedValue;

            public WhenBuildingTheObject_AfterSpecifyingDynamicPropertyBuilder_UsingFor()
            {
                _expectedValue = new TestClass { FirstName = "Bob", LastName = "Smith" };
                FluentBuilder<TestClass> propertyBuilder = new DynamicFluentBuilder<TestClass>().AliasFor(_expectedValue);

                _result = Sut.For(x => x.ReferenceProperty, propertyBuilder).build();
            }

            [Fact]
            public void It_Should_UseProvidedValue() =>
                _result.ReferenceProperty.Should().Be(_expectedValue);
        }

        public class
            WhenBuildingTheObject_AfterSpecifyingDynamicPropertyBuilder_UsingHaving : Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
        {
            private readonly TestClass _result;
            private readonly TestClass _expectedValue;

            public WhenBuildingTheObject_AfterSpecifyingDynamicPropertyBuilder_UsingHaving()
            {
                _expectedValue = new TestClass { FirstName = "Bob", LastName = "Smith" };
                FluentBuilder<TestClass> propertyBuilder = new DynamicFluentBuilder<TestClass>().AliasFor(_expectedValue);

                _result = Sut.Having(x => x.ReferenceProperty, propertyBuilder).build();
            }

            [Fact]
            public void It_Should_UseProvidedValue() =>
                _result.ReferenceProperty.Should().Be(_expectedValue);
        }

        public class
            WhenBuildingTheObject_AfterSpecifyingListPropertyValues_UsingWithListOf : Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
        {
            private readonly TestClass _result;
            private readonly TestClass _firstExpectedValue;
            private readonly TestClass _secondExpectedValue;

            public WhenBuildingTheObject_AfterSpecifyingListPropertyValues_UsingWithListOf()
            {
                _firstExpectedValue = new TestClass { FirstName = "Bob", LastName = "Smith" };
                _secondExpectedValue = new TestClass { FirstName = "Harry", LastName = "Johnson" };

                _result = Sut.WithListOf(x => x.ListProperty, _firstExpectedValue, _secondExpectedValue).build();                
            }

            [Fact]
            public void It_Should_UseProvidedValue() =>
                _result.ListProperty.Should().BeEquivalentTo(
                    new[] {_firstExpectedValue, _secondExpectedValue});
        }

        public class
            WhenBuildingTheObject_AfterSpecifyingListPropertyValues_UsingHavingListOf : Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
        {
            private readonly TestClass _result;
            private readonly TestClass _firstExpectedValue;
            private readonly TestClass _secondExpectedValue;

            public WhenBuildingTheObject_AfterSpecifyingListPropertyValues_UsingHavingListOf()
            {
                _firstExpectedValue = new TestClass { FirstName = "Bob", LastName = "Smith" };
                _secondExpectedValue = new TestClass { FirstName = "Harry", LastName = "Johnson" };

                _result = Sut.HavingListOf(x => x.ListProperty, _firstExpectedValue, _secondExpectedValue).build();
            }

            [Fact]
            public void It_Should_UseProvidedValue() =>
                _result.ListProperty.Should().BeEquivalentTo(
                    new[] { _firstExpectedValue, _secondExpectedValue });
        }

        public class
            WhenBuildingTheObject_AfterSpecifyingListPropertyBuilders_UsingHavingListOf : Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
        {
            private readonly TestClass _result;
            private readonly TestClass _firstExpectedValue;
            private readonly TestClass _secondExpectedValue;

            public WhenBuildingTheObject_AfterSpecifyingListPropertyBuilders_UsingHavingListOf()
            {
                // Create a builder that will return the expected value.
                _firstExpectedValue = new TestClass { FirstName = "Bob", LastName = "Smith" };
                FluentBuilder<TestClass> firstValueBuilder = new DynamicFluentBuilder<TestClass>().AliasFor(_firstExpectedValue);
                _secondExpectedValue = new TestClass { FirstName = "Harry", LastName = "Johnson" };
                FluentBuilder<TestClass> secondValueBuilder = new DynamicFluentBuilder<TestClass>().AliasFor(_secondExpectedValue);

                _result = Sut.HavingListOf(x => x.ListProperty, firstValueBuilder, secondValueBuilder).build();
            }

            [Fact]
            public void It_Should_UseProvidedValue() =>
                _result.ListProperty.Should().BeEquivalentTo(
                    new[] { _firstExpectedValue, _secondExpectedValue });
        }

        public class
            WhenBuildingTheObject_AfterSpecifyingListPropertyBuilders_UsingWithListOf : Given_ADynamicFluentBuilder_ThatUsesDefaultValueConventions
        {
            private readonly TestClass _result;
            private readonly TestClass _firstExpectedValue;
            private readonly TestClass _secondExpectedValue;

            public WhenBuildingTheObject_AfterSpecifyingListPropertyBuilders_UsingWithListOf()
            {
                // Create a builder that will return the expected value.
                _firstExpectedValue = new TestClass { FirstName = "Bob", LastName = "Smith" };
                FluentBuilder<TestClass> firstValueBuilder = new DynamicFluentBuilder<TestClass>().AliasFor(_firstExpectedValue);
                _secondExpectedValue = new TestClass { FirstName = "Harry", LastName = "Johnson" };
                FluentBuilder<TestClass> secondValueBuilder = new DynamicFluentBuilder<TestClass>().AliasFor(_secondExpectedValue);

                _result = Sut.WithListOf(x => x.ListProperty, firstValueBuilder, secondValueBuilder).build();
            }

            [Fact]
            public void It_Should_UseProvidedValue() =>
                _result.ListProperty.Should().BeEquivalentTo(
                    new[] { _firstExpectedValue, _secondExpectedValue });
        }
    }
}