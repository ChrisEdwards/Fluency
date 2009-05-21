using System.IO;
using FluentNHibernate.AutoMap;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using sampleapplication;


namespace SampleApplication.NHibernate
{
	public static class NHibernateHelper
	{
		private const string DbFile = "firstProject.db";
		private static ISessionFactory _sessionFactory;

		/// <summary>
		/// Gets the NHibernate session factory.
		/// </summary>
		/// <value>The session factory.</value>
		private static ISessionFactory SessionFactory
		{
			get
			{
				if ( _sessionFactory == null )
				{
					// Configure NHibernate Session Factory using FluentNHibernate.
					_sessionFactory =
							Fluently.Configure()
									.Database( SQLiteConfiguration.Standard.UsingFile( DbFile ) )
									.Mappings( m => m.AutoMappings.Add(
									                		AutoPersistenceModel.MapEntitiesFromAssemblyOf< ISampleApplicationAssembly >()
									                				.Where( t => t.Namespace == "SampleApplication.Domain" ) ) )
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
		private static void BuildSchema( Configuration config )
		{
			// delete the existing db on each run  
			if ( File.Exists( DbFile ) )
				File.Delete( DbFile );

			// this NHibernate tool takes a configuration (with mapping info in)  
			// and exports a database schema from it  
			new SchemaExport( config )
					.Create( false, true );
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