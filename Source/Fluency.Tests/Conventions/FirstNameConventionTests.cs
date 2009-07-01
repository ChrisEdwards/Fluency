using System;
using System.Reflection;
using Fluency.Conventions;
using Fluency.DataGeneration;
using FluentObjectBuilder;
using NUnit.Framework;

namespace Fluency.Tests.Conventions
{
    [TestFixture]
    public class FirstNameConventionTests
    {
        [Test]
        public void When_property_contains_FirstName__Should_apply()
        {
            var person = new {FirstName = "Bob"};

            LambdaConvention convention = Convention.ByName<string>( "FirstName", p => ARandom.FirstName());

            convention.AppliesTo(person.GetType().GetProperty("FirstName")).should_be_true();
        }

        [Test]
        public void When_property_contains_FirstName__Should_get_firstName()
        {
            var person = new {FirstName = "Bob"};

            var convention = new FirstNameConvention();

            Assert.That(convention.DefaultValue(person.GetType().GetProperty("FirstName")), Is.Not.Null);
        }
    }
}