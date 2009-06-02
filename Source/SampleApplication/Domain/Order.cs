using System;
using System.Collections.Generic;
using System.Linq;


namespace SampleApplication.Domain
{
	public class Order
	{
		private IList< LineItem > _lineItems = new List< LineItem >();
		private Customer _customer;

		public virtual int Id { get; set; }
		public virtual Customer Customer
		{
			get { return _customer; }
			set { _customer = value; }
		}

		public virtual DateTime OrderDate { get; set; }

		public virtual IList< LineItem > LineItems
		{
			get { return _lineItems; }
			set
			{
				LineItems.Clear();
				foreach (LineItem lineItem in value)
					Add( lineItem );
			}
		}


		private void Add( LineItem lineItem )
		{
			LineItems.Add( lineItem );
			lineItem.Order = this;
		}


		public virtual double TotalAmount
		{
			get { return LineItems.Sum( x => x.Amount ); }
		}
	}
}