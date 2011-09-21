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


namespace Shiloh.Persistence
{
	public class InstanceQueueBuilder< INSTANCETYPE > : TestDataBuilder< InstanceQueue< INSTANCETYPE > >, IInstanceQueueBuilder where INSTANCETYPE : class
	{
		readonly PersistableTypeInfoBuilder< INSTANCETYPE > _persistableTypeInfoBuilder = new PersistableTypeInfoBuilder< INSTANCETYPE >();


		/// <summary>
		/// Builds an object of type INSTANCETYPE based on the specs specified in the builder.
		/// </summary>
		/// <returns></returns>
		protected override InstanceQueue< INSTANCETYPE > _build()
		{
			return new InstanceQueue< INSTANCETYPE >( _persistableTypeInfoBuilder.build() );
		}


		#region Implementation of IInstanceQueueBuilder

		/// <summary>
		/// Gets the entity type (.NET type) this PersistableTypeInfoBuilder refers to.
		/// </summary>
		/// <returns></returns>
		public Type GetInstanceType()
		{
			return typeof ( INSTANCETYPE );
		}


		/// <summary>
		/// Builds an InstnceQueue strongly typed against the persistable type.
		/// </summary>
		/// <returns></returns>
		IInstanceQueue IInstanceQueueBuilder.build()
		{
			return build();
		}

		#endregion


		#region Fluent Methods Marshalled directly to PersistableTypeInfoBuilder.

		/// <summary>
		/// Sets the comparator function used to determine if two entities are the same item (by identity...or database primary key)
		/// </summary>
		/// <param name="comparator">The comparator.</param>
		/// <returns></returns>
		public InstanceQueueBuilder< INSTANCETYPE > MatchIdentityUsing( Func< INSTANCETYPE, INSTANCETYPE, bool > comparator )
		{
			_persistableTypeInfoBuilder.MatchIdentityUsing( comparator );
			return this;
		}


		/// <summary>
		/// Sets the list of lambda expressions that point to the properties on this entity referring to associated entities that should also be persisted when this one is.
		/// </summary>
		/// <param name="persistableAssociations">The dependency expressions.</param>
		/// <returns></returns>
		public InstanceQueueBuilder< INSTANCETYPE > PersistableAssociations( params Expression< Func< INSTANCETYPE, object > >[] persistableAssociations )
		{
			_persistableTypeInfoBuilder.PersistableAssociations( persistableAssociations );
			return this;
		}


		/// <summary>
		/// Sets the Action delegate that can be used to persist this entity to the database.
		/// </summary>
		/// <param name="persistAction">The persist action.</param>
		/// <returns></returns>
		public InstanceQueueBuilder< INSTANCETYPE > PersistUsing( Action< INSTANCETYPE > persistAction )
		{
			_persistableTypeInfoBuilder.PersistUsing( persistAction );
			return this;
		}


		/// <summary>
		/// Sets the Action delegate that can be used to persist this entity to the database.
		/// </summary>
		/// <param name="tableName">Name of the table.</param>
		/// <returns></returns>
		public InstanceQueueBuilder< INSTANCETYPE > TableName( string tableName )
		{
			_persistableTypeInfoBuilder.TableName( tableName );
			return this;
		}

		#endregion
	}
}