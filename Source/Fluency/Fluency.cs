using System;


namespace Fluency
{
	public static class Fluency
	{
		internal static FluencyConfiguration Configuration = new FluencyConfiguration();


		/// <summary>
		/// Initializes Fluency with the specified configuration.
		/// </summary>
		/// <param name="configurationAction">The configuration action.</param>
		public static void Initialize( Action< InitializationExpression > configurationAction )
		{
			lock ( typeof ( Fluency ) )
			{
				var expression = new InitializationExpression();
				configurationAction(expression);

				Configuration = expression.GetConfiguration();
			}
		}
	}
}