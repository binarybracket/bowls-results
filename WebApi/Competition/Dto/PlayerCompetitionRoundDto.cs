using System.Collections.Generic;
using BowlsResults.WebApi.CompetitionResult.Dto;

namespace BowlsResults.WebApi.Competition.Dto
{
	public sealed class PlayerCompetitionRoundDto : CompetitionRoundDto
	{
		public PlayerCompetitionRoundDto()
		{
			this.Results = new List<PlayerResultFixtureDto>();
		}

		public List<PlayerResultFixtureDto> Results { get; set; }
	}
}