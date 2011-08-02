namespace Fluency
{
	public interface IFluentBuilder< out T > : IFluentBuilder
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
	public interface IFluentBuilder {}
}