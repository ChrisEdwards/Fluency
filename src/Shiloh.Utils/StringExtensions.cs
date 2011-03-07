namespace Shiloh.Utils
{
	public static class StringExtensions
	{
		/// <summary>
		/// Pads or truncates the string such that is has the specified width. Prepends the string with spaces if it
		/// is not long enough, or truncates the right side of the string if it exceeds the specified width.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="width">The width.</param>
		/// <returns></returns>
		public static string ToFixedWidth(this string input, int width)
		{
			return input.Left( width ).PadLeft( width );
		}

		/// <summary>
		/// Gets the specified number of leftmost characters from the string. If the string is not as long as the requested
		/// number of characters, the string is returned unmodified.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="length">The length.</param>
		/// <returns></returns>
		public static string Left( this string text, int length )
		{
			if (text.Length <= length)
				return text;

			return text.Substring( 0, length );
		}
	}
}