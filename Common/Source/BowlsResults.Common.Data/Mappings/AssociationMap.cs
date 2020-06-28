using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Mappings
{
	public class AssociationMap : AuditableEntityClassMap<Association, int>
	{
		public AssociationMap()
		{
			this.Table("Association");
			this.LazyLoad();
			
			this.Map(x => x.Name).Column("Name").Not.Nullable();
			this.Map(x => x.ShortName).Column("ShortName");
			
			this.HasMany(x => x.Contacts).KeyColumn("AssociationID").Inverse().Cascade.AllDeleteOrphan();
		}
	}
}