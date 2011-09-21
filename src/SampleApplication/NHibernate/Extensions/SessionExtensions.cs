// Copyright 2011 Chris Edwards
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
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