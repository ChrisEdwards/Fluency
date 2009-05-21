using BancVue.Domain.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class AccountTypeBuilder : TestDataBuilder< AccountType >
    {
        /// <summary>
        /// Builds an AccountType based on the specs specified in the builder.
        /// </summary>
        /// <returns></returns>
        protected override AccountType _build()
        {
            return new AccountType
                       {
                               Id = GetUniqueId(),
                               Description = ARandom.Text( 50 )
                       };
        }
    }
}