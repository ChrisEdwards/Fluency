using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;


namespace Shiloh.Utils
{
	public static class EmbeddedResourceFile
	{
		/// <summary>
		/// Enumerates all the lines in the specified file embedded as a resource in the specified assembly.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="assembly">The assembly the file is embedded in..</param>
		public static IEnumerable< string > ForEachLineIn( string filename, Assembly assembly )
		{
			using ( StreamReader reader = GetReader( filename, assembly ) )
			{
				foreach ( string line in IO.ForEachLineIn( reader ) )
					yield return line;
			}
		}


		/// <summary>
		/// Gets a StreamReader for the specified resource file.
		/// </summary>
		/// <param name="resourceName">The filename.</param>
		/// <param name="assembly">The assembly.</param>
		/// <returns></returns>
		/// <exception cref="MissingManifestResourceException"><c>MissingManifestResourceException</c>.</exception>
		public static StreamReader GetReader( string resourceName, Assembly assembly )
		{
			Stream stream = assembly.GetManifestResourceStream( resourceName );
			
			if ( stream == null )
				throw new MissingManifestResourceException( string.Format( "Missing embedded resource file '{0}' in assembly '{1}'. Tried to access the file, but it was not found as an embedded resource.", resourceName, assembly.GetName().Name ) );

			return new StreamReader( stream );
		}


		/// <summary>
		/// Gets the resource name for a file embedded in the specified assembly.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		/// <returns></returns>
		public static string GetDefaultResourceNamespace( Assembly assembly )
		{
			// NOTE: This assumes the name of the assembly is equal to its root namespace. If this is not true, then this code would need modification.
			return assembly.GetName().Name;
		}
	}
}