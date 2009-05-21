using System;


namespace FluentObjectBuilder
{
	public abstract class TestDataBuilder< T > : ITestDataBuilder< T > where T : class
	{
		private static bool _executedBuildOnceAlready = false;
		private static int _uniqueId = -10;
		protected T _preBuiltResult;


		#region ITestDataBuilder<T> Members

		/// <summary>
		/// Either returns the override result, or builds this object.
		/// </summary>
		/// <returns></returns>
		public T build()
		{
			if ( _executedBuildOnceAlready )
				throw new Exception( "Cannot build more than once [" + GetType().FullName + "]." );

			return _preBuiltResult ?? _build();
			//_executedBuildOnceAlready = true;
		}

		#endregion


		/// <summary>
		/// Builds an object of type T based on the specs specified in the builder.
		/// </summary>
		/// <returns></returns>
		protected abstract T _build();


		/// <summary>
		/// Performs an implicit conversion from <see cref="BancVue.Tests.Common.TestDataBuilders.TestDataBuilder&lt;T&gt;"/> to <see cref="T"/>.
		/// This is so we don't have to explicitly call "build()" in the code.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator T( TestDataBuilder< T > builder )
		{
			return builder.build();
		}


		/// <summary>
		/// Gets a unique id with a negative value so that it does not conflict with existing data in the database.
		/// </summary>
		/// <returns></returns>
		protected int GetUniqueId()
		{
			return _uniqueId--;
		}


		/// <summary>
		/// Overrides the build result with the specified object. If this is called, the builder will not perform the build, but will rather, return the prebuilt result.
		/// </summary>
		/// <param name="buildResult">The build result.</param>
		public TestDataBuilder< T > AliasFor( T buildResult )
		{
			return UsePreBuiltResult( buildResult );
		}


		/// <summary>
		/// Overrides the build result with the specified object. If this is called, the builder will not perform the build, but will rather, return the prebuilt result.
		/// </summary>
		/// <param name="buildResult">The build result.</param>
		public TestDataBuilder< T > UsePreBuiltResult( T buildResult )
		{
			_preBuiltResult = buildResult;
			return this;
		}
	}
}