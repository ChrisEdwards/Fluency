namespace SampleApplication.Domain.DiscountCalculation
{
	public interface ITieredDiscountStrategyBuilder_Where
	{
		ITieredDiscountStrategyBuilder_Then OrdersGreaterThanOrEqualTo( double amount );
	}
}