using BancVue.Domain.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class FeatureTypeBuilder : TestDataBuilder< FeatureType >
    {
        private const string FeatureType_Cycle_Description = "Cycle";
        private const int FeatureType_Cycle_Id = 1;
        private const string FeatureType_SecondaryProcessing_Description = "2nd Day Processing";
        private const int FeatureType_SecondaryProcessing_Id = 3;

        private FeatureTypeCode _featureTypeCode;


        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureTypeBuilder"/> class.
        /// </summary>
        public FeatureTypeBuilder()
        {
            // Set Default Values.
            _featureTypeCode = FeatureTypeCode.Cycle;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureTypeBuilder"/> class that will build the specified FeatureType.
        /// </summary>
        /// <param name="preBuiltResult">The pre built result.</param>
        public FeatureTypeBuilder( FeatureType preBuiltResult )
        {
            _preBuiltResult = preBuiltResult;
        }


        /// <summary>
        /// Specifies the FeatureType to build by its FeatureTypeCode.
        /// </summary>
        /// <param name="featureTypeCode">The feature type code.</param>
        /// <returns></returns>
        public FeatureTypeBuilder ofType( FeatureTypeCode featureTypeCode )
        {
            _featureTypeCode = featureTypeCode;
            return this;
        }


        /// <summary>
        /// Builds a FeatureType based on the specs specified in the builder.
        /// </summary>
        /// <returns></returns>
        protected override FeatureType _build()
        {
            switch ( _featureTypeCode )
            {
                case FeatureTypeCode.SecondaryProcessing:
                    return new FeatureType
                               {
                                       Id = FeatureType_SecondaryProcessing_Id,
                                       Description = FeatureType_SecondaryProcessing_Description
                               };

                case FeatureTypeCode.Cycle:
                    return new FeatureType
                               {
                                       Id = FeatureType_Cycle_Id,
                                       Description = FeatureType_Cycle_Description
                               };

                default:
                    return null;
            }
        }
    }
}