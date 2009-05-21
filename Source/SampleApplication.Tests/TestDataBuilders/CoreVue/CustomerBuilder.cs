using System;
using BancVue.Domain.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders.CoreVue
{
    public class CustomerBuilder : TestDataBuilder< Customer >
    {
        private readonly InstitutionBuilder _institutionBuilder = new InstitutionBuilder();


        protected override Customer _build()
        {
            DateTime customerSince = ARandom.DateTimeInPastSince( 20.YearsAgo() );

            return new Customer
                       {
                               CustomerNumber = ARandom.String( 10 ),
                               Address = ARandom.Text( 20 ),
                               City = ARandom.City(),
                               State = ARandom.StateCode(),
                               Zip = ARandom.StringPattern( "99999" ),
                               BusinessPhone = ARandom.StringPattern( "(999) 999-9999" ),
                               HomePhone = ARandom.StringPattern( "(999) 999-9999" ),
                               Email = ARandom.Email(),
                               Name1 = ARandom.FirstName(),
                               Name2 = ARandom.LastName(),
                               BirthDate = ARandom.BirthDate(),
                               CustomerSince = customerSince,
                               Institution = _institutionBuilder.build(),
                               IsBancVueCustomerRelationship = true,
                               IsBancVueDailyAccount = true,
                               TaxId = ARandom.StringPattern( "9-999-99-9999-9" ),
                               LastInternetBankingDate = ARandom.DateTimeInPastSince( customerSince )
                       };
        }


        public CustomerBuilder For( Institution institution )
        {
            _institutionBuilder.AliasFor( institution );
            return this;
        }
    }
}