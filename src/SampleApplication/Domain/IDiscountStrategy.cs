namespace SampleApplication.Domain
{
	public interface IDiscountStrategy
	{
		double GetDiscount( double totalAmount );
	}
}