using System;


namespace Shiloh.Persistence
{
	/// <summary>
	/// Marker Interface to work around .NET strong typing issues.
	/// </summary>
	public interface IInstanceQueueBuilder
	{
		/// <summary>
		/// Gets the entity type (.NET type) this PersistableTypeInfoBuilder refers to.
		/// </summary>
		/// <returns></returns>
		Type GetInstanceType();


		/// <summary>
		/// Builds an InstnceQueue strongly typed against the persistable type.
		/// </summary>
		/// <returns></returns>
		IInstanceQueue build();
	}
}