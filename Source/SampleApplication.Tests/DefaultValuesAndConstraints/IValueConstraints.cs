using System;


namespace BancVue.Tests.Common.DefaultValuesAndConstraints
{
    public interface IValueConstraints
    {
        DateTime MaxDateTime { get; }
        DateTime MinDateTime { get; }
    }
}