using System.Collections.Generic;
using System.Threading.Tasks;
using Com.BinaryBracket.BowlsResults.Competition.Domain.Entities;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository
{
	public interface ICompetitionDateRepository : IIdentityRepository<CompetitionDate, int>
	{
		Task<List<CompetitionDate>> GetByCompetition(int competitionID);
	}
}