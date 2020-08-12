using System;
using System.Collections.Generic;
using BowlsResults.WebApi.Common.Dto;
using BowlsResults.WebApi.Competition.Dto;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;

namespace BowlsResults.WebApi.CompetitionResult.Dto
{
	public sealed class PlayerCompetitionResultDto
	{
		public CompetitionDto Competition { get; set; }

		public PlayerResultFixtureDto Fixture { get; set; }
	}

	public abstract class BaseResultFixtureDto
	{
		public int ID { get; set; }
		public int Legs { get; set; }
		public FixtureCalculationEngines FixtureCalculationEngineID { get; set; }
	}

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
	}

	public sealed class PlayerMatchDto
	{
		public PlayerMatchDto()
		{
			this.Games = new List<GameDto>();
		}
		public int ID { get; set; }
		public DateTime Date { get; set; }
		public byte Leg { get; set; }
		public MatchStatuses MatchStatusID { get; set; }
		public MatchCalculationEngines MatchCalculationEngineID { get; set; }
		
		public PlayerEntrantDto HomeEntrant { get; set; }
		public PlayerEntrantDto AwayEntrant { get; set; }
		public VenueTypes VenueTypeID { get; set; }
		public PitchDto Pitch { get; set; }
		public ResultDto HomeResult { get; set; }
		public ResultDto AwayResult { get; set; }
		
		public List<GameDto> Games { get; private set; }
	}

	public sealed class ResultDto
	{
		public ResultType ResultType { get; set; }
		public byte? ChalkHandicap { get; set; }
		public short GameScore { get; set; }
		public short ChalkScore { get; set; }
		public short? BonusScore { get; set; }
		public bool IsWalkover { get; set; }
	}
	
	public sealed class PitchDto
	{
		public int ID { get; set; }
		public string Name { get; set; }
	}

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
	
	public sealed class GameVariationDto
	{
		public int ID { get; set; }
		public GameFormats GameFormatID { get; set; }
		public Genders GenderID { get; set; }
		public string Name { get; set; }
	}
	
	public sealed class PlayerResultDto
	{
		public ResultType ResultType { get; set; }
		public byte? Handicap { get; set; }
		public byte Chalks { get; set; }
		public bool IsWalkover { get; set; }
	}
}