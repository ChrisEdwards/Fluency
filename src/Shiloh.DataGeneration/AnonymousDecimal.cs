using System;


namespace Shiloh.DataGeneration
{
	public class AnonymousDecimal: AnonymousNumericBase<decimal >
	{

		protected override decimal GetRandomValue()
		{
			return Between( Decimal.Zero, Decimal.MaxValue );
		}


		protected override decimal GetBetween( decimal lowerBound, decimal upperBound )
		{
			return Random.NextDecimal(lowerBound, upperBound);
		}
	}
}