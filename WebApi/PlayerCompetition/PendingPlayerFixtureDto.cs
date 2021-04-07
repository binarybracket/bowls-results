using System;
using System.Collections.Generic;
using BowlsResults.WebApi.CompetitionResult.Dto;

namespace BowlsResults.WebApi.PlayerCompetition
{
	public sealed class PendingPlayerFixtureDto : BasePlayerFixtureDto
	{
		public List<PlayerEntrantDto> Entrant1 { get; set; }
		public List<PlayerEntrantDto> Entrant2 { get; set; }
		public string Entrant1Description { get; set; }
		public string Entrant2Description { get; set; }
		
		public DateTime PendingDate { get; set; }
	}
}