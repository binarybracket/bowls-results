using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.AddCompetitionStage
{
	public sealed class AddCompetitionStageCommand
	{
		public int CompetitionID { get; set; }
		public CompetitionStageFormats CompetitionStageFormatID { get; set; }
		public string Name { get; set; }
		public byte Sequence { get; set; }

		public IList<EventTemplate> Events { get; set; }
	}
}