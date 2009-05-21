using System;


namespace BancVue.Tests.Common.DefaultValuesAndConstraints
{
    public class SqlServerDefaultValuesAndConstraints : IValueConstraints, IDefaultValues
    {
        private static readonly DateTime _minimumValidDateTimeForSqlServer = DateTime.Parse( @"1/1/1753 12:00:00 AM" );


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