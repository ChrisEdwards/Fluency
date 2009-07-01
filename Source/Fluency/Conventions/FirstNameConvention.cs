using System;
using System.Reflection;
using Fluency.DataGeneration;

namespace Fluency.Conventions
{
    public class FirstNameConvention:IDefaultConvention
    {
        public bool AppliesTo(PropertyInfo propertyInfo)
        {
            return (propertyInfo.PropertyType == typeof (String) &&
                    propertyInfo.Name.ToLower().Contains("firstname"));
        }

        public object DefaultValue(PropertyInfo propertyInfo)
        {
            return ARandom.FirstName();
        }
    }
}