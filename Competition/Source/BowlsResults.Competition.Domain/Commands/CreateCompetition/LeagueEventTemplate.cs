using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition
{
	public sealed class LeagueEventTemplate : EventTemplate
	{
		public LeagueCalculationEngines LeagueCalculationEngine { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
	}
}