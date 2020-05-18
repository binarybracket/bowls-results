using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Data2;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository
{
	public sealed class CompetitionEventRepository : IdentityRepository<CompetitionEvent, short>, ICompetitionEventRepository
	{
		public CompetitionEventRepository(ISessionProvider provider) : base(provider)
		{
		}
	}
}