namespace SampleApplication.Specifications
{
	public class OrSpecification< T > : Specification< T >
	{
		private readonly ISpecification< T > _left;
		private readonly ISpecification< T > _right;


		public OrSpecification( ISpecification< T > left, ISpecification< T > right )
		{
			_left = left;
			_right = right;
		}


		public override bool IsSatisfiedBy( T obj )
		{
			return ( _left.IsSatisfiedBy( obj ) || _right.IsSatisfiedBy( obj ) );
		}
	}
}