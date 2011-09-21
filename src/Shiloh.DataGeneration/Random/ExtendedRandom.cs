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

namespace UMMO.TestingUtils.RandomData
{
	/// <summary>
	/// Implementation of IRandom
	/// </summary>
	public class ExtendedRandom : Random, IRandom
	{
		#region IRandom Members
		/// <summary>
		/// Returns an array of bytes of the specified length filled with random numbers.
		/// </summary>
		/// <param name="bufferLength">Length of the buffer.</param>
		/// <returns>A random array of bytes.</returns>
		public byte[] NextBytes(int bufferLength)
		{
			var bytes = new byte[bufferLength];
			NextBytes(bytes);
			return bytes;
		}

		private int NextInt32()
		{
			unchecked
			{
				int firstBits = Next(0, 1 << 4) << 28;
				int lastBits = Next(0, 1 << 28);
				return firstBits | lastBits;
			}
		}

		/// <summary>
		/// Returns a random decimal number.
		/// </summary>
		/// <returns>A fixed-point decimal number</returns>
		public decimal NextDecimal()
		{
			bool sign = Next(2) == 1;
			return NextDecimal(sign);
		}

		private decimal NextDecimal(bool sign)
		{
			var scale = (byte)Next(29);
			return new decimal(NextInt32(),
								NextInt32(),
								NextInt32(),
								sign,
								scale);
		}

		private decimal NextNonNegativeDecimal()
		{
			return NextDecimal(false);
		}

		/// <summary>
		/// Returns a random decimal number less than or equal to <paramref name="maxValue"/>.
		/// </summary>
		/// <param name="maxValue">The maximum possible value.</param>
		/// <returns>
		/// A fixed-point decimal number less than or equal to <paramref name="maxValue"/>
		/// </returns>
		public decimal NextDecimal(decimal maxValue)
		{
			return (NextNonNegativeDecimal() / Decimal.MaxValue) * maxValue;
		}

		/// <summary>
		/// Returns a random decimal number between <paramref name="minValue"/> and <paramref name="maxValue"/>.
		/// </summary>
		/// <param name="minValue">The minimum value.</param>
		/// <param name="maxValue">The maximum value.</param>
		/// <returns>
		/// A fixed-point decimal number between <paramref name="minValue"/> and <paramref name="maxValue"/>.
		/// </returns>
		public decimal NextDecimal(decimal minValue, decimal maxValue)
		{
			if (minValue > maxValue)
				throw new InvalidOperationException();

			// We want to prevent overflows, so if we get a situation that would create one,
			// then change the value to Decimal.MaxValue
			decimal range = ((maxValue == Decimal.MaxValue && minValue < 0) || (minValue == Decimal.MinValue && maxValue > 0)) ? Decimal.MaxValue : maxValue - minValue;
			return NextDecimal(range) + minValue;
		}


		/// <summary>
		/// Returns a random 64-bit integer.
		/// </summary>
		/// <returns>A 64-bit signed integer.</returns>
		public long NextLong()
		{
			var bytes = new byte[sizeof(long)];
			NextBytes(bytes);
			// strip out the sign bit
			//bytes[sizeof(long) - 1] = (byte)(bytes[sizeof(long) - 1] & 0x7f);
			return BitConverter.ToInt64(bytes, 0);
		}

		/// <summary>
		/// Returns a random 64-bit integer less than <paramref name="maxValue"/>.
		/// </summary>
		/// <param name="maxValue">The maximum value.</param>
		/// <returns>A 64-bit signed integer.</returns>
		public long NextLong(long maxValue)
		{
			return (long)((Math.Abs(NextLong()) / (double)Int64.MaxValue) * maxValue);
		}

		/// <summary>
		/// Returns a random 64-bit integer between <paramref name="minValue"/> and <paramref name="maxValue"/>.
		/// </summary>
		/// <param name="minValue">The minimum value.</param>
		/// <param name="maxValue">The maximum value.</param>
		/// <returns>A 64-bit signed integer.</returns>
		public long NextLong(long minValue, long maxValue)
		{
			if (minValue > maxValue)
				throw new InvalidOperationException();
			long range = ((maxValue == Int64.MaxValue && minValue < 0) ||
						   (minValue == Int64.MinValue && maxValue >= 0))
							 ? Int64.MaxValue
							 : maxValue - minValue;

			// Some kind of weird thing is going on here... It seems that
			// Int64.MinValue wants to stay that way.
			if (minValue == Int64.MinValue)
				minValue = Int64.MinValue + 1;

			return NextLong(range) + minValue;
		}

		#endregion
	}
}