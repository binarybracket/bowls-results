using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping
{
	public class MatchFormatMap : IdentityEntityMap<MatchFormat,short > {
		public MatchFormatMap() {
			this.Table("MatchFormat");
			this.LazyLoad();
			
			this.Map(x => x.Name).Column("Name").Not.Nullable();
			this.HasMany(x => x.GameVariations).KeyColumn("MatchFormatID");
		}
	}
}