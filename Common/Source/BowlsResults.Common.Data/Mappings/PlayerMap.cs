using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Mappings
{
	public class PlayerMap : AuditableEntityClassMap<Player, int>
	{
		public PlayerMap()
		{
			this.Table("Player");
			this.LazyLoad();
			this.Map(x => x.Forename).Column("Forename").Not.Nullable();
			this.Map(x => x.Surname).Column("Surname").Not.Nullable();
			this.Map(x => x.GenderID).Column("GenderID").Not.Nullable();
			this.Map(x => x.RegistrationID).Column("RegistrationID").Not.Nullable();
			this.Map(x => x.AlternativeRegistrationID).Column("AlternativeRegistrationID");
			this.Map(x => x.Internal).Column("Internal").Not.Nullable();
		}
	}
}