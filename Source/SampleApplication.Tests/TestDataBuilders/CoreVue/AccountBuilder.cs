using BancVue.Domain;
using BancVue.Domain.CoreVue;
using BancVue.Tests.Common.TestDataBuilders.Imports;


namespace BancVue.Tests.Common.TestDataBuilders.CoreVue
{
    public class AccountBuilder : TestDataBuilder< Account >
    {
        // Builders for each dependency (foreign key or relationship).
        private readonly AccountTypeBuilder _accountTypeBuilder = new AccountTypeBuilder();
        private readonly CustomerBuilder _customerBuilder = new CustomerBuilder();
        private readonly ImportDetailBuilder _importDetailBuilder = new ImportDetailBuilder();
        private readonly InstitutionBuilder _institutionBuilder = new InstitutionBuilder();
        private readonly ProductCodeBuilder _productCodeBuilder = new ProductCodeBuilder();


        protected override Account _build()
        {
            // Build and share objects that are used by multiple builders.
            Institution institution = _institutionBuilder.build();
            _importDetailBuilder.For( institution );
            _customerBuilder.For( institution );

            // Create the instance.
            return new Account
                       {
                               AccountId = GetUniqueId(),
                               Institution = institution,
                               AccountNumber = ARandom.String( 10 ),
                               AccountType = _accountTypeBuilder.build(),
                               AchAccountNumber = ARandom.String( 10 ),
                               AvailableBalance = ARandom.CurrencyAmount(),
                               AverageBalance = ARandom.CurrencyAmount(),
                               BancVueDateOpened = ARandom.DateTimeInPast(),
                               Branch = ARandom.Title( 10 ),
                               CurrentBalance = ARandom.CurrencyAmount(),
                               Customer = _customerBuilder.build(),
                               DateOpened = ARandom.DateTimeInPast(),
                               Email = ARandom.Email(),
                               HasElectronicStatements = ARandom.Boolean(),
                               InterestAccrued = ARandom.CurrencyAmount(),
                               InterestRate = ARandom.DoubleBetween( 0.01, 9.00 ),
                               InterestYearToDate = ARandom.CurrencyAmount(),
                               IsBancVueDdaAccount = true,
                               IsClosed = false,
                               IsNativeBancVueDdaAccount = true,
                               IsPassivelyClosed = false,
                               LedgerBalance = ARandom.CurrencyAmount(),
                               Name = ARandom.Title( 20 ),
                               NumberOfDaysInEarningsCycle = ARandom.IntBetween( 28, 31 ),
                               ProductCode = _productCodeBuilder.build(),
                               ImportDetail = _importDetailBuilder.build()
                       };
        }


        public AccountBuilder withProductCode( ProductCode productCode )
        {
            _productCodeBuilder.AliasFor( productCode );
            return this;
        }

        public AccountBuilder For( Institution institution)
        {
            _institutionBuilder.AliasFor( institution );
            return this;
        }
    }
}