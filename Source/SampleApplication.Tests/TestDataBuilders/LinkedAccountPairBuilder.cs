using System.Linq;
using BancVue.Domain;
using BancVue.Domain.CoreVue;
using BancVue.Tests.Common.TestDataBuilders.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class LinkedAccountPairBuilder : TestDataBuilder< LinkedAccountPair >
    {
        private readonly AccountBuilder _destinationAccountBuilder = new AccountBuilder();
        private readonly AccountBuilder _sourceAccountBuilder = new AccountBuilder();


        protected override LinkedAccountPair _build()
        {
            return new LinkedAccountPair
                       {
                               DestinationAccount = _destinationAccountBuilder.build(),
                               SourceAccount = _sourceAccountBuilder.build()
                       };
        }


        public LinkedAccountPairBuilder For( LinkedProductPair linkedProductPair )
        {
            ProductCode sourceProductCode = linkedProductPair.SourceProductCodes.First();
            _sourceAccountBuilder.withProductCode( sourceProductCode );
            return this;
        }
    }
}