using System;
using BancVue.Domain.CoreVue;
using BancVue.Domain.Imports;
using BancVue.Tests.Common.TestDataBuilders.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders.Imports
{
    public class ImportBuilder : TestDataBuilder< Import >
    {
        private readonly InstitutionBuilder _institutionBuilder = new InstitutionBuilder();


        protected override Import _build()
        {
            DateTime beginDate = ARandom.DateTimeInPast();
            DateTime endDate = ARandom.DateTimeAfter( beginDate );

            return new Import
                       {
                               Institution = _institutionBuilder.build(),
                               ImportId = GetUniqueId(),
                               BeginDate = beginDate,
                               EndDate = endDate,
                               EffectiveDate = endDate,
                               WasSuccessful = true
                       };
        }


        public ImportBuilder For( Institution institution )
        {
            _institutionBuilder.AliasFor( institution );
            return this;
        }
    }
}