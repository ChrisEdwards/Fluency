using System;
using FluentObjectBuilder;
using FluentObjectBuilder.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.TestDataBuilders
{
	public class ProductBuilder : FluentBuilder<Product>
	{
		protected override void SetupDefaultValues( Product defaults )
		{
			defaults.Id = GetUniqueId();
			defaults.Name = ARandom.Title( 100 );
			defaults.Description = ARandom.Text( 300 );
		}


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