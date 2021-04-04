using System;
using System.Collections.Generic;
using BowlsResults.WebApi.Common.Dto;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;

namespace BowlsResults.WebApi.CompetitionResult.Dto
{
	public sealed class GameDto
	{
		public int ID { get; set; }
		public DateTime Date { get; set; }
		public GameFormats GameFormat { get; set; }
		public GameVariationDto GameVariation { get; set; }
		public PlayerResultDto HomeResult { get; set; }
		public PlayerResultDto AwayResult { get; set; }
		public List<PlayerDto> HomePlayers { get; set; }
		public List<PlayerDto> AwayPlayers { get; set; }
	}
}