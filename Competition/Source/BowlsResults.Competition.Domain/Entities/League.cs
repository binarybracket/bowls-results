using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class League : CompetitionEvent
	{
		public League()
		{
			this.CompetitionEventTypeID = CompetitionEventTypes.League;
		}

		public virtual LeagueCalculationEngines LeagueCalculationEngineID { get; set; }
		public virtual string Code { get; set; }
		public virtual string Name { get; set; }
		public virtual short? MeritTableID { get; set; }
	}
}