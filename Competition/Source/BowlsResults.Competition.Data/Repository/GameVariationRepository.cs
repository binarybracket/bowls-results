using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Repository;
using Com.BinaryBracket.Core.Data2;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;

namespace Com.BinaryBracket.BowlsResults.Competition.Data.Repository
{
	public sealed class GameVariationRepository : IdentityRepository<GameVariation, byte>, IGameVariationRepository
	{
		public GameVariationRepository(ISessionProvider provider) : base(provider)
		{
		}
	}
}