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
namespace Fluency.Probabilities
{
	public static class Probability
	{
		public static ProbabilitySpecification< T > Of< T >()
		{
			return new ProbabilitySpecification< T >();
		}


		public static bool WithPecentChanceOfTrue( int percentChanceOfTrue )
		{
			return Of< bool >()
					.PercentOutcome( percentChanceOfTrue, true )
					.GetOutcome();
		}
	}
}