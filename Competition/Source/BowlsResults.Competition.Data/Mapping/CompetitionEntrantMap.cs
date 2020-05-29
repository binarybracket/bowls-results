using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping
{
	public class CompetitionEntrantMap : IdentityEntityMap<CompetitionEntrant, int>
	{
		public CompetitionEntrantMap()
		{
			this.Table("CompetitionEntrant");
			this.LazyLoad();

			this.Map(x => x.CompetitionID).Column("CompetitionID").Not.Nullable();
			this.Map(x => x.EntrantGameFormatID).Column("EntrantGameFormatID").Not.Nullable();
			this.HasMany(x => x.Players).KeyColumn("CompetitionEntrantID").Cascade.SaveUpdate();
		}
	}
}