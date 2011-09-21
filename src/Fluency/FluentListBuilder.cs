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
using System.Linq;


namespace Fluency
{
	public class FluentListBuilder< T > : IFluentListBuilder< T > where T : class
	{
		readonly IList< IFluentBuilder< T > > _builders = new List< IFluentBuilder< T > >();
		readonly IList< T > _directItems = new List< T >();


		#region IFluentBuilder<IList<T>> Members

		/// <summary>
		/// Builds a list of all the items specified directly combined with a list of all items built by the specifed builders.
		/// </summary>
		/// <returns></returns>
		public virtual IList< T > build()
		{
			// Build each builder's item and gather into collection.
			IEnumerable< T > builtItems =
					from builder in _builders
					select builder.build();

			// Merge with items added directly (without a builder)
			IEnumerable< T > allItems = _directItems.Concat( builtItems );

			// Return these as a list.
			return new List< T >( allItems );
		}

		#endregion


		/// <summary>
		/// Adds a builder to build a specified item in the list.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public void Add( IFluentBuilder< T > builder )
		{
			_builders.Add( builder );
		}


		/// <summary>
		/// Adds the specified  item directly to the list.
		/// </summary>
		/// <param name="directItem">The direct item.</param>
		public void Add( T directItem )
		{
			_directItems.Add( directItem );
		}
	}
}