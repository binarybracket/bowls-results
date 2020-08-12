using System;
using System.Collections.Generic;
using System.Linq;
using BowlsResults.WebApi.Common.Dto;
using BowlsResults.WebApi.CompetitionResult.Dto;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;

namespace BowlsResults.WebApi.CompetitionResult.Assembler
{
	public static class PlayerResultFixtureDtoAssembler
	{
		public static PlayerResultFixtureDto AssembleDto(this PlayerFixture fixture)
		{
			var dto = new PlayerResultFixtureDto
			{
				ID = fixture.ID,
				Entrant1 = fixture.Entrant1.AssembleDto(),
				Entrant2 = fixture.Entrant2.AssembleDto(),
				Legs = fixture.Legs,
				FixtureCalculationEngineID = fixture.FixtureCalculationEngineID,
				Result1 = fixture.AssemblePlayerFixtureEntrant1Score(),
				Result2 = fixture.AssemblePlayerFixtureEntrant2Score(),
			};

			foreach (var match in fixture.Matches)
			{
				var homeEntrant = (match.Home.ID == fixture.Entrant1.ID ? dto.Entrant1 : dto.Entrant2);
				var awayEntrant = (match.Away.ID == fixture.Entrant2.ID ? dto.Entrant1 : dto.Entrant2);

				var matchDto = match.AssembleDto(homeEntrant, awayEntrant);
				dto.Matches.Add(matchDto);
			}

			return dto;
		}
	}

	public static class PlayerMatchDtoAssembler
	{
		public static PlayerMatchDto AssembleDto(this PlayerMatch match, PlayerEntrantDto homeEntrant, PlayerEntrantDto awayEntrant)
		{
			var dto = new PlayerMatchDto
			{
				ID = match.ID,
				Leg = match.Leg,
				Date = match.Date,
				MatchStatusID = match.MatchStatusID,
				MatchCalculationEngineID = match.MatchCalculationEngineID,
				Pitch = match.Pitch.AssembleDto(),
				HomeEntrant = homeEntrant,
				AwayEntrant = awayEntrant
			};

			if (match.MatchStatusID.IsProcessedWithResult())
			{
				dto.HomeResult = match.AssemblePlayerMatchHomeScore();
				dto.AwayResult = match.AssemblePlayerMatchAwayScore();

				foreach (PlayerMatchXGame playerMatchGame in match.Games)
				{
					var gameDto = playerMatchGame.Game.AssembleDto();
					dto.Games.Add(gameDto);
				}
			}

			return dto;
		}
	}

	public static class GameDtoAssembler
	{
		public static GameDto AssembleDto(this Game game)
		{
			var dto = new GameDto();
			dto.ID = game.ID;
			dto.Date = game.Date;
			dto.HomePlayers = game.HomePlayers.Select(x => x.Player).AssembleDtoList();
			dto.AwayPlayers = game.AwayPlayers.Select(x => x.Player).AssembleDtoList();
			dto.GameFormat = game.GameFormatID;
			dto.GameVariation = game.GameVariation.AssembleDto();
			dto.HomeResult = game.AssembleHomeScore();
			dto.AwayResult = game.AssembleAwayScore();

			return dto;
		}
	}

	public static class PlayerEntrantDtoAssembler
	{
		public static PlayerEntrantDto AssembleDto(this CompetitionEntrant entrant)
		{
			var dto = new PlayerEntrantDto();
			dto.Players.AddRange(entrant.Players.Select(x => x.Player.AssembleDto()));
			return dto;
		}
	}

	public static class PlayerDtoAssembler
	{
		public static List<PlayerDto> AssembleDtoList(this IEnumerable<Player> players)
		{
			var list = new List<PlayerDto>();

			foreach (var player in players)
			{
				list.Add(player.AssembleDto());
			}

			return list;
		}

		public static PlayerDto AssembleDto(this Player data)
		{
			return new PlayerDto
			{
				Forename = data.Forename,
				Surname = data.Surname,
				DisplayName = $"{data.Forename} {data.Surname}",
				ID = data.ID
			};
		}
	}

	public static class ResultDtoAssembler
	{
		public static ResultDto AssemblePlayerMatchHomeScore(this PlayerMatch match)
		{
			var dto = new ResultDto();

			dto.ResultType = match.HomeResultTypeID.Value;
			dto.GameScore = match.HomeGameScore.Value;
			dto.ChalkScore = match.HomeChalkScore.Value;
			dto.BonusScore = match.HomeBonusScore;
			dto.ChalkHandicap = match.HomeChalkHandicap;
			dto.IsWalkover = match.HomeWalkover.Value;

			return dto;
		}

		public static ResultDto AssemblePlayerMatchAwayScore(this PlayerMatch match)
		{
			var dto = new ResultDto();

			dto.ResultType = match.AwayResultTypeID.Value;
			dto.GameScore = match.AwayGameScore.Value;
			dto.ChalkScore = match.AwayChalkScore.Value;
			dto.BonusScore = match.AwayBonusScore;
			dto.ChalkHandicap = match.AwayChalkHandicap;
			dto.IsWalkover = match.AwayWalkover.Value;

			return dto;
		}

		public static ResultDto AssemblePlayerFixtureEntrant1Score(this PlayerFixture data)
		{
			ResultDto dto = null;

			if (data.FixtureStatusID == FixtureStatuses.Complete)
			{
				dto = new ResultDto();
				dto.ResultType = data.Entrant1ResultTypeID.Value;
				dto.GameScore = data.Entrant1GameScore.Value;
				dto.ChalkScore = data.Entrant1ChalkScore.Value;
				dto.BonusScore = data.Entrant1BonusScore;
				dto.ChalkHandicap = null;
				dto.IsWalkover = data.Entrant1Walkover.Value;
			}

			return dto;
		}

		public static ResultDto AssemblePlayerFixtureEntrant2Score(this PlayerFixture data)
		{
			ResultDto dto = null;

			if (data.FixtureStatusID == FixtureStatuses.Complete)
			{
				dto = new ResultDto();
				dto.ResultType = data.Entrant2ResultTypeID.Value;
				dto.GameScore = data.Entrant2GameScore.Value;
				dto.ChalkScore = data.Entrant2ChalkScore.Value;
				dto.BonusScore = data.Entrant2BonusScore;
				dto.ChalkHandicap = null;
				dto.IsWalkover = data.Entrant2Walkover.Value;
			}

			return dto;
		}
	}

	public static class PitchDtoAssembler
	{
		public static PitchDto AssembleDto(this Pitch pitch)
		{
			if (pitch != null)
			{
				return new PitchDto {ID = pitch.ID, Name = pitch.Name};
			}

			return null;
		}
	}

	public static class GameVariationDtoAssembler
	{
		public static GameVariationDto AssembleDto(this GameVariation data)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			var dto = new GameVariationDto
			{
				ID = data.ID,
				GameFormatID = data.GameFormatID,
				GenderID = data.GenderID,
				Name = data.Name
			};

			return dto;
		}
	}

	public static class PlayerResultDtoAssembler
	{
		public static PlayerResultDto AssembleHomeScore(this Game game)
		{
			if (game == null)
			{
				throw new ArgumentNullException(nameof(game));
			}

			var dto = new PlayerResultDto();

			dto.ResultType = game.HomeResultTypeID.Value;
			dto.Handicap = game.HomeHandicap;
			dto.Chalks = game.HomeScore;
			dto.IsWalkover = game.HomeWalkover;

			return dto;
		}

		public static PlayerResultDto AssembleAwayScore(this Game game)
		{
			if (game == null)
			{
				throw new ArgumentNullException(nameof(game));
			}

			var dto = new PlayerResultDto();

			dto.ResultType = game.AwayResultTypeID.Value;
			dto.Handicap = game.AwayHandicap;
			dto.Chalks = game.AwayScore;
			dto.IsWalkover = game.AwayWalkover;

			return dto;
		}
	}
}