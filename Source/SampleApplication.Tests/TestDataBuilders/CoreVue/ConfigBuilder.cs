using BancVue.Domain.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class ConfigBuilder : TestDataBuilder< Config >
    {
        /// <summary>
        /// Builds a Config based on the specs specified in the builder.
        /// </summary>
        /// <returns></returns>
        protected override Config _build()
        {
            return new Config
                       {
                               ConfigId = GetUniqueId(),
                               InstitutionId = 0
                       };
        }
    }
}