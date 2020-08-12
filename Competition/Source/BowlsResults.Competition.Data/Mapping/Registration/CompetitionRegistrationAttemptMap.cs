using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.Core.Data2.Mapping;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Mapping.Registration
{
	public class CompetitionRegistrationAttemptMap : IdentityEntityMap<CompetitionRegistrationAttempt, int>
	{
		public CompetitionRegistrationAttemptMap()
		{
			this.Table("CompetitionRegistrationAttempt");
			this.LazyLoad();

			this.Map(x => x.Data).Column("Data").Not.Nullable();
			this.Map(x => x.Response).Column("Response").Nullable();
			this.Map(x => x.Date).Column("Date").Not.Nullable();
			this.Map(x => x.RecaptchaScore).Column("RecaptchaScore").Nullable();
			this.Map(x => x.Status).Column("Status").Nullable();
		}
	}
}