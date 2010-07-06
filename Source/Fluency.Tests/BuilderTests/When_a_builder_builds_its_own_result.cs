using FluentObjectBuilder;
using SpecUnit;


namespace Fluency.Tests.BuilderTests
{
	public class When_a_builder_builds_its_own_result : ContextSpecification
	{
		protected object expectedResult;


		public class BuilderThatBuildsItsOwnResult : FluentBuilder< object >
		{
			protected override object BuildFrom( object values )
			{
				return new object();
			}
		}


		protected override void SharedContext()
		{
			expectedResult = new object();
		}
	}


	[ Concern( "FluentBuilder" ) ]
	public class When_the_builder_is_given_a_prebuilt_result : When_a_builder_builds_its_own_result
	{
		protected override void Context() {}


		[ Observation ]
		public void Should_return_the_prebuilt_result_directly()
		{
			object actualResult = new BuilderThatBuildsItsOwnResult().AliasFor( expectedResult ).build();
			actualResult.should_be( expectedResult );
		}
	}
}