// Copyright 2011 Chris Edwards
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Utils;


namespace Fluency.Utils
{
	public static class ReflectionExtensions
	{
		/// <summary>
		/// Gets the properties with a public getter.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static PropertyInfo[] GetPublicGetProperties( this Type type )
		{
			return type.FindMembers( MemberTypes.Property,
			                         BindingFlags.Public | BindingFlags.Instance,
			                         ( m, f ) => ( (PropertyInfo)m ).CanRead,
			                         null )
					.Cast< PropertyInfo >()
					.ToArray();
		}


		/// <summary>
		/// Gets the public read only properties.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static PropertyInfo[] GetPublicReadOnlyProperties( this Type type )
		{
			return type.FindMembers( MemberTypes.Property,
			                         BindingFlags.Public | BindingFlags.Instance,
			                         ( m, f ) => ( (PropertyInfo)m ).CanRead && !( (PropertyInfo)m ).CanWrite,
			                         null )
					.Cast< PropertyInfo >()
					.ToArray();
		}


		/// <summary>
		/// Gets the property info for the property specified in the expression.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="propertyExpression">The property expression.</param>
		/// <returns></returns>
		public static PropertyInfo PropertyInfoFor< T, TPropertyType >( this T source, Expression< Func< T, TPropertyType > > propertyExpression )
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
			if ( propertyInfo == null )
				throw new ArgumentNullException( "propertyInfo", "PropertyInfo cannot be null" );

			// Set the property value...throw meaningful error upon failure.
			try
			{
				propertyInfo.SetValue( instance, propertyValue, null );
			}
			catch ( Exception e )
			{
				throw new FluencyException( "Error occurred while setting default value for property [" +
				                            instance.GetType().FullName + "." + propertyInfo.Name + "] to value [" + propertyValue + "]",
				                            e );
			}
		}


		/// <summary>
		/// Invokes a method on an objet given the method's name and a list of parameters.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
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


		/// <summary>
		/// Sets the value for the property specified by an expression.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="prototype">The prototype.</param>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="propertyValue">The property value.</param>
		public static void SetProperty< TPropertyType, T >( this T prototype, Expression< Func< T, TPropertyType > > propertyExpression, TPropertyType propertyValue )
				where T : class, new()
		{
			Accessor accessor = ReflectionHelper.GetAccessor( propertyExpression );
			accessor.SetValue( prototype, propertyValue );
		}


		/// <summary>
		/// Gets the value for the property specified by an expression.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="prototype">The prototype.</param>
		/// <param name="propertyExpression">The property expression.</param>
		/// <returns></returns>
		public static TPropertyType GetProperty< TPropertyType, T >( this T prototype, Expression< Func< T, TPropertyType > > propertyExpression )
				where T : class, new()
		{
			Accessor accessor = ReflectionHelper.GetAccessor( propertyExpression );
			return (TPropertyType)accessor.GetValue( prototype );
		}
	}
}