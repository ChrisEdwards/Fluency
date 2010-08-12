using System;
using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.FluentBuilders;
using SharpTestsEx;


namespace SampleApplication.Tests.UnitTests.NHibernate
{
	[ TestFixture ]
	public class When_getting_an_existing_LineItem_from_the_database : AutoRollbackDatabaseTest
	{
		LineItem _expectedLineItem;
		LineItem _actualLineItem;


		protected override void TestSetUp()
		{
			_expectedLineItem = a.LineItem.build();

			_db.Add( _expectedLineItem )
					.Persist();

			_actualLineItem = _session.Get< LineItem >( _expectedLineItem.Id );
		}


		[ Test ]
		public void Should_retrieve_quantity()
		{
			DateTime x = _actualLineItem.Order.OrderDate;
			_actualLineItem.Quantity.Should().Be.EqualTo( _expectedLineItem.Quantity );
		}


		[ Test ]
		public void Should_retrieve_unit_price()
		{
			_actualLineItem.UnitPrice.Should().Be.EqualTo( _expectedLineItem.UnitPrice );
		}
	}
}