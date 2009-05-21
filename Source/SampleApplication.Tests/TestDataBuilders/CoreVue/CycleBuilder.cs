using System;
using BancVue.Domain.CoreVue;
using BancVue.Tests.Common.TestDataBuilders.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class CycleBuilder : TestDataBuilder< Cycle >
    {
        private Institution _institution = new InstitutionBuilder().build();
        private DateTime _statementStartDate = ARandom.DateTime();


        /// <summary>
        /// Initializes a new instance of the <see cref="CycleBuilder"/> class to return the specified Cycle when its .build() is called.
        /// </summary>
        /// <param name="preBuiltResult">The pre built result.</param>
        public CycleBuilder( Cycle preBuiltResult )
        {
            _preBuiltResult = preBuiltResult;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CycleBuilder"/> class.
        /// </summary>
        public CycleBuilder() {}


        /// <summary>
        /// Builds a Cycle based on the specs specified in the builder.
        /// </summary>
        /// <returns></returns>
        protected override Cycle _build()
        {
            DateTime statementEndDate = _statementStartDate.AddMonths( 1 ).AddDays( -1 );

            return new Cycle
                       {
                               Institution = _institution,
                               CycleId = GetUniqueId(),
                               DropDate = statementEndDate.AddDays( 1 ),
                               SecondaryProcessingDate = statementEndDate.AddDays( 2 ),
                               StatementStartDate = _statementStartDate,
                               StatementEndDate = statementEndDate,
                               QualificationStartDate = _statementStartDate.AddDays( -1 ),
                               QualificationEndDate = statementEndDate.AddDays( -1 ),
                               EarningsStartDate = DateTime.MinValue,
                               EarningsEndDate = DateTime.MinValue
                       };
        }


        public CycleBuilder PriorTo( Cycle cycle )
        {
            _statementStartDate = cycle.StatementStartDate.AddMonths( -1 );
            return this;
        }


        public CycleBuilder For( Institution institution )
        {
            _institution = institution;
            return this;
        }


        public CycleBuilder After( Cycle cycle )
        {
            _statementStartDate = cycle.StatementStartDate.AddMonths( 1 );
            return this;
        }


        public CycleBuilder withSecondaryProcessingDateOf( DateTime date )
        {
            _statementStartDate = Calculate_StatementStartDate_From_SecondaryProcessingDate( date );
            return this;
        }


        private static DateTime Calculate_StatementStartDate_From_SecondaryProcessingDate( DateTime secondaryProcessingDate )
        {
            return secondaryProcessingDate.AddMonths( -1 ).AddDays( -1 );
        }
    }
}