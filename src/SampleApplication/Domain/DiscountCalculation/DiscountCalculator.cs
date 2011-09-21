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
namespace SampleApplication.Domain.DiscountCalculation
{
	public class DiscountCalculator
	{
		readonly IDiscountStrategy _discountStrategy;


		public DiscountCalculator( IDiscountStrategy discountStrategy )
		{
			_discountStrategy = discountStrategy;
		}


		public double CalculateDiscount( Order order )
		{
			// TODO: order.TotalAmount
			// TODO: discountStrategy.GetDiscount
			double discount = _discountStrategy.GetDiscount( order.TotalAmount );
			return discount * order.TotalAmount;
		}
	}
}