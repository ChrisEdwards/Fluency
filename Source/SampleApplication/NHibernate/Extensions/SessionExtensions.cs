using System.Data;
using NHibernate;


namespace SampleApplication.NHibernate.Extensions
{
	public static class SessionExtensions
	{
		/// <summary>
		/// Creates a new IDbCommand object enlisted in the session's current transaction.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <returns></returns>
		public static IDbCommand CreateCommandWithinCurrentTransaction( this ISession session )
		{
			IDbCommand command = session.Connection.CreateCommand();
			session.Transaction.Enlist( command );
			return command;
		}


		/// <summary>
		/// Gets the I db transaction.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <returns></returns>
		public static IDbTransaction GetIDbTransaction( this ISession session )
		{
			return session.CreateCommandWithinCurrentTransaction().Transaction;
		}


		public static IDbCommand GetAdHocQueryCommand( this ISession session, string query )
		{
			IDbCommand command = session.CreateCommandWithinCurrentTransaction();
			command.CommandType = CommandType.Text;
			command.CommandText = query;
			return command;
		}


		public static void ExecuteSql( this ISession session, string sql )
		{
			session.GetAdHocQueryCommand( sql ).ExecuteNonQuery();
		}
	}
}