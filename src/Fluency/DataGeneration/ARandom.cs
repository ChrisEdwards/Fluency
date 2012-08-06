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
using System.Text;
using Fluency.Utils;


namespace Fluency.DataGeneration
{
	public static class ARandom
	{
		private static readonly Random _random = new Random();
		public static IValueConstraints _valueConstraints = new SqlServerDefaultValuesAndConstraints();


		public static string String( int size )
		{
			var builder = new StringBuilder();
			for ( var i = 0; i < size; i++ )
					//26 letters in the alfabet, ascii + 65 for the capital letters
				builder.Append( Convert.ToChar( Convert.ToInt32( Math.Floor( 26 * _random.NextDouble() + 65 ) ) ) );
			return builder.ToString();
		}


		/// <summary>
		/// Creates a randomized string constrained to characters in the specified character set.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <param name="charactersToChooseFrom">The characters to choose from.</param>
		/// <returns></returns>
		public static string StringFromCharacterSet( int size, string charactersToChooseFrom )
		{
			var characterArray = charactersToChooseFrom.ToCharArray();
			var builder = new StringBuilder( size );
			for ( var i = 0; i < size; i++ )
				builder.Append( ItemFrom( characterArray ) );
			return builder.ToString();
		}


		public static string StringPattern( string pattern )
		{
			var output = "";
			foreach ( var c in pattern )
			{
				var randomizedPattern = GetRandomizedPatternChar( c );
				output += randomizedPattern;
			}
			return output;
		}


		public static string Text( int maxChars )
		{
			var sb = new StringBuilder();
			var waffle = new WaffleEngine( _random );

			// Guess at a number of paragraphs to generate.
			const int numberOfCharactersPerParagraph = 80;
			var numberOfParagraphsToGenerate = maxChars / numberOfCharactersPerParagraph + 1;

			waffle.TextWaffle( numberOfParagraphsToGenerate, false, sb );

			return sb.ToString( 0, maxChars );
		}


		public static string Title( int maxChars )
		{
			var waffle = new WaffleEngine( _random );
			var title = waffle.GenerateTitle();
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
			var result = _random.Next( min + 1, max + 2 );
			return result - 1;
		}


		public static double Double()
		{
			return _random.NextDouble() + Int();
		}


		public static double DoubleBetween( double min, double max )
		{
			var range = max - min;
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

			var values = Enum.GetValues( typeof ( T ) );

			var randomArrayIndex = IntBetween( 0, values.Length - 1 );
			return (T)values.GetValue( randomArrayIndex );
		}


		public static DateTime DateTime()
		{
			return DateTimeBetween( _valueConstraints.MinDateTime, _valueConstraints.MaxDateTime );
		}


		public static DateTime DateTimeBetween( DateTime min, DateTime max )
		{
			if ( max <= min )
				throw new ArgumentException( string.Format( "Max [{0}] must be greater than min [{1}].", max, min ) );

			double startTick = min.Ticks;
			double endTick = max.Ticks;
			var numberOfTicksInRange = endTick - startTick;
			var randomTickInRange = startTick + numberOfTicksInRange * _random.NextDouble();
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


		private static string GetRandomizedPatternChar( char c )
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


		/// <summary>
		/// Generates a random age between 1 and 100.
		/// </summary>
		/// <returns></returns>
		public static int Age()
		{
			return IntBetween( 1, 100 );
		}


		/// <summary>
		/// Generates a random adult age between 21 and 65 (inclusive).
		/// </summary>
		/// <returns></returns>
		public static int AdultAge()
		{
			return IntBetween( 21, 65 );
		}


		/// <summary>
		/// Generates a random birthdate for a person of the specified age.
		/// </summary>
		/// <param name="age">The age.</param>
		/// <returns></returns>
		public static DateTime BirthDateForAge( int age )
		{
			var latestPossibleBirthday = age.YearsAgo().Date;
			var earliestPossibleBirthday = ( age + 1 ).YearsAgo().Date.AddDays( 1 ); // Without this extra day, would be too old.
			return DateBetween( earliestPossibleBirthday, latestPossibleBirthday );
		}


		/// <summary>
		/// Generates a random date (no time) between the two specified dates (inclusive).
		/// </summary>
		/// <param name="startDate">The start date.</param>
		/// <param name="endDate">The end date.</param>
		/// <returns></returns>
		public static DateTime DateBetween( DateTime startDate, DateTime endDate )
		{
			if ( DateTimeRangeDoesNotCrossDateBoundary( startDate, endDate ) )
			{
				throw new FluencyException(
						"No valid date exists between the two supplied date time values. For a valid date to exist, there must be a midnight value between them (since technically a date without time is a datetime for midnight on the specified day)." );
			}

			var result = DateTimeBetween( startDate, endDate ).Date;

			// Since start date includes time and result does not, stripping the time could make the result less than the start date. If so, just try again.
			if ( result < startDate )
				result = DateBetween( startDate, endDate );

			return result;
		}


		private static bool DateTimeRangeDoesNotCrossDateBoundary( DateTime startDate, DateTime endDate )
		{
			var bothOnSameDay = startDate.Date == endDate.Date;
			var neitherAreMidnight = startDate != startDate.Date && endDate != endDate.Date;
			return bothOnSameDay && neitherAreMidnight;
		}
	}
}