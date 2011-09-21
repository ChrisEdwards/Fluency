// Copyright 2011 Chris Edwards
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
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