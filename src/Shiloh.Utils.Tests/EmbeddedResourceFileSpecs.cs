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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using Machine.Specifications;


namespace Shiloh.Utils.Tests
{
	public class EmbeddedResourceFileSpecs
	{
		[ Subject( typeof ( EmbeddedResourceFile ) ) ]
		public class Given_an_embedded_resource_file
		{
			protected static string embeddedResourceName = "Shiloh.Utils.Tests.TestContent.txt";
			/* The contents of this file (which is an embedded resource in this assembly) are the following two lines:
			 * Line1
			 * Line2
			 */

			protected static Assembly thisAssembly = typeof ( EmbeddedResourceFileSpecs ).Assembly;
		}


		[ Subject( typeof ( EmbeddedResourceFile ), "ForEachLineIn" ) ]
		public class When_iterating_over_all_the_lines_in_an_embedded_resource_file : Given_an_embedded_resource_file
		{
			static List< string > lines;

			Because of = () => lines = EmbeddedResourceFile.ForEachLineIn( embeddedResourceName, thisAssembly ).ToList();

			It should_contain_the_lines_of_the_embedded_file = () =>
			                                                   	{
			                                                   		lines.Count().ShouldEqual( 2 );
			                                                   		lines[0].ShouldEqual( "Line1" );
			                                                   		lines[1].ShouldEqual( "Line2" );
			                                                   	};
		}


		[ Subject( typeof ( EmbeddedResourceFile ), "GetReader" ) ]
		public class When_getting_a_stream_reader_for_an_existing_embedded_resource_file : Given_an_embedded_resource_file
		{
			static StreamReader result;

			Because of = () => result = EmbeddedResourceFile.GetReader( embeddedResourceName, thisAssembly );

			It should_return_an_open_stream_reader = () => result.EndOfStream.ShouldBeFalse();
		}


		[ Subject( typeof ( EmbeddedResourceFile ), "GetReader" ) ]
		public class When_getting_a_stream_reader_for_an_embedded_resource_file_that_does_not_exist : Given_an_embedded_resource_file
		{
			static Exception exception;

			Because of = () => exception = Catch.Exception( () => EmbeddedResourceFile.GetReader( "unknownFile", thisAssembly ) );

			It should_fail_with_a_meaningful_error = () => exception.ShouldBeOfType< MissingManifestResourceException >();
		}


		[ Subject( typeof ( EmbeddedResourceFile ), "GetDefaultResourceNamespace" ) ]
		public class When_getting_the_default_resource_namespace_of_an_assembly : Given_an_embedded_resource_file
		{
			static string resourceName;

			Because of = () => resourceName = EmbeddedResourceFile.GetDefaultResourceNamespace( thisAssembly );
			It should_return_the_name_of_the_assembly = () => resourceName.ShouldEqual( "Shiloh.Utils.Tests" );
		}
	}
}


// ReSharper restore InconsistentNaming