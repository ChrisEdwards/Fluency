using System;


namespace Fluency
{
	public static class Fluency
	{
		public static FluencyConfiguration Configuration = new FluencyConfiguration();


		/// <summary>
		/// Initializes Fluency with the specified configuration.
		/// </summary>
		/// <param name="configurationAction">The configuration action.</param>
		public static void Initialize( Action< InitializationExpression > configurationAction )
		{
			lock ( typeof ( Fluency ) )
			{
				// Execute the user-defined configuration code against the initialization expression and get the configuration data from it.
				var expression = new InitializationExpression();
				configurationAction( expression );

				Configuration = expression.GetConfiguration();
			}
		}
	}
}