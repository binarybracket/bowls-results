using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Com.BinaryBracket.Core.Domain2.Entities;


namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration
{
	public class CompetitionRegistration : IdentityEntity<int>
	{
		public CompetitionRegistration()
		{
			this.Entrants = new HashSet<CompetitionEntrant>();
		}

		public virtual int CompetitionID { get; set; }
		public virtual string EmailAddress { get; set; }
		public virtual string Forename { get; set; }
		public virtual string Surname { get; set; }
		public virtual ISet<CompetitionEntrant> Entrants { get; set; }

		public virtual CompetitionEntrant CreateEntrant(Entities.Competition competition)
		{
			var data = new CompetitionEntrant();
			data.CompetitionRegistration = this;
			data.CompetitionEntrantStatusID = CompetitionEntrantStatuses.Pending;
			data.CompetitionID = competition.ID;
			data.EntrantGameFormatID = competition.GameVariation.GameFormatID;
			this.Entrants.Add(data);
			return data;
		}

		public virtual string DisplayName()
		{
			return $"{this.Forename} {this.Surname}";
		}

		public virtual IEnumerable<CompetitionEntrant> GetPendingOrConfirmedEntrants()
		{
			return this.Entrants.Where(x => x.CompetitionEntrantStatusID == CompetitionEntrantStatuses.Pending || x.CompetitionEntrantStatusID == CompetitionEntrantStatuses.Confirmed);
		}
	}
}