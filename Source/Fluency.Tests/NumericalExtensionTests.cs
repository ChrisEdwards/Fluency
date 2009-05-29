using Fluency.Utils;
using FluentObjectBuilder;
using NUnit.Framework;


namespace Fluency.Tests
{
	[ TestFixture ]
	public class NumericalExtensionTests
	{
		[ Test ]
		public void Ten_cents_should_convert_to_a_double_with_value_of_zero_point_one_zero()
		{
			10.cents().should_be_equal_to( 0.10 );
		}


		[ Test ]
		public void Thirty_dollars_should_convert_to_a_double_with_value_of_thirty()
		{
			30.dollars().should_be_equal_to( 30.0 );
		}


		[ Test ]
		public void Twenty_percent_should_convert_to_a_double_with_value_zero_point_two_zero()
		{
			20.percent().should_be_equal_to( 0.20 );
		}
	}
}