using Fluency;
using Fluency.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.FluentBuilders
{
	public class LineItemBuilder : FluentBuilder< LineItem >
	{
		public LineItemBuilder And
		{
			get { return this; }
		}


		protected override void SetupDefaultValues( LineItem defaults )
		{
			// TODO: Setup AutoPopulation of random values by type. Need a way to override defaults.
			defaults.UnitPrice = ARandom.CurrencyAmount();
			defaults.Quantity = 1;

			SetPropertyBuilder( x => x.Product, new ProductBuilder() );
			SetPropertyBuilder( x => x.Order, new OrderBuilder() );
		}


		protected override LineItem BuildFrom( LineItem values )
		{
			// TODO: Use Automapper
			return new LineItem
			       	{
			       			Id = values.Id,
			       			Order = values.Order,
			       			Product = values.Product,
			       			Quantity = values.Quantity,
			       			UnitPrice = values.UnitPrice
			       	};
		}


		public LineItemBuilder For( FluentBuilder< Product > productBuilder )
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