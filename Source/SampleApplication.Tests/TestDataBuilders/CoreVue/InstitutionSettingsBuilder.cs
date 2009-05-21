using BancVue.Domain.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders.CoreVue
{
    public class InstitutionSettingsBuilder : TestDataBuilder< InstitutionSettings >
    {
        // Requires Institution to be set afterwards since this is a one to one relationship...really bad design.

//        private readonly InstitutionBuilder _institutionBuilder = new InstitutionBuilder();


        protected override InstitutionSettings _build()
        {
            return new InstitutionSettings
                       {
//                               Institution = _institutionBuilder.build(),
                               ClosedAccountDetectionMode = ClosedAccountDetectionModes.RequireDateClosed,
                               DaysMissingBeforeClosing = 0
                       };
        }

//        public InstitutionSettingsBuilder For( Institution institution)
//        {
//            _institutionBuilder.AliasFor( institution );
//            return this;
//        }
    }
}