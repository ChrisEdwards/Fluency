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
using System.Linq;
using System.Reflection;
using Shiloh.Utils;


namespace Shiloh.DataGeneration
{
	public class DictionaryList
	{
		readonly Dictionary< string, string[] > _dictionaries = new Dictionary< string, string[] >();
		readonly Assembly _dictionarySource;
		readonly string _baseResourceNamespace;


		/// <summary>
		/// Initializes a new instance of the <see cref="DictionaryList"/> class.
		/// </summary>
		/// <param name="dictionarySource">The assembly containing the dictionary files as embedded resource files.</param>
		/// <param name="baseResourceNamespace">The namespace to pre-pend to the filename to get the resource name.</param>
		public DictionaryList(Assembly dictionarySource, string baseResourceNamespace)
		{
			_dictionarySource = dictionarySource;
			_baseResourceNamespace = baseResourceNamespace;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="DictionaryList"/> class.
		/// </summary>
		/// <param name="dictionarySource">The assembly containing the dictionary files as embedded resource files.</param>
		public DictionaryList(Assembly dictionarySource )
		{
			_dictionarySource = dictionarySource;

			// Get the default resource namespace.
			_baseResourceNamespace = EmbeddedResourceFile.GetDefaultResourceNamespace( dictionarySource );
		}


		public string[] this[ string dictionaryName ]
		{
			get
			{
				if ( _dictionaries.ContainsKey( dictionaryName ) )
					return _dictionaries[dictionaryName];

				string[] strings = EmbeddedResourceFile.ForEachLineIn( _baseResourceNamespace +"."+ dictionaryName + ".txt", _dictionarySource ).ToArray();
				string[] values = strings;
				_dictionaries.Add( dictionaryName, values );

				return values;
			}
		}
	}
}