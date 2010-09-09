using Fluency.IdGenerators;
using NUnit.Framework;
using SpecUnit;


namespace Fluency.Tests.BuilderTests
{
	public class When_building_two_consecutive_objects : ContextSpecification
	{
		public class ClassWithId
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


		protected override void SharedContext()
		{
			Fluency.Initialize( x => x.IdGeneratorIsConstructedBy( () => new DecrementingIdGenerator( -1 ) ) );
		}
	}


	[ Concern( "FluentBuilder" ) ]
	public class When_the_two_objects_are_of_the_same_type : When_building_two_consecutive_objects
	{
		[ Observation ]
		public void Should_return_unique_ids()
		{
			ClassWithId object1 = new BuilderWithId().build();
			ClassWithId object2 = new BuilderWithId().build();

			Assert.That( object1.Id, Is.Not.EqualTo( object2.Id ) );
		}
	}


	[ Concern( "FluentBuilder" ) ]
	public class When_the_two_objects_are_of_the_different_types : When_building_two_consecutive_objects
	{
		public class DifferentClassWithId
		{
			public int Id { get; set; }
		}


		public class DifferentBuilderWithId : FluentBuilder< DifferentClassWithId >
		{
			protected override void SetupDefaultValues()
			{
				SetProperty( x => x.Id, GenerateNewId() );
			}
		}


		[ Observation ]
		public void Should_not_return_unique_ids()
		{
			ClassWithId object1 = new BuilderWithId().build();
			DifferentClassWithId object2 = new DifferentBuilderWithId().build();

			Assert.That( object1.Id, Is.EqualTo( object2.Id ) );
		}
	}
}