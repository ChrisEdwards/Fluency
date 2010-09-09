// ReSharper disable InconsistentNaming


using System;
using Machine.Specifications;


namespace Shiloh.DataGeneration.Tests
{
	internal class AnonymousDateTests
	{
		[ Subject( typeof ( AnonymousDate ) ) ]
		public class When_getting_an_anonymous_date
		{
			static DateTime _value;
			Because of = () => _value = Anonymous.Date;

			It should_not_be_a_null_value = () => _value.ShouldNotBeNull();
		}
	}
}


// ReSharper restore InconsistentNaming