using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Mappings
{
	public class TeamMap : AuditableEntityClassMap<Team, int>
	{
		public TeamMap()
		{
			this.Table("Team");
			this.LazyLoad();

			this.References(x => x.Club).Column("ClubID").Not.Nullable().Cascade.None();
			this.References(x => x.Captain).Column("CaptainContactID").Nullable().Cascade.None();
			this.Map(x => x.GenderID).Column("GenderID");
			this.Map(x => x.AgeGroupID).Column("AgeGroupID");
			this.Map(x => x.TeamHeaderID).Column("TeamHeaderID");
			this.Map(x => x.AssociationID).Column("AssociationID");
			this.Map(x => x.Name).Column("Name");
			this.Map(x => x.Suffix).Column("Suffix").Not.Nullable();
			this.Map(x => x.PitchID).Column("PitchID");
		}
	}
}