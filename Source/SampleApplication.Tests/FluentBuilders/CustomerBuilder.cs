using System;
using FluentObjectBuilder;
using FluentObjectBuilder.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.FluentBuilders
{
	public class CustomerBuilder : FluentBuilder< Customer >
	{
		protected override void SetupDefaultValues( Customer defaults )
		{
			defaults.Id = GenerateNewId();
			defaults.FirstName = ARandom.FirstName();
			defaults.LastName = ARandom.LastName();
		}


		protected override Customer BuildFrom( Customer values )
		{
			return new Customer
			       	{
			       			Id = values.Id,
			       			FirstName = values.FirstName,
			       			LastName = values.LastName
			       	};
		}
	}
}