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
using Fluency.DataGeneration;
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
			DefaultValueConventions = new List<IDefaultConvention>
			                                 	{
			                                 			// Strings
			                                 			Convention.ByName( "Firstname", p=> ARandom.FirstName() ),
			                                 			Convention.ByName( "LastName", p => ARandom.LastName() ),
			                                 			Convention.ByName( "FullName", p => ARandom.FullName() ),
			                                 			Convention.ByName( "City", p => ARandom.City() ),
			                                 			Convention.ByName( "State", p => ARandom.StateCode() ),
			                                 			Convention.ByName( "Zip", p => ARandom.ZipCode() ),
			                                 			Convention.ByName( "ZipCode", p => ARandom.ZipCode() ),
			                                 			Convention.ByName( "PostalCode", p => ARandom.ZipCode() ),
			                                 			Convention.ByName( "Email", p => ARandom.Email() ),
			                                 			Convention.ByName( "Address", p => String.Format( "{0} {1} {2}",
			                                 			                                                  ARandom.IntBetween( 10, 9999 ),
			                                 			                                                  ARandom.LastName(),
			                                 			                                                  ARandom.ItemFrom( "Street", "Lane", "Ave.", "Blvd." ) ) ),
			                                 			Convention.ByName( "Phone", p => ARandom.StringPattern( "999-999-9999" ) ),
			                                 			Convention.ByName( "HomePhone", p => ARandom.StringPattern( "999-999-9999" ) ),
			                                 			Convention.ByName( "WorkPhone", p => ARandom.StringPattern( "999-999-9999" ) ),
			                                 			Convention.ByName( "BusinessPhone", p => ARandom.StringPattern( "999-999-9999" ) ),
			                                 			Convention.ByName( "Fax", p => ARandom.StringPattern( "999-999-9999" ) ),
			                                 			Convention.String( 20 ),
			                                 			// Dates
			                                 			Convention.ByName( "BirthDate", p => ARandom.BirthDate() ),
			                                 			Convention.DateType(),
			                                 			Convention.IntegerType(),
														Convention.ByType< Decimal>( p => ARandom.CurrencyAmount()  )
			                                 	};
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