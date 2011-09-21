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


namespace SampleApplication.Domain
{
	public class Order
	{
		readonly IList< LineItem > _lineItems = new List< LineItem >();

		public virtual int Id { get; set; }
		public virtual Customer Customer { get; set; }

		public virtual DateTime OrderDate { get; set; }

		public virtual IList< LineItem > LineItems
		{
			get { return _lineItems; }
			set
			{
				LineItems.Clear();
				foreach ( LineItem lineItem in value )
					Add( lineItem );
			}
		}


		void Add( LineItem lineItem )
		{
			LineItems.Add( lineItem );
			lineItem.Order = this;
		}


		public virtual double TotalAmount
		{
			get { return LineItems.Sum( x => x.Amount ); }
		}
	}
}