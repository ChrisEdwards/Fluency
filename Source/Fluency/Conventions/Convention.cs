using System;
using System.Reflection;

namespace Fluency.Conventions
{
    public static class Convention
    {
        public static LambdaConvention ByName<T>( string propertyName, Func<PropertyInfo, object> defaultValue)
        {
            return new LambdaConvention(
                p => p.PropertyType == typeof(T) && p.Name.ToLower().Contains(propertyName.ToLower()),
                defaultValue);
        }

        public static LambdaConvention ByType<T>( Func<PropertyInfo, object> defaultValue)
        {
            return new LambdaConvention(
                p => p.PropertyType == typeof(T),
                defaultValue);
        }
    }
}