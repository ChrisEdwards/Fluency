using System;


namespace Fluency
{
	public static class Fluency
	{
		internal static FluencyConfiguration Configuration = new FluencyConfiguration();


		public static void Initialize( Action< InitializationExpression > action )
		{
			lock ( typeof ( Fluency ) )
			{
				var expression = new InitializationExpression();
				action( expression );

				Configuration = expression.GetConfiguration();
			}
		}
	}
}