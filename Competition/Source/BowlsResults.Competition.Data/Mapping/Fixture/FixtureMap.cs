using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Fixture
{
	public class FixtureMap : AuditableEntityClassMap<Domain.Entities.Fixture.Fixture, short>
	{
		public FixtureMap()
		{
			this.Table("Fixture");
			this.LazyLoad();
			this.References(x => x.Season).Column("SeasonID");
			this.Map(x => x.FixtureStatusID).Column("FixtureStatusID");
			this.Map(x => x.FixtureCalculationEngineID).Column("FixtureCalculationEngineID");
			this.Map(x => x.CompetitionID).Column("CompetitionID").Not.Nullable();
			this.Map(x => x.Legs).Column("Legs").Not.Nullable();
			this.Map(x => x.PendingDate).Column("PendingDate");
			this.Map(x => x.Pending1FixtureID).Column("Pending1FixtureID");
			this.Map(x => x.Pending1ResultTypeID).Column("Pending1ResultTypeID").CustomType<ResultType>();
			this.Map(x => x.Pending2FixtureID).Column("Pending2FixtureID");
			this.Map(x => x.Pending2ResultTypeID).Column("Pending2ResultTypeID").CustomType<ResultType>();
			
			this.Map(x => x.Entrant1GameScore).Column("Entrant1GameScore");
			this.Map(x => x.Entrant2GameScore).Column("Entrant2GameScore");
			this.Map(x => x.Entrant1ChalkScore).Column("Entrant1ChalkScore");
			this.Map(x => x.Entrant2ChalkScore).Column("Entrant2ChalkScore");
			this.Map(x => x.Entrant1BonusScore).Column("Entrant1BonusScore");
			this.Map(x => x.Entrant2BonusScore).Column("Entrant2BonusScore");
			this.Map(x => x.Entrant1ResultTypeID).Column("Entrant1ResultTypeID").CustomType<ResultType>();
			this.Map(x => x.Entrant2ResultTypeID).Column("Entrant2ResultTypeID").CustomType<ResultType>();
			this.Map(x => x.Entrant1Walkover).Column("Entrant1Walkover");
			this.Map(x => x.Entrant2Walkover).Column("Entrant2Walkover");
			
			this.Map(x => x.Completed).Column("Completed");
			this.Map(x => x.Reference).Column("Reference");

			this.DiscriminateSubClassesOnColumn("CompetitionScopeID");
		}
	}
}