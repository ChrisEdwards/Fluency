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

		public static PropertyInfo PropertyInfoFor<T, TPropertyType>(this T source, Expression< Func< T, TPropertyType > > propertyExpression)
		{
			return ReflectionHelper.GetProperty( propertyExpression );
		}


		/// <summary>
		/// Sets the property by name on a given instance.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="propertyName">Name of the property.</param>
		/// <param name="propertyValue">The property value.</param>
		public static void SetProperty( this object instance, string propertyName, object propertyValue )
		{
			PropertyInfo propertyInfo = instance.GetType().GetProperty( propertyName );
			instance.SetProperty( propertyInfo, propertyValue );
		}


		/// <summary>
		/// Sets the property on a given instance specified by its PropertyInfo.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="propertyInfo">The property info.</param>
		/// <param name="propertyValue">The property value.</param>
		public static void SetProperty( this object instance, PropertyInfo propertyInfo, object propertyValue )
		{
			if (propertyInfo == null )
				throw new ArgumentNullException( "propertyInfo", "PropertyInfo cannot be null" );

			// Set the property value...throw meaningful error upon failure.
			try
			{
				propertyInfo.SetValue(instance, propertyValue, null);
			}
			catch (Exception e)
			{
				throw new FluencyException("Error occurred while setting default value for property [" +
				                           instance.GetType().FullName + "." + propertyInfo.Name + "] to value [" + propertyValue + "]",
				                           e);
			}
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


		public static void SetProperty< TPropertyType, T >( this T prototype, Expression< Func< T, TPropertyType > > propertyExpression, TPropertyType propertyValue ) where T : class, new()
		{
			Accessor accessor = ReflectionHelper.GetAccessor( propertyExpression );
			accessor.SetValue( prototype, propertyValue );
		}
	}
}