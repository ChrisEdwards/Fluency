using System.IO;
using System.Text;
using NUnit.Framework;


namespace SampleApplication.Tests
{
	public static class FileAssertions
	{
		public static void FilesAreEqual( string expectedFileName, string actualFileName )
		{
			// Ensure files are the same.
			var expected = new StreamReader( new FileStream( expectedFileName, FileMode.Open, FileAccess.Read )
			                                 ,
			                                 new UTF8Encoding( false, true ) );
			var actual = new StreamReader( new FileStream( actualFileName, FileMode.Open, FileAccess.Read )
			                               ,
			                               new UTF8Encoding( false, true ) );

			try
			{
				while ( !expected.EndOfStream )
					Assert.AreEqual( expected.ReadLine(), actual.ReadLine() );
				Assert.IsTrue( actual.EndOfStream, "Actual had more data than expected." );
			}
			finally
			{
				expected.Close();
				actual.Close();
			}
		}
	}
}