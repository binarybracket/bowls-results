using Com.BinaryBracket.BowlsResults.Competition.Domain.Models;
using Com.BinaryBracket.Core.Domain2.Repository;

namespace Com.BinaryBracket.BowlsResults.Competition.Domain.Repository
{
	public interface ICompetitionHeaderRepository : IIdentityRepository<CompetitionHeader, int>
	{
	}
}