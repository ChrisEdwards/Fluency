using System;
using System.Reflection;

// Much of this was taken from the UMMO project https://github.com/stephen-czetty/UMMO/blob/master/src/UMMO.TestingUtils/RandomData/RandomNumericType.cs


namespace Shiloh.DataGeneration
{
	public abstract class AnonymousNumericBase< T > : AnonymousBase< T >
			where T : struct, IComparable< T >
	{
		readonly T _maxValue;
		readonly T _minValue;


		protected AnonymousNumericBase()
		{
			FieldInfo minValueField = typeof ( T ).GetField( "MinValue", BindingFlags.Static | BindingFlags.Public );
			FieldInfo maxValueField = typeof ( T ).GetField( "MaxValue", BindingFlags.Static | BindingFlags.Public );

			// Check if the type is is compatible with this class.
			if ( minValueField == null || maxValueField == null )
				throw new ArgumentException( String.Format( "Could not create random data for type {0}", typeof ( T ).FullName ) );

			_minValue = (T)minValueField.GetValue( new T() );
			_maxValue = (T)maxValueField.GetValue( new T() );
		}


		/// <summary>
		/// Return a value between <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
		/// </summary>
		/// <param name="lowerBound">The minimum value.</param>
		/// <param name="upperBound">The maximum value.</param>
		/// <returns>A random value of type <typeparamref name="T"/></returns>
		/// <exception cref="ArgumentException">lowerBound must be less than or equal to upperBound</exception>
		public T Between( T lowerBound, T upperBound )
		{
			if ( lowerBound.CompareTo( upperBound ) > 0 )
				throw new ArgumentException( "lowerBound must be less than or equal to upperBound" );

			return GetBetween( lowerBound, upperBound );
		}


		/// <summary>
		/// Return a value between <paramref name="lowerBound"/> and <paramref name="upperBound"/> 
		/// that is not equal to either <param name="lowerBound" /> or <param name="upperBound"/>.
		/// </summary>
		/// <param name="lowerBound">The minimum value.</param>
		/// <param name="upperBound">The maximum value.</param>
		/// <returns>A random value of type <typeparamref name="T"/></returns>
		/// <exception cref="ArgumentException">The lower bound must be greater than the upper bound.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><c></c> is out of range.</exception>
		public T BetweenExclusive( T lowerBound, T upperBound )
		{
			if ( lowerBound.CompareTo(upperBound) >= 0 )
				throw new ArgumentException( "The lower bound must be greater than the upper bound." );
			
			T value;
			int tries = 0;

			// Try 3 times to find a value between the upper and lower bounds that is not equal to either of the bounds.
			do
			{
				value = GetBetween(lowerBound, upperBound);

				// If we exceeded 3 tries, fail.
				if (++tries > 3)
					throw new ArgumentOutOfRangeException(string.Format("Tried 3 times to find a value between {0} and {1} but could not. Perhaps the bounds are incorrect.", lowerBound, upperBound));

			} while (value.CompareTo(lowerBound) == 0 || value.CompareTo(upperBound) == 0);

			return value;
			//return GetBetweenExclusive( lowerBound, upperBound );
		}


		/// <summary>
		/// Return a value greater than <paramref name="upperBound"/>.
		/// </summary>
		/// <param name="upperBound">The min.</param>
		/// <returns>A random value of type <typeparamref name="T"/></returns>
		public T GreaterThan( T upperBound )
		{
			T value = Between( upperBound, _maxValue );

			// If equal to lowerBound, retry. This is not the GreaterThanOrEqualTo method.
			if ( value.CompareTo( upperBound ) == 0 )
				return GreaterThan( upperBound );

			return value;
		}


		/// <summary>
		/// Return a value greater than or equal to <paramref name="lowerBound"/>.
		/// </summary>
		/// <param name="lowerBound">The min.</param>
		/// <returns>A random value of type <typeparamref name="T"/></returns>
		public T GreaterThanOrEqualTo( T lowerBound )
		{
			return Between( lowerBound, _maxValue );
		}


		/// <summary>
		/// Return a value less than <paramref name="upperBound"/>
		/// </summary>
		/// <param name="upperBound">The maximum value.</param>
		/// <returns>A random value of type <typeparamref name="T"/></returns>
		public T LessThan( T upperBound )
		{
			T value = Between( _minValue, upperBound );

			// If equal to lowerBound, retry. This is not the LessThanOrEqualTo method.
			if ( value.CompareTo( upperBound ) == 0 )
				return LessThan( upperBound );

			return value;
		}


		/// <summary>
		/// Return a value less than or equal to<paramref name="maxValue"/>
		/// </summary>
		/// <param name="maxValue">The maximum value.</param>
		/// <returns>A random value of type <typeparamref name="T"/></returns>
		public T LessThanOrEqualTo( T maxValue )
		{
			return Between( _minValue, maxValue );
		}


		/// <summary>
		/// Return a random value of type <typeparamref name="T"/> between the minimum and maximum.
		/// </summary>
		/// <param name="lowerBound">The minimum value.</param>
		/// <param name="upperBound">The maximum value.</param>
		/// <returns></returns>
		protected abstract T GetBetween(T lowerBound, T upperBound);
	}
}