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
									//.Database( SQLiteConfiguration.Standard.UsingFile( DbFile ) )
									.Database( MsSqlConfiguration.MsSql2005.ConnectionString( c=>c.Is( @"Data Source=localhost\BANCVUE;Initial Catalog=SampleApplication;Integrated Security=SSPI;")) )
									.Mappings( m => {
									                	m.AutoMappings.Add(
									                	                  		AutoMap.AssemblyOf< ISampleApplicationAssembly >()
									                	                  				.Where( t => t.Namespace == "SampleApplication.Domain" && t.Name != "Customer") );
									                	m.HbmMappings.AddFromAssemblyOf< Customer >();
									})
									//.ExposeConfiguration( BuildSchema )
									.BuildSessionFactory();

					// Initialize NHProf.
					HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
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