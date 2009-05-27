using System;
using FluentObjectBuilder;
using FluentObjectBuilder.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.TestDataBuilders
{
	public class CustomerBuilder : FluentBuilder< Customer >
	{
		protected override void SetupDefaultValues( Customer defaults )
		{
			defaults.Id = GetUniqueId();
			defaults.FirstName = ARandom.FirstName();
			defaults.LastName = ARandom.LastName();
		}


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