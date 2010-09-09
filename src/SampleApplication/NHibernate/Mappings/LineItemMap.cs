using FluentNHibernate.Mapping;
using SampleApplication.Domain;


namespace SampleApplication.NHibernate.Mappings
{
	public class LineItemMap : ClassMap< LineItem >
	{
		public LineItemMap()
		{
			Id( x => x.Id );
			Map( x => x.Quantity );
			Map( x => x.UnitPrice );
			
			References( x => x.Order )
					.Cascade.SaveUpdate();
			References( x => x.Product )
					.Cascade.SaveUpdate();
		}
	}
}