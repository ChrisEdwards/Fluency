using System;
using System.Collections.Generic;


namespace Shiloh.Persistence
{
	/// <summary>
	/// Maintains a list of all the InstanceQueues (one for each type) that are to be persisted to the database.
	/// Handles adding new InstanceQueues for new types.
	/// </summary>
	public class TypeQueue
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TypeQueue"/> class.
		/// </summary>
		/// <param name="parent">The parent.</param>
		public TypeQueue( PersistenceQueue parent )
		{
			InstanceQueues = new Dictionary< Type, IInstanceQueue >();
			TypeOrder = new List< Type >();
			Parent = parent;
		}


		IDictionary< Type, IInstanceQueue > InstanceQueues { get; set; }
		IList< Type > TypeOrder { get; set; }
		public PersistenceQueue Parent { get; private set; }


		/// <summary>
		/// Adds a new InstanceQueue from a builder.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public void AddInstanceQueue( IInstanceQueueBuilder builder )
		{
			// Record order they were added so we can insert them into the database in the same order.
			Type entityType = builder.GetInstanceType();
			TypeOrder.Add( entityType );

			// Add the new InstanceQueue for this type.
			IInstanceQueue instanceQueue = builder.build();
			instanceQueue.ParentQueue = this;
			InstanceQueues.Add( entityType, instanceQueue );
		}


		/// <summary>
		/// Gets the InstanceQueue for the specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public InstanceQueue< T > GetInstanceQueue< T >() where T : class
		{
			return (InstanceQueue< T >)GetInstanceQueue( typeof ( T ) );
		}


		/// <summary>
		/// Gets the InstanceQueue for the specified type.
		/// </summary>
		/// <param name="instanceType">Type of the instance.</param>
		/// <returns></returns>
		public IInstanceQueue GetInstanceQueue( Type instanceType )
		{
			if ( !InstanceQueues.ContainsKey( instanceType ) )
			{
				throw new ArgumentException( "Attempting to access type [" + instanceType.FullName + "] that has not been defined for PersistenceQueue.\n" +
				                             "You must configure the type information for this type before using it in the PersistenceQueue.\n" +
				                             "Types are configured by the BancVue.Tests.Common.PersistenceConfiguration.ConfigurePersistenceQueue() method.\n" );
			}

			return InstanceQueues[instanceType];
		}


		/// <summary>
		/// Persists all the instances in the InstanceQueues.
		/// </summary>
		public void Persist()
		{
			foreach ( Type type in TypeOrder )
				InstanceQueues[type].Persist();
		}
	}
}