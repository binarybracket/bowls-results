using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition
{
	public sealed class KnockoutEventTemplate : EventTemplate
	{
		public KnockoutCalculationEngines KnockoutCalculationEngine { get; set; }
		public Dictionary<byte, CompetitionRoundTypes> Rounds { get; set; }
	}
}