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


namespace FluentNHibernate.Utils
{
	public class SingleProperty : Accessor
	{
		readonly PropertyInfo _property;


		public SingleProperty( PropertyInfo property )
		{
			_property = property;
		}


		#region Accessor Members

		public string FieldName
		{
			get { return _property.Name; }
		}

		public Type PropertyType
		{
			get { return _property.PropertyType; }
		}

		public PropertyInfo InnerProperty
		{
			get { return _property; }
		}


		public Accessor GetChildAccessor< T >( Expression< Func< T, object > > expression )
		{
			PropertyInfo property = ReflectionHelper.GetProperty( expression );
			return new PropertyChain( new[] {_property, property} );
		}


		public string Name
		{
			get { return _property.Name; }
		}


		public void SetValue( object target, object propertyValue )
		{
			if ( _property.CanWrite )
				_property.SetValue( target, propertyValue, null );
		}


		public object GetValue( object target )
		{
			return _property.GetValue( target, null );
		}

		#endregion


		public static SingleProperty Build< T >( Expression< Func< T, object > > expression )
		{
			PropertyInfo property = ReflectionHelper.GetProperty( expression );
			return new SingleProperty( property );
		}


		public static SingleProperty Build< T >( string propertyName )
		{
			PropertyInfo property = typeof ( T ).GetProperty( propertyName );
			return new SingleProperty( property );
		}
	}
}