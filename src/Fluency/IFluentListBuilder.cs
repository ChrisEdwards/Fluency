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
using System.Collections.Generic;


namespace Fluency
{
	public interface IFluentListBuilder< T > : IFluentBuilder< IList< T > > where T : class
	{
		/// <summary>
		/// Adds a builder to build a specified item in the list.
		/// </summary>
		/// <param name="builder">The builder.</param>
		void Add( IFluentBuilder< T > builder );


		/// <summary>
		/// Adds the specified  item directly to the list.
		/// </summary>
		/// <param name="directItem">The direct item.</param>
		void Add( T directItem );
	}
}