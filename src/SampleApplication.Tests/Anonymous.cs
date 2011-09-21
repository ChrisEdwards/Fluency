// Copyright 2011 Chris Edwards
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System.Collections.Generic;
using Fluency.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests
{
	public class Anonymous
	{
		int _uniqueId = -1;


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
			            			LineItems = LineItems( 3 )
			            	};
			return order;
		}


		public Order Order_ForCustomer( Customer customer )
		{
			Order order = Order();
			order.Customer = customer;
			return order;
		}


		public IList< LineItem > LineItems( int howMany )
		{
			var result = new List< LineItem >();
			for ( int i = 0; i < howMany; i++ )
				result.Add( LineItem() );
			return result;
		}


		public LineItem LineItem()
		{
			return new LineItem
			       	{
			       			Id = GetUniqueId(),
			       			Order = Order(),
			       			Product = Product(),
			       			Quantity = ARandom.IntBetween( 1, 10 ),
			       			UnitPrice = ARandom.DoubleBetween( 1, 100 )
			       	};
		}


		public LineItem LineItem_ForOrder( Order order )
		{
			LineItem lineItem = LineItem();
			lineItem.Order = order;
			return lineItem;
		}


		public LineItem LineItem_ForOrder( Order order, int quantity, double price )
		{
			LineItem lineItem = LineItem_ForOrder( order );
			lineItem.Quantity = quantity;
			lineItem.UnitPrice = price;
			return lineItem;
		}


		public LineItem LineItem_ForOrderAndProduct( Order order, Product product )
		{
			LineItem lineItem = LineItem_ForOrder( order );
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