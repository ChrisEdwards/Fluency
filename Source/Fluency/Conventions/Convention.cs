using System;
using System.Reflection;
using Fluency.DataGeneration;


namespace Fluency.Conventions
{
	/// <summary>
	/// Collection of static factory methods to create conventions to automatically assign a default value to a property.
	/// </summary>
	public static class Convention
	{
		/// <summary>
		/// Creates a convention that is applied to properties that match the specified property name and type. <br/>
		/// When applied, the convention will assign the specified value as the property's default value when the builder is first created.
		/// </summary>
		/// <typeparam name="T">The type of property to mactch on.</typeparam>
		/// <param name="propertyName">The property name to match on.</param>
		/// <param name="defaultValue">The default value to provide when the property name matches.</param>
		/// <returns>The constructed <see cref="IDefaultConvention"/></returns>
		public static LambdaConvention ByName< T >( string propertyName, Func< PropertyInfo, object > defaultValue )
		{
			return new LambdaConvention(
					p => p.PropertyType == typeof ( T ) && p.Name.ToLower().Contains( propertyName.ToLower() ),
					defaultValue );
		}


		/// <summary>
		/// Creates a convention that assigns the specified value as the default value for any property having a datatype that matches the specified type. <br/>
		/// When applied, the convention will assign the specified value as the property's default value when the builder is first created.
		/// </summary>
		/// <typeparam name="T">The type of property to match on.</typeparam>
		/// <param name="defaultValue">The default value to provide for matching properties.</param>
		/// <returns>The constructed <see cref="IDefaultConvention"/></returns>
		public static LambdaConvention ByType< T >( Func< PropertyInfo, object > defaultValue )
		{
			return new LambdaConvention(
					p => p.PropertyType == typeof ( T ),
					defaultValue );
		}


		/// <summary>
		/// Creates a convention to automatically assign a default value to a property:<br/>
		/// Assign a random first name when the property name contains "firstname" (case-insensitive)
		/// </summary>
		/// <returns>The constructed <see cref="IDefaultConvention"/></returns>
		public static IDefaultConvention FirstName()
		{
			return ByName< string >( "FirstName", p => ARandom.FirstName() );
		}


		/// <summary>
		/// Creates a convention to automatically assign a default value to a property:<br/>
		/// Assign a random <see cref="string"/> (of a specifed length) when the property is a <see cref="string"/>.
		/// </summary>
		/// <param name="length">The length of the string to create for the default value.</param>
		/// <returns>The constructed <see cref="IDefaultConvention"/></returns>
		public static IDefaultConvention String( int length )
		{
			return ByType< string >( p => ARandom.String( length ) );
		}


		/// <summary>
		/// Creates a convention to automatically assign a default value to a property:<br/>
		/// Assign a random last name as when the property name contains "lastname" (case-insensitive).
		/// </summary>
		/// <returns>The constructed <see cref="IDefaultConvention"/></returns>
		public static IDefaultConvention LastName()
		{
			return ByName< string >( "LastName", p => ARandom.LastName() );
		}


		/// <summary>
		/// Creates a convention to automatically assign a default value to a property:<br/>
		/// Assign a random <see cref="DateTime"/> (with time component set to midnight) when the property type is <see cref="DateTime"/>.
		/// </summary>
		/// <returns>The constructed <see cref="IDefaultConvention"/></returns>
		public static IDefaultConvention DateType()
		{
			return ByType< DateTime >( p => ARandom.DateTime().Date );
		}


		/// <summary>
		/// Creates a convention to automatically assign a default value to a property:<br/>
		/// Assign a random <see cref="int"/> when the property type is <see cref="int"/>.
		/// </summary>
		/// <returns>The constructed <see cref="IDefaultConvention"/></returns>
		public static IDefaultConvention IntegerType()
		{
			return ByType< int >( p => ARandom.Int() );
		}
	}
}