using FluentObjectBuilder;
using SampleApplication.Domain;


namespace SampleApplication.Tests.Utils
{
	public static class DomainAssertionExtensions
	{
		public static void should_be_equal_to( this Order actual, Order expected )
		{
			actual.Id.should_be_equal_to( expected.Id );
			actual.LineItems.should_have_same_item_count_as( expected.LineItems );
		}


		public static void should_be_equal_to( this Customer actual, Customer expected )
		{
			actual.Id.should_be_equal_to( expected.Id );
			actual.FirstName.should_be_equal_to( expected.FirstName );
			actual.LastName.should_be_equal_to( expected.LastName );
		}
	}
}