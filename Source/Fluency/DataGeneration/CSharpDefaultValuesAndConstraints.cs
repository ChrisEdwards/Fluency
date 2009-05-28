using System;


namespace FluentObjectBuilder.DataGeneration
{
	public class CSharpDefaultValuesAndConstraints : IValueConstraints, IDefaultValues
	{
		#region IDefaultValues Members

		public DateTime DefaultDateTime
		{
			get { return DateTime.MinValue; }
		}

		#endregion


		#region IValueConstraints Members

		public DateTime MaxDateTime
		{
			get { return DateTime.MaxValue; }
		}

		public DateTime MinDateTime
		{
			get { return DateTime.MinValue; }
		}

		#endregion
	}
}