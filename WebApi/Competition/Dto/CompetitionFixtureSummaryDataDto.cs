using BowlsResults.WebApi.CompetitionResult.Dto;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;

namespace BowlsResults.WebApi.Competition.Dto
{
	public sealed class FixtureSummaryDataDto
	{
		public string CompetitionName { get; set; }
		public int CompetitionVenueDefaultPitchID { get; set; }
		public string CompetitionStageDescription { get; set; }
		public string CompetitionRoundDescription { get; set; }
		public CompetitionRoundTypes CompetitionRoundType { get; set; }
		public int CompetitionRoundGameNumber { get; set; }
	}
}