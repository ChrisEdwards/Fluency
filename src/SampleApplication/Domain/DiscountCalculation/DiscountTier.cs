namespace SampleApplication.Domain.DiscountCalculation
{
	public class DiscountTier
	{
		/// <summary>
		/// Gets or sets the lowest amount to qualify for the discount.
		/// </summary>
		/// <value>The lowest amount to qualify for the discount.</value>
		public double LowestQualifyingAmount { get; set; }

		public double DiscountPercentage { get; set; }
	}
}