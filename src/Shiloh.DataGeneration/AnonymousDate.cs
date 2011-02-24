using System;


namespace Shiloh.DataGeneration
{
	public class AnonymousDate : AnonymousBase< DateTime >
	{
		private AnonymousDateTime _anonymousDateTime = new AnonymousDateTime();

		protected override DateTime GetRandomValue()
		{
			return GetRandomDate();
		}


		DateTime GetRandomDate()
		{
			return Between( Anonymous.ValueConstraints.MinDateTime, Anonymous.ValueConstraints.MaxDateTime );
		}


		public DateTime Between( DateTime min, DateTime max )
		{
			return _anonymousDateTime.Between(min,max).Date;
		}


		public DateTime InPast()
		{
			return _anonymousDateTime.InPast().Date;
		}


		public DateTime InPastSince(DateTime startDate)
		{
			return _anonymousDateTime.InPastSince(startDate).Date;
		}

		public DateTime After(DateTime afterDate)
		{
			return _anonymousDateTime.After( afterDate ).Date;
		}

		public DateTime InFuture()
		{
			// Add a day to the result since the anonymous will have a time component. When we strip it off, it could be in the past if the date is today.
			return _anonymousDateTime.InFuture().Date.AddDays( 1 );
		}
	}
}