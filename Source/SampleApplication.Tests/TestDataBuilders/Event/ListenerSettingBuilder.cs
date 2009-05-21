using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BancVue.Domain.CoreVue;
using BancVue.Domain.Event;
using BancVue.Tests.Common.TestDataBuilders.CoreVue;

namespace BancVue.Tests.Common.TestDataBuilders.Event
{
    public class ListenerSettingBuilder : TestDataBuilder<ListenerSetting>
    {
        private readonly InstitutionBuilder _institution = new InstitutionBuilder();
        private readonly ListenerBuilder _listener = new ListenerBuilder();
        private string _setting = ARandom.String(50);
        private object _value = ARandom.DateTimeInFuture();

        protected override ListenerSetting _build()
        {
            return new ListenerSetting()
                       {
                           Institution = _institution.build(),
                           Listener = _listener.build(),
                           Setting = _setting,
                           Value = _value
                       };
        }

        public ListenerSettingBuilder For(Institution institution)
        {
            _institution.AliasFor(institution);
            return this;
        }

        public ListenerSettingBuilder For(Listener listener)
        {
            _listener.AliasFor(listener);
            return For(listener.Institution);
        }

        public ListenerSettingBuilder For(ListenerBuilder listener)
        {
            return For(listener.build());
        }

        public ListenerSettingBuilder WithSetting(string setting)
        {
            _setting = setting;
            return this;
        }

        public ListenerSettingBuilder WithValue(object value)
        {
            _value = value;
            return this;
        }
    }
}
