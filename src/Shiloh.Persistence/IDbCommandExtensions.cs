using System;
using System.Data;


namespace Shiloh.Persistence
{
	public static class IDbCommandExtensions
	{
		public static IDbDataParameter SetParameter( this IDbCommand command, string parameterName, int? value )
		{
			object val = ( value.HasValue )
			             		? value.Value as object
			             		: DBNull.Value;

			return SetParameter( command, parameterName, val, DbType.Int32 );
		}


		public static IDbDataParameter SetParameter( this IDbCommand command, string parameterName, DateTime value )
		{
			object val = ( value == DateTime.MinValue )
			             		? DBNull.Value
			             		: value as object;

			return SetParameter( command, parameterName, val, DbType.DateTime );
		}


		public static IDbDataParameter SetParameter( this IDbCommand command, string parameterName, DateTime? value )
		{
			object val = ( value.HasValue )
			             		? value.Value as object
			             		: DBNull.Value;

			return SetParameter( command, parameterName, val, DbType.DateTime );
		}


		public static IDbDataParameter SetParameter( this IDbCommand command, string parameterName, bool value )
		{
			return SetParameter( command, parameterName, value, DbType.Boolean );
		}


		public static IDbDataParameter SetParameter( this IDbCommand command, string parameterName, bool? value )
		{
			object val = ( value.HasValue )
			             		? value.Value as object
			             		: DBNull.Value;

			return SetParameter( command, parameterName, value, DbType.Boolean );
		}


		public static IDbDataParameter SetParameter( this IDbCommand command, string parameterName, string value )
		{
			return SetParameter( command, parameterName, ( value as object ) ?? DBNull.Value, DbType.String );
		}


		public static IDbDataParameter SetDecimalParameter( this IDbCommand command, string parameterName, decimal value )
		{
			return SetParameter( command, parameterName, value, DbType.Decimal );
		}


		public static IDbDataParameter SetDoubleParameter( this IDbCommand command, string parameterName, double value )
		{
			return SetParameter( command, parameterName, value, DbType.Double );
		}


		public static IDbDataParameter SetParameter( this IDbCommand command, string parameterName, byte[] value )
		{
			return SetParameter( command, parameterName, value, DbType.Binary );
		}


		public static IDbDataParameter SetParameter( this IDbCommand command, string parameterName, char? value )
		{
			object val = ( value.HasValue )
			             		? value.Value as object
			             		: DBNull.Value;

			return SetParameter( command, parameterName, val, DbType.StringFixedLength );
		}


		public static IDbDataParameter SetParameter( this IDbCommand command, string parameterName, object value, DbType dbType )
		{
			IDbDataParameter param = command.CreateParameter();
			param.ParameterName = parameterName;
			param.DbType = dbType;
			param.Direction = ParameterDirection.Input;
			param.Value = value;

			command.Parameters.Add( param );

			return param;
		}


		public static IDbDataParameter GetParameter( this IDbCommand command, string parameterName, DbType dbType )
		{
			IDbDataParameter param = command.CreateParameter();
			param.ParameterName = parameterName;
			param.DbType = dbType;
			param.Direction = ParameterDirection.Output;

			command.Parameters.Add( param );

			return param;
		}


		/// <summary>
		/// Adds the parameter. (Old procedure...should update to use SetParameter)
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="parameterValue">The parameter value.</param>
		/// <returns></returns>
		public static IDbDataParameter AddParameter( IDbCommand command, string parameterName, object parameterValue )
		{
			IDbDataParameter param = command.CreateParameter();
			param.Value = parameterValue;
			param.ParameterName = parameterName;
			command.Parameters.Add( param );
			return param;
		}
	}
}