using FluentObjectBuilder;
using SampleApplication.Domain;
using SampleApplication.Tests.TestDataBuilders;


namespace SampleApplication.Tests.FluentBuilders
{
	public class OrderBuilder : FluentBuilder< Order >
	{
		private readonly CustomerBuilder _customerBuilder = new CustomerBuilder();
		private readonly ListBuilder< LineItem > _lineItemsListBuilder = new ListBuilder< LineItem >();


		protected override void SetupDefaultValues( Order defaults )
		{
			defaults.Id = GenerateNewId();

			SetPropertyBuilder( x => x.Customer, new CustomerBuilder() );
			SetPropertyListBuilder( x => x.LineItems, new ListBuilder< LineItem >() );
		}


		protected override Order BuildFrom( Order values )
		{
			return new Order
			       	{
			       			Id = values.Id,
			       			Customer = _customerBuilder.build(),
			       			LineItems = _lineItemsListBuilder.build()
			       	};
		}


		public OrderBuilder With( LineItemBuilder lineItemBuilder )
		{
			_lineItemsListBuilder.Add( lineItemBuilder );
			return this;
		}


		public OrderBuilder With( LineItem lineItem )
		{
			_lineItemsListBuilder.Add( lineItem );
			return this;
		}
	}
}