namespace SampleApplication.Domain.DiscountCalculation
{
	public interface ITieredDiscountStrategyBuilder_Then
	{
		ITieredDiscountStrategyBuilder_WhereOrBuild GetDiscountOf( double percent );
	}
}