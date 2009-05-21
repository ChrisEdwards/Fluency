using System;
using System.Data;


namespace BancVue.Tests.Common.Utils
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


        public static IDbDataParameter SetParameter( this IDbCommand command, string parameterName, string value )
        {
            return SetParameter( command, parameterName, ( value as object ) ?? DBNull.Value, DbType.String );
        }


        public static IDbDataParameter SetDecimalParameter( this IDbCommand command, string parameterName, double value )
        {
            return SetParameter( command, parameterName, value, DbType.Decimal );
        }


        public static IDbDataParameter SetParameter( this IDbCommand command, string parameterName, byte[] value )
        {
            return SetParameter( command, parameterName, value, DbType.Binary );
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
    }
}