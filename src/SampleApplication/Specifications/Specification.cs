using System;


namespace SampleApplication.Specifications
{
	public class Specification< T > : ISpecification< T >
	{
		private readonly Predicate< T > _pred;


		public Specification( Predicate< T > pred )
		{
			_pred = pred;
		}


		protected Specification() {}


		#region ISpecification<T> Members

		public virtual bool IsSatisfiedBy( T obj )
		{
			return _pred( obj );
		}


		public ISpecification< T > And( ISpecification< T > andSpec )
		{
			return new AndSpecification< T >( this, andSpec );
		}


		public ISpecification< T > Or( ISpecification< T > orSpec )
		{
			return new OrSpecification< T >( this, orSpec );
		}

		#endregion
	}
}