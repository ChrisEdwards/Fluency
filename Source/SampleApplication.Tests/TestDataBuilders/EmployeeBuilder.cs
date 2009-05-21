using FluentObjectBuilder;
using FluentObjectBuilder.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.TestDataBuilders
{
	public class EmployeeBuilder : TestDataBuilder< Employee >
	{
		protected override Employee _build()
		{
			return new Employee
			       	{
			       			Id = GetUniqueId(),
			       			FirstName = ARandom.FirstName(),
			       			LastName = ARandom.LastName()
			       	};
		}
	}
}