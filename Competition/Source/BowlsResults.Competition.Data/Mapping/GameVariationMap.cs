using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping
{
	public class GameVariationMap : IdentityEntityMap<GameVariation, byte> {
		public GameVariationMap() {
			this.Table("GameVariation");
			this.LazyLoad();
			
			this.Map(x => x.GameFormatID).Column("GameFormatID");
			this.Map(x => x.GenderID).Column("GenderID");
			this.Map(x => x.Name).Column("Name").Not.Nullable();
			this.Map(x => x.Description).Column("Description");
		}
	}
}