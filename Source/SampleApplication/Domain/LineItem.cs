using System;


namespace SampleApplication.Domain
{
	public class LineItem
	{
		public virtual int Id { get; set; }
		public virtual Order Order { get; set; }
		public virtual Product Product { get; set; }
		public virtual int Quantity { get; set; }
		public virtual double UnitPrice { get; set; }

		public virtual double Amount
		{
			get { return Quantity * UnitPrice; }
		}
	}
}