using BancVue.Domain.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class LinkedProductCodePairBuilder : TestDataBuilder< LinkedProductCodePair >
    {
        private ProductCode _destinationProductCode;
        private ProductCode _sourceProductCode;


        protected override LinkedProductCodePair _build()
        {
            _sourceProductCode = a.ProductCode.build();
            _destinationProductCode = a.ProductCode.linkedToSource( _sourceProductCode ).build();

            return new LinkedProductCodePair( _sourceProductCode, _destinationProductCode );
        }
    }


    public class LinkedProductCodePair
    {
        public LinkedProductCodePair( ProductCode sourceProductCode, ProductCode destinationProductCode )
        {
            SourceProductCode = sourceProductCode;
            DestinationProductCode = destinationProductCode;
            SourceProductCode.LinkedDestinationProductCode = destinationProductCode;
        }


        public ProductCode SourceProductCode { get; set; }

        public ProductCode DestinationProductCode { get; set; }
    }
}