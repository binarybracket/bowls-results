using System;
using System.Text;
using System.Collections.Generic;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Entities;


namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration
{
	public class CompetitionEntrantPlayer : IdentityEntity<int>
	{
		public virtual CompetitionEntrant Entrant { get; set; }
		public virtual int CompetitionID { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual Player Player { get; set; }

		public virtual void SetPlayer(Player player)
		{
			this.Player = player;
		}
	}
}