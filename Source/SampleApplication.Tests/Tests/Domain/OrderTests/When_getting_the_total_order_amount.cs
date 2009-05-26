using FluentObjectBuilder;
using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.TestDataBuilders;


namespace SampleApplication.Tests.Tests.Domain.OrderTests
{
	[ TestFixture ]
	public class When_getting_the_total_order_amount
	{
		[ Test ]
		public void For_a_single_line_item_Should_return_the_amount_for_that_line_item()
		{
			Order order = an.Order.With( a.LineItem.Costing( 10.dollars() ) )
					.build();

			order.TotalAmount.should_be_equal_to( 10.dollars() );
		}
	}
}