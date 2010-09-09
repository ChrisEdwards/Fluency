using System;


namespace Shiloh.DataGeneration.ValueConstraints
{
	public class SqlServerDefaultValuesAndConstraints : IValueConstraints, IDefaultValues
	{
		static readonly DateTime _minimumValidDateTimeForSqlServer = DateTime.Parse( @"1/1/1753 12:00:00 AM" );


		#region IDefaultValues Members

		public DateTime DefaultDateTime
		{
			get { return _minimumValidDateTimeForSqlServer; }
		}

		#endregion


		#region IValueConstraints Members

		public DateTime MaxDateTime
		{
			get { return DateTime.MaxValue; }
		}

		public DateTime MinDateTime
		{
			get { return _minimumValidDateTimeForSqlServer; }
		}

		#endregion
	}
}