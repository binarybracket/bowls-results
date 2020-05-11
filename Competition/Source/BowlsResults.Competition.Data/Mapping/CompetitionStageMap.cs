using BowlsResults.Competition.Domain.Models;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace BowlsResults.Competition.Data.Mapping
{
	public class CompetitionStageMap : IdentityEntityMap<CompetitionStage, short>
	{
		public CompetitionStageMap()
		{
			this.Table("CompetitionStage");
			this.LazyLoad();
			this.Map(x => x.CompetitionStageFormatID).Column("CompetitionStageFormatID");
			this.References(x => x.Competition).Column("CompetitionID");
			this.Map(x => x.Sequence).Column("Sequence").Not.Nullable();
			this.Map(x => x.Name).Column("Name");
		}
	}
}