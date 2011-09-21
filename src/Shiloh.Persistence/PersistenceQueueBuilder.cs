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


namespace Shiloh.Persistence
{
	/// <summary>
	/// Implements fluent interface to create and configure a PersistenceQueue instance.
	/// </summary>
	public class PersistenceQueueBuilder
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PersistenceQueueBuilder"/> class.
		/// </summary>
		public PersistenceQueueBuilder()
		{
			InstanceQueueBuilders = new List< IInstanceQueueBuilder >();
		}


		/// <summary>
		/// List of builders that create persistable type metadata objects.
		/// </summary>
		/// <value>The persistable type info builders.</value>
		public IList< IInstanceQueueBuilder > InstanceQueueBuilders { get; private set; }


		/// <summary>
		/// Fluent interface method to define the metadata for a new persistable type and add it to the PersistenceQueue.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public InstanceQueueBuilder< T > ForType< T >() where T : class
		{
			var builder = new InstanceQueueBuilder< T >();
			InstanceQueueBuilders.Add( builder );
			return builder;
		}
	}
}