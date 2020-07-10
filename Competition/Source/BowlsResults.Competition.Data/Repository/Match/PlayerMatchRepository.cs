using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Fixture;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository.Match
{
	public class PlayerMatchRepository : IdentityRepository<PlayerMatch, short>, IPlayerMatchRepository
	{
		public PlayerMatchRepository(ISessionProvider provider) : base(provider)
		{
		}
	}
}