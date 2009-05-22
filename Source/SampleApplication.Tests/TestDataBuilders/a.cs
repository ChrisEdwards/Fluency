using System;


namespace SampleApplication.Tests.TestDataBuilders
{
	public static class a
	{
		public static OrderBuilder Order
		{
			get { return new OrderBuilder(); }
		}

		public static LineItemBuilder LineItem
		{
			get { return new LineItemBuilder(); }
		}
	}


	public static class an
	{
		public static CustomerBuilder Customer
		{
			get { return new CustomerBuilder(); }
		}
	}
}