namespace Fluency.Probabilities
{
	public static class Probability
	{
		public static ProbabilitySpecification< T > Of< T >()
		{
			return new ProbabilitySpecification< T >();
		}


		public static bool WithPecentChanceOfTrue( int percentChanceOfTrue )
		{
			return Of< bool >()
					.PercentOutcome( percentChanceOfTrue, true )
					.GetOutcome();
		}
	}
}