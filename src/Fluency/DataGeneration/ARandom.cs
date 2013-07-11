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
	/// <summary>
	/// A random value generator.
	/// </summary>
	public static class ARandom
	{
		private static readonly Random _random = new Random();
		public static IValueConstraints _valueConstraints = new SqlServerDefaultValuesAndConstraints();

		private static Random Random { get { return ThreadLocalRandom.Instance; } }


		#region Strings


		/// <summary>
		/// Returns a random <see cref="string"/> of all capital letters. The length of the string equals the specified <paramref name="size"/>.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <returns></returns>
		public static string String( int size )
		{
			var builder = new StringBuilder();
			for ( var i = 0; i < size; i++ )
					//26 letters in the alfabet, ascii + 65 for the capital letters
				builder.Append( Convert.ToChar( Convert.ToInt32( Math.Floor( 26 * Random.NextDouble() + 65 ) ) ) );
			return builder.ToString();
		}


		/// <summary>
		/// Creates a random <see cref="string"/> constrained to characters in the specified <paramref name="charactersToChooseFrom"/>
		/// whose length equals the specified <paramref name="size"/>.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <param name="charactersToChooseFrom">The characters to choose from.</param>
		/// <returns></returns>
		public static string StringFromCharacterSet( int size, string charactersToChooseFrom )
		{
			if ( string.IsNullOrEmpty( charactersToChooseFrom ) )
				throw new ArgumentNullException( "charactersToChooseFrom", "charactersToChooseFrom[] was null or empty. Cannot generate a string with no chars to choose from." );

			var characterArray = charactersToChooseFrom.ToCharArray();
			var builder = new StringBuilder( size );
			for ( var i = 0; i < size; i++ )
				builder.Append( ItemFrom( characterArray ) );
			return builder.ToString();
		}


		/// <summary>
		/// Returns a <see cref="string"/> containing randomly generated text, with a length less than or equal to 
		/// the specified <paramref name="maxChars"/>.<br/>
		/// The text looks real, but is actually gibberish. Similar to lorem ipsum. Uses the <see cref="WaffleEngine"/> to generate the text.
		/// </summary>
		/// <param name="maxChars">The max chars.</param>
		/// <returns></returns>
		public static string Text( int maxChars )
		{
			if ( maxChars < 1 )
				throw new ArgumentOutOfRangeException( "maxChars", "maxChars must be greater than zero, but was [{0}]".format_using( maxChars ) );

			var sb = new StringBuilder();
			var waffle = new WaffleEngine( Random );

			// Guess at a number of paragraphs to generate.
			const int numberOfCharactersPerParagraph = 80;
			var numberOfParagraphsToGenerate = maxChars / numberOfCharactersPerParagraph + 1;

			waffle.TextWaffle( numberOfParagraphsToGenerate, false, sb );

			return sb.ToString( 0, maxChars );
		}


		/// <summary>
		/// Returns a random <see cref="string"/> containing a title of length less than or equal to the specified <paramref name="maxChars"/>.
		/// </summary>
		/// <param name="maxChars">The max chars.</param>
		/// <returns></returns>
		public static string Title( int maxChars )
		{
			if ( maxChars < 1 )
				throw new ArgumentOutOfRangeException( "maxChars", "maxChars must be greater than zero, but was [{0}]".format_using( maxChars ) );

			var waffle = new WaffleEngine( Random );
			var title = waffle.GenerateTitle();
			if ( title.Length > maxChars )
				title = title.Substring( 0, maxChars );
			return title;
		}


		/// <summary>
		/// Returns a <see cref="string"/> containing the <paramref name="pattern"/> after replacing 
		/// any <c>'9'</c> or <c>'#'</c> characters with random digits.<br/>
		/// Example:<br/>
		/// <code>ARandom.StringPattern( "(999) 999-9999" ) => "(361) 735-8254"</code>
		/// </summary>
		/// <param name="pattern">The pattern.</param>
		/// <returns></returns>
		public static string StringPattern( string pattern )
		{
			if ( pattern == null )
				throw new ArgumentNullException( "pattern" );

			var output = "";
			foreach ( var c in pattern )
			{
				var randomizedPattern = GetRandomizedPatternChar( c );
				output += randomizedPattern;
			}
			return output;
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

		#endregion


		#region Integers


		/// <summary>
		/// Returns a random <see cref="integer"/> between 1 and 9999.
		/// </summary>
		/// <remarks>
		/// I think this code was tampered with. I believe it used to include the full range of int 
		/// including negative values. Not sure.
		/// </remarks>
		/// <returns></returns>
		public static int Int()
		{
			return IntBetween( 1, 9999 );
		}


		// TODO: Decimal???
		// TODO: Byte???

		/// <summary>
		/// Returns a random positive <see cref="integer"/> between 1 and 9999.
		/// </summary>
		/// <returns></returns>
		public static int PositiveInt()
		{
			return IntBetween( 1, 9999 );
		}


		/// <summary>
		/// Returns a random <see cref="integer"/> between the specified <paramref name="min"/> and <paramref name="max"/> (inclusive).
		/// </summary>
		/// <param name="min">The min.</param>
		/// <param name="max">The max.</param>
		/// <returns></returns>
		public static int IntBetween( int min, int max )
		{
			if ( !( min <= max ) )
				throw new ArgumentException( "Min value must be less than or equal to Max value. Failed to generate a random int between {0} and {1}".format_using( min, max ) );

			// Guard against number greater than int.maxvalue
			var offset = -1;
			if ( min == int.MinValue )
			{
				// Guard against number smaller than int.minvalue
				offset = 1;

				// If doing this would cause us to violate int.maxvalue, cheat (will never generate minvalue).
				if ( max == int.MaxValue )
				{
					offset = -1;
					min = min + 1;
				}
			}

			// Use offset because Next() does not allow int.MaxValue as upper bounds.
			var result = Random.Next( min + offset, max + offset );
			return result - offset;
		}


		#endregion


		#region Doubles


		/// <summary>
		/// Returns a random <see cref="double"/> value.
		/// </summary>
		/// <returns></returns>
		public static double Double()
		{
			return Random.NextDouble() + Int();
		}


		/// <summary>
		/// Returns a random <see cref="double"/> value between the specified <paramref name="min"/> and <paramref name="max"/> (inclusive).
		/// </summary>
		/// <param name="min">The min.</param>
		/// <param name="max">The max.</param>
		/// <returns></returns>
		public static double DoubleBetween( double min, double max )
		{
			if ( min > max )
				throw new ArgumentException( "Min value must be less than Max value." );

			var range = max - min;
			return min + ( range * Random.NextDouble() );
		}


		#endregion


		#region Floats (Singles)


		/// <summary>
		/// Returns a random <see cref="float"/> value.
		/// </summary>
		/// <returns></returns>
		public static float Float()
		{
			return (float)( Random.NextDouble() + Int() );
		}


		/// <summary>
		/// Returns a random <see cref="float"/> value between the specified <paramref name="min"/> and <paramref name="max"/> (inclusive).
		/// </summary>
		/// <param name="min">The min.</param>
		/// <param name="max">The max.</param>
		/// <returns></returns>
		public static float FloatBetween( float min, float max )
		{
			if ( min > max )
				throw new ArgumentException( "Min value must be less than Max value." );

			var range = max - min;
			return min + ( range * (float)Random.NextDouble() );
		}


		#endregion


		#region Currency (Decimal)


		/// <summary>
		/// Returns a random currency amount (<see cref="Decimal"/>). <br/>
		/// The value will be between zero and 999,999,999.00 (inclusive) with a precision of two decimal places.
		/// </summary>
		/// <returns></returns>
		public static decimal CurrencyAmount()
		{
			return IntBetween( 0, 999999999 ) + ( IntBetween( 0, 100 ) / 100 );
		}


		/// <summary>
		/// Returns a random currency amount (<see cref="Decimal"/>) that is less than or equal to 
		/// the specified <paramref name="maxAmount"/>. <br/>
		/// The value will be between zero and the specified <paramref name="maxAmount"/> (inclusive) 
		/// with a precision of two decimal places.
		/// </summary>
		/// <param name="maxAmount">The max amount.</param>
		/// <returns></returns>
		public static decimal CurrencyAmountLessThan( int maxAmount )
		{
			return IntBetween( 0, maxAmount - 1 ) + ( IntBetween( 0, 100 ) / 100 );
		}


		/// <summary>
		/// Returns a random currency amount (<see cref="Decimal"/>) that is greater than or equal to the 
		/// specified <paramref name="minAmount"/> and less than or equal to  the specified 
		/// <paramref name="maxAmount"/>. <br/>
		/// The value will be between zero and the specified <paramref name="maxAmount"/> (inclusive) 
		/// with a precision of two decimal places.
		/// </summary>
		/// <param name="minAmount">The min amount. </param>
		/// <param name="maxAmount">The max amount.</param>
		/// <returns></returns>
		public static decimal CurrencyAmountBetween( int minAmount, int maxAmount )
		{
			return IntBetween( minAmount, maxAmount - 1 ) + ( IntBetween( 0, 100 ) / 100 );
		}


		#endregion


		#region Lists and Enums


		/// <summary>
		/// Returns an item of type <typeparamref name="T"/> chosen at random from the specified list of <paramref name="items"/>.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The items.</param>
		/// <returns></returns>
		public static T ItemFrom< T >( IList< T > items )
		{
			if ( items == null )
				throw new ArgumentNullException( "items", "items parameter was NULL. Cannot select a random item from a null list." );

			if ( items.Count == 0 )
				throw new ArgumentException( "items was an empty list. Cannot select a random item from an empty list.", "items" );

			return items[IntBetween( 0, items.Count - 1 )];
		}


		/// <summary>
		/// Returns an item of type <typeparamref name="T"/> chosen at random from the specified array of <paramref name="items"/>.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The items.</param>
		/// <returns></returns>
		public static T ItemFrom< T >( params T[] items )
		{
			if ( items == null )
				throw new ArgumentNullException( "items", "items parameter was NULL. Cannot select a random item from a null list." );

			if ( items.Length == 0 )
				throw new ArgumentException( "items was an empty list. Cannot select a random item from an empty list.", "items" );

			return ItemFrom< T >( new List< T >( items ) );
		}


		/// <summary>
		/// Returns a randomly selected enum value from the specified enum type <typeparamref name="TEnumType"/>.
		/// </summary>
		/// <typeparam name="TEnumType"></typeparam>
		/// <returns></returns>
		public static TEnumType EnumValue< TEnumType >()
		{
			if ( !typeof ( TEnumType ).IsSubclassOf( typeof ( Enum ) ) )
				throw new ArgumentException( "Must be enum type." );

			var values = Enum.GetValues( typeof ( TEnumType ) );

			var randomArrayIndex = IntBetween( 0, values.Length - 1 );
			return (TEnumType)values.GetValue( randomArrayIndex );
		}


		#endregion


		#region DateTimes


		/// <summary>
		/// Returns a random <see cref="DateTime"/>. <br/>
		/// Note, this may return dates far into the future as well as far into the past.
		/// </summary>
		/// <returns></returns>
		public static DateTime DateTime()
		{
			return DateTimeBetween( _valueConstraints.MinDateTime, _valueConstraints.MaxDateTime );
		}


		/// <summary>
		/// Returns a random <see cref="DateTime"/> between the specified <paramref name="min"/> and <paramref name="max"/> (inclusive).
		/// </summary>
		/// <param name="min">The min.</param>
		/// <param name="max">The max.</param>
		/// <returns></returns>
		public static DateTime DateTimeBetween( DateTime min, DateTime max )
		{
			if ( max <= min )
				throw new ArgumentException( string.Format( "Max [{0}] must be greater than min [{1}].", max, min ) );

			double startTick = min.Ticks;
			double endTick = max.Ticks;
			var numberOfTicksInRange = endTick - startTick;
			var randomTickInRange = startTick + numberOfTicksInRange * Random.NextDouble();

			// Handle overrun...that might occur.
			if ( randomTickInRange > System.DateTime.MaxValue.Ticks )
				randomTickInRange = System.DateTime.MaxValue.Ticks;

			return new DateTime( Convert.ToInt64( randomTickInRange ) );
		}


		/// <summary>
		/// Returns a random <see cref="DateTime"/> prior to the specified <paramref name="upperBounds"/>.
		/// </summary>
		/// <param name="upperBounds">The compare date time.</param>
		/// <returns></returns>
		public static DateTime DateTimeBefore( DateTime upperBounds )
		{
			return DateTimeBetween( _valueConstraints.MinDateTime, upperBounds );
		}


		/// <summary>
		/// Returns a random <see cref="DateTime"/> greater than the specified <paramref name="lowerBounds"/>.
		/// </summary>
		/// <param name="lowerBounds">The compare date time.</param>
		/// <returns></returns>
		public static DateTime DateTimeAfter( DateTime lowerBounds )
		{
			return DateTimeBetween( lowerBounds, _valueConstraints.MaxDateTime );
		}


		/// <summary>
		/// Returns a random <see cref="DateTime"/> less than now.
		/// </summary>
		/// <returns></returns>
		public static DateTime DateTimeInPast()
		{
			return DateTimeBefore( System.DateTime.Now );
		}


		/// <summary>
		/// Returns a random <see cref="DateTime"/> greater than the specified <paramref name="lowerBounds"/>
		/// and less than now.
		/// </summary>
		/// <param name="lowerBounds">The start date.</param>
		/// <returns></returns>
		public static DateTime DateTimeInPastSince( DateTime lowerBounds )
		{
			if ( lowerBounds >= System.DateTime.Now )
				throw new ArgumentException( "The datetime must be in the past.", "lowerBounds" );

			return DateTimeBetween( lowerBounds, System.DateTime.Now );
		}


		/// <summary>
		/// Returns a random <see cref="DateTime"/> greater than one year ago, and less than now.
		/// </summary>
		/// <returns></returns>
		public static DateTime DateTimeInPastYear()
		{
			return DateTimeInPastSince( 1.YearsAgo() );
		}


		/// <summary>
		/// Returns a random <see cref="DateTime"/> greater than now.
		/// </summary>
		/// <returns></returns>
		public static DateTime DateTimeInFuture()
		{
			return DateTimeAfter( System.DateTime.Now );
		}


		#endregion


		#region Dates (No Time)

		/// <summary>
		/// Generates a random date with no time component.
		/// </summary>
		/// <returns></returns>
		public static DateTime Date()
		{
			return DateTime().Date;
		}


		/// <summary>
		/// Generates a random date (no time) between the specified <paramref name="startDate"/> and <paramref name="endDate"/> (inclusive).
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


		/// <summary>
		/// Returns a random  date (<see cref="DateTime"/> with no time data) greater than the specified <paramref name="lowerBounds"/>
		/// </summary>
		/// <param name="lowerBounds">The compare date time.</param>
		/// <returns></returns>
		public static DateTime DateAfter( DateTime lowerBounds )
		{
			// Don't include lower bound date.
			var startDate = lowerBounds + TimeSpan.FromTicks( 1 );

			return DateBetween( startDate, _valueConstraints.MaxDateTime );
		}


		/// <summary>
		/// Returns a random  date (<see cref="DateTime"/> with no time data) less than today, 
		/// but greater than the specified <paramref name="lowerBounds"/>.
		/// </summary>
		/// <param name="lowerBounds">The start date.</param>
		/// <returns></returns>
		public static DateTime DateInPastSince( DateTime lowerBounds )
		{
			return DateTimeInPastSince( lowerBounds ).Date;
		}


		/// <summary>
		/// Returns a random  date (<see cref="DateTime"/> with no time data) less than today.
		/// </summary>
		/// <returns></returns>
		public static DateTime DateInPast()
		{
			return DateTimeBefore( System.DateTime.Now ).Date;
		}


		/// <summary>
		/// Returns a random date (<see cref="DateTime"/> with no time data) greater than one year ago today,
		/// and less than today.
		/// </summary>
		/// <returns></returns>
		public static DateTime DateInPastYear()
		{
			return DateTimeInPastYear().Date;
		}


		#endregion


		/// <summary>
		/// Returns a random interest rate (<see cref="Decimal"/>) between 0 and 10.
		/// </summary>
		/// <returns></returns>
		public static decimal InterestRate()
		{
			return (decimal)DoubleBetween( 0, 10 );
		}


		// TODO: Add CurrencyAmountBetween( decimal minAmount, decimal maxAmount )


		/// <summary>
		/// Returns a random <see cref="Boolean"/> value.
		/// </summary>
		/// <returns></returns>
		public static bool Boolean()
		{
			return ItemFrom( true, false );
		}


		/// <summary>
		/// Returns a <see cref="string"/> containing a random email address.<br/>
		/// It is (10 random letters)@(10 random letters).(com, org, net, etc.)<br/>
		/// Example: <br/>
		/// <code>"UDHGELEGAP@PTMWZJRKCD.com"</code>
		/// </summary>
		/// <returns></returns>
		public static string Email()
		{
			return string.Format( "{0}@{1}",
			                      String( 10 ),
			                      InternetHostName() );
		}


		/// <summary>
		/// Returns a <see cref="string"/> containing a random http url.<br/>
		/// It is of the form: http://(random hostname)/(1-5 part lowercase url path segments with no trailing slash)
		/// Example:"http://erjl.fiehoisjn.cc/sjike/ffe/serwao"
		/// </summary>
		/// <returns></returns>
		public static string HttpUrl()
		{
			return string.Format( "http://{0}/{1}",
			                      InternetHostName(),
			                      UrlPathSegments() );
		}


		static string UrlPathSegments()
		{
			var numSegments = IntBetween( 1, 5 );

			var pathSegments = new string[numSegments];
			for ( int i = 0; i < numSegments; i++ )
				pathSegments[i] = String( IntBetween( 4, 10 ) ).ToLower();
			
			return string.Join( "/", pathSegments );
		}


		/// <summary>
		/// Returns a <see cref="string"/> containing a randome internet host name.<br/>
		/// It is of the form (3-4 random letters).(3-10 random letters).(com, org, net, etc...)<br/>
		/// Example:<br/>
		/// <code>"axd.wjdfard.net"</code>
		/// </summary>
		/// <returns></returns>
		public static string InternetHostName()
		{
			return string.Format( "{0}.{1}.{2}",
			                      ItemFrom( String( 5 ), "www", String( 4 ) ),
			                      String( IntBetween( 3, 10 ) ),
			                      DomainSuffix()
					).ToLower();
		}


		/// <summary>
		/// Returns as <see cref="string"/> containing a random internet domain name suffix like com, net, org, etc.
		/// </summary>
		/// <returns></returns>
		public static string DomainSuffix()
		{
			return ItemFrom( "com", "gov", "net", "org", "biz", "cc", "tv" );
		}


		#region Names


		/// <summary>
		/// Returns a random first name.<br/>
		/// Examples: <code>"Bob", "Judy", "Josephina"</code>
		/// </summary>
		/// <returns></returns>
		public static string FirstName()
		{
			return ItemFrom( RandomData.FirstNames );
		}


		/// <summary>
		/// Returns a random last name.<br/>
		/// Examples: <code>"Smith", "Olson", "Naranja"</code>
		/// </summary>
		/// <returns></returns>
		public static string LastName()
		{
			return ItemFrom( RandomData.LastNames );
		}


		/// <summary>
		/// Returns a <see cref="string"/> containing a random full name. It is in the form of first name, followed by a space, then the last name.<br/>
		/// Examples: <code>"Bob Smith", "Judith Werner"</code>
		/// </summary>
		/// <returns></returns>
		public static string FullName()
		{
			return FirstName() + " " + LastName();
		}


		#endregion


		#region Locations and Addresses


		public static string AddressLine1()
		{
			return string.Format( "{0} {1}",
			                      IntBetween( 100, 9999 ),
			                      StreetName() );
		}

		public static string StreetName()
		{
			return ItemFrom( RandomData.Streets );
		}


		/// <summary>
		/// Returns the full name of a random US State.<br/>
		/// Examples: <code>"Texas", "Ohio", "Alaska"</code>
		/// </summary>
		/// <returns></returns>
		public static string StateName()
		{
			return ItemFrom( RandomData.States );
		}


		/// <summary>
		/// Returns a random 2-letter state code.<br/>
		/// Examples: <code>"TX", "CA", "WA"</code>
		/// </summary>
		/// <returns></returns>
		public static string StateCode()
		{
			return ItemFrom( RandomData.StateCodes );
		}


		/// <summary>
		/// Returns a random city name.<br/>
		/// Examples: <code>"Sacramento", "New York", "Austin"</code>
		/// </summary>
		/// <returns></returns>
		public static string City()
		{
			return ItemFrom( RandomData.Cities );
		}


		/// <summary>
		/// Returns a random 5-digit zip code.<br/>
		/// Example: <code>"78234"</code>
		/// </summary>
		/// <returns></returns>
		public static string ZipCode()
		{
			return StringPattern( "99999" );
		}


		#endregion


		#region Ages and Birth Dates


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
		/// Returns a random BirthDate (<see cref="DateTime"/>) for a person aged anywhere between 5 and 75 years old.<br/>
		/// Note: The returned value is a Date and does not include time information.
		/// </summary>
		/// <returns></returns>
		public static DateTime BirthDate()
		{
			return DateTimeBetween( 75.YearsAgo(), 5.YearsAgo() ).Date;
		}


		/// <summary>
		/// Generates a random birthdate  for a person of the specified <paramref name="age"/>.
		/// </summary>
		/// <param name="age">The age. (between 1 and 1000</param>
		/// <returns></returns>
		public static DateTime BirthDateForAge( int age )
		{
			if ( age < 1 || age > 1000 )
				throw new ArgumentOutOfRangeException( "age", "Age is out of range. Should be between 1 and 1000 (yeah, 1000 is large, but age of  building?)" );

			var latestPossibleBirthday = age.YearsAgo().Date;
			var earliestPossibleBirthday = ( age + 1 ).YearsAgo().Date.AddDays( 1 ); // Without this extra day, would be too old.
			return DateBetween( earliestPossibleBirthday, latestPossibleBirthday );
		}


		#endregion


		private static bool DateTimeRangeDoesNotCrossDateBoundary( DateTime startDate, DateTime endDate )
		{
			var bothOnSameDay = startDate.Date == endDate.Date;
			var neitherAreMidnight = startDate != startDate.Date && endDate != endDate.Date;
			return bothOnSameDay && neitherAreMidnight;
		}
	}
}