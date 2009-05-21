using BancVue.Domain;
using BancVue.Domain.CoreVue;
using BancVue.Tests.Common.TestDataBuilders;
using BancVue.Tests.Common.TestDataBuilders.CoreVue;
using BancVue.Tests.Common.Utils;
using NUnit.Framework;
using Rhino.Mocks;


namespace BancVue.Tests.Common.Tests.TestDataBuilders
{
    [ TestFixture ]
    public class ProductBuilderTests : TestDataBuilderTestsBase
    {
        private ProductTypeBuilder ProductTypeBuilder_ThatReturns( ProductType productType )
        {
            var productTypeBuilder = _mocks.Stub< ProductTypeBuilder >();
            SetupResult.For( productTypeBuilder.build() ).Return( productType );
            return productTypeBuilder;
        }


        [ Test ]
        public void Should_build_valid_product()
        {
            Product product = new ProductBuilder().build();

            product.should_not_be_null();
            product.ProductId.should_be_less_than( 0 );
            product.InstitutionId.should_be_less_than( 0 );
            product.Institution.should_not_be_null();
            product.Description.should_not_be_null();
            product.DiscontinuedDate.should_be_null();
            product.Features.should_contain_no_items();
            product.LaunchDate.should_not_be_null();
            product.ProductType.should_not_be_null();
        }


        [ Test ]
        public void When_a_Feature_is_specified__It_should_be_included_in_the_constructed_Product()
        {
            // Arrange
            Feature feature = a.Feature.build();

            // Act
            Product product = new ProductBuilder()
                    .with( feature )
                    .build();

            // Assert
            product.Features.should_contain( feature );
        }


        [ Test ]
        public void When_a_FeatureBuilder_is_specified__It_should_be_used_to_build_the_constructed_Product()
        {
            // Arrange
            Feature feature = a.Feature.build();
            var featureBuilder = new FeatureBuilder( feature );

            _mocks.ReplayAll();

            // Act
            Product product = new ProductBuilder()
                    .with( featureBuilder )
                    .build();

            // Assert
            product.Features.should_contain( feature );
        }


        [ Test ]
        public void When_a_ProductType_is_specified__It_should_be_included_in_the_constructed_Product()
        {
            // Arrange
            ProductType productType = a.ProductType.build();

            // Act
            Product product = new ProductBuilder()
                    .ofType( productType )
                    .build();

            // Assert
            product.ProductType.should_be( productType );
        }


        [ Test ]
        public void When_a_ProductTypeBuilder_is_specified__It_should_be_used_to_build_the_constructed_Product()
        {
            // Arrange
            ProductType productType = a.ProductType.build();
            var productTypeBuilder = new ProductTypeBuilder( productType );

            _mocks.ReplayAll();

            // Act
            Product product = new ProductBuilder()
                    .ofType( productTypeBuilder )
                    .build();

            // Assert
            product.ProductType.should_be( productType );
        }


        //[Test]
        //public void When_a_ProductTypeCode_is_specified__The_corresponding_ProductType_should_be_included_in_the_constructed_Product()
        //{
        //    // Arrange
        //    var productTypeCode = ARandom.EnumValue<ProductTypes>();

        //    // Act
        //    Product product = new ProductBuilder()
        //            .ofType(productTypeCode);

        //    // Assert
        //    product.ProductType.should_be(productTypeCode);
        //}
    }
}