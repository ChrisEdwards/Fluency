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

using Fluency.DataGeneration;
using NUnit.Framework;
using SampleApplication.Tests.FluentBuilders;
using SharpTestsEx;


namespace SampleApplication.Tests.IntegrationTests
{
	[ TestFixture ]
	public class FluentBuilderFixtureTest
	{
		[ Test ]
		public void OrderTotalShouldBeTheSumOfTheLineItems()
		{
			// Arrange.
			var order = an.Order
					.With( a.LineItem.Costing( 1.60 ) )
					.With( a.LineItem.WithQuantity( 10 ).Costing( 25.99 ) )
					.build();
			

			// Act.
			var actualTotal = order.TotalAmount;


			// Assert.
			const double expectedTotal = ( 1 * 1.60 ) + ( 10 * 25.99 );
			actualTotal.Should().Be.EqualTo( expectedTotal );
		}
	}
}