using SampleApplication.Domain;


namespace SampleApplication.DiscountCalculation
{
	public interface ITieredDiscountStrategyBuilder_Then
	{
		ITieredDiscountStrategyBuilder_WhereOrBuild GetDiscountOf( double percent );
	}
}