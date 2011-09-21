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
using System.Collections.Generic;


namespace Shiloh.DataGeneration
{
	public class AnonymousInteger : AnonymousNumericBase< int >
	{
//		public int GreaterThan( int lowerBound )
//		{
//			return Random.Next( lowerBound + 1, int.MaxValue );
//		}


//		public int GreaterThanOrEqualTo( int lowerBound )
//		{
//			return Random.Next( lowerBound, int.MaxValue );
//		}


//		public int LessThanOrEqualTo( int upperBound )
//		{
//			return Random.Next( int.MinValue, upperBound );
//		}


//		public int LessThan( int upperBound )
//		{
//			return Random.Next( int.MinValue, upperBound - 1 );
//		}


		public int InRange(int lowerBound, int upperBound)
		{
			return BetweenExclusive(lowerBound, upperBound);
		}


		protected override int GetBetween( int lowerBound, int upperBound )
		{
			return Random.Next( lowerBound, upperBound );
		}


		protected override int GetRandomValue()
		{
			return Random.Next( int.MinValue, int.MaxValue );
		}
	}
}