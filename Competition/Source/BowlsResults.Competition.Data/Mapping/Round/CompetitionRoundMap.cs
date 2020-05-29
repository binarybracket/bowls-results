using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Round
{
	public class CompetitionRoundMap : IdentityEntityMap<CompetitionRound, short>
	{
		public CompetitionRoundMap()
		{
			this.Table("CompetitionRound");
			this.LazyLoad();
			
			this.References(x => x.CompetitionEvent).Column("CompetitionEventID");
			this.References(x => x.Competition).Column("CompetitionID");
			this.References(x => x.Season).Column("SeasonID");
			this.Map(x => x.CompetitionRoundTypeID).Column("CompetitionRoundTypeID");
			this.Map(x => x.GameNumber).Column("GameNumber").Not.Nullable();
			this.Map(x => x.Notes).Column("Notes");

			this.DiscriminateSubClassesOnColumn("CompetitionScopeID");
		}
	}
}