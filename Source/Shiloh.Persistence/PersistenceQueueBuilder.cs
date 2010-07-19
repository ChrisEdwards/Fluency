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