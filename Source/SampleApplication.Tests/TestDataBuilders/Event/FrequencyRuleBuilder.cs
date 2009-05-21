using BancVue.Domain.Event;

namespace BancVue.Tests.Common.TestDataBuilders.Event
{
    public class FrequencyRuleBuilder : TestDataBuilder<FrequencyRule>
    {
        private string _rule = ARandom.String(150);

        protected override FrequencyRule _build()
        {
            return new FrequencyRule()
                       {
                           Id = GetUniqueId(),
                           Rule = _rule
                       };
        }

        public FrequencyRuleBuilder WithRule(string rule)
        {
            _rule = rule;
            return this;
        }
    }
}
