using BancVue.Domain.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class ProductCodeBuilder : TestDataBuilder< ProductCode >
    {
        private ProductCode _linkedSource;


        protected override ProductCode _build()
        {
            var productCode = new ProductCode();
            if ( _linkedSource != null ) productCode.SetLinkedSource( _linkedSource );
            return productCode;
        }


        public ProductCodeBuilder linkedToSource( ProductCode sourceProductCode )
        {
            _linkedSource = sourceProductCode;
            return this;
        }
    }
}