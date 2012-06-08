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
using Fluency.Utils;
using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.FluentBuilders;
using SharpTestsEx;


namespace SampleApplication.Tests.UnitTests.Domain.LineItemTests
{
	[ TestFixture ]
	public class When_calculating_the_amount_of_a_line_item_with_quantity_of_one
	{
		[ Test ]
		public void Should_be_equal_to_the_unit_price_of_the_item()
		{
			LineItem lineItem = a.LineItem.WithQuantity( 1 ).And.UnitPriceOf( 5.dollars() )
					.build();

			lineItem.Amount.Should().Be.EqualTo( 5.dollars() );
		}
	}
}