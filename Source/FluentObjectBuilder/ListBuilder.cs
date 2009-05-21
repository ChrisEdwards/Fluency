using System.Collections.Generic;
using System.Linq;


namespace FluentObjectBuilder
{
	public class ListBuilder< T > : ITestDataBuilder< List< T > > where T : class
	{
		private readonly IList< ITestDataBuilder< T > > _builders = new List< ITestDataBuilder< T > >();
		private readonly IList< T > _directItems = new List< T >();


		#region ITestDataBuilder<List<T>> Members

		/// <summary>
		/// Builds a list of all the items specified directly combined with a list of all items built by the specifed builders.
		/// </summary>
		/// <returns></returns>
		public List< T > build()
		{
			// Build each builder's item and gather into collection.
			IEnumerable< T > builtItems =
					from builder in _builders
					select builder.build();

			// Merge with items added directly (without a builder)
			IEnumerable< T > allItems = _directItems.Union( builtItems );

			// Return these as a list.
			return new List< T >( allItems );
		}

		#endregion


		/// <summary>
		/// Adds a builder to build a specified item in the list.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public void Add( TestDataBuilder< T > builder )
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