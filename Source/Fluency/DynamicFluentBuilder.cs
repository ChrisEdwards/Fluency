using System;
using System.Linq.Expressions;


namespace Fluency
{
	public class DynamicFluentBuilder< T > : FluentBuilder< T > where T : class, new()
	{
		public DynamicFluentBuilder< T > With< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, TPropertyType propertyValue )
		{
			SetProperty( propertyExpression, propertyValue );
			return this;
		}


		public DynamicFluentBuilder< T > Having< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, TPropertyType propertyValue )
		{
			return With( propertyExpression, propertyValue );
		}


		public DynamicFluentBuilder< T > For< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, TPropertyType propertyValue )
		{
			return With( propertyExpression, propertyValue );
		}
	}
}