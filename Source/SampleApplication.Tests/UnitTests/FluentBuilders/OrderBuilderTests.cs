using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.FluentBuilders;
using SharpTestsEx;


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