using System;
using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;

namespace BowlsResults.WebApi.CompetitionResult.Dto
{
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
}