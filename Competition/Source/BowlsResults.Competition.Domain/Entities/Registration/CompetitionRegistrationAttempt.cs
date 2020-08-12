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
		public virtual decimal? RecaptchaScore { get; set; }
		public virtual bool? Status { get; set; }
		public virtual string Response { get; set; }
	}
}