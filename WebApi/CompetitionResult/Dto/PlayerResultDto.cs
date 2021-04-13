using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace BowlsResults.WebApi.CompetitionResult.Dto
{
	public sealed class PlayerResultDto
	{
		public ResultType ResultType { get; set; }
		public byte? Handicap { get; set; }
		public byte? ChalkHandicap { get; set; }
		public byte Chalks { get; set; }
		public byte ChalkScore { get; set; }
		public bool IsWalkover { get; set; }
	}
}