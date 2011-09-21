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


namespace SampleApplication.Domain.DiscountCalculation
{
	public class DiscountStrategyBuilder : ITieredDiscountStrategyBuilder_Where,
	                                       ITieredDiscountStrategyBuilder_Then,
	                                       ITieredDiscountStrategyBuilder_WhereOrBuild
	{
		readonly IList< DiscountTier > discountTiers = new List< DiscountTier >();
		DiscountTier _tierUnderConstruction;

		public ITieredDiscountStrategyBuilder_Where Where
		{
			get { return this; }
		}


		#region ITieredDiscountStrategyBuilder_Then Members

		public ITieredDiscountStrategyBuilder_WhereOrBuild GetDiscountOf( double percent )
		{
			_tierUnderConstruction.DiscountPercentage = percent;
			discountTiers.Add( _tierUnderConstruction );
			_tierUnderConstruction = null;
			return this;
		}

		#endregion


		#region ITieredDiscountStrategyBuilder_Where Members

		public ITieredDiscountStrategyBuilder_Then OrdersGreaterThanOrEqualTo( double amount )
		{
			_tierUnderConstruction = new DiscountTier
			                         	{
			                         			LowestQualifyingAmount = amount
			                         	};
			return this;
		}

		#endregion


		#region ITieredDiscountStrategyBuilder_WhereOrBuild Members

		public IDiscountStrategy Build()
		{
			return new TieredDiscountStrategy( discountTiers );
		}

		#endregion


		public static DiscountStrategyBuilder BuildTieredStrategy()
		{
			return new DiscountStrategyBuilder();
		}
	}
}