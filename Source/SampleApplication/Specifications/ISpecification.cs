namespace SampleApplication.Specifications
{
	public interface ISpecification< T >
	{
		bool IsSatisfiedBy( T obj );
		ISpecification< T > And( ISpecification< T > lhs );
		ISpecification< T > Or( ISpecification< T > orSpec );
	}
}