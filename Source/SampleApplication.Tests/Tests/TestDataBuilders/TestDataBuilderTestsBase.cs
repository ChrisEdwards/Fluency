using NUnit.Framework;
using Rhino.Mocks;


namespace SampleApplication.Tests.Tests.TestDataBuilders
{
	public class TestDataBuilderTestsBase
	{
		protected MockRepository _mocks;


		[ SetUp ]
		public void SetUp()
		{
			_mocks = new MockRepository();
		}
	}
}