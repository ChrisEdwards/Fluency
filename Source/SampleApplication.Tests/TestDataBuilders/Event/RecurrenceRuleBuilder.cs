using System;
using BancVue.Domain.CoreVue;
using BancVue.Domain.Event;
using BancVue.Tests.Common.TestDataBuilders.CoreVue;

namespace BancVue.Tests.Common.TestDataBuilders.Event
{
    public class RecurrenceRuleBuilder : TestDataBuilder<RecurrenceRule>
    {
        private readonly InstitutionBuilder _institutionBuilder = new InstitutionBuilder();
        private readonly FrequencyRuleBuilder _frequencyRuleBuilder = new FrequencyRuleBuilder();
        private int? _x = ARandom.IntBetween(1, 9999);
        private int? _y;
        private int? _z;
        private DateTime? _at = ARandom.DateTime();
        private int? _remainingRecurrences = ARandom.IntBetween(1, 1000);

        protected override RecurrenceRule _build()
        {
            return new RecurrenceRule
                       {
                           Id = GetUniqueId(),
                           Institution = _institutionBuilder.build(),
                           FrequencyRule = _frequencyRuleBuilder.build(),
                           X = _x,
                           Y = _y,
                           Z = _z,
                           At = _at,
                           RemainingRecurrences = _remainingRecurrences
                       };
        }

        public RecurrenceRuleBuilder For(Institution institution)
        {
            _institutionBuilder.AliasFor(institution);
            return this;
        }

        public RecurrenceRuleBuilder WithFrequencyRule(FrequencyRule frequencyRule)
        {
            _frequencyRuleBuilder.AliasFor(frequencyRule);
            return this;
        }

        public RecurrenceRuleBuilder WithX(int? x)
        {
            _x = x;
            return this;
        }

        public RecurrenceRuleBuilder WithY(int? y)
        {
            _y = y;
            return this;
        }

        public RecurrenceRuleBuilder WithZ(int? z)
        {
            _z = z;
            return this;
        }

        public RecurrenceRuleBuilder At(DateTime? at)
        {
            _at = at;
            return this;
        }

        public RecurrenceRuleBuilder WithRemaining(int? remaining)
        {
            _remainingRecurrences = remaining;
            return this;
        }
    }
}
