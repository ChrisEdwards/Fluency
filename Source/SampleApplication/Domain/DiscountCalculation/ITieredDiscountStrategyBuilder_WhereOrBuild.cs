namespace SampleApplication.Domain.DiscountCalculation
{
	public interface ITieredDiscountStrategyBuilder_WhereOrBuild : ITieredDiscountStrategyBuilder_Where
	{
		IDiscountStrategy Build();
	}
}