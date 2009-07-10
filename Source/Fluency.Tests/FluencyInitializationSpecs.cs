using Fluency.IdGenerators;
using FluentObjectBuilder;
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
	}
}