using System;
using System.Reflection;

namespace Fluency.Conventions
{
    public class LambdaConvention: IDefaultConvention
    {
        private Func< PropertyInfo, object> _defaultValue;
        private Predicate<PropertyInfo> _appliesTo;

        public LambdaConvention(Predicate<PropertyInfo> appliesTo, Func<PropertyInfo,object> defaultValue)
        {
            _appliesTo = appliesTo;
            _defaultValue = defaultValue;
        }

        public bool AppliesTo(PropertyInfo propertyInfo)
        {
            return _appliesTo.Invoke(propertyInfo);
        }

        public object DefaultValue(PropertyInfo propertyInfo)
        {
            return _defaultValue.Invoke(propertyInfo);
        }
    }
}