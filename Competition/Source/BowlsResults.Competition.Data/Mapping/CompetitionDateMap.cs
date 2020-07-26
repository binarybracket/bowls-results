using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping
{
	public class CompetitionDateMap : IdentityEntityMap<CompetitionDate, int>
	{
		public CompetitionDateMap()
		{
			this.Table("CompetitionDate");
			this.LazyLoad();

			this.References(x => x.Competition).Cascade.None().Column("CompetitionID");
			this.Map(x => x.Description).Column("Description");
			this.Map(x => x.Date).Column("Date");
		}
	}
}