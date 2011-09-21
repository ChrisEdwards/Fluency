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
using System.Collections.Generic;
using System.Linq;
using Fluency.DataGeneration;


namespace Fluency.Probabilities
{
	public class ProbabilitySpecification< T >
	{
		readonly List<OutcomeSpecification< T >> _outcomes = new List< OutcomeSpecification< T > >();


		/// <summary>
		/// Adds a new possible outcome specifying what the percent chance it is for that outcome.
		/// </summary>
		/// <param name="percentChance">The percent chance.</param>
		/// <param name="outcome">The outcome.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"><c>ArgumentException</c>.</exception>
		public ProbabilitySpecification< T > PercentOutcome( int percentChance, T outcome)
		{
			int totalPercentage = _outcomes.Sum( x => x.PercentChance ) + percentChance;
			if (totalPercentage > 100)
				throw new ArgumentException(string.Format( "The total percentage chances specified for this probability has exceeded 100 percent. The total percent is [{0}].",totalPercentage) );
			
			_outcomes.Add( new OutcomeSpecification< T >( percentChance, outcome ) );
			return this;
		}


		public IEnumerable< OutcomeSpecification< T > > Outcomes
		{
			get { return _outcomes; }
		}


		public T GetOutcome()
		{
			int random = ARandom.IntBetween( 1, 100 );

			// Determine which chance's percentile the random number falls in to and return its outcome.
			int threshold = 0;
			foreach ( var outcome in _outcomes )
			{
				threshold += outcome.PercentChance;

				if (random <= threshold)
					return outcome.Outcome;
			}

			// No outcomes matched, return default value for the type.
			return default(T);
		}
	}
}