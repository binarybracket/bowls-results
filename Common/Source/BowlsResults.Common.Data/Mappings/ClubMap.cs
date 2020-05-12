using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Mappings
{
	public class ClubMap : AuditableEntityClassMap<Club, int>
	{
		public ClubMap()
		{
			this.Table("Club");
			this.LazyLoad();
			//this.Map(x => x.Name).Column("Name");
			//this.Map(x => x.PitchID).Column("PitchID");
			//this.Map(x => x.AssociationID).Column("AssociationID");
		}
	}
}