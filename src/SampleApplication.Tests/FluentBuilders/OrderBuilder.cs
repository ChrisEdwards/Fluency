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
using Fluency;
using SampleApplication.Domain;


namespace SampleApplication.Tests.FluentBuilders
{
	public class OrderBuilder : FluentBuilder< Order >
	{
		protected override void SetupDefaultValues()
		{
			SetProperty( x => x.Id, GenerateNewId() );
			SetProperty( x => x.Customer, new CustomerBuilder() );
			SetList( x => x.LineItems, new FluentListBuilder< LineItem >() );
		}


		protected override Order BuildFrom( Order values )
		{
			return new Order
			       	{
			       			Id = values.Id,
			       			Customer = values.Customer,
			       			LineItems = values.LineItems
			       	};
		}


		public OrderBuilder With( LineItemBuilder lineItemBuilder )
		{
			AddListItem( x => x.LineItems, lineItemBuilder );
			return this;
		}


		public OrderBuilder With( LineItem lineItem )
		{
			AddListItem( x => x.LineItems, lineItem );
			//AddListItem(x => x.LineItems, new LineItemBuilder().AliasFor( lineItem ));
			return this;
		}
	}
}