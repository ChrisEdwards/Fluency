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