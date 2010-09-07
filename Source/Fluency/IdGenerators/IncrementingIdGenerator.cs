namespace Fluency.IdGenerators
{
	/// <summary>
	/// Generates Id values starting at a specific number, incrementing subsequent ids by 1.
	/// </summary>
	public class IncrementingIdGenerator : IIdGenerator
	{
		int _id;

		public IncrementingIdGenerator() {}


		public IncrementingIdGenerator( int startingValue )
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
			return _id++;
		}

		#endregion
	}
}