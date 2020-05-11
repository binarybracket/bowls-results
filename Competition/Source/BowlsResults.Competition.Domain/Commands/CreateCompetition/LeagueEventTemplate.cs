using BowlsResults.Common.Domain.Models;

namespace BowlsResults.Competition.Domain.Commands.CreateCompetition
{
	public sealed class LeagueEventTemplate : EventTemplate
	{
		public LeagueCalculationEngines LeagueCalculationEngine { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
	}
}