using System;
using BowlsResults.WebApi.Competition.Dto;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;

namespace BowlsResults.WebApi.Competition.Assembler
{
	public static class CompetitionFixtureSummaryDataDtoAssembler
	{
		public static FixtureSummaryDataDto AssembleSummaryDataDto(this PlayerFixture data)
		{
			var dto = new FixtureSummaryDataDto();

			dto.CompetitionName = data.CompetitionRound.Competition.Name;
			dto.CompetitionStageDescription = GenerateCompetitionStageDescription(data);
			dto.CompetitionRoundDescription = GenerateCompetitionRoundDescription(data.CompetitionRound);
			
			return dto;
		}

		private static string GenerateCompetitionStageDescription(PlayerFixture data)
		{
			return $"{data.CompetitionRound.CompetitionEvent.CompetitionStage.Name}";
		}

		private static string GenerateCompetitionRoundDescription(PlayerCompetitionRound competitionRound)
		{
			var description = String.Empty;
			var dto = competitionRound.AssemblePlayerDto();

			if (competitionRound.CompetitionEvent is League)
			{
				var league = (League) competitionRound.CompetitionEvent;
				if (!string.IsNullOrEmpty(league.Name))
				{
					description += $"{league.Name} | ";
				}
			}

			description += dto.Description;
			return description;
		}
	}
}