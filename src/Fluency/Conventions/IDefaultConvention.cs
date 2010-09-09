using System.Reflection;


namespace Fluency.Conventions
{
	public interface IDefaultConvention
	{
		bool AppliesTo( PropertyInfo propertyInfo );
		object DefaultValue( PropertyInfo propertyInfo );
	}


	public interface IDefaultConvention< T > : IDefaultConvention
	{
		new T DefaultValue( PropertyInfo propertyInfo );
	}
}