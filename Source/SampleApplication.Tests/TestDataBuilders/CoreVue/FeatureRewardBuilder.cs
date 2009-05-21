using BancVue.Domain.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class FeatureRewardBuilder : TestDataBuilder< FeatureReward >
    {
        private readonly ConfigBuilder _configBuilder = new ConfigBuilder();
        private FeatureBuilder _featureBuilder = new FeatureBuilder();
        private RewardBuilder _rewardBuilder = new RewardBuilder();


        /// <summary>
        /// Builds a FeatureReward based on the specs specified in the builder.
        /// </summary>
        /// <returns></returns>
        protected override FeatureReward _build()
        {
            int featureRewardId = GetUniqueId();
            Feature feature = _featureBuilder.build();
            Reward reward = _rewardBuilder.build();
            Config config = _configBuilder.build();
            bool isActive = true;

            return new FeatureReward( featureRewardId, feature, reward, config, isActive );
        }


        /// <summary>
        /// Specifies the Feature for this FeatureReward using a builder.
        /// </summary>
        /// <param name="featureBuilder">The feature builder.</param>
        /// <returns></returns>
        public FeatureRewardBuilder forFeature( FeatureBuilder featureBuilder )
        {
            _featureBuilder = featureBuilder;
            return this;
        }


        /// <summary>
        /// Specifies the Feature for this FeatureReward directly.
        /// </summary>
        /// <param name="feature">The feature.</param>
        /// <returns></returns>
        public FeatureRewardBuilder forFeature( Feature feature )
        {
            _featureBuilder.UsePreBuiltResult( feature );
            return this;
        }


        /// <summary>
        /// Specifies the Reward for this FeatureReward using a builder.
        /// </summary>
        /// <param name="rewardBuilder">The reward builder.</param>
        /// <returns></returns>
        public FeatureRewardBuilder forReward( RewardBuilder rewardBuilder )
        {
            _rewardBuilder = rewardBuilder;
            return this;
        }


        /// <summary>
        /// Specifies the Reward for this FeatureReward directly.
        /// </summary>
        /// <param name="reward">The reward.</param>
        /// <returns></returns>
        public FeatureRewardBuilder forReward( Reward reward )
        {
            _rewardBuilder.UsePreBuiltResult( reward );
            return this;
        }
    }
}