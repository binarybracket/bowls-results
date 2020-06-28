using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Registration;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Registration;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository.Registration
{
	public sealed class CompetitionRegistrationConfigurationRepository : IdentityRepository<CompetitionRegistrationConfiguration, int>, ICompetitionRegistrationConfigurationRepository
	{
		public CompetitionRegistrationConfigurationRepository(ISessionProvider provider) : base(provider)
		{
		}
	}
}