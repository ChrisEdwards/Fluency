using System;
using System.Collections.Generic;
using System.Linq;


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

		public virtual double TotalAmount
		{
			get { return LineItems.Sum( x => x.Amount ); }
		}
	}
}