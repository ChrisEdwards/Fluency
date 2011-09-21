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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


namespace Shiloh.Persistence
{
	/// <summary>
	/// Maintains a queue of instances to persist to a database. Adding an instance also adds all its configured associations. 
	/// The entire queue is persisted with one call to Persist(). Persistence is done via direct sql and not through NHibernate 
	/// so that this can be used to test NHibernate.
	/// </summary>
	public class PersistenceQueue
	{
		readonly TypeQueue _typeQueue;


		/// <summary>
		/// Initializes a new instance of the <see cref="PersistenceQueue"/> class.
		/// </summary>
		public PersistenceQueue()
		{
			_typeQueue = new TypeQueue( this );
		}


		/// <summary>
		/// Creates and Configures a new PersistenceQueue using a fluent interface.
		/// The Types are inserted into the database in the order they are defined in the configurationAction.
		/// They should be defined in the order of dependency...in the order they should be inserted into the db.
		/// </summary>
		/// <param name="configurationAction">The configuration action.</param>
		/// <returns></returns>
		public static PersistenceQueue Create( Action< PersistenceQueueBuilder > configurationAction )
		{
			// Execute the configuration action provided.
			var persistenceQueueBuilder = new PersistenceQueueBuilder();
			configurationAction.Invoke( persistenceQueueBuilder );

			// Create a new PersistenceQueue initializing it from the PersistenceQueueBuilder's values.
			var persistenceQueue = new PersistenceQueue();
			foreach ( IInstanceQueueBuilder builder in persistenceQueueBuilder.InstanceQueueBuilders )
				persistenceQueue.AddInstanceQueue( builder );

			return persistenceQueue;
		}


		/// <summary>
		/// Gets the instance queue for the specified instance type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		internal InstanceQueue< T > GetInstanceQueue< T >() where T : class
		{
			return _typeQueue.GetInstanceQueue< T >();
		}


		/// <summary>
		/// Gets the InstanceQueue for the specified instance Type.
		/// </summary>
		/// <param name="instanceType">Type of the instance.</param>
		/// <returns></returns>
		IInstanceQueue GetInstanceQueue( Type instanceType )
		{
			return _typeQueue.GetInstanceQueue( instanceType );
		}


		/// <summary>
		/// Adds the specified enumerable set ot instances to the PersistenceQueue.
		/// Note: This method appears not to be used, but is called dynamically using reflection.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instances">The list.</param>
		/// <returns></returns>
		public PersistenceQueue AddAll< T >( IEnumerable< T > instances ) where T : class
		{
			foreach ( T instance in instances )
				Add( instance );
			return this;
		}


		/// <summary>
		/// Adds the specified strongly typed instance item. This queues it to be added to the database when Persist() is called.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instance">The instance to add.</param>
		/// <returns></returns>
		public PersistenceQueue Add< T >( T instance ) where T : class
		{
			return Add( typeof ( T ), instance );
		}


		/// <summary>
		/// Adds an instance by passing in an object and its type. This is to be used when the type is not able to be spec'd at compile time (like when interating through a list of different types).
		/// </summary>
		/// <param name="itemType">Type of the item.</param>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		internal PersistenceQueue Add( Type itemType, object item )
		{
			// Should this throw an error instead?
			if ( item == null )
				return this;

			// Ensure parameter types are correct.
			if ( !itemType.IsInstanceOfType( item ) )
			{
				throw new ArgumentException( "Item argument did not have the type specified by the ItemType argument.\n" +
				                             "ItemType: [" + itemType.FullName + "]\n" +
				                             "typeof( Item ): [" + item.GetType().FullName + "]\n" );
			}

			// If this is a generic enumerable object, dynamically call AddAll() to add multiple instances.
			if ( item.GetType().GetInterface( "IEnumerable`1" ) != null )
			{
				// Dynamically call AddAll() method.
				Type[] itemGenericTypeArguments = item.GetType().GetInterface( "IEnumerable`1" ).GetGenericArguments();
				MethodInfo genericAddAllMethod = GetType().GetMethod( "AddAll" ).MakeGenericMethod( itemGenericTypeArguments );
				try
				{
					genericAddAllMethod.Invoke( this, new[] {item} );
				}
				catch ( Exception e )
				{
					string errorMessage = string.Format( "Error when calling PersistenceQueue.AddAll<{0}>().", itemGenericTypeArguments[0].Name );
					throw new ApplicationException( errorMessage, e );
				}

				return this;
			}


			// Don't allow enumerables since they should have used AddAll() above.
			if ( item is IEnumerable )
			{
				throw new ArgumentException( "Attempted to add an IEnumerable to the PersistenceQueue using the Add() method.\n" +
				                             " Use the AddAll() method instead.\n" +
				                             "Type: [" + itemType.FullName + "]" );
			}

			// Add this single instance.
			GetInstanceQueue( itemType ).AddInstance( item );
			return this;
		}


		/// <summary>
		/// Adds an PersistableTypeInfo object to the set by accepting an PersistableTypeInfoBuilder with which to build the PersistableTypeInfo object from.
		/// </summary>
		/// <param name="builder">The builder used to create the PersistableTypeInfo to be added.</param>
		void AddInstanceQueue( IInstanceQueueBuilder builder )
		{
			_typeQueue.AddInstanceQueue( builder );
			return;
		}


		/// <summary>
		/// Persists all the instances in the queue to the database.
		/// </summary>
		public void Persist()
		{
			_typeQueue.Persist();
		}
	}
}