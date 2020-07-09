using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Match
{
	public class PlayerMatchMap : AuditableEntitySubclassClassMap<PlayerMatch, short>
	{
		public PlayerMatchMap()
		{
			this.DiscriminatorValue((byte)CompetitionScopes.Player);

			this.References(x => x.PlayerFixture).Column("FixtureID").Cascade.None();
			this.References(x => x.Home).Column("HomeID").Cascade.None();
			this.References(x => x.Away).Column("AwayID").Cascade.None();

			this.HasMany(x => x.Games).Cascade.SaveUpdate().Inverse().AsSet().KeyColumn("MatchID");
		}
	}
}