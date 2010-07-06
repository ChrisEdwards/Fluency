using Fluency;
using Fluency.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.FluentBuilders
{
	public class ProductBuilder : FluentBuilder< Product >
	{
		protected override void SetupDefaultValues()
		{
			SetProperty( x => x.Id, GenerateNewId() );
			SetProperty( x => x.Name, ARandom.Title( 100 ) );
			SetProperty( x => x.Description, ARandom.Text( 200 ) );
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