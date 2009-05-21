using System.Collections.Generic;
using BancVue.Domain;
using BancVue.Domain.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders.CoreVue
{
    public class InstitutionBuilder : TestDataBuilder< Institution >
    {
        private readonly List< Cycle > _cycles = new List< Cycle >();
        private readonly ListBuilder< Product > _productListBuilder = new ListBuilder< Product >();
        private OrganizationBuilder _organizationBuilder = new OrganizationBuilder();
        private readonly InstitutionSettingsBuilder _institutionSettingsBuilder = new InstitutionSettingsBuilder();


        /// <summary>
        /// Builds an Institution based on the specs specified in the builder.
        /// </summary>
        /// <returns></returns>
        protected override Institution _build()
        {
            return new Institution
                       {
                               Id = GetUniqueId(),
                               Name = ARandom.Text( 50 ),
                               LongName = ARandom.Text( 100 ),
                               Organization = _organizationBuilder.build(),
                               Products = _productListBuilder.build(),
                               Cycles = _cycles,
                               BillingId = -1,
                               Settings = _institutionSettingsBuilder.build()
                       };
        }


        /// <summary>
        /// Specifies the Organization this Institution is within by specifying its builder.
        /// </summary>
        /// <param name="organizationBuilder">The organization builder.</param>
        /// <returns></returns>
        public InstitutionBuilder within( OrganizationBuilder organizationBuilder )
        {
            _organizationBuilder = organizationBuilder;
            return this;
        }


        /// <summary>
        /// Specifies a Product for this Institution by its builder.
        /// </summary>
        /// <param name="productBuilder">The product builder.</param>
        /// <returns></returns>
        public InstitutionBuilder with( ProductBuilder productBuilder )
        {
            _productListBuilder.Add( productBuilder );
            return this;
        }


        /// <summary>
        /// Specifies a Product for this Institution directly.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public InstitutionBuilder with( Product product )
        {
            _productListBuilder.Add( product );
            return this;
        }


        /// <summary>
        /// Specifies a Cycle for this Institution directly.
        /// </summary>
        /// <param name="cycle">The cycle.</param>
        /// <returns></returns>
        public InstitutionBuilder with( Cycle cycle )
        {
            _cycles.Add( cycle );
            return this;
        }


        /// <summary>
        /// Specifies a LinkedProductPair for this Institution.
        /// </summary>
        /// <param name="linkedProductPair">The linked product pair.</param>
        /// <returns></returns>
        public InstitutionBuilder with( LinkedProductPair linkedProductPair )
        {
            return with( linkedProductPair.SourceProduct )
                    .with( linkedProductPair.DestinationProduct );
        }
    }
}