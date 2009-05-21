using BancVue.Domain.CoreVue;
using BancVue.Tests.Common.TestDataBuilders;
using BancVue.Tests.Common.Utils;
using NUnit.Framework;


namespace BancVue.Tests.Common.Tests.TestDataBuilders
{
    [ TestFixture ]
    public class FeatureRewardBuilderTests : TestDataBuilderTestsBase
    {
        [ Test ]
        public void Should_build_valid_FeatureReward()
        {
            FeatureReward featureReward = new FeatureRewardBuilder().build();

            featureReward.should_not_be_null();
            featureReward.FeatureRewardId.should_be_less_than( 0 );
            featureReward.InstitutionId.should_be_less_than( 0 );
            featureReward.Config.should_not_be_null();
            featureReward.Config.InstitutionId.should_be_equal_to( featureReward.InstitutionId );
            featureReward.Reward.should_not_be_null();
            featureReward.IsActive.should_be_true();
        }


        [ Test ]
        public void When_a_Feature_is_specified__The_constructed_FeatureReward_should_contain_it()
        {
            // Arrange
            Feature feature = a.Feature.build();

            // Act
            FeatureReward featureReward = new FeatureRewardBuilder()
                    .forFeature( feature )
                    .build();

            // Assert
            featureReward.Feature.should_be( feature );
        }


        [ Test ]
        public void When_a_FeatureBuilder_is_specified__It_should_be_used_to_construct_the_the_Feature_for_the_FeatureReward()
        {
            // Arrange
            Feature feature = a.Feature.build();
            var featureBuilder = new FeatureBuilder( feature );

            _mocks.ReplayAll();

            // Act
            FeatureReward featureReward = new FeatureRewardBuilder()
                    .forFeature( featureBuilder )
                    .build();

            // Assert
            featureReward.Feature.should_be( feature );
        }


        [ Test ]
        public void When_a_Reward_is_specified__The_constructed_FeatureReward_should_contain_it()
        {
            // Arrange
            Reward reward = a.Reward.build();

            // Act
            FeatureReward featureReward = new FeatureRewardBuilder()
                    .forReward( reward )
                    .build();

            // Assert
            featureReward.Reward.should_be( reward );
        }


        [ Test ]
        public void When_a_RewardBuilder_is_specified__It_should_be_used_to_construct_the_the_Reward_for_the_FeatureReward()
        {
            // Arrange
            Reward reward = a.Reward.build();
            var rewardBuilder = new RewardBuilder( reward );

            _mocks.ReplayAll();

            // Act
            FeatureReward featureReward = new FeatureRewardBuilder()
                    .forReward( rewardBuilder )
                    .build();

            // Assert
            featureReward.Reward.should_be( reward );
        }
    }
}