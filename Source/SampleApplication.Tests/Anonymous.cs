using System;
using System.Collections.Generic;
using Fluency.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests
{
	public class Anonymous
	{
		private int _uniqueId = -1;


		public int GetUniqueId()
		{
			return _uniqueId--;
		}


		public Order Order()
		{
			var order = new Order
			            	{
			            			Id = GetUniqueId(),
			            			Customer = Customer(),
									OrderDate = ARandom.DateTime(),
									LineItems = LineItems(3)
			            	};
			return order;
		}

		public Order Order_ForCustomer(Customer customer)
		{
			var order = Order();
			order.Customer = customer;
			return order;
		}


		public IList< LineItem > LineItems( int howMany )
		{
			var result = new List< LineItem >();
			for ( int i = 0; i < howMany; i++ )
			{
				result.Add( LineItem() );
			}
			return result;
		}


		public LineItem LineItem()
		{
			return new LineItem
			       	{
			       				Id = GetUniqueId(),
								Order = Order(),
								Product = Product(),
								Quantity = ARandom.IntBetween( 1,10 ),
								UnitPrice = ARandom.DoubleBetween( 1,100 )
			       	};
		}


		public LineItem LineItem_ForOrder( Order order )
		{
			var lineItem = LineItem();
			lineItem.Order = order;
			return lineItem;
		}

		public LineItem LineItem_ForOrder( Order order , int quantity, double price)
		{
			var lineItem = LineItem_ForOrder(order);
			lineItem.Quantity = quantity;
			lineItem.UnitPrice = price;
			return lineItem;
		}


		public LineItem LineItem_ForOrderAndProduct( Order order, Product product )
		{
			var lineItem = LineItem_ForOrder( order );
			lineItem.Product = product;
			return lineItem;
		}


		public Product Product()
		{
			return new Product
			       	{
			       			Id = GetUniqueId(),
							Name = ARandom.Title( 30 ),
							Description = ARandom.Text( 200 )
			       	};
		}


		public Customer Customer()
		{
			var customer = new Customer
			               	{
			               			Id = GetUniqueId(),
			               			FirstName = ARandom.FirstName(),
			               			LastName = ARandom.LastName()
			               	};
			return customer;
		}
	}
}