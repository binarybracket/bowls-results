using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Mappings
{
	public class PitchMap : IdentityEntityMap<Pitch, int>
	{
		public PitchMap()
		{
			this.Table("Pitch");
			this.LazyLoad();
			this.Map(x => x.Name).Column("Name").Not.Nullable();
		}
	}
}