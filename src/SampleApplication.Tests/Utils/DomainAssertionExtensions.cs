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
using Fluency.Tests;
using Fluency.Tests.Deprecated;
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