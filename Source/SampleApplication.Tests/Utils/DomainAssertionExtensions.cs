using FluentObjectBuilder;
using SampleApplication.Domain;
using SharpTestsEx;


namespace SampleApplication.Tests.Utils
{
	public static class DomainAssertionExtensions
	{
		public static void should_be_equal_to( this Order actual, Order expected )
		{
			actual.Id.Should().Be.EqualTo( expected.Id );
			actual.LineItems.should_have_same_item_count_as( expected.LineItems );
		}


		public static void should_be_equal_to( this Customer actual, Customer expected )
		{
			actual.Id.Should().Be.EqualTo( expected.Id );
			actual.FirstName.Should().Be.EqualTo( expected.FirstName );
			actual.LastName.Should().Be.EqualTo( expected.LastName );
		}
	}
}