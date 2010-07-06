using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.FluentBuilders;


namespace SampleApplication.Tests.IntegrationTests
{
	[ TestFixture ]
	public class FluentBuilderFixtureTest : AutoRollbackDatabaseTest
	{
		protected override void TestSetUp()
		{
			Order order =
					an.Order
							.With( a.LineItem.Costing( 1.60 ) )
							.With( a.LineItem.WithQuantity( 10 ).Costing( 25.99 ) )
							.build();
		}


		[ Test ]
		public void SomeTest()
		{
			// Test with Db.
		}
	}
}