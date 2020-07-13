using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Match
{
	public class MatchMap : IdentityEntityMap<Domain.Entities.Match.Match, short>
	{
		public MatchMap()
		{
			this.Table("Match2");
			this.LazyLoad();

			this.References(x => x.MatchFormat).Column("MatchFormatID").Not.Nullable().Cascade.None();
			
			this.Map(x => x.MatchStatusID).Column("MatchStatusID").Not.Nullable();
			this.Map(x => x.Date).Column("Date").Not.Nullable();
			this.Map(x => x.Leg).Column("Leg").Not.Nullable();
			this.References(x => x.Pitch).Column("PitchID").Nullable();
			this.Map(x => x.VenueTypeID).Column("VenueTypeID").Not.Nullable();
			this.Map(x => x.HomeChalkHandicap).Column("HomeChalkHandicap");
			this.Map(x => x.AwayChalkHandicap).Column("AwayChalkHandicap");
			this.Map(x => x.HomeGameScore).Column("HomeGameScore");
			this.Map(x => x.AwayGameScore).Column("AwayGameScore");
			this.Map(x => x.HomeBonusScore).Column("HomeBonusScore");
			this.Map(x => x.AwayBonusScore).Column("AwayBonusScore");
			this.Map(x => x.HomeChalkScore).Column("HomeChalkScore");
			this.Map(x => x.AwayChalkScore).Column("AwayChalkScore");
			this.Map(x => x.HomeWalkover).Column("HomeWalkover");
			this.Map(x => x.AwayWalkover).Column("AwayWalkover");
			this.Map(x => x.HomeResultTypeID).Column("HomeResultTypeID").CustomType<ResultType>();
			this.Map(x => x.AwayResultTypeID).Column("AwayResultTypeID").CustomType<ResultType>();
			this.Map(x => x.MatchCalculationEngineID).Column("MatchCalculationEngineID").Not.Nullable();
			this.Map(x => x.Sequence).Column("Sequence");
			this.Map(x => x.DataString1).Column("DataString1");
			this.Map(x => x.DataString2).Column("DataString2");

			this.DiscriminateSubClassesOnColumn("ScopeID");
		}
	}
}