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

using Fluency.IdGenerators;
using Machine.Specifications;
using NUnit.Framework;

// ReSharper disable InconsistentNaming


namespace Fluency.Tests.Deprecated.BuilderTests
{
	public class when_building_two_consecutive_objects
	{
		public class ClassWithId
		{
			public int Id { get; set; }
		}


		public class DifferentClassWithId
		{
			public int Id { get; set; }
		}


		public class BuilderWithId : FluentBuilder< ClassWithId >
		{
			protected override void SetupDefaultValues()
			{
				SetProperty( x => x.Id, GenerateNewId() );
			}
		}


		public class DifferentBuilderWithId : FluentBuilder< DifferentClassWithId >
		{
			protected override void SetupDefaultValues()
			{
				SetProperty( x => x.Id, GenerateNewId() );
			}
		}


		Establish context = () => Fluency.Initialize( x => x.IdGeneratorIsConstructedBy( () => new DecrementingIdGenerator( -1 ) ) );
	}


	[ Subject( "FluentBuilder" ) ]
	public class when_the_two_objects_are_of_the_same_type : when_building_two_consecutive_objects
	{
		It should_return_unique_ids = () =>
		                              	{
		                              		ClassWithId object1 = new BuilderWithId().build();
		                              		ClassWithId object2 = new BuilderWithId().build();

		                              		Assert.That( object1.Id, Is.Not.EqualTo( object2.Id ) );
		                              	};
	}


	[ Subject( "FluentBuilder" ) ]
	public class when_the_two_objects_are_of_different_types : when_building_two_consecutive_objects
	{
		It should_not_return_unique_ids = () =>
		                                  	{
		                                  		ClassWithId object1 = new BuilderWithId().build();
		                                  		DifferentClassWithId object2 = new DifferentBuilderWithId().build();

		                                  		Assert.That( object1.Id, Is.EqualTo( object2.Id ) );
		                                  	};
	}
}


// ReSharper restore InconsistentNaming