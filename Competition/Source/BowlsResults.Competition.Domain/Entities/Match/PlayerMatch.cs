using System;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Entrant;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Fixture;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match
{
	public class PlayerMatch : Match
	{
		public PlayerMatch()
		{
		}
				
		public virtual PlayerFixture PlayerFixture { get; set; }
		
		public virtual CompetitionEntrant Home { get; set; }
		public virtual CompetitionEntrant Away { get; set; }
	}
}