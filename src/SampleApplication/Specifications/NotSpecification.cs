namespace SampleApplication.Specifications
{
	public class NotSpecification<T> : Specification<T>
	{
		private readonly ISpecification< T > _spec;

		public NotSpecification( ISpecification<T> spec)
		{
			_spec = spec;
		}

		public override bool IsSatisfiedBy(T obj)
		{
			return !_spec.IsSatisfiedBy( obj );
		}
	}
}