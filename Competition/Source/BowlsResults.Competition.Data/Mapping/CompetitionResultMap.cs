using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping
{
	public class CompetitionResultMap : IdentityEntityMap<CompetitionResult, int>
	{
		public CompetitionResultMap()
		{
			this.Table("CompetitionResult");
			this.LazyLoad();
			this.DiscriminateSubClassesOnColumn("CompetitionScopeID");

			this.Map(x => x.SeasonID).Column("SeasonID").Not.Nullable();
			this.References(x => x.Competition).Column("CompetitionID").Not.Nullable().Cascade.None();
		}
	}
}