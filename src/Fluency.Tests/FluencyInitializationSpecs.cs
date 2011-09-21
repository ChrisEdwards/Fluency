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
using SpecUnit;


namespace Fluency.Tests
{
	public class FluencyInitializationSpecs
	{
		#region Test Builder

		public class TestItem
		{
			public int Id { get; set; }
		}


		public class TestItemBuilder : FluentBuilder< TestItem >
		{
			protected override void SetupDefaultValues()
			{
				SetProperty( x => x.Id, GenerateNewId() );
			}
		}

		#endregion


		public class FluencySpecsContext : ContextSpecification
		{
			protected TestItem item;

			protected override void SharedContext() {}


			protected override void Because()
			{
				item = new TestItemBuilder().build();
			}
		}


		[ Concern( "FluencyInitialization" ) ]
		public class when_fluency_is_configured_to_use_decrementing_ids : FluencySpecsContext
		{
			protected override void Context()
			{
				Fluency.Initialize( x => x.IdGeneratorIsConstructedBy( () => new DecrementingIdGenerator() ) );
			}


			[ Observation ]
			public void should_generate_a_negative_id_value()
			{
				item.Id.should_be_less_than( 0 );
			}
		}


		[ Concern( "FluencyInitialization" ) ]
		public class when_fluency_is_configured_to_use_zero_for_ids : FluencySpecsContext
		{
			protected override void Context()
			{
				Fluency.Initialize( x => x.IdGeneratorIsConstructedBy( () => new StaticValueIdGenerator( 0 ) ) );
			}


			[ Observation ]
			public void should_generate_a_zero_id_value()
			{
				item.Id.should_be_equal_to( 0 );
			}
		}


		[ Concern( "FluencyInitialization" ) ]
		public class when_no_id_generator_is_specified_for_fluency : FluencySpecsContext
		{
			protected override void Context()
			{
				Fluency.Initialize( x => x.IdGeneratorIsConstructedBy( () => new StaticValueIdGenerator( 0 ) ) );
			}


			[ Observation ]
			public void should_use_zero_id_value_generator_by_default()
			{
				item.Id.should_be_equal_to( 0 );
			}
		}


		[ Concern( "FluencyInitialization" ) ]
		public class when_no_default_value_conventions_are_specified : FluencySpecsContext
		{
			protected override void Context() {}


			[ Observation ]
			public void default_conventions_should_be_empty()
			{
				Fluency.Configuration.DefaultValueConventions.Count.should_be_equal_to( 0 );
			}
		}


		[ Concern( "FluencyInitialization" ) ]
		public class when_default_value_conventions_are_specified : FluencySpecsContext
		{
			protected override void Context()
			{
				Fluency.Initialize( x => x.UseDefaultValueConventions() );
			}


			[ Observation ]
			public void default_conventions_should_be_used()
			{
				Fluency.Configuration.DefaultValueConventions.Count.should_be_equal_to( 5 );
			}
		}
	}
}