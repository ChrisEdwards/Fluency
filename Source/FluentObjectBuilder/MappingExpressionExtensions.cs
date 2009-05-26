using System;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;


namespace FluentObjectBuilder
{
	public static class MappingExpressionExtensions
	{
		public static void IgnoreReadOnlyProperties< T >( this IMappingExpression< T, T > map ) where T : class, new()
		{
			foreach ( PropertyInfo propertyInfo in typeof(T).GetPublicReadOnlyProperties() )
				map.IgnorePropertyMapping( propertyInfo );
		}


		public static void IgnorePropertyMapping< T >( this IMappingExpression< T, T > map, PropertyInfo propertyInfo ) where T : class, new()
		{
			map.ForMember( GetPropertyAccessorExpression< T >( propertyInfo ), x => x.Ignore() );
		}


		public static Expression< Func< T, object > > GetPropertyAccessorExpression< T >( PropertyInfo propertyInfo ) where T : class, new()
		{
			ParameterExpression objectToAccess = Expression.Parameter( typeof ( T ), "x" );
			return Expression.Lambda< Func< T, object > >(
					Expression.Property( objectToAccess, propertyInfo ),
					objectToAccess
					);
		}
	}
}