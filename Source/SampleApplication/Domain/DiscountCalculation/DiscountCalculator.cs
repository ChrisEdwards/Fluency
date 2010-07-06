namespace SampleApplication.Domain.DiscountCalculation
{
	public class DiscountCalculator
	{
		readonly IDiscountStrategy _discountStrategy;


		public DiscountCalculator( IDiscountStrategy discountStrategy )
		{
			_discountStrategy = discountStrategy;
		}


		public double CalculateDiscount( Order order )
		{
			// TODO: order.TotalAmount
			// TODO: discountStrategy.GetDiscount
			double discount = _discountStrategy.GetDiscount( order.TotalAmount );
			return discount * order.TotalAmount;
		}
	}
}