using System.Collections.Generic;
using System.Linq;


namespace SampleApplication.Domain.DiscountCalculation
{
	public class TieredDiscountStrategy : IDiscountStrategy
	{
		readonly IList< DiscountTier > _discountTiers;


		public TieredDiscountStrategy( IList< DiscountTier > discountTiers )
		{
			_discountTiers = discountTiers;
		}


		#region IDiscountStrategy Members

		public double GetDiscount( double totalAmount )
		{
			foreach ( DiscountTier discountTier in _discountTiers.OrderBy( x => x.LowestQualifyingAmount ) )
			{
				if ( totalAmount >= discountTier.LowestQualifyingAmount )
					return discountTier.DiscountPercentage;
			}
			return 0.0;
		}

		#endregion
	}
}