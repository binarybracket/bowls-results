using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.Core.Data2;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Repository
{
	public sealed class SeasonRepository : IdentityRepository<Season, short>, ISeasonRepository
	{
		public SeasonRepository(ISessionProvider provider) : base(provider)
		{
		}
	}
}