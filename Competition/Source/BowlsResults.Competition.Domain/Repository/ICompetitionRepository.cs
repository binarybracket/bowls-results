using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository
{
	public interface ICompetitionRepository : IIdentityRepository<Entities.Competition, int>
	{
		Task<Entities.Competition> GetWithStages(int competitionID);
		Task<List<Entities.Competition>> GetPendingPlayerCompetitions();

		Task<Entities.Competition> GetWithRegistrationConfiguration(int competitionID);

		Task<List<Entities.Competition>> GetPastPlayerCompetitions();
	}
}