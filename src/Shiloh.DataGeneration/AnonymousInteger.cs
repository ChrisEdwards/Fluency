using System.Collections.Generic;


namespace Shiloh.DataGeneration
{
	public class AnonymousInteger : AnonymousNumericBase< int >
	{
//		public int GreaterThan( int lowerBound )
//		{
//			return Random.Next( lowerBound + 1, int.MaxValue );
//		}


//		public int GreaterThanOrEqualTo( int lowerBound )
//		{
//			return Random.Next( lowerBound, int.MaxValue );
//		}


//		public int LessThanOrEqualTo( int upperBound )
//		{
//			return Random.Next( int.MinValue, upperBound );
//		}


//		public int LessThan( int upperBound )
//		{
//			return Random.Next( int.MinValue, upperBound - 1 );
//		}


		public int InRange(int lowerBound, int upperBound)
		{
			return BetweenExclusive(lowerBound, upperBound);
		}


		protected override int GetBetween( int lowerBound, int upperBound )
		{
			return Random.Next( lowerBound, upperBound );
		}


		protected override int GetRandomValue()
		{
			return Random.Next( int.MinValue, int.MaxValue );
		}
	}
}