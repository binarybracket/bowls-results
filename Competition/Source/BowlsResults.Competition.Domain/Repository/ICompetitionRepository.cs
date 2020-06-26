using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository
{
	public interface ICompetitionRepository : IIdentityRepository<Entities.Competition, int>
	{
		Task<List<Entities.Competition>> GetPendingPlayerCompetitions();
	}
}