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
	}
}