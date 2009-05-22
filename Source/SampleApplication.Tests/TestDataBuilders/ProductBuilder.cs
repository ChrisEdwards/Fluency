using System;
using FluentObjectBuilder;
using FluentObjectBuilder.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.TestDataBuilders
{
	public class ProductBuilder : TestDataBuilder<Product>
	{
		protected override Product _build()
		{
			return new Product
			       	{
			       			Id = GetUniqueId(),
			       			Name = ARandom.Title( 100 ),
			       			Description = ARandom.Text( 300 )
			       	};
		}
	}
}