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

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;


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
		public static string ToFixedWidth( this string input, int width )
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
			if ( text.Length <= length )
				return text;

			return text.Substring( 0, length );
		}


		/// <summary>
		/// Surrounds a string with brackets.
		/// </summary>
		/// <param name="theString">The string.</param>
		/// <returns></returns>
		public static string WithinBrackets( this string theString )
		{
			return theString.Within( "[", "]" );
		}


		/// <summary>
		/// Surrounds a string with braces
		/// </summary>
		/// <param name="theString">The name.</param>
		/// <returns></returns>
		public static string WithinBraces( this string theString )
		{
			return theString.Within( "{", "}" );
		}


		/// <summary>
		/// Surrounds a string with opening and closing strings.
		/// <code>"Bob".Within( "{", "}" ) = "{Bob}"</code>
		/// </summary>
		/// <param name="theString">The string.</param>
		/// <param name="opening">The opening.</param>
		/// <param name="closing">The closing.</param>
		/// <returns></returns>
		public static string Within( this string theString, string opening, string closing )
		{
			var toReturn = new StringBuilder();

			if ( !theString.StartsWith( opening ) )
				toReturn.Append( opening );

			toReturn.Append( theString );

			if ( !theString.EndsWith( closing ) )
				toReturn.Append( closing );

			return toReturn.ToString();
		}


		public static string Format( this string format, params object[] args )
		{
			return String.Format( format, args );
		}


		/// <summary>
		/// Gets the camelCase version of this text.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public static string ToCamelCase( this string source )
		{
			if (source == String.Empty) 
				return String.Empty;

			var firstChar = source.Substring( 0, 1 );
			var restOfString = source.Remove( 0, 1 );
			return firstChar.ToLower() + restOfString;
		}


		public static string ToXml< T >( this T toSerialize, Type contract )
		{
			Stream xmlData = new MemoryStream();
			var serializer = new XmlSerializer( contract );
			serializer.Serialize( xmlData, toSerialize );
			xmlData.Seek( 0, SeekOrigin.Begin );
			return new StreamReader( xmlData ).ReadToEnd();
		}


		/// <summary>
		/// Validates wheter a string is a valid IPv4 address.
		/// </summary>
		/// <param name="s">The s.</param>
		/// <returns>
		/// <c>true</c> if the specified string is a valid ip address; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsValidIpAddress( this string s )
		{
			return Regex.IsMatch( s,
			                      @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b" );
		}


		/// <summary>
		/// Strips the double quotes from the string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string StripQuotes( this string value )
		{
			return value.Replace( "\"", "" );
		}


		/// <summary>
		/// Strips any whitespace chars from the string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string StripWhitespace( this string value )
		{
			return Regex.Replace( value, @"\s", "" );
		}


		/// <summary>
		/// Checks if this string value exists in a list of string values.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="expectedValues">The expected values.</param>
		/// <returns></returns>
		public static bool In( this string value, params string[] expectedValues )
		{
			return expectedValues.Contains( value );
		}
	}
}