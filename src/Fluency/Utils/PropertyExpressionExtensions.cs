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
using System.Reflection;
using FluentNHibernate.Utils;


namespace Fluency.Utils
{
	public static class PropertyExpressionExtensions
	{
	    /// <summary>
	    /// Gets the property info for the specified expression targeting a generic list.
	    /// </summary>
	    /// <typeparam name="TPropertyType">The type of the property type.</typeparam>
	    /// <typeparam name="T">The type of the class.</typeparam>
	    /// <param name="propertyExpression">The property expression.</param>
	    /// <returns></returns>
	    public static PropertyInfo GetPropertyInfo< TPropertyType, T >( this Expression< Func< T, IList< TPropertyType > > > propertyExpression ) where T : class
		{
			return ReflectionHelper.GetProperty( propertyExpression );
		}


	    /// <summary>
	    /// Gets the property info for the specified expression.
	    /// </summary>
	    /// <typeparam name="TPropertyType">The type of the property type.</typeparam>
	    /// <typeparam name="T">The type of the class.</typeparam>
	    /// <param name="propertyExpression">The property expression.</param>
	    /// <returns></returns>
	    public static PropertyInfo GetPropertyInfo< TPropertyType, T >( this Expression< Func< T, TPropertyType > > propertyExpression ) where T : class
		{
			return ReflectionHelper.GetProperty( propertyExpression );
		}
	}
}