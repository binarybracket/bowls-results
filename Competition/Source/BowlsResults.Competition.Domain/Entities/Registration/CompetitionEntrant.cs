using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
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
		
		public virtual IList<Player> GetPlayers()
		{
			return this.Players.Select(x => x.Player).ToList();
		}
		
		public virtual IList<int> GetPlayerIDs()
		{
			return this.Players.Select(x => x.Player.ID).ToList();
		}
		
		public virtual CompetitionEntrantPlayer CreatePlayer(string firstName, string lastName)
		{
			var player = new CompetitionEntrantPlayer
			{
				Entrant = this,
				CompetitionID = this.CompetitionID,
				FirstName = firstName,
				LastName = lastName
			};
			this.Players.Add(player);
			return player;
		}

		public virtual void Cancel()
		{
			this.CompetitionEntrantStatusID = CompetitionEntrantStatuses.Cancelled;
		}

		public virtual void Confirm()
		{
			if (!this.Players.All(x => x.Player != null))
			{
				throw new InvalidOperationException("Entrant cannot be confirmed unless all Players have been set");
			}

			this.CompetitionEntrantStatusID = CompetitionEntrantStatuses.Confirmed;
		}
	}
}