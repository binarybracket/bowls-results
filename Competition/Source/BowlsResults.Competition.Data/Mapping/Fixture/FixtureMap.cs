using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Fixture
{
	public class FixtureMap : AuditableEntityClassMap<Domain.Entities.Fixture.Fixture, short>
	{
		public FixtureMap()
		{
			this.Table("Fixture");
			this.LazyLoad();
			this.Map(x => x.CompetitionRoundID).Column("CompetitionRoundID");
			this.References(x => x.Season).Column("SeasonID");
			this.Map(x => x.FixtureStatusID).Column("FixtureStatusID");
			this.Map(x => x.FixtureCalculationEngineID).Column("FixtureCalculationEngineID");
			this.Map(x => x.CompetitionID).Column("CompetitionID").Not.Nullable();
			this.Map(x => x.Legs).Column("Legs").Not.Nullable();
			this.Map(x => x.PendingDate).Column("PendingDate");
			this.Map(x => x.Entrant1ID).Column("Entrant1ID");
			this.Map(x => x.Entrant2ID).Column("Entrant2ID");
			this.Map(x => x.Pending1FixtureID).Column("Pending1FixtureID");
			this.Map(x => x.Pending1ResultTypeID).Column("Pending1ResultTypeID");
			this.Map(x => x.Pending2FixtureID).Column("Pending2FixtureID");
			this.Map(x => x.Pending2ResultTypeID).Column("Pending2ResultTypeID");
			this.Map(x => x.Completed).Column("Completed");
			this.Map(x => x.Reference).Column("Reference");
		}
	}
}