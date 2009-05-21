using System;
using System.Collections.Generic;
using BancVue.Domain;
using BancVue.Domain.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders.CoreVue
{
    public class ProductBuilder : TestDataBuilder< Product >
    {
        private readonly ListBuilder< Feature > _featureListBuilder = new ListBuilder< Feature >();
        private readonly List< Feature > _features = new List< Feature >();
        private readonly InstitutionBuilder _institutionBuilder = new InstitutionBuilder();
        private readonly ListBuilder< ProductCode > _productCodes = new ListBuilder< ProductCode >();
        private ProductTypeBuilder _productTypeBuilder = new ProductTypeBuilder();
        private ProductTypes _productTypeCode = ProductTypes.RewardChecking;


        /// <summary>
        /// Initializes a new instance of the <see cref="ProductBuilder"/> class.
        /// </summary>
        public ProductBuilder() {}


        /// <summary>
        /// Initializes a new instance of the <see cref="ProductBuilder"/> class that builds the specifed Product.
        /// </summary>
        /// <param name="preBuiltResult">The pre built result.</param>
        public ProductBuilder( Product preBuiltResult )
        {
            _preBuiltResult = preBuiltResult;
        }


        /// <summary>
        /// Builds a Product based on the specs specified in the builder.
        /// </summary>
        /// <returns></returns>
        protected override Product _build()
        {
            int productId = GetUniqueId();
            string description = ARandom.Text( 50 );
            var discontinuedDate = new DateTime?();
            Institution institution = _institutionBuilder.build();
            DateTime launchDate = DateTime.Now.AddYears( -1 );
            ProductType productType = _productTypeBuilder.build();
            List< Feature > features = _featureListBuilder.build();
            List< ProductCode > productCodes = _productCodes.build();

            return new Product( productId, institution, description, launchDate, discontinuedDate, productType, features, productCodes );
        }


        /// <summary>
        /// Specifies the type of this Product by its ProductTypeCode.
        /// </summary>
        /// <param name="productTypeCode">The product type code.</param>
        /// <returns></returns>
        public ProductBuilder ofType( ProductTypes productTypeCode )
        {
            _productTypeCode = productTypeCode;
            return this;
        }


        /// <summary>
        /// Specifies the type of this Product directly.
        /// </summary>
        /// <param name="productType">Type of the product.</param>
        /// <returns></returns>
        public ProductBuilder ofType( ProductType productType )
        {
            _productTypeBuilder.AliasFor( productType );
            return this;
        }


        /// <summary>
        /// Specifies the type of this Product by specifying its builder.
        /// </summary>
        /// <param name="productTypeBuilder">The product type builder.</param>
        /// <returns></returns>
        public ProductBuilder ofType( ProductTypeBuilder productTypeBuilder )
        {
            _productTypeBuilder = productTypeBuilder;
            return this;
        }


        /// <summary>
        /// Specifies the Feature of this Product by specifying its builder.
        /// </summary>
        /// <param name="featureBuilder">The feature builder.</param>
        /// <returns></returns>
        public ProductBuilder with( FeatureBuilder featureBuilder )
        {
            _featureListBuilder.Add( featureBuilder );
            return this;
        }


        /// <summary>
        /// Specifies the Feature of this Product directly.
        /// </summary>
        /// <param name="feature">The feature.</param>
        /// <returns></returns>
        public ProductBuilder with( Feature feature )
        {
            _featureListBuilder.Add( feature );
            return this;
        }


        /// <summary>
        /// Specifies a ProductCode for this Product directly.
        /// </summary>
        /// <param name="productCode">The product code.</param>
        /// <returns></returns>
        public ProductBuilder with( ProductCode productCode )
        {
            _productCodes.Add( productCode );
            return this;
        }


        /// <summary>
        /// Specifes the Institution for thie Product directly.
        /// </summary>
        /// <param name="institution">The institution.</param>
        /// <returns></returns>
        public ProductBuilder For( Institution institution )
        {
            _institutionBuilder.AliasFor( institution );
            return this;
        }
    }
}