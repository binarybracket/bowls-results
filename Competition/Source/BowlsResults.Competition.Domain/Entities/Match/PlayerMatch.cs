using System;
using System.Collections;
using System.Collections.Generic;
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
				
		public virtual IList<PlayerMatchXGame> Games { get; set; }
		
		public virtual PlayerFixture PlayerFixture { get; set; }
		
		public virtual CompetitionEntrant Home { get; set; }
		public virtual CompetitionEntrant Away { get; set; }
	}
}