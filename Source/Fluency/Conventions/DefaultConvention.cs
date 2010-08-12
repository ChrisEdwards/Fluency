using System;
using System.Reflection;


namespace Fluency.Conventions
{
	public abstract class DefaultConvention<T>:IDefaultConvention<T>
	{
		public abstract bool AppliesTo( PropertyInfo propertyInfo );
		public abstract T DefaultValue( PropertyInfo propertyInfo );


		/// <summary>
		/// Gets the default value for the specirfied property.
		/// </summary>
		/// <param name="propertyInfo">The property info.</param>
		/// <returns></returns>
		object IDefaultConvention.DefaultValue( PropertyInfo propertyInfo )
		{
			// Fake covariance by returning object when cast as IDefaultConvetion.
			return DefaultValue( propertyInfo );
		}
	}
}