using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Common.Domain.Models;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition
{
	public sealed class CompetitionStageTemplate
	{
		public CompetitionStageFormats CompetitionStageFormatID { get; set; }
		public string Name { get; set; }
		public byte Sequence { get; set; }

		public List	<EventTemplate> Events { get; set; }
	}
}