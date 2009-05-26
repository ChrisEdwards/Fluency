using System;


namespace FluentObjectBuilder
{
	public static class NumericalExtensions
	{
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

		public static double cents(this int cents)
		{
			double value = (double) cents / 100;
			return Math.Round( value, 2 );
		}

		public static double percent(this int percent)
		{
			double value = (double) percent / 100;
			return Math.Round( value, 2 );
		}

		public static double dollars(this int dollars)
		{
			return (double) dollars;
		}
	}
}