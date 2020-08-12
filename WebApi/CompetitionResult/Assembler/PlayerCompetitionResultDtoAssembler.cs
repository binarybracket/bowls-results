using System;
using System.Collections.Generic;
using System.Linq;
using BowlsResults.WebApi.Competition.Assembler;
using BowlsResults.WebApi.CompetitionResult.Dto;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

namespace BowlsResults.WebApi.CompetitionResult.Assembler
{
	public static class PlayerCompetitionResultDtoAssembler
	{
		public static List<PlayerCompetitionResultDto> AssembleDtoList(this IEnumerable<PlayerCompetitionResult> competitionResults)
		{
			var list = new List<PlayerCompetitionResultDto>();

			foreach (var competitionResult in competitionResults)
			{
				list.Add(competitionResult.AssembleDto());
			}

			return list;
		}

		public static PlayerCompetitionResultDto AssembleDto(this PlayerCompetitionResult competitionResult)
		{
			var dto = new PlayerCompetitionResultDto
			{
				Competition = competitionResult.Competition.AssembleDto()
			};

			dto.Fixture = competitionResult.Fixture.AssembleDto();
			
			return dto;
		}

		
	}
}