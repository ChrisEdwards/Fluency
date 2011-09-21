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
using NUnit.Framework;
using SampleApplication.NHibernate;
using SampleApplication.NHibernate.Extensions;


namespace SampleApplication.Tests
{
	[ TestFixture ]
	public class AutoRollbackDatabaseTest
	{
		#region Setup/Teardown

		[ SetUp ]
		public void SetUp()
		{
			// Get a connection to the db and start a transaction.
			_session = NHibernateHelper.OpenSession();
			_session.BeginTransaction();

			// Create test data db wrapper to insert test data with.
			_db = new TestDatabase( _session );

			// Allow derived class to setup test.
			TestSetUp();
		}


		[ TearDown ]
		public void TearDown()
		{
			// Clean up test data by rolling back the transaction and close the session.
			_session.Transaction.Rollback();
			_session.Close();
		}

		#endregion


		protected TestDatabase _db;
		protected ISession _session;


		static AutoRollbackDatabaseTest()
		{
			Log4NetConfiguration.Configure();
		}


		/// <summary>
		/// Override to allow derived classes acces to [SetUp] method.
		/// </summary>
		protected virtual void TestSetUp() {}


		protected IDbCommand GetStoredProcCommand( string procedureName )
		{
			IDbCommand command = _session.CreateCommandWithinCurrentTransaction();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = procedureName;
			return command;
		}


		protected IDbCommand GetScalarFunctionCommand( string functionName )
		{
			IDbCommand command = _session.CreateCommandWithinCurrentTransaction();
			command.CommandType = CommandType.Text;
			command.CommandText = "select " + functionName;
			return command;
		}
	}
}