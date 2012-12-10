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
using System.Collections.Generic;
using System.Linq;


namespace Shiloh.Utils
{
	public static class NumericalExtensions
	{
		public static TimeSpan Days( this int days )
		{
			return TimeSpan.FromDays( days );
		}


		public static TimeSpan Hours( this int hours )
		{
			return TimeSpan.FromHours( hours );
		}


		public static TimeSpan Minutes( this int minutes )
		{
			return TimeSpan.FromMinutes( minutes );
		}


		public static TimeSpan Seconds( this int seconds )
		{
			return TimeSpan.FromSeconds( seconds );
		}


		public static TimeSpan Milliseconds( this int milliseconds )
		{
			return TimeSpan.FromMilliseconds( milliseconds );
		}


		public static TimeSpan Ticks( this int ticks )
		{
			return TimeSpan.FromTicks( ticks );
		}


		public static DateTime YearsAgo( this int years )
		{
			return DateTime.Now.AddYears( years * -1 );
		}


		public static DateTime MonthsAgo( this int months )
		{
			return DateTime.Now.AddMonths( months * -1 );
		}


		public static DateTime DaysAgo( this int days )
		{
			return DateTime.Now.AddDays( days * -1 );
		}


		public static DateTime YearsFromNow( this int years )
		{
			return DateTime.Now.AddYears( years );
		}


		public static DateTime MonthsFromNow( this int months )
		{
			return DateTime.Now.AddMonths( months );
		}


		public static DateTime DaysFromNow( this int days )
		{
			return DateTime.Now.AddDays( days );
		}


		public static double Cents( this int cents )
		{
			var value = (double)cents / 100;
			return Math.Round( value, 2 );
		}


		public static double Percent( this int percent )
		{
			var value = (double)percent / 100;
			return Math.Round( value, 2 );
		}


		public static double dollars( this int dollars )
		{
			return dollars;
		}


		public static IEnumerable< int > Times( this int times )
		{
			return Enumerable.Range( 0, times );
		}


		/// <summary>
		/// Repeats the given function the specified number of times returning 
		/// an IEnumerable containing the specified count of return values.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="count">The count.</param>
		/// <param name="func">The func.</param>
		/// <returns></returns>
		public static IEnumerable< T > Of< T >( this int count, Func< T > func )
		{
			for ( var i = 0; i < count; i++ )
				yield return func.Invoke();
		}
	}
}