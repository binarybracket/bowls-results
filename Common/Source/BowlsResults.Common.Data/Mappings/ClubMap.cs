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

			this.Map(x => x.AssociationID).Column("AssociationID").Not.Nullable();
			this.Map(x => x.Name).Column("Name");
			this.References(x => x.Pitch).Column("PitchID").Nullable();
			this.Map(x => x.Longitude).Column("Longitude").Nullable();
			this.Map(x => x.Latitude).Column("Latitude").Nullable();
			this.Map(x => x.Active).Column("Active").Not.Nullable();

			this.HasMany(x => x.Contacts).KeyColumn("ClubID").Inverse().Cascade.AllDeleteOrphan();
		}
	}
}