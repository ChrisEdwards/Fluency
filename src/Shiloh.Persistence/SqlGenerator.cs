using System;
using System.Linq;


namespace Shiloh.Persistence
{
	public static class SqlGenerator
	{
		public static string GenerateInsertSql( string tableName, string[] fieldNames, bool hasIdentityColumn )
		{
			string sqlTemplate =
					@"INSERT INTO {0}
                                    ({1})
                        VALUES
                                    ({2});";

			if ( hasIdentityColumn )
			{
				sqlTemplate =
						@"
                    SET IDENTITY_INSERT {0} ON;

                    " + sqlTemplate + @"

                    SET IDENTITY_INSERT {0} OFF;";
			}

			return GenerateInsertSql_FromTemplate( sqlTemplate, tableName, fieldNames );
		}


		static string GenerateInsertSql_FromTemplate( string insertSqlTemplate, string tableName, string[] fieldNames )
		{
			return String.Format(
					insertSqlTemplate,
					tableName,
					SqlFieldsListFrom( fieldNames ),
					ParametersListFrom( fieldNames ) );
		}


		static string SqlFieldsListFrom( string[] fieldNames )
		{
			return String.Join( ", ", fieldNames );
		}


		static string ParametersListFrom( string[] fieldNames )
		{
			// Simply prepend @ in front of every field name.
			string[] fieldNamesFormattedAsSqlParameters = (
			                                              		from fieldName in fieldNames
			                                              		select "@" + fieldName
			                                              ).ToArray();

			return String.Join( ", ", fieldNamesFormattedAsSqlParameters );
		}
	}
}