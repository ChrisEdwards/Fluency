using System.Collections.Generic;
using Fluency.Utils;
using NHibernate;
using SampleApplication.Domain;


namespace SampleApplication.Tests
{
	public class TestDatabase
	{
		readonly IList< Customer > _customers = new List< Customer >();
		readonly DbHelper _dbHelper;
		readonly IList< LineItem > _lineItems = new List< LineItem >();
		readonly IList< Order > _orders = new List< Order >();
		readonly IList< Product > _products = new List< Product >();
		readonly ISession _session;


		public TestDatabase( ISession session )
		{
			_session = session;
			_dbHelper = new DbHelper( session );
		}


		public TestDatabase Then
		{
			get { return this; }
		}


		public void Persist()
		{
			// Insert data in order of dependency.
			foreach ( Customer customer in _customers )
				_dbHelper.Insert( customer );

			foreach ( Product product in _products )
				_dbHelper.Insert( product );

			foreach ( Order order in _orders )
				_dbHelper.Insert( order );

			foreach ( LineItem lineItem in _lineItems )
				_dbHelper.Insert( lineItem );
		}


		public TestDatabase Add( Order order )
		{
			// Exit if null or if this has already been added.
			if ( _orders.AddIfUnique( order, x => x.Id == order.Id ) )
			{
				// Add parents.
				Add( order.Customer );

				// Add children.
				Add( order.LineItems );
			}
			return this;
		}


		public TestDatabase Add( IList< LineItem > lineItems )
		{
			foreach ( LineItem lineItem in lineItems )
				Add( lineItem );
			return this;
		}


		public TestDatabase Add( LineItem lineItem )
		{
			// Exit if null or if this has already been added.
			if ( _lineItems.AddIfUnique( lineItem, x => x.Id == lineItem.Id ) )
			{
				// Add parents.
				Add( lineItem.Order );

				// Add children.
				Add( lineItem.Product );
			}
			return this;
		}


		public TestDatabase Add( Product product )
		{
			// Exit if null or if this has already been added.
			if ( _products.AddIfUnique( product, x => x.Id == product.Id ) )
			{
				// Add parents.
				// Add children.
			}
			return this;
		}


		public TestDatabase Add( Customer customer )
		{
			// Exit if null or if this has already been added.
			if ( _customers.AddIfUnique( customer, x => x.Id == customer.Id ) )
			{
				// Add parents.
				// Add children.
			}
			return this;
		}
	}
}