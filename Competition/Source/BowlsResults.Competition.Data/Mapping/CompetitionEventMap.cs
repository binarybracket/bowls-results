using Com.BinaryBracket.BowlsResults.Competition.Domain.Models;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping
{
	public class CompetitionEventMap : IdentityEntityMap<CompetitionEvent, short>
	{
		public CompetitionEventMap()
		{
			this.Table("CompetitionEvent");
			this.LazyLoad();
			
			//this.Map(x => x.CompetitionEventTypeID).Column("CompetitionEventTypeID");
			this.References(x => x.CompetitionStage).Column("CompetitionStageID");
			this.Map(x => x.SaveResultRuleSetID).Column("SaveResultRuleSetID");
			this.Map(x => x.DataInt1).Column("DataInt1");
			this.Map(x => x.DataInt2).Column("DataInt2");
		}
	}
}