// ReSharper disable InconsistentNaming


using Machine.Specifications;


namespace Shiloh.DataGeneration.Tests
{
	public class AnonymousAddressSpecs
	{
		[ Subject( typeof ( AnonymousAddress ) ) ]
		public class When_getting_an_anonymous_street_name
		{
			static string result;

			Because of = () =>  result = Anonymous.Address.StreetName(); 

			It should_return_a_street_name = () => result.ShouldNotBeNull();
		}
	}
}


// ReSharper restore InconsistentNaming