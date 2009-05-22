
using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.TestDataBuilders;


namespace SampleApplication.Tests.Tests.TestDataBuilders
{
	[ TestFixture ]
	public class EmployeeBuilderTests : TestDataBuilderTestsBase
	{
		[ Test ]
		public void Build_creates_anonymous_Employee()
		{
			Customer customer = new CustomerBuilder().build();

			Assert.IsNotNull( customer );
			Assert.IsNotNullOrEmpty( customer.FirstName );
			Assert.IsNotNullOrEmpty( customer.LastName );
		}
	}
}