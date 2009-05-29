using System.Data;
using System.Reflection;
using log4net;
using NHibernate;
using SampleApplication.Domain;
using SampleApplication.NHibernate.Extensions;
using SampleApplication.Tests.Utils;


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


		public void Insert( Order order )
		{
			IDbCommand command = _session.CreateCommandWithinCurrentTransaction();

			command.CommandText = SqlGenerator.GenerateInsertSql(
					"Order",
					new[]
						{
								"Id",
								"Customer_id"
						},
					HasIdentityColumn );

			command.SetParameter( "@Id", order.Id );
			command.SetParameter( "@Customer_id", order.Customer.Id );

			LogCommand( command );
			command.ExecuteNonQuery();
		}


		public void Insert( LineItem lineItem )
		{
			IDbCommand command = _session.CreateCommandWithinCurrentTransaction();

			command.CommandText = SqlGenerator.GenerateInsertSql(
					"LineItem",
					new[]
						{
								"Id",
								"Order_id",
								"Product_id",
								"Quantity",
								"UnitPrice"
						},
					HasIdentityColumn );

			command.SetParameter( "@Id", lineItem.Id );
			command.SetParameter( "@Order_id", lineItem.Order.Id );
			command.SetParameter( "@Product_id", lineItem.Product.Id );
			command.SetParameter( "@Quantity", lineItem.Quantity );
			command.SetDecimalParameter( "@UnitPrice", lineItem.UnitPrice );

			LogCommand( command );
			command.ExecuteNonQuery();
		}


		public void Insert( Customer customer )
		{
			IDbCommand command = _session.CreateCommandWithinCurrentTransaction();

			command.CommandText = SqlGenerator.GenerateInsertSql(
					"Customer",
					new[]
						{
								"Id",
								"FirstName",
								"LastName"
						},
					HasIdentityColumn );

			command.SetParameter( "@Id", customer.Id );
			command.SetParameter( "@FirstName", customer.FirstName );
			command.SetParameter( "@LastName", customer.LastName );

			LogCommand( command );
			command.ExecuteNonQuery();
		}


		public void Insert( Product product )
		{
			IDbCommand command = _session.CreateCommandWithinCurrentTransaction();

			command.CommandText = SqlGenerator.GenerateInsertSql(
					"Product",
					new[]
						{
								"Id",
								"Name",
								"Description"
						},
					HasIdentityColumn );

			command.SetParameter( "@Id", product.Id );
			command.SetParameter( "@Name", product.Name );
			command.SetParameter( "@Description", product.Description );

			LogCommand( command );
			command.ExecuteNonQuery();
		}
	}
}