using FluentNHibernate.Mapping;


namespace SampleApplication.NHibernate.Mappings
{
	public class AccountMap : ClassMap< Account >
	{
		public AccountMap()
		{
			WithTable( "CoreVue.dbo.Dda" );
			UseCompositeId()
					.WithKeyReference( x => x.Institution, "InstitutionID" )
					.WithKeyProperty( x => x.AccountNumber, "AccountNumber" );

			Map( x => x.AchAccountNumber, "ACHAccountNumber" );
			Map( x => x.AccountId, "bvAccountID" );
			Map( x => x.Name, "Name1" );
			Map( x => x.DateOpened, "DateOpened" );
			Map( x => x.DateClosed, "DateClosed" );
			Map( x => x.CurrentBalance, "CurrentBalance" );
			Map( x => x.LedgerBalance, "LedgerBalance" );
			Map( x => x.CollectedBalance, "CollectedBalance" );
			Map( x => x.AvailableBalance, "AvailableBalance" );
			Map( x => x.AverageBalance, "AverageBalance" );
			Map( x => x.Status, "Status" );
			Map( x => x.ServiceChargeCode, "ServiceChargeCode" );
			Map( x => x.Branch, "Branch" );
			Map( x => x.InterestRate, "InterestRate" );

			// TASK: [2009/03/31 11:53:19] :  Enable Account NHibernat Relationships
//            References( x => x.AccountType, "AccountType" );
			//References( x => x.Customer ).WithColumns( "CustomerNumber", "InstitutionID" );
		}
	}
}