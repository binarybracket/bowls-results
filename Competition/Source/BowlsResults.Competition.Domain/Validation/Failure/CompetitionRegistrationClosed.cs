using System;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Validation.Failure
{
	[Serializable]
	public sealed class CompetitionRegistrationClosed : FluentValidation.Results.ValidationFailure
	{
		public CompetitionRegistrationClosed(DateTime closeDate) : base("RegistrationStatus", "Entries for this competition have closed")
		{
			this.CloseDate = closeDate;
		}

		public CompetitionRegistrationClosed(DateTime closeDate, object attemptedValue) : base("RegistrationStatus", "Entries for this competition closed have closed", attemptedValue)
		{
			this.CloseDate = closeDate;
		}
		
		public DateTime CloseDate { get; set; }
	}
}