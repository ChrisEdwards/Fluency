using Fluency.Utils;
using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Domain.DiscountCalculation;
using SampleApplication.Tests.FluentBuilders;
using SharpTestsEx;


namespace SampleApplication.Tests.UnitTests.Domain.DiscountCalculation.DiscountCalculatorTests
{
	[ TestFixture ]
	public class When_calculating_a_tiered_discount : AutoRollbackDatabaseTest
	{
		DiscountCalculator _calculator;


		protected override void TestSetUp()
		{
			IDiscountStrategy tieredDiscountStrategy = DiscountStrategyBuilder.BuildTieredStrategy()
					.Where.OrdersGreaterThanOrEqualTo( 100.dollars() ).GetDiscountOf( 10.Percent() )
					.Build();

			_calculator = new DiscountCalculator( tieredDiscountStrategy );
		}


		[ Test ]
		public void For_an_order_less_than_100_dollars()
		{
			Order order = an.Order
					.With( a.LineItem
					       		.Costing( 100.dollars() ).build()
					)
					.build();

			_db.Add( order ).Persist();

			double discount = _calculator.CalculateDiscount( order );

			discount.Should().Be.EqualTo( 10.dollars() );
		}
	}
}