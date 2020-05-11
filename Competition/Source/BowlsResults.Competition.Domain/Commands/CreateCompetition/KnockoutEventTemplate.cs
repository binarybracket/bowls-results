using System.Collections.Generic;
using BowlsResults.Common.Domain.Models;

namespace BowlsResults.Competition.Domain.Commands.CreateCompetition
{
	public sealed class KnockoutEventTemplate : EventTemplate
	{
		public KnockoutCalculationEngines KnockoutCalculationEngine { get; set; }
		public Dictionary<byte, CompetitionRoundTypes> Rounds { get; set; }
	}
}