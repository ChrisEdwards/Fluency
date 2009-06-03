using System;
using System.Collections.Generic;
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
			AddListItem( x=>x.LineItems, lineItemBuilder );
			return this;
		}


		public OrderBuilder With( LineItem lineItem )
		{
			AddListItem(x => x.LineItems, lineItem);
			//AddListItem(x => x.LineItems, new LineItemBuilder().AliasFor( lineItem ));
			return this;
		}
	}
}