using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.Core.Data2.Mapping;
using FluentNHibernate.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Fixture
{
	public class PlayerFixtureMap : IdentityEntitySubclassMap<PlayerFixture, short>
	{
		public PlayerFixtureMap()
		{
			this.Table("PlayerFixture");
			this.LazyLoad();
			
			this.KeyColumn("PlayerFixtureID");
			this.Map(x => x.Player1GameScore).Column("Player1GameScore");
			this.Map(x => x.Player2GameScore).Column("Player2GameScore");
			this.Map(x => x.Player1ChalkScore).Column("Player1ChalkScore");
			this.Map(x => x.Player2ChalkScore).Column("Player2ChalkScore");
			this.Map(x => x.Player1ResultTypeID).Column("Player1ResultTypeID");
			this.Map(x => x.Player2ResultTypeID).Column("Player2ResultTypeID");
			this.Map(x => x.Player1Walkover).Column("Player1Walkover");
			this.Map(x => x.Player2Walkover).Column("Player2Walkover");

			this.References(x => x.Entrant1).Column("Entrant1ID").Cascade.None();
			this.References(x => x.Entrant2).Column("Entrant2ID").Cascade.None();
			
			this.HasMany(x => x.Matches).Fetch.Join().Cascade.None().Inverse().Access.CamelCaseField(Prefix.Underscore).AsSet();
		}
	}
}