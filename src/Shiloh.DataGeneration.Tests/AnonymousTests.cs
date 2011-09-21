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
using System.Text.RegularExpressions;
using Machine.Specifications;
using SharpTestsEx;

// ReSharper disable InconsistentNaming


namespace Shiloh.DataGeneration.Tests
{
	public class AnonymousTests
	{
		[ Subject( typeof ( Anonymous ) ) ]
		public class When_getting_an_anonymous_first_name
		{
			static string _value;
			Because of = () => _value = Anonymous.FirstName();

			It should_not_be_an_empty_string = () => _value.Should().Not.Be.NullOrEmpty();
			It should_be_one_of_the_first_names_from_the_random_list = () => RandomData.FirstNames.Should().Contain( _value );
		}


		[ Subject( typeof ( Anonymous ) ) ]
		public class When_getting_an_anonymous_last_name
		{
			static string _value;
			Because of = () => _value = Anonymous.LastName();

			It should_not_be_an_empty_string = () => _value.Should().Not.Be.NullOrEmpty();
			It should_be_one_of_the_last_names_from_the_random_list = () => RandomData.LastNames.Should().Contain( _value );
		}


		[ Subject( typeof ( Anonymous ) ) ]
		public class When_getting_an_anonymous_full_name
		{
			static string _value;
			Because of = () => _value = Anonymous.FullName();

			It should_not_be_an_empty_string = () => _value.Should().Not.Be.NullOrEmpty();
			It should_contain_at_least_one_space = () => _value.Should().Contain( " " );
		}


		[ Subject( typeof ( Anonymous ) ) ]
		public class When_getting_an_anonymous_city_name
		{
			static string _value;
			Because of = () => _value = Anonymous.City();

			It should_not_be_an_empty_string = () => _value.Should().Not.Be.NullOrEmpty();
			It should_be_a_valid_city_name = () => RandomData.Cities.Should().Contain( _value );
		}


		[ Subject( typeof ( Anonymous ) ) ]
		public class When_getting_an_anonymous_state_name
		{
			static string _value;
			Because of = () => _value = Anonymous.StateName();

			It should_not_be_an_empty_string = () => _value.Should().Not.Be.NullOrEmpty();
			It should_be_a_valid_state_name = () => RandomData.States.Should().Contain( _value );
		}


		[ Subject( typeof ( Anonymous ) ) ]
		public class When_getting_an_anonymous_state_code
		{
			static string _value;
			Because of = () => _value = Anonymous.StateCode();

			It should_not_be_an_empty_string = () => _value.Should().Not.Be.NullOrEmpty();
			It should_be_a_valid_state_code = () => RandomData.StateCodes.Should().Contain( _value );
		}


		[ Subject( typeof ( Anonymous ) ) ]
		public class When_getting_an_anonymous_string
		{
			static string _value;
			Because of = () => _value = Anonymous.String();

			It should_not_be_an_empty_string = () => _value.Should().Not.Be.NullOrEmpty();
			It should_ony_consist_of_capital_letters = () => _value.Should().Match( new Regex( "^[A-Z]+$", RegexOptions.None ) );
		}


		[ Subject( typeof ( Anonymous ) ) ]
		public class When_getting_an_anonymous_string_with_a_spcecified_length
		{
			static string _value;
			static int _length;
			Establish context = () => _length = Anonymous.Int.Between( 1, 1000 );

			Because of = () => _value = Anonymous.String( _length );

			It should_not_be_an_empty_string = () => _value.Should().Not.Be.NullOrEmpty();
			It should_ony_consist_of_capital_letters = () => _value.Should().Match( new Regex( "^[A-Z]+$", RegexOptions.None ) );
			It should_be_the_specified_length = () => _value.Length.Should().Be.EqualTo( _length );
		}


		[ Subject( typeof ( Anonymous ) ) ]
		public class When_getting_an_anonymous_email_address
		{
			static string _value;

			static string _strictEmailRegexPattern = @"^(([^<>()[\]\\.,;:\s@\""]+"
			                                         + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
			                                         + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
			                                         + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
			                                         + @"[a-zA-Z]{2,}))$";

			Because of = () => _value = Anonymous.Email();

			It should_not_be_an_empty_string = () => _value.Should().Not.Be.NullOrEmpty();
			It should_ony_consist_of_capital_letters = () => _value.Should().Match( new Regex( _strictEmailRegexPattern ) );
		}
	}
}


// ReSharper restore InconsistentNaming