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
using NUnit.Framework;
using SampleApplication.Domain;


namespace SampleApplication.Tests.IntegrationTests
{
	[ TestFixture ]
	public class ObjectMotherTests : AutoRollbackDatabaseTest
	{
		readonly Anonymous a = new Anonymous();


		protected override void TestSetUp()
		{
			Order order = a.Order();
			LineItem lineItem1 = a.LineItem_ForOrder( order, 1, 1.60 );
			LineItem lineItem2 = a.LineItem_ForOrder( order, 10, 25.99 );
		}


		[ Test ]
		[ Ignore( "This is a sample...not meant to run" ) ]
		public void SomeTest()
		{
			// Test with Db.
		}
	}
}