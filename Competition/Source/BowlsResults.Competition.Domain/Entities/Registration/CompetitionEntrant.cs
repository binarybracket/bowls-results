using System;
using System.Text;
using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Game;
using Com.BinaryBracket.Core.Domain2.Entities;


namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration
{
	public class CompetitionEntrant : IdentityEntity<int>
	{
		public CompetitionEntrant()
		{
			this.Players = new List<CompetitionEntrantPlayer>();
		}

		public virtual CompetitionRegistration CompetitionRegistration { get; set; }
		public virtual CompetitionEntrantStatuses CompetitionEntrantStatusID { get; set; }
		public virtual int CompetitionID { get; set; }
		public virtual GameFormats EntrantGameFormatID { get; set; }
		public virtual IList<CompetitionEntrantPlayer> Players { get; set; }
	}
}