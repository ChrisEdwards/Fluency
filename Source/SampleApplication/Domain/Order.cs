using System.Collections.Generic;


namespace SampleApplication.Domain
{
	public class Order
	{
		private IList< LineItem > _lineItems = new List< LineItem >();

		public virtual int Id { get; set; }
		public virtual Customer Customer { get; set; }

		public virtual IList< LineItem > LineItems
		{
			get { return _lineItems; }
			set { _lineItems = value; }
		}
	}
}