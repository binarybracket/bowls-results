using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Match
{
	public class PlayerMatchMap : AuditableEntitySubclassClassMap<PlayerMatch, short>
	{
		public PlayerMatchMap()
		{
			this.Table("PlayerMatch");
			this.LazyLoad();
			this.KeyColumn("PlayerMatchID");

			this.References(x => x.PlayerFixture).Column("PlayerFixtureID").Cascade.None();
			this.References(x => x.Game).Column("GameID").Cascade.SaveUpdate();
			this.Map(x => x.ResultTypeID).Column("ResultTypeID");
			this.Map(x => x.Leg).Column("Leg").Not.Nullable();
			this.Map(x => x.Player1Home).Column("Player1Home").Not.Nullable();
		}
	}
}