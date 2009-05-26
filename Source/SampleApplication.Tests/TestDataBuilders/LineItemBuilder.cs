using FluentObjectBuilder;
using FluentObjectBuilder.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.TestDataBuilders
{
	public class LineItemBuilder : TestDataBuilder< LineItem >
	{

		public LineItemBuilder()
		{
			// TODO: Setup AutoPopulation of random values by type. Need a way to override defaults.
			_prototype.UnitPrice = ARandom.CurrencyAmount();
			_prototype.Quantity = 1;

			SetPropertyBuilder( x => x.Product, new ProductBuilder() );
			SetPropertyBuilder( x => x.Order, new OrderBuilder() );
		}


		public LineItemBuilder And
		{
			get { return this; }
		}


		protected override LineItem _build()
		{
			// TODO: Use Automapper
			return new LineItem
			       	{
			       			Id = GetUniqueId(),
			       			Order = _prototype.Order,
			       			Product = _prototype.Product,
			       			Quantity = _prototype.Quantity,
			       			UnitPrice = _prototype.UnitPrice
			       	};
		}


		public LineItemBuilder For( TestDataBuilder< Product > productBuilder )
		{
			SetPropertyBuilder( x => x.Product, productBuilder );
			return this;
		}


		public LineItemBuilder Costing( double unitPrice )
		{
			SetPropertyValue( x => x.UnitPrice, unitPrice );
			return this;
		}


		public LineItemBuilder WithQuantity( int howMany )
		{
			SetPropertyValue( x => x.Quantity, howMany );
			return this;
		}


		public LineItemBuilder UnitPriceOf( double unitPrice )
		{
			SetPropertyValue( x => x.UnitPrice, unitPrice );
			return this;
		}
	}
}