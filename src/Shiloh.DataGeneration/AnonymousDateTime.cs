using System;


namespace Shiloh.DataGeneration
{
	public class AnonymousDateTime : AnonymousTypedBase< DateTime >
	{
		protected override DateTime GetRandomValue()
		{
			return GetRandomDateTime();
		}


		DateTime GetRandomDateTime()
		{
			return Between( ValueConstraints.MinDateTime, ValueConstraints.MaxDateTime );
		}


		public DateTime Between( DateTime min, DateTime max )
		{
			if ( max <= min )
				throw new ArgumentException( "Max must be greater than min." );

			double startTick = min.Ticks;
			double endTick = max.Ticks;
			double numberOfTicksInRange = endTick - startTick;
			double randomTickInRange = startTick + numberOfTicksInRange * Random.NextDouble();
			return new DateTime( Convert.ToInt64( randomTickInRange ) );
		}


		public DateTime PriorTo( DateTime priorToDateTime )
		{
			return Between( ValueConstraints.MinDateTime, priorToDateTime );
		}


		public DateTime Before( DateTime priorToDateTime )
		{
			return PriorTo( priorToDateTime );
		}


		public DateTime InPast()
		{
			return Before( DateTime.Now );
		}


		public DateTime InPastSince( DateTime startDate )
		{
			return Between( startDate, DateTime.Now );
		}


		public DateTime After( DateTime afterDateTime )
		{
			return Between( afterDateTime, ValueConstraints.MaxDateTime );
		}


		public DateTime InFuture()
		{
			return After( DateTime.Now );
		}
	}
}