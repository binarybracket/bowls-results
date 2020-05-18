using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition;
using Com.BinaryBracket.Core.Domain2.Commands;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.AddCompetitionStage
{
	public sealed class AddCompetitionStageCommand : ICommand<DefaultCommandResponse>
	{
		public AddCompetitionStageCommand()
		{
			this.Events = new List<EventTemplate>();
		}

		public int CompetitionID { get; set; }
		public CompetitionStageFormats CompetitionStageFormatID { get; set; }
		public string Name { get; set; }
		public byte Sequence { get; set; }

		public IList<EventTemplate> Events { get; set; }
	}
}