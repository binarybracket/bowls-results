using BowlsResults.Competition.Domain.Models;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace BowlsResults.Competition.Data.Mapping
{
	public class LeagueMap : IdentityEntitySubclassMap<League, short>
	{
		public LeagueMap()
		{
			this.Table("League");
			this.KeyColumn("LeagueID");
			this.LazyLoad();
			this.References(x => x.Competition).Column("CompetitionID");
			this.References(x => x.Season);
			this.Map(x => x.LeagueCalculationEngineID).Column("LeagueCalculationEngineID");
			this.Map(x => x.Code).Column("Code");
			this.Map(x => x.Name).Column("Name");
			this.Map(x => x.MeritTableID).Column("MeritTableID");
		}
	}
}