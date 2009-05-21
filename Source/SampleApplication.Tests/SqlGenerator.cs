using System;
using System.Collections.Generic;
using System.Linq;


namespace SampleApplication.Tests
{
	public static class SqlGenerator
	{


		/// <summary>
		/// Generates the insert SQL.
		/// </summary>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="fieldNames">The field names.</param>
		/// <param name="hasIdentityColumn">if set to <c>true</c> [has identity column].</param>
		/// <returns></returns>
		public static string GenerateInsertSql( string tableName, string[] fieldNames, bool hasIdentityColumn )
		{
			string sqlTemplate=
					@"INSERT INTO {0}
                                    ({1})
                        VALUES
                                    ({2});";

//			if (hasIdentityColumn)
//				sqlTemplate =
//						@"
//                    SET IDENTITY_INSERT {0} ON;
//
//                    " + sqlTemplate + @"
//
//                    SET IDENTITY_INSERT {0} OFF;";

			return GenerateInsertSql_FromTemplate( sqlTemplate, tableName, fieldNames );
		}


		/// <summary>
		/// Generates the insert SQL_ from template.
		/// </summary>
		/// <param name="insertSqlTemplate">The insert SQL template.</param>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="fieldNames">The field names.</param>
		/// <returns></returns>
		private static string GenerateInsertSql_FromTemplate( string insertSqlTemplate, string tableName, string[] fieldNames )
		{
			return String.Format(
					insertSqlTemplate,
					tableName,
					SqlFieldsListFrom( fieldNames ),
					ParametersListFrom( fieldNames ) );
		}


		/// <summary>
		/// SQLs the fields list from.
		/// </summary>
		/// <param name="fieldNames">The field names.</param>
		/// <returns></returns>
		private static string SqlFieldsListFrom( string[] fieldNames )
		{
			return String.Join( ", ", fieldNames );
		}


		/// <summary>
		/// Parameterses the list from.
		/// </summary>
		/// <param name="fieldNames">The field names.</param>
		/// <returns></returns>
		private static string ParametersListFrom( IEnumerable< string > fieldNames )
		{
			string[] fieldNamesFormattedAsSqlParameters = (
			                                              		from fieldName in fieldNames
			                                              		select "@" + fieldName
			                                              ).ToArray();

			return String.Join( ", ", fieldNamesFormattedAsSqlParameters );
		}
	}
}