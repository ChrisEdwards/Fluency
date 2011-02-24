// ReSharper disable InconsistentNaming


using System;
using Machine.Specifications;


namespace Shiloh.DataGeneration.Tests
{
	public class AnonymousDateTests
	{
		[ Subject( typeof ( AnonymousDate ) ) ]
		public class When_getting_an_anonymous_date
		{
			static DateTime _value;
			Because of = () => _value = Anonymous.Date;

			It should_not_be_a_null_value = () => _value.ShouldNotBeNull();
			It should_not_contain_a_time_component = () => _value.ShouldEqual( _value.Date );
		}
	}
}


// ReSharper restore InconsistentNaming