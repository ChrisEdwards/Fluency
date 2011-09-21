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
using System.Reflection;


namespace Fluency.Conventions
{
	public abstract class DefaultConvention< T > : IDefaultConvention< T >
	{
		public abstract bool AppliesTo( PropertyInfo propertyInfo );
		public abstract T DefaultValue( PropertyInfo propertyInfo );


		/// <summary>
		/// Gets the default value for the specirfied property.
		/// </summary>
		/// <param name="propertyInfo">The property info.</param>
		/// <returns></returns>
		object IDefaultConvention.DefaultValue( PropertyInfo propertyInfo )
		{
			// Fake covariance by returning object when cast as IDefaultConvetion.
			return DefaultValue( propertyInfo );
		}
	}
}