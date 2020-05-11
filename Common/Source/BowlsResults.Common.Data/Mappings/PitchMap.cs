using BowlsResults.Common.Domain.Models;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace BowlsResults.Common.Data.Mappings
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