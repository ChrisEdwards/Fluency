using SampleApplication.Domain;


namespace SampleApplication.Tests.Utils
{
	public static class DomainAssertionExtensions
	{

		public static void should_be_equal_to(this Project actual, Project expected)
		{
			actual.Id.should_be_equal_to(expected.Id);
			actual.Name.should_be_equal_to(expected.Name);
		}

		public static void should_be_equal_to(this Employee actual, Employee expected)
		{
			actual.Id.should_be_equal_to(expected.Id);
			actual.FirstName.should_be_equal_to(expected.FirstName);
			actual.LastName.should_be_equal_to(expected.LastName);
		}
	}
}