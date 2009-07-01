using System.Reflection;
using Fluency.Conventions;
using Fluency.DataGeneration;
using FluentObjectBuilder;
using NUnit.Framework;


namespace Fluency.Tests.Conventions
{
	[ TestFixture ]
	public class TypeConventionTests
	{
		[ Test ]
		public void Incorrect_type_should_not_apply()
		{
			var person = new {BoolVal = false};
			PropertyInfo propertyInfo = person.GetType().GetProperty( "BoolVal" );

			LambdaConvention convention = Convention.ByType< string >( p => ARandom.String( 10 ) );

			convention.AppliesTo( propertyInfo ).should_be_false();
		}


		[ Test ]
		public void String_type_should_apply()
		{
			var person = new {Name = "stringvalue"};
			PropertyInfo propertyInfo = person.GetType().GetProperty( "Name" );

			LambdaConvention convention = Convention.ByType< string >( p => ARandom.String( 10 ) );

			convention.AppliesTo( propertyInfo ).should_be_true();
		}


		[ Test ]
		public void String_type_should_return_provided_default_string()
		{
			var person = new {Name = "stringvalue"};
			PropertyInfo propertyInfo = person.GetType().GetProperty( "Name" );

			LambdaConvention convention = Convention.ByType< string >( p => "test" );

			convention.DefaultValue( propertyInfo ).should_be_equal_to( "test" );
		}
	}
}