using Fluency.Utils;
using FluentObjectBuilder;
using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.FluentBuilders;


namespace SampleApplication.Tests.Tests.Domain.LineItemTests
{
	[ TestFixture ]
	public class When_calculating_the_amount_of_a_line_item_with_quantity_of_one
	{
		[ Test ]
		public void Should_be_equal_to_the_unit_price_of_the_item()
		{
			LineItem lineItem = a.LineItem.WithQuantity( 1 ).And.UnitPriceOf( 5.dollars() )
					.build();

			lineItem.Amount.should_be_equal_to( 5.dollars() );
		}
	}
}