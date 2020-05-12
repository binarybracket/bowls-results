using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Mappings
{
	public class SeasonMap : IdentityEntityMap<Season, short>
	{
		public SeasonMap()
		{
			this.Table("Season");
			this.LazyLoad();
			this.Map(x => x.StartDate).Column("StartDate").Not.Nullable();
			this.Map(x => x.EndDate).Column("EndDate").Not.Nullable();
			this.Map(x => x.DisplayName).Column("DisplayName").Not.Nullable();
		}
	}
}