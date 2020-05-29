using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Entrant;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Entrant
{
	public class CompetitionEntrantPlayerMap : IdentityEntityMap<CompetitionEntrantPlayer, int>
	{
		public CompetitionEntrantPlayerMap()
		{
			this.Table("CompetitionEntrantPlayer");
			this.LazyLoad();
			this.References(x => x.CompetitionEntrant).Column("CompetitionEntrantID").Not.Nullable().Cascade.None();
			this.References(x => x.Player).Column("PlayerID").Nullable().Cascade.None();
			this.Map(x => x.CompetitionID).Column("CompetitionID").Not.Nullable();
			this.Map(x => x.FirstName).Column("FirstName").Not.Nullable();
			this.Map(x => x.LastName).Column("LastName").Not.Nullable();						
		}
	}
}