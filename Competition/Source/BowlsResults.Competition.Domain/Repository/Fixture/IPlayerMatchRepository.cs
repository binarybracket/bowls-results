using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities.Match;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository.Fixture
{
	public interface IPlayerMatchRepository : IIdentityRepository<PlayerMatch, short>
	{
	}
}