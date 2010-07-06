using System;
using System.Collections.Generic;
using Fluency.Conventions;
using Fluency.IdGenerators;


namespace Fluency
{
	internal class FluencyConfiguration
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