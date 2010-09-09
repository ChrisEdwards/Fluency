namespace Shiloh.Persistence
{
	public interface ITestDataBuilder< T > where T : class
	{
		/// <summary>
		/// Builds this instance.
		/// </summary>
		/// <returns></returns>
		T build();
	}
}