using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository
{
	public interface ICompetitionResultRepository : IIdentityRepository<CompetitionResult, int>
	{
		Task<List<PlayerCompetitionResult>> GetPlayerCompetitionResults(int season);
	}
}