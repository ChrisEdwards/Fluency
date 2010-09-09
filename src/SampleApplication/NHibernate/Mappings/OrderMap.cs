using FluentNHibernate.Mapping;
using SampleApplication.Domain;


namespace SampleApplication.NHibernate.Mappings
{
	public class OrderMap : ClassMap< Order >
	{
		public OrderMap()
		{
			Id( x => x.Id );
			References( x => x.Customer );
			HasMany( x => x.LineItems )
					.Cascade.AllDeleteOrphan()
					.Inverse();
		}
	}
}