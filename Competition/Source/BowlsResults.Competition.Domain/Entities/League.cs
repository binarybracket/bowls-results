using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class League : CompetitionEvent
	{
		public League()
		{
			this.CompetitionEventTypeID = CompetitionEventTypes.League;
		}

		public static League Create(string code, string name, LeagueCalculationEngines leagueCalculationEngine, CompetitionStage stage)
		{
			var data = new League
			{
				Code = code,
				Competition = stage.Competition,
				Name = name,
				LeagueCalculationEngineID = leagueCalculationEngine,
				Season = stage.Competition.Season,
				CompetitionStage = stage
			};

			return data;
		}

		public virtual LeagueCalculationEngines LeagueCalculationEngineID { get; set; }
		public virtual string Code { get; set; }
		public virtual string Name { get; set; }
		public virtual short? MeritTableID { get; set; }
	}
}