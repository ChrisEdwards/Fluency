using NUnit.Framework;
using SampleApplication.Domain;


namespace SampleApplication.Tests.IntegrationTests
{
	[ TestFixture ]
	public class ObjectMotherTests : AutoRollbackDatabaseTest
	{
		private readonly Anonymous a = new Anonymous();


		protected override void TestSetUp()
		{
			Order order = a.Order();
			LineItem lineItem1 = a.LineItem_ForOrder( order, 1, 1.60 );
			LineItem lineItem2 = a.LineItem_ForOrder( order, 10, 25.99 );
		}


		[ Test ]
		[ Ignore( "This is a sample...not meant to run" ) ]
		public void SomeTest()
		{
			// Test with Db.
		}
	}
}