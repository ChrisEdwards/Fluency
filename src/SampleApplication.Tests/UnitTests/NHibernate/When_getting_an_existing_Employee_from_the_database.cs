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
using SampleApplication.Tests.FluentBuilders;


namespace SampleApplication.Tests.Tests.NHibernate
{
	[ TestFixture ]
	public class When_getting_an_existing_Customer_from_the_database : AutoRollbackDatabaseTest
	{
		Customer _expectedCustomer;
		Customer _actualCustomer;


		protected override void TestSetUp()
		{
			_expectedCustomer = a.Customer.build();

			_db.Add( _expectedCustomer )
			   .Persist();

			_actualCustomer = _session.Load< Customer >( _expectedCustomer.Id );
		}


		[ Test ]
		[ Ignore( "Requires SQL Lite" ) ]
		public void Should_retrieve_first_name()
		{
			Assert.That( _actualCustomer.FirstName, Is.EqualTo( _expectedCustomer.FirstName ) );
		}


		[ Test ]
		[ Ignore( "Requires SQL Lite" ) ]
		public void Should_retrieve_last_name()
		{
			Assert.That( _actualCustomer.LastName, Is.EqualTo( _expectedCustomer.LastName ) );
		}
	}
}