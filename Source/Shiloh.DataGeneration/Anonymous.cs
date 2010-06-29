using System;
using System.Text;


namespace Shiloh.DataGeneration
{
	public static class Anonymous
	{
		static readonly Random _random = new Random();

		public static AnonymousInteger Integer
		{
			get { return new AnonymousInteger(); }
		}

		public static AnonymousInteger Int
		{
			get { return new AnonymousInteger(); }
		}

		public static AnonymousValue Value
		{
			get { return new AnonymousValue(); }
		}


		public static string FirstName()
		{
			return Value.From( RandomData.FirstNames );
		}


		public static string LastName()
		{
			return Value.From( RandomData.LastNames );
		}


		public static string FullName()
		{
			return FirstName() + " " + LastName();
		}


		public static string StateName()
		{
			return Value.From( RandomData.States );
		}


		public static string StateCode()
		{
			return Value.From( RandomData.StateCodes );
		}


		public static string City()
		{
			return Value.From( RandomData.Cities );
		}


		public static string String()
		{
			return String( 10 );
		}


		public static string String( int size )
		{
			var builder = new StringBuilder();
			for ( int i = 0; i < size; i++ )
					//26 letters in the alfabet, ascii + 65 for the capital letters
				builder.Append( Convert.ToChar( Convert.ToInt32( Math.Floor( 26 * _random.NextDouble() + 65 ) ) ) );
			return builder.ToString();
		}


		public static string Email()
		{
			return System.String.Format( "{0}@{1}.{2}",
			                             // user name
			                             String( 10 ),
			                             // domain
			                             String( 10 ),
			                             Value.From( "com", "org", "net" ) ); // domain ext.
		}
	}
}