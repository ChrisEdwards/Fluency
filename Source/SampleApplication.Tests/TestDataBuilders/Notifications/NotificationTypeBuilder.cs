using BancVue.Domain.Notifications;


namespace BancVue.Tests.Common.TestDataBuilders.Notifications
{
    public class NotificationTypeBuilder : TestDataBuilder< NotificationType >
    {
        private readonly ContactMethodBuilder _contactMethodBuilder = new ContactMethodBuilder();
        private readonly ProductTypeBuilder _productTypeBuilder = new ProductTypeBuilder();


        protected override NotificationType _build()
        {
            return new NotificationType
                       {
                               Id = GetUniqueId(),
                               Description = ARandom.Title( 50 ),
                               RefusalMethod = ARandom.Text( 50 ),
                               XmlContent = ARandom.Text( 200 ),
                               ProductType = _productTypeBuilder.build(),
                               ContactMethod = _contactMethodBuilder.build()
                       };
        }
    }
}