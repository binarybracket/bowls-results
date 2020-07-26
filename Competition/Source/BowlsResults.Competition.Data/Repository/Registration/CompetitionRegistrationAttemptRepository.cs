using BowlsResults.WebApi;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Data2.Repositories;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository.Registration
{
	public class CompetitionRegistrationAttemptRepository : IdentityRepository<CompetitionRegistrationAttempt, int>, ICompetitionRegistrationAttemptRepository
	{
		public CompetitionRegistrationAttemptRepository(IRegistrationSessionProvider provider) : base(provider)
		{
		}
	}
}