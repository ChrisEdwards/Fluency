using System;


namespace Fluency
{
	public class FluencyException : Exception
	{
		public FluencyException( string message )
				: base( message ) {}


		public FluencyException( string message, Exception innerException )
				: base( message, innerException ) {}
	}
}