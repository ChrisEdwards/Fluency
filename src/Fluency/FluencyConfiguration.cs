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
using Fluency.Conventions;
using Fluency.IdGenerators;


namespace Fluency
{
	public class FluencyConfiguration
	{
		internal Func< IIdGenerator > ConstructIdGenerator { get; set; }

		public List< IDefaultConvention > DefaultValueConventions { get; set; }

		readonly Dictionary< Type, IIdGenerator > _idGenerators = new Dictionary< Type, IIdGenerator >();


		public FluencyConfiguration()
		{
			// Initialize default values.
			ConstructIdGenerator = () => new StaticValueIdGenerator( 0 );
			DefaultValueConventions = new List< IDefaultConvention >();
		}


		/// <summary>
		/// Gets the id generator for the specified type..
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		internal IIdGenerator GetIdGenerator< T >()
		{
			if ( !_idGenerators.ContainsKey( typeof ( T ) ) )
				_idGenerators.Add( typeof ( T ), ConstructIdGenerator.Invoke() );

			return _idGenerators[typeof ( T )];
		}
	}
}