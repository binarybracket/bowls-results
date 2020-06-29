using System;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Validation.Failure
{
	[Serializable]
	public sealed class CompetitionRegistrationNotOpen : FluentValidation.Results.ValidationFailure
	{
		public CompetitionRegistrationNotOpen(DateTime openDate) : base("RegistrationStatus", "Entries for this competition will only open on %date%")
		{
			this.OpenDate = openDate;
		}

		public CompetitionRegistrationNotOpen(DateTime openDate, object attemptedValue) : base("RegistrationStatus", "Entries for this competition will only open on %date%", attemptedValue)
		{
			this.OpenDate = openDate;
		}
		
		public DateTime OpenDate { get; set; }
	}
}