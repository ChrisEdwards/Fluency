using System;
using System.Collections.Generic;
using System.Text;
using Fluency.Utils;


namespace Fluency.DataGeneration
{
	public static class ARandom
	{
		static readonly Random _random = new Random();
		public static IValueConstraints _valueConstraints = new SqlServerDefaultValuesAndConstraints();


		public static string String( int size )
		{
			var builder = new StringBuilder();
			for ( int i = 0; i < size; i++ )
					//26 letters in the alfabet, ascii + 65 for the capital letters
				builder.Append( Convert.ToChar( Convert.ToInt32( Math.Floor( 26 * _random.NextDouble() + 65 ) ) ) );
			return builder.ToString();
		}


		public static string StringPattern( string pattern )
		{
			string output = "";
			foreach ( char c in pattern )
			{
				string randomizedPattern = GetRandomizedPatternChar( c );
				output += randomizedPattern;
			}
			return output;
		}


		public static string Text( int maxChars )
		{
			var sb = new StringBuilder();
			var waffle = new WaffleEngine( _random );

			// Guess at a number of paragraphs to generate.
			const int numberOfCharactersPerParagraph = 100;
			int numberOfParagraphsToGenerate = maxChars / numberOfCharactersPerParagraph + 1;

			waffle.TextWaffle( numberOfParagraphsToGenerate, false, sb );

			return sb.ToString( 0, maxChars );
		}


		public static string Title( int maxChars )
		{
			var waffle = new WaffleEngine( _random );
			string title = waffle.GenerateTitle();
			if ( title.Length > maxChars )
				title = title.Substring( 0, maxChars );
			return title;
		}


		public static int Int()
		{
			return IntBetween( 1, 9999 );
		}


		// TODO: Decimal???
		// TODO: Byte???

		public static int PositiveInt()
		{
			return IntBetween( 1, 9999 );
		}


		public static int IntBetween( int min, int max )
		{
			int result = _random.Next( min + 1, max + 2 );
			return result - 1;
		}


		public static double Double()
		{
			return _random.NextDouble() + Int();
		}


		public static double DoubleBetween( double min, double max )
		{
			double range = max - min;
			return min + ( range * _random.NextDouble() );
		}


		public static T ItemFrom< T >( IList< T > items )
		{
			return items[IntBetween( 0, items.Count - 1 )];
		}


		public static T ItemFrom< T >( params T[] items )
		{
			return ItemFrom< T >( new List< T >( items ) );
		}


		public static T EnumValue< T >()
		{
			if ( !typeof ( T ).IsSubclassOf( typeof ( Enum ) ) )
				throw new ArgumentException( "Must be enum type." );

			Array values = Enum.GetValues( typeof ( T ) );

			int randomArrayIndex = IntBetween( 0, values.Length - 1 );
			return (T)values.GetValue( randomArrayIndex );
		}


		public static DateTime DateTime()
		{
			return DateTimeBetween( _valueConstraints.MinDateTime, _valueConstraints.MaxDateTime );
		}


		public static DateTime DateTimeBetween( DateTime min, DateTime max )
		{
			if ( max <= min )
				throw new ArgumentException( "Max must be greater than min." );

			double startTick = min.Ticks;
			double endTick = max.Ticks;
			double numberOfTicksInRange = endTick - startTick;
			double randomTickInRange = startTick + numberOfTicksInRange * _random.NextDouble();
			return new DateTime( Convert.ToInt64( randomTickInRange ) );
		}


		public static DateTime DateTimeBefore( DateTime compareDateTime )
		{
			return DateTimeBetween( _valueConstraints.MinDateTime, compareDateTime );
		}


		public static DateTime DateTimeAfter( DateTime compareDateTime )
		{
			return DateTimeBetween( compareDateTime, _valueConstraints.MaxDateTime );
		}


		public static DateTime DateAfter( DateTime compareDateTime )
		{
			return DateTimeAfter( compareDateTime ).Date;
		}


		public static DateTime DateInPastSince( DateTime startDate )
		{
			return DateTimeInPastSince( startDate ).Date;
		}


		public static DateTime DateTimeInPast()
		{
			return DateTimeBefore( System.DateTime.Now );
		}


		public static DateTime DateInPast()
		{
			return DateTimeBefore( System.DateTime.Now ).Date;
		}


		public static DateTime DateTimeInPastYear()
		{
			return DateTimeInPastSince( 1.YearsAgo() );
		}


		public static DateTime DateInPastYear()
		{
			return DateTimeInPastYear().Date;
		}


		public static DateTime DateTimeInFuture()
		{
			return DateTimeAfter( System.DateTime.Now );
		}


		public static DateTime DateTimeInPastSince( DateTime startDate )
		{
			return DateTimeBetween( startDate, System.DateTime.Now );
		}


		static string GetRandomizedPatternChar( char c )
		{
			switch ( c )
			{
				case '9':
				case '#':
					return IntBetween( 0, 9 ).ToString();

				default:
					return c.ToString();
			}
		}


		public static decimal InterestRate()
		{
			return (decimal)DoubleBetween( 0, 10 );
		}


		public static decimal CurrencyAmount()
		{
			return IntBetween( 0, 999999999 ) + ( IntBetween( 0, 100 ) / 100 );
		}


		public static decimal CurrencyAmountLessThan( int maxAmount )
		{
			return IntBetween( 0, maxAmount - 1 ) + ( IntBetween( 0, 100 ) / 100 );
		}


		public static bool Boolean()
		{
			return ItemFrom( true, false );
		}


		public static string Email()
		{
			return System.String.Format( "{0}@{1}.{2}",
			                             String( 10 ),
			                             // user name
			                             String( 10 ),
			                             // domain
			                             ItemFrom( "com", "org", "net" ) ); // domain ext.
		}


		public static string FirstName()
		{
			return ItemFrom( RandomData.FirstNames );
		}


		public static string LastName()
		{
			return ItemFrom( RandomData.LastNames );
		}


		public static string FullName()
		{
			return FirstName() + " " + LastName();
		}


		public static string StateName()
		{
			return ItemFrom( RandomData.States );
		}


		public static string StateCode()
		{
			return ItemFrom( RandomData.StateCodes );
		}


		public static string City()
		{
			return ItemFrom( RandomData.Cities );
		}


		public static DateTime BirthDate()
		{
			return DateTimeBetween( 75.YearsAgo(), 5.YearsAgo() ).Date;
		}


		public static string ZipCode()
		{
			return StringPattern( "99999" );
		}
	}
}