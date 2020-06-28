using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Mappings
{
	public class AssociationXContactMap : IdentityEntityMap<AssociationXContact, int>
	{
		public AssociationXContactMap()
		{
			this.Table("AssociationXContact");
			this.LazyLoad();

			this.References(x => x.Association).Column("AssociationID").Cascade.None();
			this.References(x => x.Contact).Column("ContactID").Cascade.None();
		}
	}
}