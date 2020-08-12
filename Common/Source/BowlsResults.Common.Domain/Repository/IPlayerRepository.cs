using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Common.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Repository
{
	public interface IPlayerRepository : IIdentityRepository<Player, int>
	{
		Task<List<Player>> Get(int[] ids);
		Task<Player> GetSingle(string forename, string surname);
	}
}