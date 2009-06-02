namespace Fluency.IdGenerators
{
	/// <summary>
	/// Generates Id values starting at a specific number, incrementing subsequent ids by 1.
	/// </summary>
	public class IncrementingIdGenerator : IIdGenerator
	{
		private int _id;

		public IncrementingIdGenerator() {}


		public IncrementingIdGenerator( int startingValue )
		{
			_id = startingValue;
		}


		#region IIdGenerator Members

		public int GetNextId()
		{
			return _id++;
		}

		#endregion
	}
}