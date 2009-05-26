using BancVue.Tests.Common;
using FluentObjectBuilder;
using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.TestDataBuilders;


namespace SampleApplication.Tests.Tests.NHibernate
{
	[ TestFixture ]
	public class When_getting_an_existing_LineItem_from_the_database : AutoRollbackDatabaseTest
	{
		private LineItem _expectedLineItem;
		private LineItem _actualLineItem;

		protected override void TestSetUp()
		{
			_expectedLineItem = a.LineItem.build();

			_db.Add(_expectedLineItem)
				.Persist();

			_actualLineItem = _session.Get<LineItem>(_expectedLineItem.Id);
		}
		 
		[ Test ]
		public void Should_retrieve_quantity()
		{
			_actualLineItem.Quantity.should_be_equal_to( _expectedLineItem.Quantity );
		}
		 
		[ Test ]
		public void Should_retrieve_unit_price()
		{
			_actualLineItem.UnitPrice.should_be_equal_to( _expectedLineItem.UnitPrice );
		}

	}
}