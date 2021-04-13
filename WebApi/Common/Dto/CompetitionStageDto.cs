using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace BowlsResults.WebApi.Common.Dto
{
	public sealed class CompetitionStageDto
	{
		public CompetitionStageFormats CompetitionStageFormatID { get; set; }
		public byte Sequence { get; set; }
		public string Name { get; set; }
	}
}