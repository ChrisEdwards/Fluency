using FluentObjectBuilder;
using SpecUnit;


namespace Fluency.Tests.BuilderTests
{
	public class When_setting_a_reference_property : ContextSpecification
	{
		protected ReferenceType expectedValue;


		public class ReferenceType
		{
			public string Name { get; set; }
		}


		public class ClassWithReferenceProperty
		{
			public ReferenceType ReferenceProperty { get; set; }
		}


		protected override void SharedContext()
		{
			expectedValue = new ReferenceType();
		}
	}


	[ Concern( "FluentBuilder" ) ]
	public class When_setting_a_reference_property_with_no_default_builder : When_setting_a_reference_property
	{
		public class BuilderWithReferenceProperty_WithNoDefaultBuilder : FluentBuilder< ClassWithReferenceProperty >
		{
			public BuilderWithReferenceProperty_WithNoDefaultBuilder With( ReferenceType value )
			{
				SetProperty( x => x.ReferenceProperty, value );
				return this;
			}
		}


		[ Observation ]
		public void Build_should_return_the_same_reference()
		{
			var builder = new BuilderWithReferenceProperty_WithNoDefaultBuilder();
			ClassWithReferenceProperty instance = builder.With( expectedValue ).build();
			instance.ReferenceProperty.ShouldBeTheSameAs( expectedValue );
		}
	}


	public class When_setting_a_reference_property_with_default_builder : When_setting_a_reference_property
	{
		public class BuilderWithReferenceProperty_WithDefaultBuilder : FluentBuilder< ClassWithReferenceProperty >
		{
			protected override void SetupDefaultValues()
			{
				SetProperty( x => x.ReferenceProperty, new FluentBuilder< ReferenceType >() );
			}


			public BuilderWithReferenceProperty_WithDefaultBuilder With( ReferenceType value )
			{
				SetProperty( x => x.ReferenceProperty, value );
				return this;
			}
		}


		[ Observation ]
		public void Build_should_return_the_same_reference()
		{
			var builder = new BuilderWithReferenceProperty_WithDefaultBuilder();
			ClassWithReferenceProperty instance = builder.With( expectedValue ).build();
			instance.ReferenceProperty.should_be( expectedValue );
		}
	}
}