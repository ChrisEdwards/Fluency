namespace Fluency.IdGenerators
{
	/// <summary>
	/// Generates all Ids as the same static value. Use zero for NHibernate classes since NHibernate creates the ids on its own.
	/// </summary>
	public class StaticValueIdGenerator : IIdGenerator
	{
		private readonly int _staticIdValue;


		public StaticValueIdGenerator( int staticIdValue )
		{
			_staticIdValue = staticIdValue;
		}


		#region IIdGenerator Members

		public int GetNextId()
		{
			return _staticIdValue;
		}

		#endregion
	}
}