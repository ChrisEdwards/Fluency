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