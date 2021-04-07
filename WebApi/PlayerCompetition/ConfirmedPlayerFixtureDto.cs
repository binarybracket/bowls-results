using System.Collections.Generic;
using BowlsResults.WebApi.CompetitionResult.Dto;

namespace BowlsResults.WebApi.PlayerCompetition
{
	public sealed class ConfirmedPlayerFixtureDto : BasePlayerFixtureDto
	{
		public ConfirmedPlayerFixtureDto()
		{
			this.Matches = new List<PlayerMatchDto>();
		}

		public PlayerEntrantDto Entrant1 { get; set; }
		public PlayerEntrantDto Entrant2 { get; set; }
		public ResultDto Result1 { get; set; }
		public ResultDto Result2 { get; set; }

		public List<PlayerMatchDto> Matches { get; private set; }
	}
}