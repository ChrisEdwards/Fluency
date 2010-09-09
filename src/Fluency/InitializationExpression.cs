using System;
using System.Collections.Generic;
using Fluency.Conventions;
using Fluency.IdGenerators;


namespace Fluency
{
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


		public void UseDefaultValueConventions()
		{
			config.DefaultValueConventions = new List< IDefaultConvention >
			                                 	{
			                                 			Convention.DateType(),
			                                 			Convention.IntegerType(),
			                                 			Convention.String( 20 ),
			                                 			Convention.FirstName(),
			                                 			Convention.LastName()
			                                 	};
		}
	}
}