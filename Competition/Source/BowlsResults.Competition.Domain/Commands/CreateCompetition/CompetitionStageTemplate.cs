using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Commands.CreateCompetition
{
	public sealed class CompetitionStageTemplate
	{
		public CompetitionStageFormats CompetitionStageFormatID { get; set; }
		public string Name { get; set; }
		public byte Sequence { get; set; }
	}
}