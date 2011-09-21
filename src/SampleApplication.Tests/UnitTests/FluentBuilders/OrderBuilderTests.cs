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
using SharpTestsEx;

// ReSharper disable InconsistentNaming


namespace SampleApplication.Tests.UnitTests.FluentBuilders
{
	[ TestFixture ]
	public class Adding_a_LineItem
	{
		#region Setup/Teardown

		[ SetUp ]
		public void SetUp()
		{
			_order = an.Order
					.With( _lineItem = a.LineItem.build() )
					.build();
		}

		#endregion


		LineItem _lineItem;
		Order _order;


		[ Test ]
		public void Should_build_the_LineItem_with_reference_to_the_Order()
		{
			_lineItem.Order.Should().Be.SameInstanceAs( _order );
		}


		[ Test ]
		public void Should_build_the_Order_containing_new_LineItem()
		{
			_order.LineItems.Should().Contain( _lineItem );
		}
	}
}


// ReSharper restore InconsistentNaming