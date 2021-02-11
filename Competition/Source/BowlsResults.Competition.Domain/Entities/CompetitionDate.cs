using System;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities
{
	public class CompetitionDate : IdentityEntity<int>
	{
		public virtual Competition Competition { get; set; }
		public virtual string Description { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual bool Qualifier { get; set; }
		public virtual CompetitionDateStatuses CompetitionDateStatusID { get; set; }
	}
}