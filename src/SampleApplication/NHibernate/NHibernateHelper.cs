using System.IO;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using sampleapplication;
using SampleApplication.Domain;


namespace SampleApplication.NHibernate
{
	public static class NHibernateHelper
	{
		const string DbFile = "firstProject.db";
		static ISessionFactory _sessionFactory;

		/// <summary>
		/// Gets the NHibernate session factory.
		/// </summary>
		/// <value>The session factory.</value>
		static ISessionFactory SessionFactory
		{
			get
			{
				if ( _sessionFactory == null )
				{
					// Configure NHibernate Session Factory using FluentNHibernate.
					_sessionFactory =
							Fluently.Configure()
									.Database( SQLiteConfiguration.Standard.UsingFile( DbFile ) )
									//.Database( MsSqlConfiguration.MsSql2005.ConnectionString( c => c.Is( @"Data Source=localhost;Initial Catalog=SampleApplication;Integrated Security=SSPI;" ) ) )
									.Mappings( m =>
									           	{
									           		m.AutoMappings.Add(
									           				AutoMap.AssemblyOf< ISampleApplicationAssembly >()
									           						.Where( t => t.Namespace == "SampleApplication.Domain" && t.Name != "Customer" )
									           						// FNH 1.1 decided to fail on readonly fields. Quick fix till I override the automapping configuration.
									           						.Override< LineItem >( map => map.IgnoreProperty( x => x.Amount ) )
									           						.Override< Order >( map => map.IgnoreProperty( x => x.TotalAmount ) ) );
									           		m.HbmMappings.AddFromAssemblyOf< Customer >();
									           	} )
									.ExposeConfiguration( BuildSchema )
									.BuildSessionFactory();
				}

				return _sessionFactory;
			}
		}


		/// <summary>
		/// Builds the schema.
		/// </summary>
		/// <param name="config">The config.</param>
		static void BuildSchema( Configuration config )
		{
			// delete the existing db on each run  
			if ( File.Exists( DbFile ) )
				File.Delete( DbFile );

			// this NHibernate tool takes a configuration (with mapping info in)  
			// and exports a database schema from it  
			new SchemaExport( config )
					.Create( true, true );
		}


		/// <summary>
		/// Opens a new NHibernate session.
		/// </summary>
		/// <returns></returns>
		public static ISession OpenSession()
		{
			return SessionFactory.OpenSession();
		}
	}
}