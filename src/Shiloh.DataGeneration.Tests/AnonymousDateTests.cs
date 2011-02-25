// ReSharper disable InconsistentNaming


using System;
using Machine.Specifications;
using SharpTestsEx;
using Shiloh.Utils;


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


		[ Subject( typeof ( AnonymousDate ), "Before(date)" ) ]
		public class When_getting_an_anonymous_date_before_a_specified_date
		{
			static DateTime _priorToDate, _value;

			Establish context = () => _priorToDate = 1.YearsAgo();

			Because of = () => _value = Anonymous.Date.Before( _priorToDate );

			It should_not_be_a_null_value = () => _value.ShouldNotBeNull();
			It should_not_contain_a_time_component = () => _value.ShouldEqual( _value.Date );
			It should_be_prior_to_the_specified_date = () => _value.ShouldBeLessThan( _priorToDate );
		}


		[ Subject( typeof ( AnonymousDate ), "PriorTo(date)" ) ]
		public class When_getting_an_anonymous_date_prior_to_a_specified_date
		{
			static DateTime _priorToDate, _value;

			Establish context = () => _priorToDate = 1.YearsAgo();

			Because of = () => _value = Anonymous.Date.PriorTo( _priorToDate );

			It should_not_be_a_null_value = () => _value.ShouldNotBeNull();
			It should_not_contain_a_time_component = () => _value.ShouldEqual( _value.Date );
			It should_be_prior_to_the_specified_date = () => _value.ShouldBeLessThan( _priorToDate );
		}


		[ Subject( typeof ( AnonymousDate ), "InPast" ) ]
		public class When_getting_an_anonymous_date_in_past
		{
			static DateTime _value;

			Because of = () => _value = Anonymous.Date.InPast();

			It should_not_be_a_null_value = () => _value.ShouldNotBeNull();
			It should_not_contain_a_time_component = () => _value.ShouldEqual( _value.Date );
			It should_be_prior_to_now = () => _value.ShouldBeLessThan( DateTime.Now );
		}


		[ Subject( typeof ( AnonymousDate ), "InPastSince(date)" ) ]
		public class When_getting_an_anonymous_date_in_past_since
		{
			static DateTime _minDate, _value;

			Establish context = () => _minDate = DateTime.Now.AddYears( -1 );

			Because of = () => _value = Anonymous.Date.InPastSince( _minDate );

			It should_not_be_a_null_value = () => _value.ShouldNotBeNull();
			It should_not_contain_a_time_component = () => _value.ShouldEqual( _value.Date );
			It should_be_prior_to_now = () => _value.ShouldBeLessThan( DateTime.Now );
			It should_be_after_to_the_specified_min_date = () => _value.ShouldBeGreaterThan( _minDate );
		}


		[ Subject( typeof ( AnonymousDate ), "InPastSince(date)" ) ]
		public class When_getting_an_anonymous_date_in_past_since_a_date_in_the_future
		{
			static Exception _exception;
			Because of = () => _exception = Catch.Exception( () => Anonymous.Date.InPastSince( DateTime.Now.AddDays( 1 ) ) );
			It should_fail = () => _exception.Should().Be.OfType< ArgumentException >();
		}


		[ Subject( typeof ( AnonymousDate ), "InPastYear" ) ]
		public class When_getting_an_anonymous_date_in_past_year
		{
			static DateTime _oneYearAgo, _value;

			Establish context = () => _oneYearAgo = 1.YearsAgo();

			Because of = () => _value = Anonymous.Date.InPastYear();

			It should_not_be_a_null_value = () => _value.ShouldNotBeNull();
			It should_not_contain_a_time_component = () => _value.ShouldEqual( _value.Date );
			It should_be_prior_to_now = () => _value.ShouldBeLessThan( DateTime.Now );
			It should_be_after_one_year_ago_today = () => _value.ShouldBeGreaterThan( _oneYearAgo );
		}


		[ Subject( typeof ( AnonymousDate ), "After" ) ]
		public class When_getting_an_anonymous_date_after_a_specified_date
		{
			static DateTime _afterDate, _value;

			Establish context = () => _afterDate = 1.YearsAgo();

			Because of = () => _value = Anonymous.Date.After( _afterDate );

			It should_not_be_a_null_value = () => _value.ShouldNotBeNull();
			It should_not_contain_a_time_component = () => _value.ShouldEqual( _value.Date );
			It should_be_after_to_the_specified_date = () => _value.ShouldBeGreaterThan( _afterDate );
		}


		[ Subject( typeof ( AnonymousDate ), "InFuture" ) ]
		public class When_getting_an_anonymous_date_in_the_future
		{
			static DateTime _value;

			Because of = () => _value = Anonymous.Date.InFuture();

			It should_not_be_a_null_value = () => _value.ShouldNotBeNull();
			It should_not_contain_a_time_component = () => _value.ShouldEqual( _value.Date );
			It should_be_after_to_the_specified_date = () => _value.ShouldBeGreaterThan( DateTime.Now );
		}
	}
}


// ReSharper restore InconsistentNaming