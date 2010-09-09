using NHibernate;
using SampleApplication.NHibernate.Extensions;


namespace SampleApplication.Tests
{
	public static class TestData
	{
		public static void InsertTestData( ISession session )
		{
			// Insert Customers
			session.ExecuteSql( @"INSERT INTO Customers (Id, FirstName, LastName) VALUES (1, 'Kevin', 'Mitnick')" );
			session.ExecuteSql( @"INSERT INTO Customers (Id, FirstName, LastName) VALUES (2, 'Han', 'Solo')" );
			session.ExecuteSql( @"INSERT INTO Customers (Id, FirstName, LastName) VALUES (3, 'Jimmy', 'Hendrix')" );

			// Insert Products
			session.ExecuteSql( @"INSERT INTO Products (Id, Name, Description) VALUES (1, 'BFG2000', 'A really big gun')" );
			session.ExecuteSql( @"INSERT INTO Products (Id, Name, Description) VALUES (2, 'Motivator', 'Guaranteed to put the pep back in your droid')" );
			session.ExecuteSql( @"INSERT INTO Products (Id, Name, Description) VALUES (3, '1541 Floppy Drive', 'Load with ,8,')" );
			session.ExecuteSql( @"INSERT INTO Products (Id, Name, Description) VALUES (4, 'Microsoft BOB', 'The word processor of the future!')" );

			// Order 1
			session.ExecuteSql( @"INSERT INTO Orders (Id, CustomerId, OrderDate) VALUES (1, 1, '10-20-1980')" );
			session.ExecuteSql( @"INSERT INTO LineItems (Id, OrderId, ProductId, Quantity, UnitPrice) VALUES (1, 1, 3, 2, 10.00)" );
			session.ExecuteSql( @"INSERT INTO LineItems (Id, OrderId, ProductId, Quantity, UnitPrice) VALUES (2, 1, 4, 1, 75.99)" );

			// Order 2
			session.ExecuteSql( @"INSERT INTO Orders (Id, CustomerId, OrderDate) VALUES (2, 2, '09-15-1978')" );
			session.ExecuteSql( @"INSERT INTO LineItems (Id, OrderId, ProductId, Quantity, UnitPrice) VALUES (3, 2, 2, 5, 10342.00)" );

			// Order 3
			session.ExecuteSql( @"INSERT INTO Orders (Id, CustomerId, OrderDate) VALUES (3, 3, '1-2-1968')" );
			session.ExecuteSql( @"INSERT INTO LineItems (Id, OrderId, ProductId, Quantity, UnitPrice) VALUES (4, 1, 1, 1, 10000.00)" );
		}
	}
}