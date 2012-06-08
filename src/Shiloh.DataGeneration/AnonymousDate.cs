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
using Fluency.Utils;


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