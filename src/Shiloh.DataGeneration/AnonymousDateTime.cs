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