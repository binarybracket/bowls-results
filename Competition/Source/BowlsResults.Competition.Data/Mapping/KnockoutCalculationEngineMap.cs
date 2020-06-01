using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping
{
	public class KnockoutCalculationEngineMap : IdentityEntityMap<KnockoutCalculationEngine, KnockoutCalculationEngines> {
		public KnockoutCalculationEngineMap() {
			this.Table("KnockoutCalculationEngine");
			this.LazyLoad();
			
			this.References(x => x.MatchFormat).Column("MatchFormatID").Not.Nullable().Cascade.None();
			this.Map(x => x.FixtureCalculationEngineID).Column("FixtureCalculationEngineID").Not.Nullable();
			this.Map(x => x.Name).Column("Name").Not.Nullable();
			this.Map(x => x.Description).Column("Description");
			this.Map(x => x.MatchCalculationEngineID).Column("MatchCalculationEngineID").Not.Nullable();			
		}
	}
}