using System;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration
{
	public class CompetitionRegistrationSummary : IdentityEntity<int>
	{
		public virtual int CompetitionID { get; set; }
		public virtual bool SummarySent { get; set; }
		public virtual DateTime SummarySentDate { get; set; }
	}
}