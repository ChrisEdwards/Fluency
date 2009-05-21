using BancVue.Domain.CoreVue;
using BancVue.Tests.Common.TestDataBuilders;
using NUnit.Framework;


namespace BancVue.Tests.Common.Tests.TestDataBuilders
{
    [ TestFixture ]
    public class ConfigBuilderTests : TestDataBuilderTestsBase
    {
        [ Test ]
        public void Build_creates_anonymous_config()
        {
            Config config = new ConfigBuilder().build();

            Assert.IsNotNull( config );
            Assert.IsTrue( config.ConfigId < 0 );
            Assert.IsTrue( config.InstitutionId == 0 );
        }
    }
}