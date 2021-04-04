using BowlsResults.WebApi.Competition.Dto;

namespace BowlsResults.WebApi.CompetitionResult.Dto
{
	public sealed class PlayerCompetitionResultDto : CompetitionResultDto
	{
		public CompetitionDto Competition { get; set; }

		public PlayerResultFixtureDto Fixture { get; set; }
	}
}