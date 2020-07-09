using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.BowlsResults.Common.Domain.Repository;
using Com.BinaryBracket.Core.Data2.Repositories;
using Com.BinaryBracket.Core.Data2.SessionProvider;
using NHibernate.Linq;

namespace Com.BinaryBracket.BowlsResults.Common.Data.Repository
{
	public sealed class PlayerRepository : IdentityRepository<Player, int>, IPlayerRepository
	{
		public PlayerRepository(ISessionProvider provider) : base(provider)
		{
		}

		public Task<List<Player>> Get(int[] ids)
		{
			return this.SessionQuery()
				.Where(x => ids.Contains(x.ID))
				.ToListAsync();
		}
	}
}