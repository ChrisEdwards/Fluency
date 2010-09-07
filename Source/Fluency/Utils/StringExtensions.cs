namespace Fluency.Utils
{
	public static class StringExtensions
	{
		/// <summary>
		/// Extension for string.Format, since I always forget to type format till the end of the string.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		/// <returns></returns>
		public static string format_using( this string format, params object[] args )
		{
			return string.Format( format, args );
		}
	}
}