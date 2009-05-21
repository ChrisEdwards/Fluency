using BancVue.Domain;
using BancVue.Domain.CoreVue;
using BancVue.Tests.Common.TestDataBuilders.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders.Notifications
{
    public class NotificationBuilder : TestDataBuilder< Notification >
    {
        private readonly InstitutionBuilder _institutionBuilder = new InstitutionBuilder();
        private readonly NotificationTypeBuilder _notificationTypeBuilder = new NotificationTypeBuilder();
        private readonly ProductBuilder _productBuilder = new ProductBuilder();


        protected override Notification _build()
        {
            Institution institution = _institutionBuilder.build();
            _productBuilder.For( institution );

            return new Notification
                       {
                               Institution = institution,
                               NotificationId = GetUniqueId(),
                               NotificationType = _notificationTypeBuilder.build(),
                               Name = ARandom.Title( 50 ),
                               Product = _productBuilder.build(),
                               SenderName = ARandom.FullName(),
                               SenderContact = ARandom.Text( 500 )
                       };
        }
    }
}