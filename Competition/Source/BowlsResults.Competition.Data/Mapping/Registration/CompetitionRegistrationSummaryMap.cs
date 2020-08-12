using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Registration
{
	public class CompetitionRegistrationSummaryMap : IdentityEntityMap<CompetitionRegistrationSummary, int>
	{
		public CompetitionRegistrationSummaryMap()
		{
			this.Table("CompetitionRegistrationSummary");
			this.LazyLoad();
			
			this.Map(x => x.CompetitionID).Column("CompetitionID").Not.Nullable();
			this.Map(x => x.SummarySent).Column("SummarySent").Not.Nullable();
			this.Map(x => x.SummarySentDate).Column("SummarySentDate").Not.Nullable();
		}
	}
}