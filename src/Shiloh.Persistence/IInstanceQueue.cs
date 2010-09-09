namespace Shiloh.Persistence
{
	/// <summary>
	/// Non-Generic interface for an InstanceQueue.
	/// </summary>
	public interface IInstanceQueue
	{
		/// <summary>
		/// The parent TypeQueue this InstanceQueue is a member of.
		/// </summary>
		/// <value>The type queue.</value>
		TypeQueue ParentQueue { get; set; }


		/// <summary>
		/// Persists the instances to the database using the defined PersistAction.
		/// </summary>
		void Persist();


		/// <summary>
		/// Adds the instance from the non-strongly typed interface...thus, we need to cast..
		/// This property is only visible if you are explicitly looking at the object as an IInstanceQueue.
		/// </summary>
		/// <param name="item">The instance item to add.</param>
		void AddInstance( object item );
	}
}