using System;
using System.Reflection;
using Fluency.DataGeneration;


namespace Fluency.Conventions
{
	public class LastNameConvention : IDefaultConvention
	{
		public bool AppliesTo( PropertyInfo propertyInfo )
		{
			return ( propertyInfo.PropertyType == typeof ( String ) &&
			         propertyInfo.Name.ToLower().Contains( "lastname" ) );
		}


		public object DefaultValue( PropertyInfo propertyInfo )
		{
			return ARandom.LastName();
		}
	}
}