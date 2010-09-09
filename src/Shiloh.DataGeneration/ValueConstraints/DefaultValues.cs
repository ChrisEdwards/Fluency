namespace Shiloh.DataGeneration.ValueConstraints
{
	/// <summary>
	/// Defines the current set of default values used for data.
	/// </summary>
	/// <remarks>
	/// This is a static class because we need to ensure that the defaults and constraints used by the RandomGenerator
	/// to create anonymous test data are the same defaults and constraints used by the DbHelper to insert those values
	/// into the database. The main case for this is when a date value is supposed to be null, the min sql value is used (since
	/// it cannot be null in memory). When inserting the data..if the value is the min value, a DBNull is actually inserted. 
	/// The comparison against min value to determine if a DBNull should be inserted is the purpose for this class and the
	/// DefaultConstraints class.
	/// </remarks>
	public static class DefaultValues
	{
		// Default to using SqlServer Default Values.
		static IDefaultValues _currentDefaultValues = new SqlServerDefaultValuesAndConstraints();


		/// <summary>
		/// Gets the current set of values to use as defaults.
		/// </summary>
		/// <value>The get.</value>
		public static IDefaultValues Get
		{
			get { return _currentDefaultValues; }
		}


		/// <summary>
		/// Uses the specified default values for all subsequent calls to Get.
		/// </summary>
		/// <param name="defaultValues">The default values.</param>
		public static void Use( IDefaultValues defaultValues )
		{
			_currentDefaultValues = defaultValues;
		}
	}
}