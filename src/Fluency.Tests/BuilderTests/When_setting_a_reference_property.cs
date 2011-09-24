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
using Machine.Specifications;
using NUnit.Framework;

// ReSharper disable InconsistentNaming


namespace Fluency.Tests.BuilderTests
{
	public class when_setting_a_reference_property
	{
		public class ReferenceType
		{
			public string Name { get; set; }
		}


		public class ClassWithReferenceProperty
		{
			public ReferenceType ReferenceProperty { get; set; }
		}


		protected static ReferenceType _expectedValue;

		Establish context = () => _expectedValue = new ReferenceType();
	}


	[ Subject( "FluentBuilder" ) ]
	public class when_setting_a_reference_property_with_no_default_builder : when_setting_a_reference_property
	{
		public class BuilderWithReferenceProperty_WithNoDefaultBuilder : FluentBuilder< ClassWithReferenceProperty >
		{
			public BuilderWithReferenceProperty_WithNoDefaultBuilder With( ReferenceType value )
			{
				SetProperty( x => x.ReferenceProperty, value );
				return this;
			}
		}


		It should_return_the_same_reference = () =>
		                                      	{
		                                      		var builder = new BuilderWithReferenceProperty_WithNoDefaultBuilder();
		                                      		ClassWithReferenceProperty instance = builder.With( _expectedValue ).build();
		                                      		Assert.That( instance.ReferenceProperty, Is.SameAs( _expectedValue ) );
		                                      	};
	}


	[ Subject( "FluentBuilder" ) ]
	public class when_setting_a_reference_property_with_a_default_builder : when_setting_a_reference_property
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


		It should_return_the_same_reference = () =>
		                                      	{
		                                      		var builder = new BuilderWithReferenceProperty_WithDefaultBuilder();
		                                      		ClassWithReferenceProperty instance = builder.With( _expectedValue ).build();
		                                      		instance.ReferenceProperty.should_be( _expectedValue );
		                                      	};
	}
}


// ReSharper restore InconsistentNaming