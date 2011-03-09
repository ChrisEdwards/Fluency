// ReSharper disable InconsistentNaming


using System.Collections.Generic;
using System.IO;
using System.Linq;
using Machine.Specifications;


namespace Shiloh.Utils.Tests
{
	public class IOSpecs
	{
		[ Subject( typeof ( IO ), "ForEachLineIn" ) ]
		public class When_iterating_over_all_the_lines_in_a_file
		{
			static List< string > lines;

			Establish context = () =>
			                    	{
			                    		using ( var writer = new StreamWriter( "test.txt" ) )
			                    		{
			                    			writer.WriteLine( "Line1" );
			                    			writer.WriteLine( "Line2" );
			                    		}
			                    	};

			Because of = () => lines = IO.ForEachLineIn( "test.txt" ).ToList();

			It should_return_all_the_lines = () =>
			                                 	{
			                                 		lines.Count().ShouldEqual( 2 );
			                                 		lines[0].ShouldEqual( "Line1" );
			                                 		lines[1].ShouldEqual( "Line2" );
			                                 	};

			Cleanup after_each = () => File.Delete( "test.txt" );
		}
	}
}


// ReSharper restore InconsistentNaming