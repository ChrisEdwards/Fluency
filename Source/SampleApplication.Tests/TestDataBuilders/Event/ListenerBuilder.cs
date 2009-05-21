using System;
using BancVue.Domain.CoreVue;
using BancVue.Domain.Event;
using BancVue.Tests.Common.TestDataBuilders.CoreVue;

namespace BancVue.Tests.Common.TestDataBuilders.Event
{
    public class ListenerBuilder : TestDataBuilder<Listener>
    {
        private readonly InstitutionBuilder _institutionBuilder = new InstitutionBuilder();
        private readonly EventTypeBuilder _eventTypeBuilder = new EventTypeBuilder();
        private readonly RecurrenceRuleBuilder _recurrenceRuleBuilder = new RecurrenceRuleBuilder();
        private string _method = ARandom.String(200);
        private DateTime? _runDate = ARandom.DateTimeInFuture();
        private int _priority = ARandom.IntBetween(0, 1000);

        protected override Listener _build()
        {
            Institution institution = _institutionBuilder.build();
            _recurrenceRuleBuilder.For(institution);

            return new Listener()
                       {
                           Id = GetUniqueId(),
                           Institution = institution,
                           EventType = _eventTypeBuilder.build(),
                           Method = _method,
                           RunDate = _runDate,
                           Priority = _priority,
                           RecurrenceRule = _recurrenceRuleBuilder.build()
                       };
        }

        public ListenerBuilder For(Institution institution)
        {
            _institutionBuilder.AliasFor(institution);
            return this;
        }

        public ListenerBuilder WithEventType(EventType eventType)
        {
            _eventTypeBuilder.AliasFor(eventType);
            return this;
        }

        public ListenerBuilder WithRecurrenceRule(RecurrenceRule recurrenceRule)
        {
            _recurrenceRuleBuilder.AliasFor(recurrenceRule);
            return this;
        }

        public ListenerBuilder WithMethod(string method)
        {
            _method = method;
            return this;
        }

        public ListenerBuilder WithRunDate(DateTime? runDate)
        {
            _runDate = runDate;
            return this;
        }

        public ListenerBuilder WithPriority(int priority)
        {
            _priority = priority;
            return this;
        }
    }
}
