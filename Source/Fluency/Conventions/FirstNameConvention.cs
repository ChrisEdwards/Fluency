using System;
using System.Reflection;
using Fluency.DataGeneration;


namespace Fluency.Conventions
{
	public class FirstNameConvention : DefaultConvention<string>
	{
		public override bool AppliesTo( PropertyInfo propertyInfo )
		{
			return ( propertyInfo.PropertyType == typeof ( String ) &&
			         propertyInfo.Name.ToLower().Contains( "firstname" ) );
		}


		public override string DefaultValue( PropertyInfo propertyInfo )
		{
			return ARandom.FirstName();
		}
	}
}