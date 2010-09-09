using NUnit.Framework;


namespace SampleApplication.Tests.IntegrationTests
{
	[ TestFixture ]
	public class BigFrigginInsertTest : AutoRollbackDatabaseTest
	{
		protected override void TestSetUp()
		{
			TestData.InsertTestData( _session );
		}


		[ Test ]
		[ Ignore( "This is a sample...not meant to run" ) ]
		public void TestSomething()
		{
			// Test using the db.
		}
	}
}