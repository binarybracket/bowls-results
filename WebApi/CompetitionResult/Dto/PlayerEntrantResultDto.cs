using System.Collections.Generic;
using BowlsResults.WebApi.Common.Dto;

namespace BowlsResults.WebApi.CompetitionResult.Dto
{
	public sealed class PlayerEntrantDto
	{
		public PlayerEntrantDto()
		{
			this.Players = new List<PlayerDto>();
		}
		public List<PlayerDto> Players { get; private set; }
	}
}