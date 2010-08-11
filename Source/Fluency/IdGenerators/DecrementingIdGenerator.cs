namespace Fluency.IdGenerators
{
	/// <summary>
	/// Generates Id values starting at a specific number, decrementing subsequent ids by 1.
	/// </summary>
	public class DecrementingIdGenerator : IIdGenerator
	{
		int _id = -1;

		/// <summary>
		/// Initializes a new instance of the <see cref="DecrementingIdGenerator"/> class. By default, the first Id value that will be used is -1.
		/// </summary>
		public DecrementingIdGenerator() {}


		/// <summary>
		/// Initializes a new instance of the <see cref="DecrementingIdGenerator"/> class providing the first value to use as an Id.
		/// </summary>
		/// <param name="startingValue">The first Id value that will be used.</param>
		public DecrementingIdGenerator( int startingValue )
		{
			_id = startingValue;
		}


		#region IIdGenerator Members

		/// <summary>
		/// Gets the next Id.
		/// </summary>
		/// <returns></returns>
		public int GetNextId()
		{
			return _id--;
		}

		#endregion
	}
}