using System;


namespace Shiloh.DataGeneration
{
	public class AnonymousDate : AnonymousBase< DateTime >
	{
		protected override DateTime GetRandomValue()
		{
			return GetRandomDate();
		}


		DateTime GetRandomDate()
		{
			return DateTimeBetween( Anonymous.ValueConstraints.MinDateTime, Anonymous.ValueConstraints.MaxDateTime );
		}


		public static DateTime DateTimeBetween( DateTime min, DateTime max )
		{
			if ( max <= min )
				throw new ArgumentException( "Max must be greater than min." );

			double startTick = min.Ticks;
			double endTick = max.Ticks;
			double numberOfTicksInRange = endTick - startTick;
			double randomTickInRange = startTick + numberOfTicksInRange * _random.NextDouble();
			return new DateTime( Convert.ToInt64( randomTickInRange ) ).Date;
		}
	}
}