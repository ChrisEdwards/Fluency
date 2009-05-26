using SampleApplication.Domain;


namespace SampleApplication.DiscountCalculation
{
	public interface ITieredDiscountStrategyBuilder_WhereOrBuild : ITieredDiscountStrategyBuilder_Where
	{
		IDiscountStrategy Build();
	}
}