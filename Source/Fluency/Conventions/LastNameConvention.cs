using System;
using System.Reflection;
using Fluency.DataGeneration;


namespace Fluency.Conventions
{
	public class LastNameConvention : DefaultConvention<string>
	{
		public override bool AppliesTo( PropertyInfo propertyInfo )
		{
			return ( propertyInfo.PropertyType == typeof ( String ) &&
			         propertyInfo.Name.ToLower().Contains( "lastname" ) );
		}


		public override string DefaultValue( PropertyInfo propertyInfo )
		{
			return ARandom.LastName();
		}
	}
}