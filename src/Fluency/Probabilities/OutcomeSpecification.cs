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


namespace Fluency.Probabilities
{
	public class OutcomeSpecification< T >
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OutcomeSpecification{T}"/> class.
		/// </summary>
		/// <param name="percentChance">The percent chance.</param>
		/// <param name="outcome">The outcome.</param>
		/// <exception cref="ArgumentException"><c>ArgumentException</c>.</exception>
		public OutcomeSpecification( int percentChance, T outcome )
		{
			if ( percentChance > 100 )
				throw new ArgumentException( string.Format( "Percent Chance cannot exceed 100. Encountered Invalid Percent Chance of [{0}].", percentChance ) );
			if ( percentChance < 0 )
				throw new ArgumentException( string.Format( "Percent Chance cannot be less than 0, must be beween 1 and 100. Encountered Invalid Percent Chance of [{0}].", percentChance ) );

			PercentChance = percentChance;
			Outcome = outcome;
		}


		public int PercentChance { get; set; }
		public T Outcome { get; set; }
	}
}