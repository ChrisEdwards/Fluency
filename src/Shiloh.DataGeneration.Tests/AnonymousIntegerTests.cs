// Copyright 2011 Chris Edwards
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System;
using System.Collections.Generic;
using Machine.Specifications;
using SharpTestsEx;

// ReSharper disable InconsistentNaming


namespace Shiloh.DataGeneration.Tests
{
	public class AnonymousIntegerTests
	{
		[ Subject( typeof ( AnonymousInteger ) ) ]
		public class When_getting_an_anonymous_integer
		{
			static int _value;
			Because of = () => _value = Anonymous.Integer;

			It should_not_be_the_default_value_of_zero_ = () => _value.Should().Not.Be.EqualTo( 0 );
			It should_maintain_its_value = () => ( _value ).Should().Be.EqualTo( _value );
		}


		[ Subject( typeof ( AnonymousInteger ), "GreaterThan" ) ]
		public class When_getting_an_anonymous_integer_greater_than_a_specified_value
		{
			static int _value;
			static int _lowerBound;
			Establish context = () => _lowerBound = new Random().Next( int.MinValue, int.MaxValue - 1 );

			Because of = () => _value = Anonymous.Integer.GreaterThan( _lowerBound );

			It should_be_greater_than_the_specified_value = () => _value.Should().Be.GreaterThan( _lowerBound );
		}


		[ Subject( typeof ( AnonymousInteger ), "GreaterThanOrEqualTo" ) ]
		public class When_getting_an_anonymous_integer_greater_than_or_equal_to_a_specified_value
		{
			static int _value;
			static int _lowerBound;
			Establish context = () => _lowerBound = new Random().Next( int.MinValue, int.MaxValue - 1 );

			Because of = () => _value = Anonymous.Integer.GreaterThanOrEqualTo( _lowerBound );

			It should_be_greater_than_the_specified_value = () => _value.Should().Be.GreaterThanOrEqualTo( _lowerBound );
		}


		[ Subject( typeof ( AnonymousInteger ), "LessThan" ) ]
		public class When_getting_an_anonymous_integer_less_than_a_specified_value
		{
			static int _value;
			static int _upperBound;
			Establish context = () => _upperBound = new Random().Next( int.MinValue + 1, int.MaxValue );

			Because of = () => _value = Anonymous.Integer.LessThan( _upperBound );

			It should_be_less_than_or_equal_to_the_specified_value = () => _value.Should().Be.LessThan( _upperBound );
		}


		[ Subject( typeof ( AnonymousInteger ), "LessThanOrEqualTo" ) ]
		public class When_getting_an_anonymous_integer_less_than_or_equal_to_a_specified_value
		{
			static int _value;
			static int _upperBound;
			Establish context = () => _upperBound = new Random().Next( int.MinValue + 1, int.MaxValue );

			Because of = () => _value = Anonymous.Integer.LessThanOrEqualTo( _upperBound );

			It should_be_less_than_or_equal_to_the_specified_value = () => _value.Should().Be.LessThanOrEqualTo( _upperBound );
		}


		[ Subject( typeof ( AnonymousInteger ), "Between" ) ]
		public class When_getting_an_anonymous_integer_between_two_specified_values
		{
			static int _value;
			static int _lowerBound;
			static int _upperBound;

			Establish context = () =>
			                    	{
			                    		_lowerBound = new Random().Next( int.MinValue, int.MaxValue - 1 );
			                    		_upperBound = new Random().Next( _lowerBound, int.MaxValue );
			                    	};

			Because of = () => _value = Anonymous.Integer.Between( _lowerBound, _upperBound );

			It should_be_less_than_or_equal_tothe_specified_upper_bound_value = () => _value.Should().Be.LessThanOrEqualTo( _upperBound );
			It should_be_greater_than_or_equal_to_the_specified_lower_bound_value = () => _value.Should().Be.GreaterThanOrEqualTo( _lowerBound );
		}


		[ Subject( typeof ( AnonymousInteger ), "Between" ) ]
		public class When_getting_an_anonymous_integer_between_two_specified_values_where_the_lower_bound_is_greater_than_the_upper_bound
		{
			static Exception _exception;
			Because of = () => _exception = Catch.Exception( () => Anonymous.Integer.Between( 100, 1 ) );
			It should_fail = () => _exception.Should().Be.OfType< ArgumentException >();
		}


		[ Subject( typeof ( AnonymousInteger ), "BetweenExclusive" ) ]
		public class When_getting_an_anonymous_integer_between_two_specified_values_exclusively
		{
			static int _value;
			static int _lowerBound;
			static int _upperBound;

			Establish context = () =>
			                    	{
			                    		_lowerBound = new Random().Next( int.MinValue, int.MaxValue - 1 );
			                    		_upperBound = new Random().Next( _lowerBound, int.MaxValue );
			                    	};

			Because of = () => _value = Anonymous.Integer.BetweenExclusive( _lowerBound, _upperBound );

			It should_be_less_than_the_specified_upper_bound_value = () => _value.Should().Be.LessThan( _upperBound );
			It should_be_greater_than_the_specified_lower_bound_value = () => _value.Should().Be.GreaterThan( _lowerBound );
		}


		[ Subject( typeof ( AnonymousInteger ), "BetweenExclusive" ) ]
		public class When_getting_an_anonymous_integer_between_two_specified_values_exclusively_where_the_lower_bound_is_greater_than_the_upper_bound
		{
			static Exception _exception;
			Because of = () => _exception = Catch.Exception( () => Anonymous.Integer.BetweenExclusive( 100, 1 ) );
			It should_fail = () => _exception.Should().Be.OfType< ArgumentException >();
		}


		[ Subject( typeof ( AnonymousInteger ), "BetweenExclusive" ) ]
		public class When_getting_an_anonymous_integer_between_two_specified_values_exclusively_where_the_lower_bound_is_equal_to_the_upper_bound
		{
			static Exception _exception;
			Because of = () => _exception = Catch.Exception( () => Anonymous.Integer.BetweenExclusive( 100, 100 ) );
			It should_fail = () => _exception.Should().Be.OfType< ArgumentException >();
		}


		[ Subject( typeof ( AnonymousInteger ), "BetweenExclusive" ) ]
		public class When_getting_an_anonymous_integer_between_two_specified_values_exclusively_where_the_upper_and_lower_bounds_are_adjacent
		{
			static Exception _exception;
			Because of = () => _exception = Catch.Exception( () => Anonymous.Integer.BetweenExclusive( 100, 101 ) );
			It should_fail = () => _exception.Should().Be.OfType< ArgumentOutOfRangeException >();
		}


		[ Subject( typeof ( AnonymousInteger ), "InRange" ) ]
		public class When_getting_an_anonymous_integer_in_the_range_of_two_specified_values
		{
			static int _value;
			static int _lowerBound;
			static int _upperBound;

			Establish context = () =>
			                    	{
			                    		_lowerBound = new Random().Next( int.MinValue, int.MaxValue - 1 );
			                    		_upperBound = new Random().Next( _lowerBound, int.MaxValue );
			                    	};

			Because of = () => _value = Anonymous.Integer.InRange( _lowerBound, _upperBound );

			It should_be_less_than_or_equal_to_the_specified_upper_bound_value = () => _value.Should().Be.LessThanOrEqualTo( _upperBound );
			It should_be_greater_than_or_equal_to_the_specified_lower_bound_value = () => _value.Should().Be.GreaterThanOrEqualTo( _lowerBound );
		}


		[ Subject( typeof ( AnonymousInteger ), "InRange" ) ]
		public class When_getting_an_anonymous_integer_in_the_range_two_specified_values_where_the_lower_bound_is_greater_than_the_upper_bound
		{
			static Exception _exception;
			Because of = () => _exception = Catch.Exception( () => Anonymous.Integer.InRange( 100, 1 ) );
			It should_fail = () => _exception.Should().Be.OfType< ArgumentException >();
		}


		[ Subject( typeof ( AnonymousInteger ), "From" ) ]
		public class When_getting_an_anonymous_integer_from_a_predefined_list_of_integers
		{
			static int _result;
			static IList< int > _predefinedList;
			Establish context = () => _predefinedList = new List< int > {Anonymous.Int, Anonymous.Int, Anonymous.Int};

			Because of = () => _result = Anonymous.Int.From( _predefinedList );

			It should_be_one_of_the_predefined_integers_on_the_list = () => _predefinedList.Should().Contain( _result );
		}


		[ Subject( typeof ( AnonymousInteger ), "From" ) ]
		public class When_getting_an_anonymous_integer_from_a_params_array_of_integers
		{
			static int _result;
			static IList< int > _predefinedList;
			Establish context = () => _predefinedList = new List< int > {1, 2, 3, 4, 5};

			Because of = () => _result = Anonymous.Int.From( 1, 2, 3, 4, 5 );

			It should_be_one_of_the_integers_in_the_params_array = () => _predefinedList.Should().Contain( _result );
		}
	}
}


// ReSharper restore InconsistentNaming