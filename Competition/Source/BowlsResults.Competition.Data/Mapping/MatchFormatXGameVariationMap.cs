using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping
{
	public class MatchFormatXGameVariationMap : IdentityEntityMap<MatchFormatXGameVariation, short>
	{
		public MatchFormatXGameVariationMap()
		{
			this.Table("MatchFormatXGameVariation");
			this.LazyLoad();

			this.References(x => x.MatchFormat).Column("MatchFormatID").Not.Nullable().Cascade.None();
			this.References(x => x.GameVariation).Column("GameVariationID").Not.Nullable().Cascade.None();

			this.Map(x => x.GameCalculationEngineID).Column("GameCalculationEngineID");
			this.Map(x => x.Sequence).Column("Sequence").Not.Nullable();
			this.Map(x => x.Handicap).Column("Handicap");
		}
	}
}