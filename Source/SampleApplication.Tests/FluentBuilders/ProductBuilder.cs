using Fluency;
using Fluency.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.FluentBuilders
{
	public class ProductBuilder : FluentBuilder< Product >
	{
		protected override void SetupDefaultValues( Product defaults )
		{
			defaults.Id = GenerateNewId();
			defaults.Name = ARandom.Title( 100 );
			defaults.Description = ARandom.Text( 300 );
		}


		protected override Product BuildFrom( Product values )
		{
			return new Product
			       	{
			       			Id = values.Id,
			       			Name = values.Name,
			       			Description = values.Description
			       	};
		}
	}
}