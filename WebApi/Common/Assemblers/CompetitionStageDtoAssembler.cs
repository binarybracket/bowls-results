using System.Collections.Generic;
using BowlsResults.WebApi.Common.Dto;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;

namespace BowlsResults.WebApi.Common.Assemblers
{
	public static class CompetitionStageDtoAssembler
	{
		public static List<CompetitionStageDto> AssembleDtoList(this IEnumerable<CompetitionStage> competitionStages)
		{
			var list = new List<CompetitionStageDto>();

			foreach (var competitionStage in competitionStages)
			{
				list.Add(competitionStage.AssembleDto());
			}

			return list;
		}
		public static CompetitionStageDto AssembleDto(this CompetitionStage competitionStage)
		{
			return  new CompetitionStageDto
			{
				Name = competitionStage.Name,
				Sequence = competitionStage.Sequence,
				CompetitionStageFormatID = competitionStage.CompetitionStageFormatID
			};
		}
	}
}