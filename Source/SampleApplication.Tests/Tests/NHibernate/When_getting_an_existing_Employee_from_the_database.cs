using BancVue.Tests.Common;
using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.TestDataBuilders;


namespace SampleApplication.Tests.Tests.NHibernate
{
	[ TestFixture ]
	public class When_getting_an_existing_Employee_from_the_database : AutoRollbackDatabaseTest
	{
		private Employee _expectedEmployee;
		private Employee _actualEmployee;

		protected override void TestSetUp()
		{
			_expectedEmployee = an.Employee.build();

			_db.Add( _expectedEmployee )
				.Persist();

			_actualEmployee = _session.Get< Employee >( _expectedEmployee.Id );
		}

		[ Test ]
		public void Should_retrieve_first_name()
		{
			Assert.That( _actualEmployee.FirstName, Is.EqualTo( _expectedEmployee.FirstName ));
		}

	}
}