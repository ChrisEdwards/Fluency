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
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Fluency
{
	/// <summary>
	/// Extends the <see cref="FluentBuilder{T}"/> class to add dynamic property setters using lambda expressions. <br/>
	/// For example: <code>builder.With( x => x.MyProperty, "value" );</code><br/>
	/// When no custom behaviour is needed, an instance of <see cref="DynamicFluentBuilder{T}"/> can be used rather than creating a custom subclass of <see cref="FluentBuilder{T}"/>
	/// </summary>
	/// <typeparam name="T">The type of object to build</typeparam>
	public class DynamicFluentBuilder< T > : FluentBuilder< T > where T : class, new()
	{
		/// <summary>
		/// Specify the value of a property on the build result.<br/>
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


		/// <summary>
		/// Specify a builder for the value of a property on the build result.<br/>
		/// Usage: <code>builder.With( x => x.MyProperty, otherFluentBuilder );</code>
		/// </summary>
		/// <example></example>
		/// <typeparam name="TPropertyType">The type of the property to set. This is inferred.</typeparam>
		/// <param name="propertyExpression">Lambda expression identifying the property to set.</param>
		/// <param name="propertyValueBuilder">The builder to use to build the property's value.</param>
		/// <returns></returns>
		public DynamicFluentBuilder< T > With< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, FluentBuilder< TPropertyType > propertyValueBuilder )
				where TPropertyType : class, new()
		{
			SetProperty( propertyExpression, propertyValueBuilder );
			return this;
		}


		/// <summary>
		/// Specify the value of a property on the build result.<br/>
		/// Usage: <code>builder.Having( x => x.MyProperty, "value to assign" );</code>
		/// </summary>
		/// <example></example>
		/// <typeparam name="TPropertyType">The type of the property to set. This is inferred.</typeparam>
		/// <param name="propertyExpression">Lambda expression identifying the property to set.</param>
		/// <param name="propertyValue">The property's value.</param>
		/// <returns></returns>
		public DynamicFluentBuilder< T > Having< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, TPropertyType propertyValue )
		{
			return With( propertyExpression, propertyValue );
		}


		/// <summary>
		/// Specify a builder for the value of a property on the build result.<br/>
		/// Usage: <code>builder.Having( x => x.MyProperty, otherFluentBuilder );</code>
		/// </summary>
		/// <example></example>
		/// <typeparam name="TPropertyType">The type of the property to set. This is inferred.</typeparam>
		/// <param name="propertyExpression">Lambda expression identifying the property to set.</param>
		/// <param name="propertyValueBuilder">The builder to use to build the property's value.</param>
		/// <returns></returns>
		public DynamicFluentBuilder< T > Having< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, FluentBuilder< TPropertyType > propertyValueBuilder )
				where TPropertyType : class, new()
		{
			return With( propertyExpression, propertyValueBuilder );
		}


		/// <summary>
		/// Specify the value of a property on the build result.<br/>
		/// Usage: <code>builder.For( x => x.MyProperty, "value to assign" );</code>
		/// </summary>
		/// <example></example>
		/// <typeparam name="TPropertyType">The type of the property to set. This is inferred.</typeparam>
		/// <param name="propertyExpression">Lambda expression identifying the property to set.</param>
		/// <param name="propertyValue">The property's value.</param>
		/// <returns></returns>
		public DynamicFluentBuilder< T > For< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, TPropertyType propertyValue )
		{
			return With( propertyExpression, propertyValue );
		}


		/// <summary>
		/// Specify a builder for the value of a property on the build result.<br/>
		/// Usage: <code>builder.For( x => x.MyProperty, otherFluentBuilder );</code>
		/// </summary>
		/// <example></example>
		/// <typeparam name="TPropertyType">The type of the property to set. This is inferred.</typeparam>
		/// <param name="propertyExpression">Lambda expression identifying the property to set.</param>
		/// <param name="propertyValueBuilder">The builder to use to build the property's value.</param>
		/// <returns></returns>
		public DynamicFluentBuilder< T > For< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, FluentBuilder< TPropertyType > propertyValueBuilder )
				where TPropertyType : class, new()
		{
			return With( propertyExpression, propertyValueBuilder );
		}


		/// <summary>
		/// Specify a builder for the value of a property on the build result.<br/>
		/// Usage: <code>builder.For( x => x.MyProperty, otherFluentBuilder );</code>
		/// </summary>
		/// <example></example>
		/// <typeparam name="TPropertyType">The type of the property to set. This is inferred.</typeparam>
		/// <param name="propertyExpression">Lambda expression identifying the property to set.</param>
		/// <param name="values">The values to add to the list</param>
		/// <returns></returns>
		public DynamicFluentBuilder< T > WithListOf< TPropertyType >( Expression< Func< T, IList< TPropertyType > > > propertyExpression, params TPropertyType[] values )
				where TPropertyType : class, new()
		{
			SetList( propertyExpression, new FluentListBuilder< TPropertyType >() );
			foreach ( var value in values )
				AddListItem( propertyExpression, value );
			return this;
		}


		/// <summary>
		/// Specify a builder for the value of a property on the build result.<br/>
		/// Usage: <code>builder.For( x => x.MyProperty, otherFluentBuilder );</code>
		/// </summary>
		/// <example></example>
		/// <typeparam name="TPropertyType">The type of the property to set. This is inferred.</typeparam>
		/// <param name="propertyExpression">Lambda expression identifying the property to set.</param>
		/// <param name="values">The values to add to the list</param>
		/// <returns></returns>
		public DynamicFluentBuilder< T > HavingListOf< TPropertyType >( Expression< Func< T, IList< TPropertyType > > > propertyExpression, params TPropertyType[] values )
				where TPropertyType : class, new()
		{
			return WithListOf( propertyExpression, values );
		}
	}
}