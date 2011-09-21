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
			string sqlTemplate =
					@"INSERT INTO [{0}]
                                    ({1})
                        VALUES
                                    ({2});";

			if ( hasIdentityColumn )
			{
				sqlTemplate =
						@"
                    SET IDENTITY_INSERT [{0}] ON;

                    " + sqlTemplate + @"

                    SET IDENTITY_INSERT [{0}] OFF;";
			}

			return GenerateInsertSql_FromTemplate( sqlTemplate, tableName, fieldNames );
		}


		/// <summary>
		/// Generates the insert SQL_ from template.
		/// </summary>
		/// <param name="insertSqlTemplate">The insert SQL template.</param>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="fieldNames">The field names.</param>
		/// <returns></returns>
		static string GenerateInsertSql_FromTemplate( string insertSqlTemplate, string tableName, string[] fieldNames )
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
		static string SqlFieldsListFrom( string[] fieldNames )
		{
			return String.Join( ", ", fieldNames );
		}


		/// <summary>
		/// Parameterses the list from.
		/// </summary>
		/// <param name="fieldNames">The field names.</param>
		/// <returns></returns>
		static string ParametersListFrom( IEnumerable< string > fieldNames )
		{
			string[] fieldNamesFormattedAsSqlParameters = (
			                                              		from fieldName in fieldNames
			                                              		select "@" + fieldName
			                                              ).ToArray();

			return String.Join( ", ", fieldNamesFormattedAsSqlParameters );
		}
	}
}