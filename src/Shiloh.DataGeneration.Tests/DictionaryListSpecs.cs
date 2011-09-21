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
using System.Reflection;
using System.Resources;
using Machine.Specifications;


namespace Shiloh.DataGeneration.Tests
{
	public class DictionaryListSpecs
	{
		// TODO: Rewrite these tests for embedded resource files.
		/*
		[ Subject( typeof ( DictionaryList ) ) ]
		public class Given_a_dictionary_file
		{
			protected static string _filename = "dictionary.txt";
			protected static DictionaryList _dictionaryList = new DictionaryList();
			protected static string[] lines;

			Establish context = () =>
			                    	{
			                    		using ( var writer = new StreamWriter( _filename ) )
			                    		{
			                    			writer.WriteLine( "Item1" );
			                    			writer.WriteLine( "Item2" );
			                    		}
			                    	};

			Cleanup after_each = () => { if ( File.Exists( _filename ) ) File.Delete( _filename ); };
		}


		[ Subject( typeof ( DictionaryList ), "Indexer[]" ) ]
		public class When_accessing_a_dictionary_that_has_not_been_loaded_yet : Given_a_dictionary_file
		{
			Because of = () => lines = _dictionaryList["dictionary"];

			It should_return_all_the_items_from_the_dictionary_file = () =>
			                                                          	{
			                                                          		lines.Length.ShouldEqual( 2 );
			                                                          		lines[0].ShouldEqual( "Item1" );
			                                                          		lines[1].ShouldEqual( "Item2" );
			                                                          	};
		}


		[ Subject( typeof ( DictionaryList ), "Indexer[]" ) ]
		public class When_accessing_a_dictionary_that_has_already_been_loaded : Given_a_dictionary_file
		{
			Establish context = () =>
			                    	{
			                    		string[] dummy = _dictionaryList["dictionary"]; // Initially load the dictionary file.
			                    		File.Delete( _filename ); // Delete the file so it can't load from file again (proves it loaded from cache).
			                    	};

			Because of = () => lines = _dictionaryList["dictionary"];

			It should_return_all_the_items_from_memory_and_not_the_file = () =>
			                                                              	{
			                                                              		lines.Length.ShouldEqual( 2 );
			                                                              		lines[0].ShouldEqual( "Item1" );
			                                                              		lines[1].ShouldEqual( "Item2" );
			                                                              	};
		}
		*/


		[ Subject( typeof ( DictionaryList ), "Indexer[]" ) ]
		public class When_accessing_a_dictionary_with_a_missing_dictionary_file //: Given_a_dictionary_file
		{
			static Exception exception;
			protected static DictionaryList _dictionaryList = new DictionaryList( Assembly.GetExecutingAssembly() );
			protected static string[] lines;

			Because of = () => exception = Catch.Exception( () => lines = _dictionaryList["unknownDictionary"] );

			It should_fail = () => exception.ShouldBeOfType< MissingManifestResourceException >();
		}
	}
}


// ReSharper restore InconsistentNaming