// Copyright 2011 Chris Edwards
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
namespace Shiloh.DataGeneration.ValueConstraints
{
	/// <summary>
	/// Defines the current set of value constraints (or range limits) for generating random data.
	/// </summary>
	/// <remarks>
	/// This is a static class because we need to ensure that the defaults and constraints used by the RandomGenerator
	/// to create anonymous test data are the same defaults and constraints used by the DbHelper to insert those values
	/// into the database. The main case for this is when a date value is supposed to be null, the min sql value is used (since
	/// it cannot be null in memory). When inserting the data..if the value is the min value, a DBNull is actually inserted. 
	/// The comparison against min value to determine if a DBNull should be inserted is the purpose for this class and the
	/// DefaultConstraints class.
	/// </remarks>
	public static class ValueConstraints
	{
		/// <summary>
		/// Sql Server's minimum date value is 1/1/1753 12:00:00 AM. DateTime.MinValue is much less than this number. 
		/// The RandomGenerator uses these constraints to generate data that we will insert into a Sql database, we 
		/// need to default to enforce Sql limits or we risk throwing an error by inserting an invalid date time value.
		/// </summary>
		static IValueConstraints _currentValueConstraints = new SqlServerDefaultValuesAndConstraints();


		/// <summary>
		/// Gets the current set of ValueConstraints to use in creating random data.
		/// </summary>
		/// <value>The get.</value>
		public static IValueConstraints Get
		{
			get { return _currentValueConstraints; }
		}


		/// <summary>
		/// Uses the specified ValueConstraints for all subsequent calls to Get.
		/// </summary>
		/// <param name="valueConstraints">The value constraints.</param>
		public static void Use( IValueConstraints valueConstraints )
		{
			_currentValueConstraints = valueConstraints;
		}
	}
}