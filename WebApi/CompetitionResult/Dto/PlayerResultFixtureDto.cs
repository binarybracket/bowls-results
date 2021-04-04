using System.Collections.Generic;
using BowlsResults.WebApi.Competition.Dto;

namespace BowlsResults.WebApi.CompetitionResult.Dto
{
	public sealed class PlayerResultFixtureDto : BaseResultFixtureDto
	{
		public PlayerResultFixtureDto()
		{
			this.Matches = new List<PlayerMatchDto>();
		}
		public PlayerEntrantDto Entrant1 { get; set; }
		public PlayerEntrantDto Entrant2 { get; set; }
	
		public ResultDto Result1 { get; set; }
		public ResultDto Result2 { get; set; }
		
		public List<PlayerMatchDto> Matches { get; private set; }
		public FixtureSummaryDataDto SummaryData { get; set; }
	}
}