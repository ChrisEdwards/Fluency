using System.Collections.Generic;


namespace SampleApplication.Domain.DiscountCalculation
{
	public class DiscountStrategyBuilder : ITieredDiscountStrategyBuilder_Where,
	                                       ITieredDiscountStrategyBuilder_Then,
	                                       ITieredDiscountStrategyBuilder_WhereOrBuild
	{
		private readonly IList< DiscountTier > discountTiers = new List< DiscountTier >();
		private DiscountTier _tierUnderConstruction;

		public ITieredDiscountStrategyBuilder_Where Where
		{
			get { return this; }
		}


		#region ITieredDiscountStrategyBuilder_Then Members

		public ITieredDiscountStrategyBuilder_WhereOrBuild GetDiscountOf( double percent )
		{
			_tierUnderConstruction.DiscountPercentage = percent;
			discountTiers.Add( _tierUnderConstruction );
			_tierUnderConstruction = null;
			return this;
		}

		#endregion


		#region ITieredDiscountStrategyBuilder_Where Members

		public ITieredDiscountStrategyBuilder_Then OrdersGreaterThanOrEqualTo( double amount )
		{
			_tierUnderConstruction = new DiscountTier
			                         	{
			                         			LowestQualifyingAmount = amount
			                         	};
			return this;
		}

		#endregion


		#region ITieredDiscountStrategyBuilder_WhereOrBuild Members

		public IDiscountStrategy Build()
		{
			return new TieredDiscountStrategy( discountTiers );
		}

		#endregion


		public static DiscountStrategyBuilder BuildTieredStrategy()
		{
			return new DiscountStrategyBuilder();
		}
	}
}