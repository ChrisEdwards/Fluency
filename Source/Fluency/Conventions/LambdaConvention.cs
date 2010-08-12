using System;
using System.Reflection;


namespace Fluency.Conventions
{
	public class LambdaConvention<T> : IDefaultConvention<T>
	{
		readonly Func< PropertyInfo, T > _defaultValue;
		readonly Predicate< PropertyInfo > _appliesTo;


		public LambdaConvention( Predicate< PropertyInfo > appliesTo, Func< PropertyInfo, T > defaultValue )
		{
			_appliesTo = appliesTo;
			_defaultValue = defaultValue;
		}


		public bool AppliesTo( PropertyInfo propertyInfo )
		{
			return _appliesTo.Invoke( propertyInfo );
		}


		object IDefaultConvention.DefaultValue( PropertyInfo propertyInfo )
		{
			return DefaultValue( propertyInfo );
		}


		public T DefaultValue( PropertyInfo propertyInfo )
		{
			return AppliesTo( propertyInfo ) ? _defaultValue.Invoke( propertyInfo ) : default(T);
		}
	}
}