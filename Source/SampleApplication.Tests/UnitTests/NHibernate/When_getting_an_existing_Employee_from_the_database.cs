using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.FluentBuilders;


namespace SampleApplication.Tests.Tests.NHibernate
{
	[ TestFixture ]
	public class When_getting_an_existing_Customer_from_the_database : AutoRollbackDatabaseTest
	{
		private Customer _expectedCustomer;
		private Customer _actualCustomer;


		protected override void TestSetUp()
		{
			_expectedCustomer = a.Customer.build();

			_db.Add( _expectedCustomer )
					.Persist();

			_actualCustomer = _session.Get< Customer >( _expectedCustomer.Id );
		}


		[ Test ]
		public void Should_retrieve_first_name()
		{
			Assert.That( _actualCustomer.FirstName, Is.EqualTo( _expectedCustomer.FirstName ) );
		}


		[ Test ]
		public void Should_retrieve_last_name()
		{
			Assert.That( _actualCustomer.LastName, Is.EqualTo( _expectedCustomer.LastName ) );
		}
	}
}