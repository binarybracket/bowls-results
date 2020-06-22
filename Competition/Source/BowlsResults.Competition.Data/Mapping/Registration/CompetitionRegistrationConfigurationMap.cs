using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Registration
{
	public class CompetitionRegistrationConfigurationMap : IdentityEntityMap<CompetitionRegistrationConfiguration, int>
	{
		public CompetitionRegistrationConfigurationMap()
		{
			this.Table("CompetitionRegistrationConfiguration");
			this.LazyLoad();
			
			this.References(x => x.Competition).Column("CompetitionID").Not.Nullable().Cascade.None();
			this.Map(x => x.CompetitionRegistrationModeID).Column("CompetitionRegistrationModeID").Not.Nullable();
			this.Map(x => x.OpenDate).Column("OpenDate");
			this.Map(x => x.CloseDate).Column("CloseDate");
			this.Map(x => x.Amount).Column("Amount").Not.Nullable();
		}
	}
}