using Fluency.Utils;
using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.FluentBuilders;
using SharpTestsEx;
using Shiloh.Utils;


namespace SampleApplication.Tests.UnitTests.Domain.OrderTests
{
	[ TestFixture ]
	public class When_getting_the_total_order_amount
	{
		[ Test ]
		public void For_a_single_line_item_Should_return_the_amount_for_that_line_item()
		{
			Order order = an.Order.With( a.LineItem.Costing( 10.dollars() ) )
					.build();

			order.TotalAmount.Should().Be.EqualTo( 10.dollars() );
		}
	}
}