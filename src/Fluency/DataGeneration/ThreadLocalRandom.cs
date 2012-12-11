using System;
using System.Threading;


namespace Fluency.DataGeneration
{
	/// <summary> 
	/// Convenience class for dealing with randomness in a thread-safe manner.
	/// Taken from http://msmvps.com/blogs/jon_skeet/archive/2009/11/04/revisiting-randomness.aspx
	/// </summary> 
	public static class ThreadLocalRandom
	{
		static ThreadLocalRandom()
		{
			// Generate as random a seed as possible by using a GUID (which includes many components that help it be more random than the default seed)
			var guid = Guid.NewGuid().ToString( "N" ).Replace( "a", "" ).Replace( "b", "" ).Replace( "c", "" ).Replace( "d", "" ).Replace( "e", "" ).Replace( "f", "" );
			var seed = int.Parse( guid.Substring( 0, 5 ) );
			GlobalRandom = new Random( seed );
		}


		/// <summary> 
		/// Random number generator used to generate seeds, 
		/// which are then used to create new random number 
		/// generators on a per-thread basis. 
		/// </summary> 
		static readonly Random GlobalRandom = new Random();

		static readonly object GlobalLock = new object();

		/// <summary> 
		/// Random number generator 
		/// </summary> 
		static readonly ThreadLocal< Random > ThreadRandom = new ThreadLocal< Random >( NewRandom );


		/// <summary> 
		/// Creates a new instance of Random. The seed is derived 
		/// from a global (static) instance of Random, rather 
		/// than time. 
		/// </summary> 
		static Random NewRandom()
		{
			lock ( GlobalLock )
			{
				// Include threadId so that multiple appdomains don't get the same values.
				var seed = GlobalRandom.Next() + ( AppDomain.CurrentDomain.Id ^ ( Thread.CurrentThread.ManagedThreadId + 1 ) );
				return new Random( seed );
			}
		}


		/// <summary> 
		/// Returns an instance of Random which can be used freely 
		/// within the current thread. 
		/// </summary> 
		public static Random Instance { get { return ThreadRandom.Value; } }
	}
}