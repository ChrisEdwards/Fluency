namespace Fluency.IdGenerators
{
	/// <summary>
	/// Generates all Ids as the same static value. Use zero for NHibernate classes since NHibernate creates the ids on its own.
	/// </summary>
	public class StaticValueIdGenerator : IIdGenerator
	{
		readonly int _staticIdValue;


		/// <summary>
		/// Initializes a new instance of the <see cref="StaticValueIdGenerator"/> class.
		/// </summary>
		/// <param name="staticIdValue">The static id value.</param>
		public StaticValueIdGenerator( int staticIdValue )
		{
			_staticIdValue = staticIdValue;
		}


		#region IIdGenerator Members

		/// <summary>
		/// Gets the next Id.
		/// </summary>
		/// <returns></returns>
		public int GetNextId()
		{
			return _staticIdValue;
		}

		#endregion
	}
}