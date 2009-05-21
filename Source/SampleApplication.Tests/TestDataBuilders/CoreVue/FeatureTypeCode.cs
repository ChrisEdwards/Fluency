namespace BancVue.Tests.Common.TestDataBuilders
{
    /// <summary>
    /// Defines the types of Features that are available.
    /// </summary>
    public enum FeatureTypeCode
    {
        /// <summary>
        /// Feature whose rewards are run daily.
        /// </summary>
        Daily,
        /// <summary>
        /// Feature whose rewards are run once per cycle on the EOC date.
        /// </summary>
        Cycle,
        /// <summary>
        /// Feature whose rewards are run after the EOC date on the SecondaryProcessingDay.
        /// </summary>
        SecondaryProcessing
    }
}