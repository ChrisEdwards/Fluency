namespace FluentObjectBuilder
{
	public interface ITestDataBuilder< T > : ITestDataBuilder where T : class
	{
		/// <summary>
		/// Builds this instance.
		/// </summary>
		/// <returns></returns>
		T build();
	}

	/// <summary>
	/// Marker interface.
	/// </summary>
	public interface ITestDataBuilder{}
}