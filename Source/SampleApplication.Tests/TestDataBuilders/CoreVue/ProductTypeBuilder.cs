using BancVue.Domain.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class ProductTypeBuilder : TestDataBuilder< ProductType >
    {
        private ProductTypes _productTypeCode = ProductTypes.RewardChecking;


        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypeBuilder"/> class the builds the specified ProductType.
        /// </summary>
        /// <param name="preBuiltResult">The pre built result.</param>
        public ProductTypeBuilder( ProductType preBuiltResult )
        {
            _preBuiltResult = preBuiltResult;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypeBuilder"/> class.
        /// </summary>
        public ProductTypeBuilder() {}


        /// <summary>
        /// Specifies which ProductType to build by specifying its ProductTypeCode.
        /// </summary>
        /// <param name="productTypeCode">The product type code.</param>
        /// <returns></returns>
        public ProductTypeBuilder ofType( ProductTypes productTypeCode )
        {
            _productTypeCode = productTypeCode;

            //switch ( productTypeCode )
            //{
            //    case ProductTypes.RewardChecking:
            //        break;

            //    case ProductTypes.RealSaver_Checking:
            //        break;

            //    case ProductTypes.RealSaver_Savings:
            //        break;

            //    default:
            //        throw new ArgumentException(
            //                "Invalid ProductTypeCode [{0}] passed to ProductTypeBuilder".format_using(
            //                        Enum.GetName( typeof ( ProductTypes ), productTypeCode ) ) );
            //        break;
            //}

            return this;
        }


        /// <summary>
        /// Builds a ProductyType based on the specs specified in the builder.
        /// </summary>
        /// <returns></returns>
        protected override ProductType _build()
        {
            return new ProductType
                       {
                               Id = GetUniqueId(),
                               AccountType = new AccountTypeBuilder().build(),
                               Description = ARandom.Text( 50 ),
                               IsBancVueProduct = true,
                               LinkedDestinationProductType = null,
                               LinkedSourceProductType = null
                       };
        }
    }
}