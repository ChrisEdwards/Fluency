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