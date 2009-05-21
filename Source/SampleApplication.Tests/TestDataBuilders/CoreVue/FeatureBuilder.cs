using System.Collections.Generic;
using BancVue.Domain;
using BancVue.Domain.CoreVue;
using BancVue.Tests.Common.TestDataBuilders.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class FeatureBuilder : TestDataBuilder< Feature >
    {
        private readonly ListBuilder< FeatureReward > _featureRewards = new ListBuilder< FeatureReward >();
        private FeatureTypeBuilder _featureTypeBuilder = new FeatureTypeBuilder();


        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureBuilder"/> class.
        /// </summary>
        public FeatureBuilder() {}


        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureBuilder"/> class that returns the specifed Feature when its .build() is called.
        /// </summary>
        /// <param name="preBuiltResult">The pre built result.</param>
        public FeatureBuilder( Feature preBuiltResult )
        {
            _preBuiltResult = preBuiltResult;
        }


        /// <summary>
        /// Builds a Feature based on the specs specified in the builder.
        /// </summary>
        /// <returns></returns>
        protected override Feature _build()
        {
            int featureId = GetUniqueId();
            Feature defaultFeature = null;
            string description = ARandom.Text( 50 );
            FeatureType featureType = _featureTypeBuilder.build();
            bool isActive = true;
            Product product = new ProductBuilder().build();
            List< FeatureReward > featureRewards = _featureRewards.build();

            var feature = new Feature( featureId, description, featureType, product, defaultFeature, isActive, featureRewards );
            product.Add( feature );

            return feature;
        }


        /// <summary>
        /// Specifies the feature's Rewards should run on the Secondary Processing Day.
        /// </summary>
        /// <returns></returns>
        public FeatureBuilder forSecondaryProcessing()
        {
            _featureTypeBuilder.ofType( FeatureTypeCode.SecondaryProcessing );
            return this;
        }


        /// <summary>
        /// Specifies the type of Feature this is, by supplyinig the FeatureTypeCode, which defines when, and how often to run rewards.
        /// </summary>
        /// <param name="featureTypeCode">The feature type code.</param>
        /// <returns></returns>
        public FeatureBuilder ofType( FeatureTypeCode featureTypeCode )
        {
            _featureTypeBuilder.ofType( featureTypeCode );
            return this;
        }


        /// <summary>
        /// Specifies the type of Feature this is, by supplying the FeatureType, which defines when, and how often to run rewards.
        /// </summary>
        /// <param name="featureType">Type of the feature.</param>
        /// <returns></returns>
        public FeatureBuilder ofType( FeatureType featureType )
        {
            _featureTypeBuilder.UsePreBuiltResult( featureType );
            return this;
        }


        /// <summary>
        /// Specifies the type of Feature this is, by supplying a FeatureTypeBuilder, which defines when, and how often to run rewards.
        /// </summary>
        /// <param name="featureTypeBuilder">The feature type builder.</param>
        /// <returns></returns>
        public FeatureBuilder ofType( FeatureTypeBuilder featureTypeBuilder )
        {
            _featureTypeBuilder = featureTypeBuilder;
            return this;
        }


        /// <summary>
        /// Specifies a Reward to be added to this Feature, by specifying its RewardBuilder. This silently creates a FeatureReward object to represent the relationship.
        /// </summary>
        /// <param name="rewardBuilder">The reward builder.</param>
        /// <returns></returns>
        public FeatureBuilder with( RewardBuilder rewardBuilder )
        {
            _featureRewards.Add( a.FeatureReward
                                         .forReward( rewardBuilder ) );
            return this;
        }


        /// <summary>
        /// Specifies a Reward to be added to this Feature, by specifying its Reward directly. This silently creates a FeatureReward object to represent the relationship.
        /// </summary>
        /// <param name="reward">The reward.</param>
        /// <returns></returns>
        public FeatureBuilder with( Reward reward )
        {
            _featureRewards.Add( a.FeatureReward
                                         .forReward( reward ) );
            return this;
        }
    }
}