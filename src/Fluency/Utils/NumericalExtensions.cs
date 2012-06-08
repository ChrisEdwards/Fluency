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


namespace Fluency.Utils
{
	public static class NumericalExtensions
	{
		public static DateTime YearsAgo(this int years)
		{
			return DateTime.Now.AddYears(years * -1);
		}


		public static DateTime MonthsAgo(this int months)
		{
			return DateTime.Now.AddMonths(months * -1);
		}


		public static DateTime DaysAgo(this int days)
		{
			return DateTime.Now.AddDays(days * -1);
		}


		public static DateTime YearsFromNow(this int years)
		{
			return DateTime.Now.AddYears(years);
		}


		public static DateTime MonthsFromNow(this int months)
		{
			return DateTime.Now.AddMonths(months);
		}


		public static DateTime DaysFromNow(this int days)
		{
			return DateTime.Now.AddDays(days);
		}


		public static double Cents(this int cents)
		{
			double value = (double)cents / 100;
			return Math.Round(value, 2);
		}


		public static double Percent(this int percent)
		{
			double value = (double)percent / 100;
			return Math.Round(value, 2);
		}


		public static double dollars(this int dollars)
		{
			return dollars;
		}

		public static IEnumerable<int> Times(this int times)
		{
			return Enumerable.Range(0, times);
		}
	}
}