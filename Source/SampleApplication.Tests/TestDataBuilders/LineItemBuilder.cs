using System;
using FluentObjectBuilder;
using FluentObjectBuilder.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.TestDataBuilders
{
	public class LineItemBuilder:TestDataBuilder<LineItem>
	{
		private double _unitPrice;
		private int _quantity;
		private OrderBuilder _orderBuilder;
		private ProductBuilder _productBuilder;

		public LineItemBuilder()
		{
			_unitPrice = ARandom.CurrencyAmount();
			_quantity = ARandom.IntBetween( 1, 20 );
			_orderBuilder = new OrderBuilder();
			_productBuilder = new ProductBuilder();
		}

		protected override LineItem _build()
		{
			return new LineItem
			       	{
			       			Id = GetUniqueId(),
			       			Order = _orderBuilder.build(),
			       			Product = _productBuilder.build(),
			       			Quantity = _quantity,
			       			UnitPrice = _unitPrice
			       	};
		}
	}
}