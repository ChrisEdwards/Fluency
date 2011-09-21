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
using System.Collections.Generic;
using System.IO;


namespace Shiloh.Utils
{
	public static class IO
	{
		/// <summary>
		/// Enumerates all the lines in the specified file.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <returns></returns>
		public static IEnumerable< string > ForEachLineIn( string filename )
		{
			using ( var streamReader = new StreamReader( filename ) )
			{
				foreach ( string line in ForEachLineIn( streamReader ) )
					yield return line;
			}
		}


		/// <summary>
		/// Enumerates all the lines read from the specified StreamReader.
		/// </summary>
		/// <param name="streamReader">The StreamReader.</param>
		/// <returns></returns>
		public static IEnumerable< string > ForEachLineIn( StreamReader streamReader )
		{
			string line;
			while ( ( line = streamReader.ReadLine() ) != null )
				yield return line;
		}
	}
}