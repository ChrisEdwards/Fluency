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

// Copied from FluentNHibernate source to remove dependency on FluentNHibernate. Need to remove dependency on this code.


namespace FluentNHibernate.Utils
{
	public class PropertyChain : Accessor
	{
		readonly PropertyInfo[] _chain;
		readonly SingleProperty _innerProperty;


		public PropertyChain( PropertyInfo[] properties )
		{
			_chain = new PropertyInfo[properties.Length - 1];
			for ( int i = 0; i < _chain.Length; i++ )
				_chain[i] = properties[i];

			_innerProperty = new SingleProperty( properties[properties.Length - 1] );
		}


		#region Accessor Members

		public void SetValue( object target, object propertyValue )
		{
			target = findInnerMostTarget( target );
			if ( target == null )
				return;

			_innerProperty.SetValue( target, propertyValue );
		}


		public object GetValue( object target )
		{
			target = findInnerMostTarget( target );

			if ( target == null )
				return null;

			return _innerProperty.GetValue( target );
		}


		public string FieldName
		{
			get { return _innerProperty.FieldName; }
		}

		public Type PropertyType
		{
			get { return _innerProperty.PropertyType; }
		}

		public PropertyInfo InnerProperty
		{
			get { return _innerProperty.InnerProperty; }
		}


		public Accessor GetChildAccessor< T >( Expression< Func< T, object > > expression )
		{
			PropertyInfo property = ReflectionHelper.GetProperty( expression );
			var list = new List< PropertyInfo >( _chain );
			list.Add( _innerProperty.InnerProperty );
			list.Add( property );

			return new PropertyChain( list.ToArray() );
		}


		public string Name
		{
			get
			{
				string returnValue = string.Empty;
				foreach ( PropertyInfo info in _chain )
					returnValue += info.Name;

				returnValue += _innerProperty.Name;

				return returnValue;
			}
		}

		#endregion


		object findInnerMostTarget( object target )
		{
			foreach ( PropertyInfo info in _chain )
			{
				target = info.GetValue( target, null );
				if ( target == null )
					return null;
			}

			return target;
		}
	}
}