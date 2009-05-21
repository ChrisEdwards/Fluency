using BancVue.Domain.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class OrganizationBuilder : TestDataBuilder< Organization >
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationBuilder"/> class.
        /// </summary>
        public OrganizationBuilder() {}


        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationBuilder"/> class that builds the specified Organization.
        /// </summary>
        /// <param name="preBuiltResult">The pre built result.</param>
        public OrganizationBuilder( Organization preBuiltResult )
        {
            _preBuiltResult = preBuiltResult;
        }


        /// <summary>
        /// Builds an Organization based on the specs specified in the builder.
        /// </summary>
        /// <returns></returns>
        protected override Organization _build()
        {
            return new Organization
                       {
                               Id = GetUniqueId(),
                               Name = ARandom.Title( 50 )
                       };
        }
    }
}