using System;
using FluentObjectBuilder;
using NHibernate.Criterion;
using NUnit.Framework;
using SampleApplication.Domain;
using SampleApplication.Tests.FluentBuilders;
using Order = SampleApplication.Domain.Order;


namespace SampleApplication.Tests.Tests.NHibernate
{
	[TestFixture]
	public class TestMe : AutoRollbackDatabaseTest
	{
		
		[Test]
		public void Should_retrieve_quantity()
		{
//			Order o = _session.CreateQuery( "from Order o where o.Id=2" )
//				.UniqueResult<Order>();
			Customer o = _session.CreateCriteria( typeof ( Customer ) )

					.Add<Customer>()


			Console.WriteLine(o.FirstName);
		}

	}
}