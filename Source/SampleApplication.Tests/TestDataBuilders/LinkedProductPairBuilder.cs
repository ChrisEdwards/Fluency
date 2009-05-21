using BancVue.Domain;
using BancVue.Tests.Common.TestDataBuilders.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class LinkedProductPairBuilder : TestDataBuilder< LinkedProductPair >
    {
        private readonly ProductBuilder _destinationProductBuilder = new ProductBuilder();
        private readonly ProductBuilder _sourceProductBuilder = new ProductBuilder();


        public LinkedProductPairBuilder()
        {
            // Initialize to linked product codes.
            LinkedProductCodePair linkedProductCodes = a.LinkedProductCodePair.build();
            Using( linkedProductCodes );
        }


        public LinkedProductPairBuilder Using( LinkedProductCodePair linkedProductCodes )
        {
            _sourceProductBuilder.with( linkedProductCodes.SourceProductCode );
            _destinationProductBuilder.with( linkedProductCodes.DestinationProductCode );
            return this;
        }


        protected override LinkedProductPair _build()
        {
            return new LinkedProductPair( _sourceProductBuilder.build(), _destinationProductBuilder.build() );
        }
    }
}