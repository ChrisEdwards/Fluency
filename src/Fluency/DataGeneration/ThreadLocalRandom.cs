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
        /// <summary> 
        /// Random number generator used to generate seeds, 
        /// which are then used to create new random number 
        /// generators on a per-thread basis. 
        /// </summary> 
        private static readonly Random GlobalRandom = new Random(); 
        private static readonly object GlobalLock = new object(); 

        /// <summary> 
        /// Random number generator 
        /// </summary> 
        private static readonly ThreadLocal<Random> ThreadRandom = new ThreadLocal<Random>(NewRandom); 

        /// <summary> 
        /// Creates a new instance of Random. The seed is derived 
        /// from a global (static) instance of Random, rather 
        /// than time. 
        /// </summary> 
        private static Random NewRandom() 
        { 
            lock (GlobalLock) 
            { 
                return new Random(GlobalRandom.Next()); 
            } 
        } 

        /// <summary> 
        /// Returns an instance of Random which can be used freely 
        /// within the current thread. 
        /// </summary> 
        public static Random Instance { get { return ThreadRandom.Value; } } 
    } 
}