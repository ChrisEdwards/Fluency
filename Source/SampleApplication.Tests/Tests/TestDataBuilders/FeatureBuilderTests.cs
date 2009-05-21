using BancVue.Domain.CoreVue;
using BancVue.Tests.Common.TestDataBuilders;
using BancVue.Tests.Common.Utils;
using NUnit.Framework;


namespace BancVue.Tests.Common.Tests.TestDataBuilders
{
    [ TestFixture ]
    public class FeatureBuilderTests : TestDataBuilderTestsBase
    {
        [ Test ]
        public void Should_build_valid_Feature()
        {
            Feature feature = new FeatureBuilder().build();

            feature.should_not_be_null();
            feature.FeatureId.should_be_less_than( 0 );
            feature.InstitutionId.should_be_less_than( 0 );
            feature.Institution.should_not_be_null();
            feature.Description.should_not_be_null();
            feature.DefaultFeature.should_be_null();
            feature.FeatureRewards.should_contain_no_items();
            feature.FeatureType.should_not_be_null();
            feature.Product.should_not_be_null();
            feature.IsActive.should_be_true();
        }


        [ Test ]
        public void When_a_FeatureType_is_specified__The_constructed_Feature_should_be_of_that_type()
        {
            // Arrange
            FeatureType featureType = a.FeatureType.build();

            // Act
            Feature feature = new FeatureBuilder()
                    .ofType( featureType )
                    .build();

            // Assert
            feature.FeatureType.should_be( featureType );
        }


        [ Test ]
        public void When_a_FeatureTypeBuilder_is_specified__It_should_be_used_to_construct_the_the_FeatureType_for_the_Feature()
        {
            // Arrange
            FeatureType featureType = a.FeatureType.build();
            var featureTypeBuilder = new FeatureTypeBuilder( featureType );

            _mocks.ReplayAll();

            // Act
            Feature feature = new FeatureBuilder()
                    .ofType( featureTypeBuilder )
                    .build();

            // Assert
            feature.FeatureType.should_be( featureType );
        }


        [ Test ]
        public void When_a_Reward_is_specified__The_constructed_Feature_should_contain_a_FeatureReward_for_that_Reward()
        {
            // Arrange
            Reward reward = a.Reward.build();

            // Act
            Feature feature = new FeatureBuilder()
                    .with( reward )
                    .build();

            // Assert
            feature.FeatureRewards.should_have_item_count_of( 1 );
            feature.FeatureRewards.should_contain_item_matching( item => item.Reward == reward );
        }


        [ Test ]
        public void When_a_RewardBuilder_is_specified__It_should_be_used_to_construct_the_the_FeatureReward_for_the_Feature()
        {
            // Arrange
            Reward reward = a.Reward.build();
            var rewardBuilder = new RewardBuilder();
            rewardBuilder.UsePreBuiltResult( reward );

            _mocks.ReplayAll();

            // Act
            Feature feature = new FeatureBuilder()
                    .with( rewardBuilder )
                    .build();

            // Assert
            feature.FeatureRewards.should_have_item_count_of( 1 );
            feature.FeatureRewards.should_contain_item_matching( item => item.Reward == reward ); // should ser reward of FeatureReward
            feature.FeatureRewards.should_contain_item_matching( item => item.Feature == feature ); // should set back-reference to Feature on FeatureReward
        }
    }
}