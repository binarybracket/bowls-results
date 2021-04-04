using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace BowlsResults.WebApi.CompetitionResult.Dto
{
	public sealed class ResultDto
	{
		public ResultType ResultType { get; set; }
		public byte? ChalkHandicap { get; set; }
		public short GameScore { get; set; }
		public short ChalkScore { get; set; }
		public short? BonusScore { get; set; }
		public bool IsWalkover { get; set; }
	}
}