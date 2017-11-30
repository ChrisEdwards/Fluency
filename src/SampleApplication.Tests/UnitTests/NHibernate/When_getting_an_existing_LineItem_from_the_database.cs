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
using FluentAssertions;
using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.FluentBuilders;

namespace SampleApplication.Tests.UnitTests.NHibernate
{
	[ TestFixture ]
	public class When_getting_an_existing_LineItem_from_the_database : AutoRollbackDatabaseTest
	{
		LineItem _expectedLineItem;
		LineItem _actualLineItem;


		protected override void TestSetUp()
		{
			_expectedLineItem = a.LineItem.build();

			_db.Add( _expectedLineItem )
					.Persist();

			_actualLineItem = _session.Get< LineItem >( _expectedLineItem.Id );
		}


		[ Test ]
		[ Ignore( "Requires SQL Lite" ) ]
		public void Should_retrieve_quantity()
		{
			DateTime x = _actualLineItem.Order.OrderDate;
			_actualLineItem.Quantity.Should().Be( _expectedLineItem.Quantity );
		}


		[ Test ]
		[ Ignore( "Requires SQL Lite" ) ]
		public void Should_retrieve_unit_price()
		{
			_actualLineItem.UnitPrice.Should().Be( _expectedLineItem.UnitPrice );
		}
	}
}