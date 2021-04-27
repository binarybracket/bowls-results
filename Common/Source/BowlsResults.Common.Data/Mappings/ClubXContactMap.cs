using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Mappings
{
	public class ClubXContactMap : IdentityEntityMap<ClubXContact, int>
	{
		public ClubXContactMap()
		{
			this.Table("ClubXContact");
			this.LazyLoad();
			
			this.References(x => x.Club).Column("ClubID").Cascade.None();
			this.References(x => x.Contact).Column("ContactID").Cascade.None();
			this.References(x => x.Team).Column("TeamID").Nullable().Cascade.None();
		}
		
	}
}