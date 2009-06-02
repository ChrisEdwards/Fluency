namespace Fluency.IdGenerators
{
	/// <summary>
	/// Generates Id values starting at a specific number, decrementing subsequent ids by 1.
	/// </summary>
	public class DecrementingIdGenerator : IIdGenerator
	{
		private int _id = -1;

		public DecrementingIdGenerator() {}


		public DecrementingIdGenerator( int startingValue )
		{
			_id = startingValue;
		}


		#region IIdGenerator Members

		public int GetNextId()
		{
			return _id--;
		}

		#endregion
	}
}