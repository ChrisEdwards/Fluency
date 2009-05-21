using BancVue.Domain.CoreVue;


namespace BancVue.Tests.Common.TestDataBuilders
{
    public class RewardBuilder : TestDataBuilder< Reward >
    {
        private readonly ConfigBuilder _configBuilder = new ConfigBuilder();
        private readonly string _rewardViewName = "VIEW_" + ARandom.Text( 100 );
        private string _procedureName = "PROC_" + ARandom.Text( 100 );


        /// <summary>
        /// Initializes a new instance of the <see cref="RewardBuilder"/> class.
        /// </summary>
        public RewardBuilder() {}


        /// <summary>
        /// Initializes a new instance of the <see cref="RewardBuilder"/> class that builds the specified Reward.
        /// </summary>
        /// <param name="preBuiltResult">The pre built result.</param>
        public RewardBuilder( Reward preBuiltResult )
        {
            _preBuiltResult = preBuiltResult;
        }


        /// <summary>
        /// Builds a Reward based on the specs specified in the builder.
        /// </summary>
        /// <returns></returns>
        protected override Reward _build()
        {
            return new Reward
                       {
                               Id = GetUniqueId(),
                               DefaultConfig = _configBuilder.build(),
                               Description = ARandom.Text( 50 ),
                               ResultsAreEditable = true,
                               RewardProcedureName = _procedureName,
                               RewardViewName = _rewardViewName
                       };
        }


        /// <summary>
        /// Specifies the stored procedure used to execute the reward calculations for this Reward
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns></returns>
        public RewardBuilder usingMethod( string storedProcedureName )
        {
            _procedureName = storedProcedureName;
            return this;
        }
    }
}