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
	public class ProductBuilder : FluentBuilder< Product >
	{
		protected override void SetupDefaultValues()
		{
			SetProperty( x => x.Id, GenerateNewId() );
			SetProperty( x => x.Name, ARandom.Title( 100 ) );
			SetProperty( x => x.Description, ARandom.Text( 200 ) );
		}


		protected override Product BuildFrom( Product values )
		{
			return new Product
			       	{
			       			Id = values.Id,
			       			Name = values.Name,
			       			Description = values.Description
			       	};
		}
	}
}