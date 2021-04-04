using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace BowlsResults.WebApi.Competition.Dto
{
	public abstract class CompetitionRoundDto
	{
		public CompetitionRoundTypes CompetitionRoundTypeID { get; set; }
		public string Description { get; set; }
		public short GameNumber { get; set; }
		public string Notes { get; set; }
	}
}