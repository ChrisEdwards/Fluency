using System;
using System.Data;
using System.Reflection;
using BancVue.Tests.Common.Utils;
using log4net;
using NHibernate;
using SampleApplication.Domain;
using SampleApplication.NHibernate;


namespace SampleApplication.Tests
{
	public class DbHelper
	{
		public const bool HasIdentityColumn = true;
		public const bool HasNoIdentityColumn = false;
		private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

		private readonly ISession _session;


		public DbHelper( ISession session )
		{
			_session = session;
		}


		private static void LogCommand( IDbCommand command )
		{
			// Log command call.
			log.Debug( "SQL COMMAND: " + command.CommandText );

			// Log parameters.
			foreach ( IDataParameter parameter in command.Parameters )
				log.Debug( string.Format( "SQL PARAM: Name='{0}' Value='{1}'", parameter.ParameterName, parameter.Value ) );
		}


		private static bool IsPreExistingId( int id )
		{
			return id >= 0;
		}


		public void Insert( Project project )
		{
			IDbCommand command = _session.CreateCommandWithinCurrentTransaction();

			command.CommandText = SqlGenerator.GenerateInsertSql(
					"Project",
					new[]
						{
								"Id",
								"Name"
						},
					HasIdentityColumn );

			command.SetParameter( "@Id", project.Id );
			command.SetParameter( "@Name", project.Name );

			LogCommand( command );
			command.ExecuteNonQuery();
		}


		public void Insert( Employee employee )
		{
			IDbCommand command = _session.CreateCommandWithinCurrentTransaction();

			command.CommandText = SqlGenerator.GenerateInsertSql(
					"Employee",
					new[]
						{
								"Id",
								"FirstName",
                                "LastName"
						},
					HasIdentityColumn);

			command.SetParameter("@Id", employee.Id);
			command.SetParameter("@FirstName", employee.FirstName);
			command.SetParameter("@LastName", employee.LastName);

			LogCommand(command);
			command.ExecuteNonQuery();
		}
	}
}