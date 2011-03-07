// ReSharper disable InconsistentNaming


using System;
using Fluency.Probabilities;
using Machine.Specifications;


namespace Fluency.Tests.Probabilities
{
	public class PercentChanceOfSpecs
	{
		[ Subject( typeof ( PercentChanceOfSpecs ) ) ]
		public class Declaring_a_percent_chance_that_is_greater_than_100
		{
			static Exception exception;
			Because of = () => exception = Catch.Exception( ()=> new OutcomeSpecification<int >(110,0) );
			It should_fail = () => exception.should_be_an_instance_of< ArgumentException >();
		}


		[Subject(typeof(PercentChanceOfSpecs))]
		public class Declaring_a_percent_chance_that_is_less_than_0
		{
			static Exception exception;
			Because of = () => exception = Catch.Exception(() => new OutcomeSpecification<int>(-1, 0));
			It should_fail = () => exception.should_be_an_instance_of<ArgumentException>();
		}


		[Subject(typeof(PercentChanceOfSpecs))]
		public class Declaring_a_percent_chance_that_is_0
		{
			static OutcomeSpecification<int> result;
			Because of = () => result =  new OutcomeSpecification<int>(0, 0);
			It should_succeed = () => result.should_be_an_instance_of<OutcomeSpecification<int>>();
			It should_have_a_zero_percent_chance = () => result.PercentChance.should_be_equal_to( 0 );
		}


		[Subject(typeof(PercentChanceOfSpecs))]
		public class Declaring_a_percent_chance_that_is_100
		{
			static OutcomeSpecification<int> result;
			Because of = () => result =  new OutcomeSpecification<int>(100, 0);
			It should_succeed = () => result.should_be_an_instance_of<OutcomeSpecification<int>>();
			It should_have_a_100_percent_chance = () => result.PercentChance.should_be_equal_to( 100 );
		}

		
	}
}


// ReSharper restore InconsistentNaming