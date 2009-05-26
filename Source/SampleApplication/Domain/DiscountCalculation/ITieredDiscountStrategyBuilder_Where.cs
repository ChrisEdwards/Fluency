namespace SampleApplication.DiscountCalculation
{
	public interface ITieredDiscountStrategyBuilder_Where
	{
		ITieredDiscountStrategyBuilder_Then OrdersGreaterThanOrEqualTo( double amount );
	}
}