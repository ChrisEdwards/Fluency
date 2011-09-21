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


namespace Shiloh.Persistence
{
	/// <summary>
	/// Information about an entity...its comparator, dependencies, etc.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PersistableTypeInfo< T > : IPersistableTypeInfo where T : class
	{
		/// <summary>
		/// List of lambda expressions that point to the properties on this entity referring to associated entities that should also be persisted when this one is.
		/// </summary>
		/// <value>The dependency expressions.</value>
		public IList< Expression< Func< T, object > > > AssociationExpressions { get; set; }

		/// <summary>
		/// Action delegate that can be used to persist this entity to the database.
		/// </summary>
		/// <value>The persist action.</value>
		public Action< T > PersistAction { get; set; }

		/// <summary>
		/// Comparator to determine if two entities are the same item (by identity...or database primary key)
		/// </summary>
		/// <value>The identity comparator.</value>
		public Func< T, T, bool > IdentityComparator { get; set; }

		public string TableName { get; set; }
	}
}