using System.Collections.Generic;
using System.Linq;


namespace Fluency
{
	public class FluentListBuilder< T > : IFluentListBuilder< T > where T : class
	{
		readonly IList< IFluentBuilder< T > > _builders = new List< IFluentBuilder< T > >();
		readonly IList< T > _directItems = new List< T >();


		#region IFluentBuilder<IList<T>> Members

		/// <summary>
		/// Builds a list of all the items specified directly combined with a list of all items built by the specifed builders.
		/// </summary>
		/// <returns></returns>
		public virtual IList< T > build()
		{
			// Build each builder's item and gather into collection.
			IEnumerable< T > builtItems =
					from builder in _builders
					select builder.build();

			// Merge with items added directly (without a builder)
			IEnumerable< T > allItems = _directItems.Concat( builtItems );

			// Return these as a list.
			return new List< T >( allItems );
		}

		#endregion


		/// <summary>
		/// Adds a builder to build a specified item in the list.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public void Add( IFluentBuilder< T > builder )
		{
			_builders.Add( builder );
		}


		/// <summary>
		/// Adds the specified  item directly to the list.
		/// </summary>
		/// <param name="directItem">The direct item.</param>
		public void Add( T directItem )
		{
			_directItems.Add( directItem );
		}
	}
}