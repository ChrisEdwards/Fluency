using System;


namespace Shiloh.DataGeneration
{
	public class AnonymousDouble : AnonymousNumericBase< double >
	{
		protected override double GetRandomValue()
		{
			return Random.NextDouble() * Random.Next();
		}

		protected override double GetBetween( double lowerBound, double upperBound )
		{
			double range = upperBound - lowerBound;
			return lowerBound + (range * Random.NextDouble());
		}
	}
}