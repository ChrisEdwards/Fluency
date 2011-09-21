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
using Fluency;
using Fluency.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.FluentBuilders
{
	public class LineItemBuilder : FluentBuilder< LineItem >
	{
		public LineItemBuilder And
		{
			get { return this; }
		}


		protected override void SetupDefaultValues()
		{
			// TODO: Setup AutoPopulation of random values by type. Need a way to override defaults.
			SetProperty( x => x.Id, GenerateNewId() );
			SetProperty( x => x.UnitPrice, (double)ARandom.CurrencyAmount() );
			SetProperty( x => x.Quantity, 1 );
			SetProperty( x => x.Product, new ProductBuilder() );
			SetProperty( x => x.Order, new OrderBuilder() );
		}


		protected override LineItem BuildFrom( LineItem values )
		{
			// TODO: Use Automapper
			return new LineItem
			       	{
			       			Id = values.Id,
			       			Order = values.Order,
			       			Product = values.Product,
			       			Quantity = values.Quantity,
			       			UnitPrice = values.UnitPrice
			       	};
		}


		public LineItemBuilder For( FluentBuilder< Product > productBuilder )
		{
			SetProperty( x => x.Product, productBuilder );
			return this;
		}


		public LineItemBuilder Costing( double unitPrice )
		{
			SetProperty( x => x.UnitPrice, unitPrice );
			return this;
		}


		public LineItemBuilder WithQuantity( int howMany )
		{
			SetProperty( x => x.Quantity, howMany );
			return this;
		}


		public LineItemBuilder UnitPriceOf( double unitPrice )
		{
			SetProperty( x => x.UnitPrice, unitPrice );
			return this;
		}
	}
}