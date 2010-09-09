using System;
using System.Collections.Generic;


namespace Shiloh.DataGeneration
{
	public class AnonymousInteger : AnonymousBase< int >
	{
		public int GreaterThan( int lowerBound )
		{
			return _random.Next( lowerBound + 1, int.MaxValue );
		}


		public int GreaterThanOrEqualTo( int lowerBound )
		{
			return _random.Next( lowerBound, int.MaxValue );
		}


		public int LessThanOrEqualTo( int upperBound )
		{
			return _random.Next( int.MinValue, upperBound );
		}


		public int LessThan( int upperBound )
		{
			return _random.Next( int.MinValue, upperBound - 1 );
		}


		public int Between( int lowerBound, int upperBound )
		{
			if ( lowerBound > upperBound )
				throw new ArgumentException( "The lower bounds must be greater than the upper bound." );

			return _random.Next( lowerBound, upperBound );
		}


		public int InRange( int lowerBound, int upperBound )
		{
			return BetweenExclusive( lowerBound, upperBound );
		}


		public int BetweenExclusive( int lowerBound, int upperBound )
		{
			if ( lowerBound >= upperBound )
				throw new ArgumentException( "The lower bounds must be greater than the upper bound." );

			return _random.Next( lowerBound + 1, upperBound - 1 );
		}


		public int From( IList< int > list )
		{
			return Anonymous.Value.From( list );
		}


		public int From( params int[] list )
		{
			return Anonymous.Value.From( list );
		}


		protected override int GetRandomValue()
		{
			return GetRandomInteger();
		}


		public int GetRandomInteger()
		{
			return _random.Next( int.MinValue, int.MaxValue );
		}
	}
}