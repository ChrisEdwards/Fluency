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