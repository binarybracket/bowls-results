using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.Core.Data2.Mapping;
using FluentNHibernate.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Fixture
{
	public class PlayerFixtureMap : IdentityEntitySubclassMap<PlayerFixture, short>
	{
		public PlayerFixtureMap()
		{
			//this.KeyColumn("PlayerFixtureID");

			this.References(x => x.CompetitionRound).Column("CompetitionRoundID").Cascade.None();
			
			this.References(x => x.PendingPlayer1Fixture).Column("Pending1FixtureID").Cascade.None();
			this.References(x => x.PendingPlayer2Fixture).Column("Pending2FixtureID").Cascade.None();
			
			this.References(x => x.Entrant1).Column("Entrant1ID").Cascade.None();
			this.References(x => x.Entrant2).Column("Entrant2ID").Cascade.None();
			
			this.HasMany(x => x.Matches).KeyColumn("FixtureID").Fetch.Join().Cascade.All().Inverse().Access.CamelCaseField(Prefix.Underscore).AsSet().KeyColumn("FixtureID");
			
			this.DiscriminatorValue((int)CompetitionScopes.Player);
		}
	}
}