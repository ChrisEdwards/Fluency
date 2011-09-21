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
using System.Globalization;
using System.Reflection;

// Copied from FluentNHibernate source to remove dependency on FluentNHibernate. Need to remove dependency on this code.


namespace FluentNHibernate
{
	public sealed class DummyPropertyInfo : PropertyInfo
	{
		readonly string name;
		readonly Type type;


		public DummyPropertyInfo( string name, Type type )
		{
			if ( name == null ) throw new ArgumentNullException( "name" );
			if ( type == null ) throw new ArgumentNullException( "type" );

			this.name = name;
			this.type = type;
		}


		public override object[] GetCustomAttributes( bool inherit )
		{
			throw new NotImplementedException();
		}


		public override bool IsDefined( Type attributeType, bool inherit )
		{
			throw new NotImplementedException();
		}


		public override object GetValue( object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture )
		{
			throw new NotImplementedException();
		}


		public override void SetValue( object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture )
		{
			throw new NotImplementedException();
		}


		public override MethodInfo[] GetAccessors( bool nonPublic )
		{
			throw new NotImplementedException();
		}


		public override MethodInfo GetGetMethod( bool nonPublic )
		{
			throw new NotImplementedException();
		}


		public override MethodInfo GetSetMethod( bool nonPublic )
		{
			throw new NotImplementedException();
		}


		public override ParameterInfo[] GetIndexParameters()
		{
			throw new NotImplementedException();
		}


		public override string Name
		{
			get { return name; }
		}

		public override Type DeclaringType
		{
			get { throw new NotImplementedException(); }
		}

		public override Type ReflectedType
		{
			get { throw new NotImplementedException(); }
		}

		public override Type PropertyType
		{
			get { return type; }
		}

		public override PropertyAttributes Attributes
		{
			get { throw new NotImplementedException(); }
		}

		public override bool CanRead
		{
			get { throw new NotImplementedException(); }
		}

		public override bool CanWrite
		{
			get { throw new NotImplementedException(); }
		}


		public override object[] GetCustomAttributes( Type attributeType, bool inherit )
		{
			throw new NotImplementedException();
		}
	}
}