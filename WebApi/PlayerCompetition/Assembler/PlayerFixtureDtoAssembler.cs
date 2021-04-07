using System;
using System.Collections.Generic;
using BowlsResults.WebApi.Competition.Assembler;
using BowlsResults.WebApi.CompetitionResult.Assembler;
using BowlsResults.WebApi.CompetitionResult.Dto;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

namespace BowlsResults.WebApi.PlayerCompetition.Assembler
{
	public static class PlayerFixtureDtoAssembler
	{
		public static List<BasePlayerFixtureDto> AssembleDtoList(this IEnumerable<PlayerFixture> fixtures)
		{
			var list = new List<BasePlayerFixtureDto>();

			foreach (var fixture in fixtures)
			{
				list.Add(fixture.AssembleDto());
			}

			return list;
		}

		public static BasePlayerFixtureDto AssembleDto(this PlayerFixture fixture)
		{
			switch (fixture.FixtureStatusID)
			{
				case FixtureStatuses.Pending:
					return AssemblePendingDto(fixture);
				case FixtureStatuses.Incomplete:
				case FixtureStatuses.Complete:
					return AssembleConfirmedDto(fixture);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static ConfirmedPlayerFixtureDto AssembleConfirmedDto(PlayerFixture fixture)
		{
			var dto = new ConfirmedPlayerFixtureDto();
			PopulateBaseValues(dto, fixture);

			dto.Entrant1 = fixture.Entrant1.AssembleDto();
			dto.Entrant2 = fixture.Entrant2.AssembleDto();
			dto.Result1 = fixture.AssemblePlayerFixtureEntrant1Score();
			dto.Result2 = fixture.AssemblePlayerFixtureEntrant2Score();

			foreach (var match in fixture.Matches)
			{
				var homeEntrant = (match.Home.ID == fixture.Entrant1.ID ? dto.Entrant1 : dto.Entrant2);
				var awayEntrant = (match.Away.ID == fixture.Entrant2.ID ? dto.Entrant2 : dto.Entrant1);

				var matchDto = match.AssembleDto(homeEntrant, awayEntrant);
				dto.Matches.Add(matchDto);
			}
			
			return dto;
		}

		private static PendingPlayerFixtureDto AssemblePendingDto(PlayerFixture fixture)
		{
			var dto = new PendingPlayerFixtureDto();
			PopulateBaseValues(dto, fixture);
			dto.PendingDate = fixture.PendingDate.Value;

			var entrant1 = new List<PlayerEntrantDto>();
			var entrant2 = new List<PlayerEntrantDto>();
			dto.Entrant1 = entrant1;
			dto.Entrant2 = entrant2;

			if (fixture.Entrant1 != null)
			{
				entrant1.Add(fixture.Entrant1.AssembleDto());
			}
			else
			{
				GetTeams(entrant1, fixture.PendingPlayer1Fixture);
				dto.Entrant1Description = MapResultTypeDescription(fixture.Pending1ResultTypeID.Value, fixture.PendingPlayer1Fixture.Reference);
			}
			
			if (fixture.Entrant2 != null)
			{
				entrant2.Add(fixture.Entrant2.AssembleDto());
			}
			else
			{
				GetTeams(entrant2, fixture.PendingPlayer2Fixture);
				dto.Entrant2Description = MapResultTypeDescription(fixture.Pending2ResultTypeID.Value, fixture.PendingPlayer2Fixture.Reference);
			}
			
			return dto;
		}

		private static void PopulateBaseValues(BasePlayerFixtureDto dto, PlayerFixture data)
		{
			dto.ID = data.ID;
			dto.Legs = data.Legs;
			dto.FixtureCalculationEngineID = data.FixtureCalculationEngineID;
			dto.FixtureStatusID = data.FixtureStatusID;
			dto.SummaryData = data.AssembleSummaryDataDto();
			dto.Reference = data.Reference;
		}

		private static void GetTeams(List<PlayerEntrantDto> players, PlayerFixture pendingFixture)
		{
			if (pendingFixture.Entrant1 != null)
			{
				players.Add(pendingFixture.Entrant1.AssembleDto());
			}
			if (pendingFixture.Entrant2 != null)
			{
				players.Add(pendingFixture.Entrant2.AssembleDto());
			}

			if (pendingFixture.PendingPlayer1Fixture != null)
			{
				GetTeams(players, pendingFixture.PendingPlayer1Fixture);
			}
			
			if (pendingFixture.PendingPlayer2Fixture != null)
			{
				GetTeams(players, pendingFixture.PendingPlayer2Fixture);
			}
		}

		private static string MapResultTypeDescription(ResultType resultType, string reference)
		{
			switch (resultType)
			{
				case ResultType.Win:
					return $"Winner of {reference}";
					break;
				case ResultType.Lose:
					return $"Loser of {reference}";
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(resultType), resultType, null);
			}
		}
	}
}