using System;
using System.Text;
using System.Collections.Generic;
using Com.BinaryBracket.Core.Domain2.Entities;


namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration
{
	public class CompetitionRegistration : IdentityEntity<int>
	{
		public CompetitionRegistration()
		{
			this.Entrants = new List<CompetitionEntrant>();
		}

		public virtual Competition Competition { get; set; }
		public virtual string EmailAddress { get; set; }
		public virtual string Forename { get; set; }
		public virtual string Surname { get; set; }
		public virtual IList<CompetitionEntrant> Entrants { get; set; }
	}
}