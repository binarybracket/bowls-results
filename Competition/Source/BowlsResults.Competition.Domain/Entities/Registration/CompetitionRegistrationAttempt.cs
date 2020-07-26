using System;
using Com.BinaryBracket.Core.Domain2.Entities;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration
{
	public class CompetitionRegistrationAttempt : IdentityEntity<int>
	{
		public CompetitionRegistrationAttempt()
		{
			this.Date = DateTime.UtcNow;
		}
		public virtual string Data { get; set; }
		public virtual DateTime Date { get; set; }
	}
}