namespace SampleApplication.Specifications
{
	public class AndSpecification< T > : Specification< T >
	{
		private readonly ISpecification< T > _spec;
		private readonly ISpecification< T > _spec1;


		public AndSpecification( ISpecification< T > spec1, ISpecification< T > spec )
		{
			_spec1 = spec1;
			_spec = spec;
		}


		public override bool IsSatisfiedBy( T obj )
		{
			return ( _spec.IsSatisfiedBy( obj ) && _spec1.IsSatisfiedBy( obj ) );
		}
	}
}