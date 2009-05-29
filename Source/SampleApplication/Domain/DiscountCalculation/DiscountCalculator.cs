namespace SampleApplication.Domain.DiscountCalculation
{
	public class DiscountCalculator
	{
		private readonly IDiscountStrategy _discountStrategy;


		public DiscountCalculator( IDiscountStrategy discountStrategy )
		{
			_discountStrategy = discountStrategy;
		}


		public double CalculateDiscount( Order order )
		{
			// TODO: order.TotalAmount
			// TODO: discountStrategy.GetDiscount
			return _discountStrategy.GetDiscount( order.TotalAmount ) * order.TotalAmount;
		}
	}
}