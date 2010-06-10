using FluentObjectBuilder;
using SampleApplication.Domain;
using Should;


namespace SampleApplication.Tests.Utils
{
	public static class DomainAssertionExtensions
	{
		public static void should_be_equal_to( this Order actual, Order expected )
		{
			actual.Id.ShouldEqual( expected.Id );
			actual.LineItems.should_have_same_item_count_as( expected.LineItems );
		}


		public static void should_be_equal_to( this Customer actual, Customer expected )
		{
			actual.Id.ShouldEqual( expected.Id );
			actual.FirstName.ShouldEqual( expected.FirstName );
			actual.LastName.ShouldEqual( expected.LastName );
		}
	}
}