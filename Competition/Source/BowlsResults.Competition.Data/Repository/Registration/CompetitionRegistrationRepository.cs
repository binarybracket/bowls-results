using BowlsResults.WebApi;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Data2.Repositories;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository.Registration
{
	public sealed class CompetitionRegistrationRepository : IdentityRepository<CompetitionRegistration, int>, ICompetitionRegistrationRepository
	{
		public CompetitionRegistrationRepository(IRegistrationSessionProvider provider) : base(provider)
		{
		}
	}
}