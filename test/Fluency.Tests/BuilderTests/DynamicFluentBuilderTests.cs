using System;
using System.Collections.Generic;

namespace Fluency.Tests.BuilderTests
{
    public class DynamicFluentBuilderTests
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

        public DynamicFluentBuilderTests()
        {
            Fluency.Initialize(x => x.UseDefaultValueConventions());
            Sut = new DynamicFluentBuilder<TestClass>();
        }

        
    }
}