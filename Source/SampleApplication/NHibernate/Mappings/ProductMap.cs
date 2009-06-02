using FluentNHibernate.Mapping;
using SampleApplication.Domain;


namespace SampleApplication.NHibernate.Mappings
{
	public class ProductMap : ClassMap< Product >
	{
		public ProductMap()
		{
			Id( x => x.Id );
			Map( x => x.Name );
			Map( x => x.Description );
		}
	}
}