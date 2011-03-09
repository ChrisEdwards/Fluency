using System;
using Fluency.Utils;
using Shiloh.Utils;


namespace Shiloh.DataGeneration
{
	public class AnonymousDate : AnonymousTypedBase< DateTime >
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


		public DateTime PriorTo( DateTime priorToDate )
		{
			return _anonymousDateTime.Before( priorToDate ).Date;
		}


		public DateTime Before( DateTime priorToDate )
		{
			return PriorTo( priorToDate );
		}


		public DateTime InPast()
		{
			return _anonymousDateTime.InPast().Date;
		}


		public DateTime InPastSince(DateTime startDate)
		{
			return _anonymousDateTime.InPastSince(startDate).Date;
		}


		public DateTime InPastYear()
		{
			return _anonymousDateTime.InPastSince(1.YearsAgo()).Date;
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