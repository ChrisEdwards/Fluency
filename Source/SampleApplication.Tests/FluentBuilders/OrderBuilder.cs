using System;
using System.Collections.Generic;
using FluentObjectBuilder;
using FluentObjectBuilder.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.TestDataBuilders
{
	public class OrderBuilder :FluentBuilder<Order >
	{
		
		private readonly ListBuilder<LineItem> _lineItemsListBuilder = new ListBuilder<LineItem>();
		private CustomerBuilder _customerBuilder = new CustomerBuilder();

		protected override void SetupDefaultValues( Order defaults )
		{
			SetPropertyListBuilder( (Order x)=>x.LineItems, new ListBuilder<LineItem>() );
		}


		protected override Order _build()
		{
			return new Order
			       	{
			       			Id = GetUniqueId(),
			       			Customer = _customerBuilder.build(),
							LineItems = _lineItemsListBuilder.build()
			       	};
		}


		public OrderBuilder With( LineItemBuilder lineItemBuilder )
		{
			_lineItemsListBuilder.Add( lineItemBuilder );
			return this;
		}
	}
}