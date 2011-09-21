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
using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.FluentBuilders;


namespace SampleApplication.Tests.IntegrationTests
{
	[ TestFixture ]
	public class FluentBuilderFixtureTest
	{
		Order _order;


		[ SetUp ]
		protected void TestSetUp()
		{
			_order = an.Order
					.With( a.LineItem.Costing( 1.60 ) )
					.With( a.LineItem.WithQuantity( 10 ).Costing( 25.99 ) )
					.build();
		}


		[ Test ]
		public void SomeTest()
		{
			Assert.That( _order.Customer, Is.Not.Null );
			Assert.That( _order.LineItems[0].Product, Is.Not.Null );
			Assert.That( _order.OrderDate, Is.Not.Null );
			// And so on....
		}
	}
}