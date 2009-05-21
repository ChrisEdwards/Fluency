using BancVue.Domain.Event;

namespace BancVue.Tests.Common.TestDataBuilders.Event
{
    public class EventTypeBuilder : TestDataBuilder<EventType>
    {
        private string _name = ARandom.FullName();
        private string _description = ARandom.Text(200);

        protected override EventType _build()
        {
            return new EventType
                       {
                           Id = GetUniqueId(),
                           Name = _name,
                           Description = _description
                       };
        }

        public EventTypeBuilder WithName(string Name)
        {
            _name = Name;
            return this;
        }

        public EventTypeBuilder WithDescription(string Description)
        {
            _description = Description;
            return this;
        }
    }
}
