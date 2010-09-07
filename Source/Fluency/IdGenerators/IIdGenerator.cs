namespace Fluency.IdGenerators
{
	/// <summary>
	/// Gets the next Id.
	/// </summary>
	/// <returns></returns>
	public interface IIdGenerator
	{
		int GetNextId();
	}
}