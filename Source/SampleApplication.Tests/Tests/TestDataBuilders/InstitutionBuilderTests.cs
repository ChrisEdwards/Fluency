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
    public class InstitutionBuilderTests : TestDataBuilderTestsBase
    {
        private OrganizationBuilder OrganizationBuilder_ThatReturns( Organization expectedOrganization )
        {
            var organizationBuilder = _mocks.Stub< OrganizationBuilder >();
            SetupResult.For( organizationBuilder.build() ).Return( expectedOrganization );
            return organizationBuilder;
        }


        [ Test ]
        public void Should_build_valid_institution()
        {
            Institution institution = new InstitutionBuilder().build();

            institution.should_not_be_null();
            institution.Id.should_be_less_than( 0 );
            institution.BillingId.should_be_less_than( 0 );
            institution.Cycles.should_contain_no_items();
            institution.LongName.should_not_be_null();
            institution.Name.should_not_be_null();
            institution.Organization.should_not_be_null();
            institution.Products.should_contain_no_items();
            institution.Settings.should_not_be_null();
        }


        [ Test ]
        public void When_a_Cycle_is_specified__It_should_be_included_in_the_constructed_Institution()
        {
            // Arrange
            Cycle cycle = a.Cycle.build();

            // Act
            Institution institution = new InstitutionBuilder()
                    .with( cycle )
                    .build();

            // Assert
            institution.Cycles.should_contain( cycle );
        }


//        [Test]
//        public void When_a_CycleBuilder_is_specified__It_should_be_used_to_construct_the_the_Cycle_for_the_Institution()
//        {
//            // Arrange
//            Cycle expectedCycle = a.Cycle.build();
//            var cycleBuilder = new CycleBuilder( expectedCycle );
//
//            _mocks.ReplayAll();
//
//            // Act
//            Institution institution = new InstitutionBuilder()
//                    .with( cycleBuilder )
//                    .build();
//
//            // Assert
//            institution.Cycles.should_contain( expectedCycle );
//        }


        [ Test ]
        public void When_a_product_is_specified__It_should_be_included_in_the_constructed_Institution()
        {
            Product product = a.Product.build();

            // Act
            Institution institution = new InstitutionBuilder()
                    .with( product )
                    .build();

            // Assert
            institution.Products.should_have_item_count_of( 1 );
            institution.Products.should_contain( product );
        }


        [ Test ]
        public void When_a_ProductBuilder_is_specified__It_should_be_used_to_construct_the_Product_for_the_Institution()
        {
            // Arrange
            Product expectedProduct = a.Product.build();
            var productBuilder = new ProductBuilder( expectedProduct );

            _mocks.ReplayAll();

            // Act
            Institution institution = new InstitutionBuilder()
                    .with( productBuilder )
                    .build();

            // Assert
            institution.Products.should_contain( expectedProduct );
        }


        [ Test ]
        public void When_an_OrganizationBuilder_is_specified__It_should_be_used_to_construct_the_Organization_for_the_Institution()
        {
            // Arrange
            Organization expectedOrganization = an.Organization.build();
            var organizationBuilder = new OrganizationBuilder( expectedOrganization );

            _mocks.ReplayAll();

            // Act
            Institution institution = new InstitutionBuilder()
                    .within( organizationBuilder )
                    .build();

            // Assert
            institution.Organization.should_be( expectedOrganization );
        }


        [ Test ]
        public void When_two_products_are_specified__They_both_should_be_included_in_the_constructed_Institution()
        {
            Product product1 = a.Product.build();
            Product product2 = a.Product.build();

            // Act
            Institution institution = new InstitutionBuilder()
                    .with( product1 )
                    .with( product2 )
                    .build();

            // Assert
            institution.Products.should_have_item_count_of( 2 );
            institution.Products.should_contain( product1 );
            institution.Products.should_contain( product2 );
        }
    }
}