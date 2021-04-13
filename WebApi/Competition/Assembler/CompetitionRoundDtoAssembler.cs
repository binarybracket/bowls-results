using System;
using System.Collections.Generic;
using BowlsResults.WebApi.Competition.Dto;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Round;

namespace BowlsResults.WebApi.Competition.Assembler
{
	public static class CompetitionRoundDtoAssembler
	{
		public static List<PlayerCompetitionRoundDto> AssemblePlayerDtoList(this IEnumerable<CompetitionRound> competitionRounds)
		{
			var list = new List<PlayerCompetitionRoundDto>();

			foreach (var competitionRound in competitionRounds)
			{
				list.Add(competitionRound.AssemblePlayerDto());
			}

			return list;
		}
		public static PlayerCompetitionRoundDto AssemblePlayerDto(this CompetitionRound competitionRound)
		{
			return  new PlayerCompetitionRoundDto
			{
				Notes = competitionRound.Notes,
				GameNumber = competitionRound.GameNumber,
				CompetitionRoundTypeID = competitionRound.CompetitionRoundTypeID,
				Description = GenerateDescription(competitionRound)
			};
		}

		private static string GenerateDescription(CompetitionRound competitionRound)
		{
			switch (competitionRound.CompetitionRoundTypeID)
			{
				case CompetitionRoundTypes.LeagueMatches:
					return $"Game {competitionRound.GameNumber}";
				case CompetitionRoundTypes.Prelims:
					return $"Prelim";
				case CompetitionRoundTypes.Rounds123:
					return $"Round {competitionRound.GameNumber}";
				case CompetitionRoundTypes.Last16:
					return $"Last 16";
				case CompetitionRoundTypes.QuarterFinals:
					return $"Quarter Final";
				case CompetitionRoundTypes.SemiFinals:
					return $"Semi Final";
				case CompetitionRoundTypes.Final:
					return $"Final";
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}