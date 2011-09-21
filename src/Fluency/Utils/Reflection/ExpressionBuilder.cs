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
using System.Linq.Expressions;
using System.Reflection;

// Copied from FluentNHibernate source to remove dependency on FluentNHibernate. Need to remove dependency on this code.


namespace FluentNHibernate.Utils.Reflection
{
	public class ExpressionBuilder
	{
		public static Expression< Func< T, object > > Create< T >( PropertyInfo property )
		{
			return (Expression< Func< T, object > >)Create( property, typeof ( T ) );
		}


		public static object Create( PropertyInfo property, Type type )
		{
			ParameterExpression param = Expression.Parameter( type, "entity" );
			MemberExpression expression = Expression.Property( param, property );
			UnaryExpression castedProperty = Expression.Convert( expression, typeof ( object ) );
			return Expression.Lambda( castedProperty, param );
		}
	}
}