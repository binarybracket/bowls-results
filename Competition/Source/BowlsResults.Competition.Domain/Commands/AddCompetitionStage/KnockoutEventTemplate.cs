using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.AddCompetitionStage
{
	public sealed class KnockoutEventTemplate : EventTemplate
	{
		public KnockoutEventTemplate()
		{
			this.Rounds = new Dictionary<byte, CompetitionRoundTypes>();
		}
		
		public KnockoutCalculationEngines KnockoutCalculationEngine { get; set; }
		public Dictionary<byte, CompetitionRoundTypes> Rounds { get; set; }
	}
}