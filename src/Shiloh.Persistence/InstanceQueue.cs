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
using System.Linq;
using System.Linq.Expressions;
using FluentNHibernate.Utils;


namespace Shiloh.Persistence
{
	/// <summary>
	/// A queue of instances of a PersistableType to be persisted to the database.
	/// </summary>
	/// <typeparam name="INSTANCETYPE">The type of the Instance this queue is for.</typeparam>
	public class InstanceQueue< INSTANCETYPE > : IInstanceQueue where INSTANCETYPE : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InstanceQueue&lt;INSTANCETYPE&gt;"/> class given its PersistableTypeInfo.
		/// </summary>
		/// <param name="persistableTypeInfo">The persistable type info.</param>
		public InstanceQueue( PersistableTypeInfo< INSTANCETYPE > persistableTypeInfo )
		{
			PersistableTypeInfo = persistableTypeInfo;
			Instances = new List< INSTANCETYPE >();
		}


		/// <summary>
		/// Metadata about the instance type that is persisted by this InstanceQueue
		/// </summary>
		/// <value>The persistable type info.</value>
		public PersistableTypeInfo< INSTANCETYPE > PersistableTypeInfo { get; private set; }

		/// <summary>
		/// The list of items queued to be persisted to the database.
		/// </summary>
		/// <value>The items.</value>
		public List< INSTANCETYPE > Instances { get; private set; }


		#region IInstanceQueue Members

		/// <summary>
		/// The parent TypeQueue this InstanceQueue is a member of.
		/// </summary>
		/// <value>The type queue.</value>
		public TypeQueue ParentQueue { get; set; }


		/// <summary>
		/// Persists the instances to the database using the defined PersistAction.
		/// </summary>
		public void Persist()
		{
			foreach ( INSTANCETYPE instance in Instances )
				PersistableTypeInfo.PersistAction.Invoke( instance );
		}


		/// <summary>
		/// Adds the instance from the non-strongly typed interface...thus, we need to cast..
		/// This property is only visible if you are explicitly looking at the object as an IInstanceQueue.
		/// </summary>
		/// <param name="item">The instance item to add.</param>
		void IInstanceQueue.AddInstance( object item )
		{
			// Ensure they passed in type is correct.
			Type targetType = typeof ( INSTANCETYPE );
			Type sourceType = item.GetType();

			if ( !targetType.IsAssignableFrom( sourceType ) )
			{
				throw new ArgumentException( "The entity passed to AddInstance() had an invalid object type! \n" +
				                             "Expected Type: [" + targetType.Name + "]\n" +
				                             "Actual Type: [" + sourceType.Name + "]\n" );
			}

			// Cast and call the overloaded strongly typed AddInstance method.
			AddInstance( (INSTANCETYPE)item );
		}

		#endregion


		/// <summary>
		/// Adds the instance from the strongly typed class interface.
		/// </summary>
		/// <param name="instanceToAdd">The item.</param>
		public void AddInstance( INSTANCETYPE instanceToAdd )
		{
			// If instanceToAdd already exists, or is null, don't try to add it.
			if ( instanceToAdd == null || Instances.Any( x => PersistableTypeInfo.IdentityComparator.Invoke( x, instanceToAdd ) ) )
				return;

			// Add this instance to the queue.
			Instances.Add( instanceToAdd );

			// Add all this instance's associated objects.
			AddAssociatedInstances( instanceToAdd );
		}


		/// <summary>
		/// Adds the associated objects of this item to their associated InstanceQueues.
		/// </summary>
		/// <param name="instance">The item.</param>
		void AddAssociatedInstances( INSTANCETYPE instance )
		{
			foreach ( Expression< Func< INSTANCETYPE, object > > associationExpression in PersistableTypeInfo.AssociationExpressions )
			{
				// Use reflection to dynamically determine the type and value of this association.
				Type propertyType = ReflectionHelper.GetProperty( associationExpression ).PropertyType;
				object propertyValue = associationExpression.Compile().Invoke( instance );

				// Call back to the root to add each associated object.
				ParentQueue.Parent.Add( propertyType, propertyValue );
			}
		}
	}
}