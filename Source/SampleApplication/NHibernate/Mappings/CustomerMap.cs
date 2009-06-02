using FluentNHibernate.Mapping;
using SampleApplication.Domain;


namespace SampleApplication.NHibernate.Mappings
{
	public class CustomerMap : ClassMap< Customer >
	{
		public CustomerMap()
		{
			Id( x => x.Id );
			Map( x => x.FirstName );
			Map( x => x.LastName );
		}
	}
}