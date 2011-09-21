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
using System.Linq;


namespace SampleApplication.Domain.DiscountCalculation
{
	public class TieredDiscountStrategy : IDiscountStrategy
	{
		readonly IList< DiscountTier > _discountTiers;


		public TieredDiscountStrategy( IList< DiscountTier > discountTiers )
		{
			_discountTiers = discountTiers;
		}


		#region IDiscountStrategy Members

		public double GetDiscount( double totalAmount )
		{
			foreach ( DiscountTier discountTier in _discountTiers.OrderBy( x => x.LowestQualifyingAmount ) )
			{
				if ( totalAmount >= discountTier.LowestQualifyingAmount )
					return discountTier.DiscountPercentage;
			}
			return 0.0;
		}

		#endregion
	}
}