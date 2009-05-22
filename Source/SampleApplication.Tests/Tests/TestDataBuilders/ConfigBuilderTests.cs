
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
			Employee employee = new EmployeeBuilder().build();

			Assert.IsNotNull( employee );
			Assert.IsNotNullOrEmpty( employee.FirstName );
			Assert.IsNotNullOrEmpty( employee.LastName );
		}
	}
}