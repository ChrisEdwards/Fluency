using NUnit.Framework;
using SampleApplication.NHibernate.Extensions;


namespace SampleApplication.Tests.IntegrationTests
{
	[ TestFixture ]
	public class InlineFixtureTest : AutoRollbackDatabaseTest
	{
		protected override void TestSetUp()
		{
			_session.ExecuteSql( @"INSERT INTO Customers (Id, FirstName, LastName) VALUES (1, 'Bob', 'Smith')" );
			_session.ExecuteSql( @"INSERT INTO Products (Id, Name, Description) VALUES (1, 'Product1', 'Test Product 1 Description')" );
			_session.ExecuteSql( @"INSERT INTO Orders (Id, CustomerId, OrderDate) VALUES (1, 1, '10-20-2008')" );
			_session.ExecuteSql( @"INSERT INTO LineItems (Id, OrderId, ProductId, Quantity, UnitPrice) VALUES (1, 1, 1, 1, 10.00)" );
		}


		[ Test ]
		[ Ignore( "This is a sample...not meant to run" ) ]
		public void TestThis()
		{
			// Test using db.
		}
	}
}