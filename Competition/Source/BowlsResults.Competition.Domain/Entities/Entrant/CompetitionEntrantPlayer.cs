using System;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Entrant
{
	public class CompetitionEntrantPlayer : IdentityEntity<int>
	{
		public virtual CompetitionEntrant CompetitionEntrant { get; set; }
		public virtual int CompetitionID { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual Player Player { get; set; }

		public virtual void SetPlayer(Player player1)
		{
			this.Player = player1;
		}
	}
}