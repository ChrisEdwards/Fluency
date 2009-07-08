using System;
using System.Reflection;


namespace Fluency.Conventions
{
	public class LambdaConvention : IDefaultConvention
	{
		readonly Func< PropertyInfo, object > _defaultValue;
		readonly Predicate< PropertyInfo > _appliesTo;


		public LambdaConvention( Predicate< PropertyInfo > appliesTo, Func< PropertyInfo, object > defaultValue )
		{
			_appliesTo = appliesTo;
			_defaultValue = defaultValue;
		}


		public bool AppliesTo( PropertyInfo propertyInfo )
		{
			return _appliesTo.Invoke( propertyInfo );
		}


		public object DefaultValue( PropertyInfo propertyInfo )
		{
			return AppliesTo( propertyInfo ) ? _defaultValue.Invoke( propertyInfo ) : null;
		}
	}
}