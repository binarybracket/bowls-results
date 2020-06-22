using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Match
{
	public class MatchXGameMap : IdentityEntityMap<MatchXGame, int>
	{
		public MatchXGameMap()
		{
			this.Table("MatchXGame");
			this.LazyLoad();
			
			this.References(x => x.MatchFormatXGameVariation).Column("MatchFormatXGameVariationID").Not.Nullable().Cascade.None();
			this.References(x => x.Game).Column("GameID").Not.Nullable().Cascade.SaveUpdate();

			this.DiscriminateSubClassesOnColumn("ScopeID");
		}
	}
}