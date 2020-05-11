using BowlsResults.Competition.Domain.Models;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace BowlsResults.Competition.Data.Mapping
{
	public class CompetitionHeaderMap : IdentityEntityMap<CompetitionHeader, int>
	{
		public CompetitionHeaderMap()
		{
			this.Table("CompetitionHeader");
			this.LazyLoad();
			this.Map(x => x.Name).Column("Name").Not.Nullable();
			this.Map(x => x.AssociationID).Column("AssociationID").Not.Nullable();
			this.Map(x => x.ShortName).Column("ShortName");
			this.Map(x => x.Priority).Column("Priority").Not.Nullable();
			
			//this.ReadOnly();
			this.Cache.ReadWrite();
		}
	}
}