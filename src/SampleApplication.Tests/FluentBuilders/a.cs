namespace SampleApplication.Tests.FluentBuilders
{
	public static class a
	{
		public static CustomerBuilder Customer
		{
			get { return new CustomerBuilder(); }
		}

		public static LineItemBuilder LineItem
		{
			get { return new LineItemBuilder(); }
		}

		public static ProductBuilder Product
		{
			get { return new ProductBuilder(); }
		}
	}


	public static class an
	{
		public static OrderBuilder Order
		{
			get { return new OrderBuilder(); }
		}
	}
}