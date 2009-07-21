using System;
using System.Collections.Generic;
using Fluency.IdGenerators;


namespace Fluency
{
	public static class Fluency
	{
		internal static FluencyConfiguration Configuration = new FluencyConfiguration();

		public static void Initialize( Action< InitializationExpression > action )
		{
			lock (typeof(Fluency))
			{
				var expression = new InitializationExpression();
				action(expression);

				Configuration = expression.GetConfiguration();
			}
		}
	}

	internal class FluencyConfiguration
	{
		internal Func<IIdGenerator> ConstructIdGenerator { get; set; }
		private Dictionary<Type, IIdGenerator> _idGenerators = new Dictionary< Type, IIdGenerator >();

		public FluencyConfiguration()
		{
			// Initialize default values.
			ConstructIdGenerator= () => new StaticValueIdGenerator(0);
		}


		internal IIdGenerator GetIdGenerator< T >()
		{
			if (!_idGenerators.ContainsKey(typeof(T)))
				_idGenerators.Add( typeof(T), ConstructIdGenerator.Invoke() );

			return _idGenerators[typeof ( T )];
		}
	}

	public class InitializationExpression
	{
		readonly FluencyConfiguration config = new FluencyConfiguration();

		internal FluencyConfiguration GetConfiguration()
		{
			return config;
		}

		public void IdGeneratorIsConstructedBy( Func< IIdGenerator > func )
		{
			config.ConstructIdGenerator = func;
		}
	}

}