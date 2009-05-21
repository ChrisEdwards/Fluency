using BancVue.Domain.CoreVue;
using BancVue.Domain.Imports;
using BancVue.Tests.Common.TestDataBuilders.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders.Imports
{
    public class ImportDetailBuilder : TestDataBuilder< ImportDetail >
    {
        private readonly ImportBuilder _importBuilder = new ImportBuilder();
        private readonly InstitutionBuilder _institutionBuilder = new InstitutionBuilder();


        protected override ImportDetail _build()
        {
            return new ImportDetail
                       {
                               Institution = _institutionBuilder.build(),
                               Import = _importBuilder.build(),
                               ImportDetailId = GetUniqueId(),
                               RowsInserted = ARandom.IntBetween( 0, 2000 ),
                               RowsUpdated = ARandom.IntBetween( 0, 2000 ),
                               Sha1 = new byte[20],
                               SourceId = 0 // TASK: [2009/03/30 15:38:48] :  Generate valid anonymous ImportDetail.SourceID	
                       };
        }


        public ImportDetailBuilder For( Institution institution )
        {
            _institutionBuilder.AliasFor( institution );
            _importBuilder.For( institution );
            return this;
        }
    }
}