

using Machine.Specifications;
using SharpTestsEx;

// ReSharper disable InconsistentNaming


namespace SampleApplication.Tests
{
	public class AnonymousSpecs
	{
		[ Subject( typeof ( TYPE_UNDER_TEST ) ) ]
		public class Given_
		{
			public static TYPE_UNDER_TEST _typeUnderTest;

			private Establish context = () => CONTEXT;
		}


		[ Subject( typeof ( TYPE_UNDER_TEST ) ) ]
		public class When_ : Given_
		{
			private Because of = () => BECAUSE

			private It should_ = () => ASSERTION
		}
	}
}


// ReSharper restore InconsistentNaming