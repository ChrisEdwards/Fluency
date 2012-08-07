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

using System;
using NUnit.Framework;
using SampleApplication.Domain;
using SharpTestsEx;


namespace SampleApplication.Tests.IntegrationTests
{
	[ TestFixture ]
	public class ManualObjectCreationTests
	{
		[ Test ]
		[ Ignore( "This is a sample...not meant to run" ) ]
		public void OrderTotalShouldBeTheSumOfTheLineItems()
		{
			// Arrange.
			var customer = new Customer
			               	{
			               			FirstName = "Bob",
			               			LastName = "Smith"
			               	};

			var order = new Order
			            	{
			            			Customer = customer,
			            			OrderDate = DateTime.Now
			            	};

			var lineItem1 = new LineItem
			                	{
			                			Order = order,
			                			Product = new Product
			                			          	{
			                			          			Name = "Product1",
			                			          			Description = "Product1 Description"
			                			          	},
			                			Quantity = 1,
			                			UnitPrice = 1.60
			                	};

			var lineItem2 = new LineItem
			                	{
			                			Order = order,
			                			Product = new Product
			                			          	{
			                			          			Name = "Product2",
			                			          			Description = "Product2 Description"
			                			          	},
			                			Quantity = 10,
			                			UnitPrice = 25.99
			                	};

			order.LineItems.Add( lineItem1 );
			order.LineItems.Add( lineItem2 );


			// Act.
			var actualTotal = order.TotalAmount;


			// Assert.
			const double expectedTotal = ( 1 * 1.60 ) + ( 10 * 25.99 );
			actualTotal.Should().Be.EqualTo( expectedTotal );
		}
	}
}