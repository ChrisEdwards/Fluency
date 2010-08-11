using System;
using System.Linq.Expressions;


namespace Fluency
{
	public class DynamicFluentBuilder< T > : FluentBuilder< T > where T : class, new()
	{
		/// <summary>
		/// Sets the value of a property on the object to be built.<br/>
		/// Usage: <code>builder.With( x => x.MyProperty, "value to assign" );</code>
		/// </summary>
		/// <example></example>
		/// <typeparam name="TPropertyType">The type of the property to set. This is inferred.</typeparam>
		/// <param name="propertyExpression">Lambda expression identifying the property to set.</param>
		/// <param name="propertyValue">The property's value.</param>
		/// <returns></returns>
		public DynamicFluentBuilder< T > With< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, TPropertyType propertyValue )
		{
			SetProperty( propertyExpression, propertyValue );
			return this;
		}


		public DynamicFluentBuilder< T > With< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, FluentBuilder< TPropertyType > propertyValue )
				where TPropertyType : class, new()
		{
			SetProperty( propertyExpression, propertyValue );
			return this;
		}


		public DynamicFluentBuilder< T > Having< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, TPropertyType propertyValue )
		{
			return With( propertyExpression, propertyValue );
		}


		public DynamicFluentBuilder< T > Having< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, FluentBuilder< TPropertyType > propertyValue )
				where TPropertyType : class, new()
		{
			SetProperty( propertyExpression, propertyValue );
			return this;
		}


		public DynamicFluentBuilder< T > For< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, TPropertyType propertyValue )
		{
			return With( propertyExpression, propertyValue );
		}


		public DynamicFluentBuilder< T > For< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, FluentBuilder< TPropertyType > propertyValue )
				where TPropertyType : class, new()
		{
			SetProperty( propertyExpression, propertyValue );
			return this;
		}
	}
}