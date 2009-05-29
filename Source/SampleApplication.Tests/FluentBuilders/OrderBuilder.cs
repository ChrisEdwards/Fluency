using Fluency;
using SampleApplication.Domain;


namespace SampleApplication.Tests.FluentBuilders
{
	public class OrderBuilder : FluentBuilder< Order >
	{
		private readonly CustomerBuilder _customerBuilder = new CustomerBuilder();
		private readonly FluentListBuilder< LineItem > _lineItemsListBuilder = new FluentListBuilder< LineItem >();


		protected override void SetupDefaultValues( Order defaults )
		{
			defaults.Id = GenerateNewId();

			SetPropertyBuilder( x => x.Customer, new CustomerBuilder() );
			SetPropertyListBuilder( x => x.LineItems, new FluentListBuilder< LineItem >() );
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