using FluentObjectBuilder;
using FluentObjectBuilder.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.TestDataBuilders
{
	public class CustomerBuilder : TestDataBuilder< Customer >
	{
		protected override Customer _build()
		{
			return new Customer
			       	{
			       			Id = GetUniqueId(),
			       			FirstName = ARandom.FirstName(),
			       			LastName = ARandom.LastName()
			       	};
		}
	}
}