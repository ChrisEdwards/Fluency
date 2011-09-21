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
namespace UMMO.TestingUtils
{
	/// <summary>
	/// Random number generation. Absraction from System.Random for testability.
	/// </summary>
	public interface IRandom
	{
		/// <summary>
		/// Returns a nonnegative random number.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to zero and less than <see cref="F:System.Int32.MaxValue"/>.
		/// </returns>
		int Next();

		/// <summary>
		/// Returns a nonnegative random number less than the specified maximum.
		/// </summary>
		/// <param name="maxValue">The exclusive upper bound of the random number to be generated. <paramref name="maxValue"/> must be greater than or equal to zero.</param>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to zero, and less than <paramref name="maxValue"/>; that is, the range of return values ordinarily includes zero but not <paramref name="maxValue"/>. However, if <paramref name="maxValue"/> equals zero, <paramref name="maxValue"/> is returned.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// <paramref name="maxValue"/> is less than zero.
		/// </exception>
		int Next(int maxValue);

		/// <summary>
		/// Returns a random number within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The exclusive upper bound of the random number returned. <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>.</param>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to <paramref name="minValue"/> and less than <paramref name="maxValue"/>; that is, the range of return values includes <paramref name="minValue"/> but not <paramref name="maxValue"/>. If <paramref name="minValue"/> equals <paramref name="maxValue"/>, <paramref name="minValue"/> is returned.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// <paramref name="minValue"/> is greater than <paramref name="maxValue"/>.
		/// </exception>
		int Next(int minValue, int maxValue);

		/// <summary>
		/// Fills the elements of a specified array of bytes with random numbers.
		/// </summary>
		/// <param name="buffer">An array of bytes to contain random numbers.</param>
		/// <exception cref="T:System.ArgumentNullException">
		/// <paramref name="buffer"/> is null.
		/// </exception>
		void NextBytes(byte[] buffer);

		/// <summary>
		/// Returns an array of bytes of the specified length filled with random numbers.
		/// </summary>
		/// <param name="bufferLength">Length of the buffer.</param>
		/// <returns>A random array of bytes.</returns>
		byte[] NextBytes(int bufferLength);

		/// <summary>
		/// Returns a random number between 0.0 and 1.0.
		/// </summary>
		/// <returns>
		/// A double-precision floating point number greater than or equal to 0.0, and less than 1.0.
		/// </returns>
		double NextDouble();

		/// <summary>
		/// Returns a random decimal number.
		/// </summary>
		/// <returns>A fixed-point decimal number</returns>
		decimal NextDecimal();

		/// <summary>
		/// Returns a random decimal number less than or equal to <paramref name="maxValue"/>.
		/// </summary>
		/// <param name="maxValue">The maximum possible value.</param>
		/// <returns>A fixed-point decimal number less than or equal to <paramref name="maxValue"/></returns>
		decimal NextDecimal(decimal maxValue);

		/// <summary>
		/// Returns a random decimal number between <paramref name="minValue"/> and <paramref name="maxValue"/>.
		/// </summary>
		/// <param name="minValue">The minimum value.</param>
		/// <param name="maxValue">The maximum value.</param>
		/// <returns>A fixed-point decimal number between <paramref name="minValue"/> and <paramref name="maxValue"/>.</returns>
		decimal NextDecimal(decimal minValue, decimal maxValue);

		/// <summary>
		/// Returns a random 64-bit integer.
		/// </summary>
		/// <returns>A 64-bit signed integer.</returns>
		long NextLong();

		/// <summary>
		/// Returns a random 64-bit integer less than <paramref name="maxValue"/>.
		/// </summary>
		/// <param name="maxValue">The maximum value.</param>
		/// <returns>A 64-bit signed integer.</returns>
		long NextLong(long maxValue);

		/// <summary>
		/// Returns a random 64-bit integer between <paramref name="minValue"/> and <paramref name="maxValue"/>.
		/// </summary>
		/// <param name="minValue">The minimum value.</param>
		/// <param name="maxValue">The maximum value.</param>
		/// <returns>A 64-bit signed integer.</returns>
		long NextLong(long minValue, long maxValue);
	}
}