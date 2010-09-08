using System.Collections.Generic;


namespace Fluency
{
	public interface IFluentListBuilder< T > : IFluentBuilder< IList< T > > where T : class, new()
	{
		/// <summary>
		/// Adds a builder to build a specified item in the list.
		/// </summary>
		/// <param name="builder">The builder.</param>
		void Add( FluentBuilder< T > builder );


		/// <summary>
		/// Adds the specified  item directly to the list.
		/// </summary>
		/// <param name="directItem">The direct item.</param>
		void Add( T directItem );
	}
}