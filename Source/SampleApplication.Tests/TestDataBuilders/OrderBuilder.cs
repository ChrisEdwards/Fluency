using FluentObjectBuilder;
using FluentObjectBuilder.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.TestDataBuilders
{
	public class OrderBuilder :TestDataBuilder<Order >
	{
		private CustomerBuilder _customerBuilder = new CustomerBuilder();

		protected override Order _build()
		{
			return new Order
			       	{
			       			Id = GetUniqueId(),
			       			Customer = _customerBuilder.build()
			       	};
		}
	}
}