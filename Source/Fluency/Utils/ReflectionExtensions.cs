using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Utils;


namespace Fluency.Utils
{
	public static class ReflectionExtensions
	{
		public static PropertyInfo[] GetPublicGetProperties( this Type type )
		{
			return type.FindMembers( MemberTypes.Property,
			                         BindingFlags.Public | BindingFlags.Instance,
			                         ( m, f ) => ( (PropertyInfo)m ).CanRead,
			                         null )
					.Cast< PropertyInfo >()
					.ToArray();
		}


		public static PropertyInfo[] GetPublicReadOnlyProperties( this Type type )
		{
			return type.FindMembers( MemberTypes.Property,
			                         BindingFlags.Public | BindingFlags.Instance,
			                         ( m, f ) => ( (PropertyInfo)m ).CanRead && !( (PropertyInfo)m ).CanWrite,
			                         null )
					.Cast< PropertyInfo >()
					.ToArray();
		}

		public static PropertyInfo PropertyInfoFor<T, TPropertyType>(this T source, Expression<Func<T, TPropertyType>> propertyExpression)
		{
			return ReflectionHelper.GetProperty( propertyExpression );
		}


		public static void SetProperty( this object prototype, string propertyName, object propertyValue )
		{
			PropertyInfo property = prototype.GetType().GetProperty( propertyName );
			property.SetValue( prototype, propertyValue, null );
		}


		public static object InvokeMethod( this object target, string methodName, params object[] parameters )
		{
			MethodInfo buildMethod = target.GetType().GetMethod( methodName );
			return buildMethod.Invoke( target, parameters );
		}


		/// <summary>
		/// Builds a new object with values the builder has specified in the prototype.
		/// </summary>
		/// <param name="prototype">The prototype.</param>
		/// <returns></returns>
		public static T ShallowClone< T >( this T prototype ) where T : new()
		{
			// Create a new object to fill with all the property values in the prototype.
			var newObject = new T();

			// Copy all the property values from the prototype.
			foreach ( PropertyInfo propertyInfo in typeof ( T ).GetProperties() )
			{
				// Only copy properties that are read-write (get/set).
				if ( propertyInfo.CanRead && propertyInfo.CanWrite )
					propertyInfo.SetValue( newObject, propertyInfo.GetValue( prototype, null ), null );
			}

			// Return the new object.
			return newObject;
		}
	}
}