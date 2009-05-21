using BancVue.Domain.Notifications;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class ContactMethodBuilder : TestDataBuilder< ContactMethod >
    {
        protected override ContactMethod _build()
        {
            return new ContactMethod
                       {
                               Id = GetUniqueId(),
                               Description = ARandom.Title( 50 )
                       };
        }
    }
}